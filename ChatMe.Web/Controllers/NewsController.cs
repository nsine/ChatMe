using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsController : Controller
    {
        private IPostService postService;

        public NewsController(IPostService postService) {
            this.postService = postService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult> GetNews(string userId) {
            var newsData = await postService.GetNews(userId);

            Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>()
                .ForMember("AvatarUrl", opt => opt.MapFrom(p =>
                    Url.Action("GetAvatar", "Users", new { id = p.AuthorId }))
                )
                .ForMember("AuthorLink", opt => opt.MapFrom(p =>
                    Url.RouteUrl("UserProfile", new { userName = p.AuthorUserName }))
                )
            );

            var news = Mapper.Map<IEnumerable<PostViewModel>>(newsData);

            return Json(news.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{userId}/{postId}")]
        public ActionResult Get(string userId) {
            return RedirectToAction("Get", "Posts");
        }
    }
}