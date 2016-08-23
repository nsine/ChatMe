using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class MessagesViewModel
    {
        const int SNIPPET_LENGTH = 97;

        public MessagesViewModel(User me) {
            Dialogs = new List<DialogViewModel>();

            foreach (var rowDialog in me.Dialogs) {
                var authors = rowDialog.Users
                    .Where(u => u.Id != me.Id)
                    .Select(u => u.UserName);
                var authorString = string.Join(", ", authors);
                var msgSnippet = rowDialog.Messages
                    .OrderByDescending(m => m.Time)
                    .FirstOrDefault()?.Body;

                if (msgSnippet.Length > 100) {
                    msgSnippet = msgSnippet.Substring(0, SNIPPET_LENGTH) + "...";
                }

                Dialogs.Add(new DialogViewModel
                {
                    Author = authorString,
                    LastMessageSnippet = msgSnippet
                });
            }
        }

        public ICollection<DialogViewModel> Dialogs { get; set; }
    }
}