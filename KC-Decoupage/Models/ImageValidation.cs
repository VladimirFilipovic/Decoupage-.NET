using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace KC_Decoupage.Models
{
    public class ImageValidation : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }

          

            try
            {
                var extension = Path.GetExtension(file.FileName);
                var isImage = extension == ".jpeg" || extension == ".jpg" || extension == ".png";
                return isImage;
            }
            catch
            {
            }

            return false;
        }
    }
}