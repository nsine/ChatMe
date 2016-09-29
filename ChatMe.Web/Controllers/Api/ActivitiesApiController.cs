using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/activity")]
    public class ActivitiesApiController : ApiController
    {
        private IActivityService activityService;

        public ActivitiesApiController(IActivityService activityService) {
            this.activityService = activityService;
        }

        [HttpPost]
        [Route("like")]
        public async Task LikeAction(int postId) {
            var likeData = new LikedPostDTO {
                PostId = postId,
                UserId = User.Identity.GetUserId()
            };

            await activityService.ChangeLike(likeData);
        }

        [HttpPost]
        [Route("follow")]
        public async Task FollowAction([FromBody]FollowingLinkViewModel viewModel) {
            var followerId = RequestContext.Principal.Identity.GetUserId();
            var name = User.Identity.GetUserName();

            var followerData = new FollowerLinkDTO {
                UserId = viewModel.UserId,
                FollowingUserId = viewModel.FollowingUserId
            };

            await activityService.ChangeFollow(followerData);
        }
    }
}