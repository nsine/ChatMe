using ChatMe.BussinessLogic.DTO;
using System.Collections.Generic;
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
        Task<IEnumerable<PostDTO>> GetNews(string userId);
    }
}
