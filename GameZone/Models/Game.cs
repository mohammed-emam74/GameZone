﻿
namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; }= string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();
    }
}
