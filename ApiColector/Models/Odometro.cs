namespace ApiColector.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Odometro
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Step { get; set; }
    }
}
