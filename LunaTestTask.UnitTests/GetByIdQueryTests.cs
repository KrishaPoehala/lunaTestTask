using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.Common.Exceptions;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Application.User.Queries.GetTaskByIdQuery;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using Moq;

namespace LunaTestTask.UnitTests;

public class GetByIdQueryTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICurrentUserService> _mockCurrentUserService;

    public GetByIdQueryTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockCurrentUserService = new Mock<ICurrentUserService>();
    }

    [Fact]
    public async Task GetTaskByIdQueryHandler_ReturnsTaskDto_WhenTaskExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var taskEntity = new TaskEntity { Id = taskId, UserId = userId };
        var taskDto = new TaskDto { Id = taskId };

        _mockTaskRepository.Setup(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);
        _mockMapper.Setup(m => m.Map<TaskDto>(It.IsAny<TaskEntity>()))
            .Returns(taskDto);
        _mockCurrentUserService.Setup(u => u.Id).Returns(userId);

        var handler = new GetTaskByIdQueryHandler(_mockTaskRepository.Object, _mockMapper.Object, _mockCurrentUserService.Object);
        var query = new GetTaskByIdQuery(taskId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskDto, result);
        _mockTaskRepository.Verify(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()), Times.Once);
        _mockMapper.Verify(m => m.Map<TaskDto>(It.IsAny<TaskEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetTaskByIdQueryHandler_ThrowsForbiddenException_WhenUserDoesNotOwnTask()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var taskEntity = new TaskEntity { Id = taskId, UserId = userId1 };

        _mockTaskRepository.Setup(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);
        _mockCurrentUserService.Setup(u => u.Id).Returns(userId2);

        var handler = new GetTaskByIdQueryHandler(_mockTaskRepository.Object, Mock.Of<IMapper>(), _mockCurrentUserService.Object);
        var query = new GetTaskByIdQuery(taskId);

        // Act & Assert
        await Assert.ThrowsAsync<ForbiddenException>(async () => await handler.Handle(query, CancellationToken.None));
        _mockTaskRepository.Verify(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTaskByIdQueryHandler_ThrowsNotFoundException_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var handler = new GetTaskByIdQueryHandler(_mockTaskRepository.Object, Mock.Of<IMapper>(), Mock.Of<ICurrentUserService>());
        var query = new GetTaskByIdQuery(taskId);

        _mockTaskRepository.Setup(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskEntity)null);

        // Act/Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        _mockTaskRepository.Verify(repo => repo.GetById(taskId, It.IsAny<CancellationToken>()), Times.Once);
    }

}