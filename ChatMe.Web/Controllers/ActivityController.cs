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
    [RoutePrefix("api/activity")]
    public class ActivityController : IdentityController
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService) {
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
    }
}