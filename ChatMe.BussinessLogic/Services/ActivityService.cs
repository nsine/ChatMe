using ChatMe.BussinessLogic.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;

namespace ChatMe.BussinessLogic.Services
{
    public class ActivityService : IActivityService
    {
        private IUnitOfWork db;

        public ActivityService(IUnitOfWork unitOfWork) {
            db = unitOfWork;
        }

        public async Task ChangeFollow(FollowerLinkDTO followData) {
            if (IsFollowing(followData)) {
                await Unfollow(followData);
            } else {
                await Follow(followData);
            }
        }

        public bool IsFollowing(FollowerLinkDTO followData) {
            return db.Users
                .FindById(followData.UserId)
                .FollowingUsers
                .Any(u => u.Id == followData.FollowingUserId);
        }

        public async Task Follow(FollowerLinkDTO followData) {
            var follower = db.Users.FindById(followData.UserId);
            var followingUser = db.Users.FindById(followData.FollowingUserId);

            followingUser.Followers.Add(follower);
            await db.SaveChangesAsync();
        }

        public async Task Unfollow(FollowerLinkDTO followData) {
            var follower = db.Users.FindById(followData.UserId);
            var followingUser = db.Users.FindById(followData.FollowingUserId);

            followingUser.Followers.Remove(follower);
            await db.SaveChangesAsync();
        }

        public async Task ChangeLike(LikedPostDTO likeData) {
            if (IsLiked(likeData)) {
                await UndoLike(likeData);
            } else {
                await Like(likeData);
            }
        }

        public bool IsLiked(LikedPostDTO likeData) {
            return db.Posts
                .Find(likeData.PostId)
                .Likes
                .Where(like => like.UserId == likeData.UserId)
                .Count() != 0;
        }

        public async Task Like(LikedPostDTO likeData) {
            var like = new Like {
                PostId = likeData.PostId,
                UserId = likeData.UserId
            };

            db.Likes.Add(like);
            await db.SaveChangesAsync();
        }

        public async Task UndoLike(LikedPostDTO likeData) {
            var like = db.Posts
                .Find(likeData.PostId)
                .Likes
                .Where(x => x.UserId == likeData.UserId)
                .FirstOrDefault();

            db.Likes.Remove(like.Id);
            await db.SaveChangesAsync();
        }
    }
}
