using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KC_Decoupage.Dtos
{
    public class PostDto
    {
        //[Required],[StringLength(255)] annotations migrations for validation
        public int Id { get; set; }
        //dok ne dodam image ne znam sta cu sa validatoriam za to
        public string ImagePath { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The filed must contain at least 5 characters")]
        [Required(ErrorMessage = "Please enter the title")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Title { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The filed must contain at least 5 characters")]
        [Required(ErrorMessage = "Please enter the content")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Content { get; set; }
        public string Creator { get; set; }
        public int Likes { get; set; }
        public string LikedBy { get; set; }
        public int Dislikes { get; set; }
        public string DislikedBy { get; set; }
        public string Comments { get; set; }
        public string Commentators { get; set; }
    }
}