using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalDiary.Models
{
    public class User
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("UserName"), Required]
        public string UserName { get; set; }

        [DisplayName("PassWord"), Required, DataType(DataType.Password)]
        public string PassWord { get; set; }

        [DisplayName("Blogs")]
        public virtual List<Blog> Blogs { get; set; }
    }
}