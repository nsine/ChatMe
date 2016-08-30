using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;

namespace ChatMe.BussinessLogic.Services
{
    public class PostService : IPostService
    {

        private IUnitOfWork unitOfWork;
        private IUserService userService;
        private IActivityService activityService;

        public PostService(IUnitOfWork unitOfWork, IUserService userService, IActivityService activityService) {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.activityService = activityService;
        }

        public async Task<bool> Create(NewPostDTO data) {
            var newPost = new Post {
                Body = data.Body,
                UserId = data.UserId,
                Time = DateTime.Now
            };

            unitOfWork.Posts.Create(newPost);
            await unitOfWork.SaveAsync();
            return true;
        }

        public Task<bool> Delete(int dialogId) {
            throw new NotImplementedException();
        }

        public PostDTO Get(string userId, string currentUserId, int postId) {
            var rawPost = unitOfWork.Users
                .Get(userId).Posts
                .Where(p => p.Id == postId)
                .FirstOrDefault();
            return new PostDTO {
                Author = userService.GetUserDisplayName(rawPost.User),
                AuthorId = rawPost.UserId,
                Body = rawPost.Body,
                Id = rawPost.Id,
                Likes = rawPost.Likes.Count,
                Time = rawPost.Time,
                IsLikedByMe = activityService.IsLiked(new LikedPostDTO {
                    PostId = rawPost.Id,
                    UserId = currentUserId
                })
            };
        }

        public IEnumerable<PostDTO> GetChunk(string userId, string currentUserId, int startIndex, int chunkSize) {
            var user = unitOfWork.Users.Get(userId);
            var posts = user.Posts
                .OrderByDescending(p => p.Time)
                .Skip(startIndex)
                .Select(p => new PostDTO {
                    Id = p.Id,
                    Body = p.Body,
                    Time = p.Time,
                    Likes = p.Likes.Count,
                    Author = userService.GetUserDisplayName(p.User),
                    AuthorId = p.UserId,
                    IsLikedByMe = activityService.IsLiked(new LikedPostDTO {
                        PostId = p.Id,
                        UserId = currentUserId
                    })
                });

            if (chunkSize != 0) {
                posts = posts.Take(chunkSize);
            }

            return posts;
        }

        public async Task<bool> Update(NewPostDTO data, int postId) {
            var post = unitOfWork.Posts.Get(postId);
            post.Body = data.Body;
            unitOfWork.Posts.Update(post);
            await unitOfWork.SaveAsync();
            return true;
        }
    }
}
