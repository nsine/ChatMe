using ChatMe.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using ChatMe.BussinessLogic.Services.Abstract;
using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private IPostService postService;

        public PostsController(IPostService postService) {
            this.postService = postService;

            Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>()
                .ForMember("AvatarUrl", opt => opt.MapFrom(p =>
                    Url.Route("Avatar", new { userId = p.AuthorId }))
                )
                .ForMember("AuthorLink", opt => opt.MapFrom(p =>
                    Url.Route("UserProfile", new { userName = p.AuthorUserName }))
                )
            );
        }
        
        [HttpGet]
        [Route("{userId}")]
        public IEnumerable<PostViewModel> GetAll(string userId, int startIndex = 0, int count = 0) {
            var postsData = postService.GetChunk(userId, User.Identity.GetUserId(), startIndex, count);
            Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>()
                .ForMember("AvatarUrl", opt => opt.MapFrom(p =>
                   Url.Route("Avatar", new { userId = p.AuthorId }))
                )
                .ForMember("AuthorLink", opt => opt.MapFrom(p =>
                    Url.Route("UserProfile", new { userName = p.AuthorUserName }))
                )
            );

            var posts = Mapper.Map<IEnumerable<PostViewModel>>(postsData);
            return posts;
        }

        [HttpGet]
        [Route("{userId}/{postId}")]
        public PostViewModel Get(string userId, int postId) {
            var postData = postService.Get(userId, User.Identity.GetUserId(), postId);
            return Mapper.Map<PostViewModel>(postData);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task Post(string userId, NewPostViewModel postModel) {
            var newPostData = new NewPostDTO {
                Body = postModel.Body,
                UserId = userId
            };

            await postService.Create(newPostData);
        }

        [HttpPut]
        [Route("{postId}")]
        public async Task Put(int postId, NewPostViewModel postModel) {
            var newPostData = new NewPostDTO {
                Body = postModel.Body
            };

            await postService.Update(newPostData, postId);
        }

        [HttpDelete]
        [Route("{postId}")]
        public async Task Delete(int postId) {
            await postService.Delete(postId);
        }
    }
}
