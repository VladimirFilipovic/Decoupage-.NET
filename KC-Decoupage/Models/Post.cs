using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KC_Decoupage.Models
{
    public class Post
    {
        //[Required],[StringLength(255)] annotations migrations for validation
        public int Id { get; set; }
        //dok ne dodam image ne znam sta cu sa validatoriam za to
        //[DisplayName="Image"]
        [Required(ErrorMessage = "Please upload a picture")]

        public string ImagePath { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The filed must contain at least 5 characters")]
        [Required(ErrorMessage = "Please enter the title")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Title { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The filed must contain at least 5 characters")]
        [Required(ErrorMessage = "Please enter the content")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string Content { get; set; }
        public string Creator { get; set; }
        public int Likes { get; set; }
        public string LikedBy { get; set; }
        public int Dislikes { get; set; }
        public string DislikedBy { get; set; }
        public string Comments { get; set; }
        public string Commentators { get; set; }

        [NotMapped]
        [ImageValidation(ErrorMessage = "Please provide png,jpeg,jpg file format")]
        public HttpPostedFileBase File { get; set; }

    }
}
