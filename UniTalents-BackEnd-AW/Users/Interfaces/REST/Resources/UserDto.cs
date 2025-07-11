namespace UniTalents_BackEnd_AW.Users.Interfaces.REST.Resources;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}