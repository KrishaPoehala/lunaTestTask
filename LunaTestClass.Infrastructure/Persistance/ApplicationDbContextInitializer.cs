using LunaTestClass.Infrastructure.Persistance;
using LunaTestTask.Domain.Entities;

namespace LunaTestTask.Infrastructure.Persistance;

public class ApplicationDbContextInitializer(ApplicationDbContext context)
{
    public async Task InitializeAsync()
    {
        var users = new List<User>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Email = "string2",
                //string
                PasswordHash = "AQAAAAIAAYagAAAAEBXCCLTia+8IEy3+cMHQ0F1CvKgmfVS/b025juYqwUE8lTt2ag0U90LPA9BlzEBUZQ==",
                Username="string1",
            }
        };

        if (!context.Users.Any())
        {
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        var userId = context.Users.First().Id;
        List<TaskEntity> tasks =
        [
            new() { Title = "Complete project report", DueDate = DateTime.Now.AddDays(7), UserId = userId },
            new() { Title = "Attend team meeting", DueDate = DateTime.Now.AddDays(2), UserId = userId },
        ];

        if (!context.Tasks.Any())
        {
            context.Tasks.AddRange(tasks);
            await context.SaveChangesAsync();
        }
    }
}
