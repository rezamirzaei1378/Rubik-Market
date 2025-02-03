using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.ViewModels.Blog.BlogGroups
{
    public class AddBlogGroupViewModel
    {
        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupName { get; set; }
    }

    public enum AddBlogGroupResult
    {
        Success,
        Error
    }
}
