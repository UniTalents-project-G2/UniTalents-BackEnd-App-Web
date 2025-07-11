namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

public class RegisterRequest
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
}