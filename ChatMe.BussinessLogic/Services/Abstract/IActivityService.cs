using ChatMe.BussinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IActivityService
    {
        Task ChangeLike(LikedPostDTO likeData);
        Task Like(LikedPostDTO likeData);
        Task UndoLike(LikedPostDTO likeData);
        bool IsLiked(LikedPostDTO likeData);

        Task ChangeFollow(FollowerLinkDTO followData);
        bool IsFollowing(FollowerLinkDTO followData);
        Task Follow(FollowerLinkDTO followData);
        Task Unfollow(FollowerLinkDTO followData);
    }
}
