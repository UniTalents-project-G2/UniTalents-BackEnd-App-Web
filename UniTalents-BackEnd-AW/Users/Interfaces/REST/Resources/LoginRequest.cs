namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}