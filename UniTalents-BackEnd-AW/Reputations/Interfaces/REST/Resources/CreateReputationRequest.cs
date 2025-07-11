namespace UniTalents_BackEnd_AW.Reputations.Interfaces.REST.Resources;

public class CreateReputationRequest
{
    public int ProjectId  { get; set; }
    public int Rating     { get; set; }  // 1-5
    public string Comment { get; set; } = string.Empty;
}