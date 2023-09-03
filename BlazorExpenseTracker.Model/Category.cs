using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Category name is requerid")]
        public string? Name { get; set; }
    }
}
