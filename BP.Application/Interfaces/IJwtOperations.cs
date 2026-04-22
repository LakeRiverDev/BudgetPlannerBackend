using BP.Core.Users;

namespace BP.Application.Interfaces;

public interface IJwtOperations
{
    string Generate(User user);
}