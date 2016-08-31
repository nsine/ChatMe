using System;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IAvatarService
    {
        AvatarInfo GetPath(string userId);
        AvatarInfo GetPath(User user, Func<string, string> pathResolver);
    }
}