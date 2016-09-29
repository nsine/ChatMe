using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        private IPostService postService;

        public NewsController(IPostService postService) {
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
        public async Task<IEnumerable<PostViewModel>> GetNews(string userId) {
            var newsData = await postService.GetNews(userId);
            var news = Mapper.Map<IEnumerable<PostViewModel>>(newsData);

            return news.ToList();   
        }

        [HttpGet]
        [Route("{userId}/{postId}")]
        public PostViewModel Get(string userId, int postId) {
            var postData = postService.Get(userId, User.Identity.GetUserId(), postId);
            return Mapper.Map<PostViewModel>(postData);
        }
    }
}