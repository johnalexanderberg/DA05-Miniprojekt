using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DA05_Miniprojekt
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
        List<User> Likes { get; set; } = new List<User>();

    }


}


