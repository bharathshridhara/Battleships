using System.ComponentModel.DataAnnotations;

namespace Battleships.Services.Dto
{
    public class AttackDto
    {
        [Required] public int AttackX { get; set; }

        [Required] public int AttackY { get; set; }
    }
}