﻿using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;

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

            unitOfWork.Posts.Add(newPost);
            await unitOfWork.SaveAsync();
            return true;
        }

        public Task<bool> Delete(int dialogId) {
            throw new NotImplementedException();
        }

        public PostDTO Get(string userId, string currentUserId, int postId) {
            var rawPost = unitOfWork.Posts
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
                })
            };
        }

        public IEnumerable<PostDTO> GetChunk(string userId, string currentUserId, int startIndex, int chunkSize) {
            var user = unitOfWork.Users.FindById(userId);
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
                    })
                });

            if (chunkSize != 0) {
                posts = posts.Take(chunkSize);
            }

            return posts;
        }

        public async Task<bool> Update(NewPostDTO data, int postId) {
            var post = unitOfWork.Posts.FindById(postId);
            post.Body = data.Body;
            unitOfWork.Posts.Update(post);
            await unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PostDTO>> GetNews(string userId) {
            var user = await unitOfWork.Users.FindByIdAsync(userId);
            IEnumerable<PostDTO> news = user.FollowingUsers.Join(unitOfWork.Posts.GetAll(), u => u.Id, p => p.UserId,
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
                    Time = p.Time
                });

            return news;
        }
    }
}
