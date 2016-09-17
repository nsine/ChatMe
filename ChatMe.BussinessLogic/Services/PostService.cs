using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;

namespace ChatMe.BussinessLogic.Services
{
    public class PostService : IPostService
    {

        private IUnitOfWork db;
        private IUserService userService;
        private IActivityService activityService;

        public PostService(IUnitOfWork unitOfWork, IUserService userService, IActivityService activityService) {
            this.db = unitOfWork;
            this.userService = userService;
            this.activityService = activityService;
        }

        public async Task<bool> Create(NewPostDTO data) {
            var newPost = new Post {
                Body = data.Body,
                UserId = data.UserId,
                Time = DateTime.Now
            };

            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int dialogId) {
            throw new NotImplementedException();
        }

        public PostDTO Get(string userId, string currentUserId, int postId) {
            var rawPost = db.Posts
                .Where(p => p.Id == postId)
                .FirstOrDefault();
            return new PostDTO {
                Author = userService.GetUserDisplayName(rawPost.User),
                AuthorId = rawPost.UserId,
                AuthorUserName = rawPost.User.UserName,
                Body = rawPost.Body,
                Id = rawPost.Id,
                Likes = rawPost.Likes.Count,
                Time = rawPost.Time,
                IsLikedByMe = activityService.IsLiked(new LikedPostDTO {
                    PostId = rawPost.Id,
                    UserId = currentUserId
                }),
                IsAuthorOnline = rawPost.User.IsOnline
            };
        }

        public IEnumerable<PostDTO> GetChunk(string userId, string currentUserId, int startIndex, int chunkSize) {
            var user = db.Users.FindById(userId);
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
                    AuthorUserName = p.User.UserName,
                    IsLikedByMe = activityService.IsLiked(new LikedPostDTO {
                        PostId = p.Id,
                        UserId = currentUserId
                    }),
                    IsAuthorOnline = p.User.IsOnline
                });

            if (chunkSize != 0) {
                posts = posts.Take(chunkSize);
            }

            return posts;
        }

        public async Task<bool> Update(NewPostDTO data, int postId) {
            var post = db.Posts.Find(postId);
            post.Body = data.Body;
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PostDTO>> GetNews(string userId) {
            var user = await db.Users.FindByIdAsync(userId);
            IEnumerable<PostDTO> news = user.FollowingUsers.Join(db.Posts.GetAll(), u => u.Id, p => p.UserId,
                (u, p) => new PostDTO {
                    Id = p.Id,
                    Author = p.User.DisplayName,
                    AuthorId = p.UserId,
                    AuthorUserName = p.User.UserName,
                    Body = p.Body,
                    IsLikedByMe = activityService.IsLiked(new LikedPostDTO {
                        PostId = p.Id,
                        UserId = userId
                    }),
                    Likes = p.Likes.Count,
                    Time = p.Time,
                    IsAuthorOnline = p.User.IsOnline
                });

            return news;
        }
    }
}
