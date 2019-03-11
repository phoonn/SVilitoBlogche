﻿using DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<PostComment>
    {
        int CountCommentsAsync(int postId);
    }
}
