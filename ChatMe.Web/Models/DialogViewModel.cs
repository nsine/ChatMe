using ChatMe.BussinessLogic;
using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ChatMe.Web.Models
{
    public class DialogViewModel
    {
        const int SNIPPET_LENGTH = 97;

        public DialogViewModel(Dialog rowDialog, User me) {
            var authors = rowDialog.Users
                    .Where(u => u.Id != me.Id)
                    .Select(u => u.UserName);
            var authorString = string.Join(", ", authors);
            var lastMessage = rowDialog.Messages
                .OrderByDescending(m => m.Time)
                .FirstOrDefault();
            var lastAuthor = lastMessage?.User.DisplayName;
            var msgSnippet = new StringBuilder();

            if (lastMessage == null) {
                msgSnippet.Append("Empty dialog");
            } else {
                msgSnippet.Append($"{lastAuthor}: ");
                if (msgSnippet.Length > 100) {
                    msgSnippet.Append(lastMessage.Body.Substring(0, SNIPPET_LENGTH) + "...");
                } else {
                    msgSnippet.Append(lastMessage.Body);
                }
            }

            Author = authorString;
            LastMessageSnippet = msgSnippet.ToString();
            Id = rowDialog.Id;
        }

        public int Id { get; set; }

        public string Author { get; set; }
        public string LastMessageSnippet { get; set; }
        public string AvatarUrl { get; set; }
    }
}