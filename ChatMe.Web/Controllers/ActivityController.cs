using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Controllers.Abstract;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    public class ActivityController : IdentityController
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService) {
            this.activityService = activityService;
        }

        [HttpPost]
        [Route("api/activity/like")]
        public async Task LikeAction(int postId) {
            var likeData = new LikedPostDTO {
                PostId = postId,
                UserId = User.Identity.GetUserId()
            };

            await activityService.ChangeLike(likeData);
        }

        [HttpPost]
        public async Task<ActionResult> FollowAction(FollowingLinkViewModel viewModel) {
            var followerId = User.Identity.GetUserId();

            var followerData = new FollowerLinkDTO {
                UserId = User.Identity.GetUserId(),
                FollowingUserId = viewModel.UserId
            };

            await activityService.ChangeFollow(followerData);

            viewModel.IsFollowing = !viewModel.IsFollowing;

            return PartialView(viewModel);
        }
    }
}