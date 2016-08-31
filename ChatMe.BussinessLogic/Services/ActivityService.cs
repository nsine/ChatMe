using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Entities;

namespace ChatMe.BussinessLogic.Services
{
    public class ActivityService : IActivityService
    {
        private IUnitOfWork unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public async Task ChangeFollow(FollowerLinkDTO followData) {
            if (IsFollowing(followData)) {
                await Unfollow(followData);
            } else {
                await Follow(followData);
            }
        }

        public bool IsFollowing(FollowerLinkDTO followData) {
            return unitOfWork.Users
                .Get(followData.UserId)
                .FollowingUsers
                .Any(u => u.Id == followData.FollowingUserId);
        }

        public async Task Follow(FollowerLinkDTO followData) {
            var followerLink = new FollowerLink {
                UserId = followData.UserId,
                FollowingUserId = followData.FollowingUserId
            };

            unitOfWork.FollowerLinks.Create(followerLink);
            await unitOfWork.SaveAsync();
        }

        public async Task Unfollow(FollowerLinkDTO followData) {
            var followerLink = unitOfWork.FollowerLinks.Find(f => {
                return f.UserId == followData.UserId && f.FollowingUserId == followData.FollowingUserId;
            }).FirstOrDefault();

            unitOfWork.FollowerLinks.Delete(new string[] { followerLink?.UserId, followerLink?.FollowingUserId });
            await unitOfWork.SaveAsync();
        }

        public async Task ChangeLike(LikedPostDTO likeData) {
            if (IsLiked(likeData)) {
                await UndoLike(likeData);
            } else {
                await Like(likeData);
            }
        }

        public bool IsLiked(LikedPostDTO likeData) {
            return unitOfWork.Posts
                .Get(likeData.PostId)
                .Likes
                .Where(like => like.UserId == likeData.UserId)
                .Count() != 0;
        }

        public async Task Like(LikedPostDTO likeData) {
            var like = new Like {
                PostId = likeData.PostId,
                UserId = likeData.UserId
            };

            unitOfWork.Likes.Create(like);
            await unitOfWork.SaveAsync();
        }

        public async Task UndoLike(LikedPostDTO likeData) {
            var like = unitOfWork.Posts
                .Get(likeData.PostId)
                .Likes
                .Where(x => x.UserId == likeData.UserId)
                .FirstOrDefault();

            unitOfWork.Likes.Delete(like.Id);
            await unitOfWork.SaveAsync();
        }
    }
}
