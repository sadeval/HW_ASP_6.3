using System.ComponentModel.DataAnnotations;

namespace BattleSimulation.Models
{
    public class Fighter
    {
        [Required]
        [Display(Name = "Имя бойца")]
        public string? Name { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Здоровье")]
        public int Health { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Сила")]
        public int Strength { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Ловкость")]
        public int Agility { get; set; }

        [Required]
        [Range(1, 200)]
        [Display(Name = "Масса")]
        public int Mass { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Защита")]
        public int Defense { get; set; }
    }
}
