using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DA05_Miniprojekt
{
    [Index(nameof(Name), IsUnique = true)]
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();


    }


}


