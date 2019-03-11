using DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;

namespace DataAcess.Repositories
{
    public class CommentRepository : BaseRepository<PostComment>, ICommentRepository
    {

        public CommentRepository(IUnitOfWork unit) : base(unit)
        {
        }

        public int CountCommentsAsync(int postId)
        {
           return Items.Where(i => i.BlogpostId == postId).Count();
        }
    }
}
