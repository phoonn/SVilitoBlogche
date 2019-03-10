using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Identity
{
    public class PostComment
    {
        [Key, Column(Order = 1, TypeName = "int")]
        public int Id { get; set; }
        [Column(Order = 2), ForeignKey("User")]
        public int UserId { get; set; }
        [Column(Order = 3), ForeignKey("BlogPost")]
        public int BlogpostId { get; set; }
        [Column(Order = 4, TypeName = "VARCHAR(MAX)"),Required]
        public string TextComment { get; set; }
        [Column(Order = 5, TypeName = "datetime")]
        public DateTime DateOfComment { get; set; }


        public virtual BlogPost BlogPost { get; set; }
        public virtual User User { get; set; }
    }
}
