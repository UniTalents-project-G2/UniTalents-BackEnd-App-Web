using UniTalents_BackEnd_AW.Users.Domain.Entities;

namespace UniTalents_BackEnd_AW.Shared.Infrastructure.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}