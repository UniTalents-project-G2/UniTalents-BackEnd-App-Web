using System.Text.Json.Serialization;

namespace UniTalents_BackEnd_AW.Projects.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProjectStatus
{
    Open,
    InProgress,
    Finished,
    Cancelled
}
