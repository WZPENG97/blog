﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class Blog
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Title"), Required]
        public string Title { get; set; }

        [DisplayName("Content"), Required]
        public string Content { get; set; }

        [DisplayName("PubDate"),DataType(DataType.Date)]
        public DateTime? PubDate { get; set; }

        [DisplayName("UserId")]
        public int UserId { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }
    }
}