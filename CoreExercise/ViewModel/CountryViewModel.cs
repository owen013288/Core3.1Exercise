using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreExercise.ViewModel
{
    public class CountryViewModel
    {
        //[Required(ErrorMessage ="Country欄位不得為空白")]
        [Required]
        public string Country { get; set; }

        // using Microsoft.AspNetCore.Mvc.Rendering; => SelectListItem
        public List<SelectListItem> Countries { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "USA", Value="US" },
            new SelectListItem { Text = "Canada", Value="CA" },
            new SelectListItem { Text = "Japan", Value="JP" },
        };
    }
}
