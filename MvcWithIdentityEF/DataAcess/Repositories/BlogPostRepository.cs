using DataModel.Identity;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(IUnitOfWork unit) : base(unit)
        {
        }
        public override void Update(BlogPost entityToUpdate)
        {
            BlogPost Entity = base.GetById(entityToUpdate.Id);
            Entity.TextPost = entityToUpdate.TextPost;
            Entity.TitleOfPost = entityToUpdate.TitleOfPost;
            if (entityToUpdate.PictureContent != null)
            {
                Entity.PictureContent = entityToUpdate.PictureContent;
            }
            base.Update(Entity);
        }
    }
}
