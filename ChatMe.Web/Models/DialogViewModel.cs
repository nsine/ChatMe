using AutoMapper;
using ChatMe.BussinessLogic;
using ChatMe.BussinessLogic.DTO;
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

        public DialogViewModel(DialogPreviewDTO dialogData) {
            var authors = dialogData.Users
                    .Select(u => u.UserName);
            var authorString = string.Join(", ", authors);
            var lastAuthor = dialogData.LastMessageAuthor;
            var lastMessage = dialogData.LastMessage;

            var msgSnippet = new StringBuilder();

            if (lastMessage == null) {
                msgSnippet.Append("Empty dialog");
            } else {
                msgSnippet.Append($"{lastAuthor}: ");
                if (msgSnippet.Length > 100) {
                    msgSnippet.Append(lastMessage.Substring(0, SNIPPET_LENGTH) + "...");
                } else {
                    msgSnippet.Append(lastMessage);
                }
            }

            Author = authorString;
            LastMessageSnippet = msgSnippet.ToString();
            Id = dialogData.Id;
        }

        public int Id { get; set; }

        public string Author { get; set; }
        public string LastMessageSnippet { get; set; }
        public string AvatarUrl { get; set; }
    }
}