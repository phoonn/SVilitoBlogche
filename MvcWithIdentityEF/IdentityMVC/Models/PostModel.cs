using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Mvc;

namespace IdentityMVC.Models
{
	public class PostModel
	{
        public PostModel(int PostId,string PostContent, string Title, byte[] Image)
        {
            this.PostId = PostId;
            this.PostContent = PostContent;
            this.Title = Title;
            this.Image = Image;
        }

        public PostModel() { }

        [HiddenInput(DisplayValue = false)]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [AllowHtml]
        public string PostContent { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public byte[] Image { get; set; }
        

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
	}
}