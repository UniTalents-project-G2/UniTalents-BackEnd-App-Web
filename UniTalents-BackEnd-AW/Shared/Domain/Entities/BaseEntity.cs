using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniTalents_BackEnd_AW.Shared.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}