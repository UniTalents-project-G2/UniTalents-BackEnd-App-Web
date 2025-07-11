namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

public class LoginResponse
{
    public string Token { get; set; } = default!;
    public UserDto User { get; set; } = default!;
}