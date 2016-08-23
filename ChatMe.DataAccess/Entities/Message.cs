﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ChatMe.DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string UserFromId { get; set; }
        public virtual User UserFrom { get; set; }

        [Required]
        public string UserToId { get; set; }
        public virtual User UserTo { get; set; }
    }
}