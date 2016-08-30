using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetChunk(string userId, string currentUserId, int startIndex, int chunkSize);
        PostDTO Get(string userId, string currentUserId, int postId);
        Task<bool> Create(NewPostDTO data);
        Task<bool> Delete(int dialogId);
        Task<bool> Update(NewPostDTO data, int postId);
    }
}
