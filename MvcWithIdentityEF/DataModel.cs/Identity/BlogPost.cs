using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataModel.Identity;

namespace DataModel.Identity
{
    public class BlogPost
    {
        [Key, Column(Order = 1, TypeName = "int")]
        public int Id { get; set; }
        [Column(Order = 2), ForeignKey("User")]
        public int UserId { get; set; }
        [Column(Order = 3, TypeName = "VARCHAR(MAX)")]
        public string PostContent { get;set; }
        [Column(Order = 4, TypeName = "datetime")]
        public DateTime DateOfPost { get; set; }
        [Column(Order = 5)]
        public string TitleOfPost { get; set; }
        [Column(Order = 6)]
        public byte[] PictureContent { get; set; }



        public virtual User User { get; set; }
    }
}
