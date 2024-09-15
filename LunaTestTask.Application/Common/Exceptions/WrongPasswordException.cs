namespace LunaTestTask.Application.Common.Exceptions;

public class WrongPasswordException:ApplicationException
{
    public WrongPasswordException()
        :base("Wrong password")
    { }
}