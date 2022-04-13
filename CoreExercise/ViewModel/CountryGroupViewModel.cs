using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreExercise.ViewModel
{
    public class CountryGroupViewModel
    {
        public string Country { get; set; }

        public IEnumerable<string> CountryCodes { get; set; }

        static SelectListGroup NorthAmericaGroup { get; } = new SelectListGroup { Name = "北美洲" };
        static SelectListGroup EuropeGroup { get; } = new SelectListGroup { Name = "歐洲" };

        public List<SelectListItem> Countries { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Mexico", Value = "MX", Group = NorthAmericaGroup },
            new SelectListItem { Text = "Canada", Value = "CA", Group = NorthAmericaGroup, Selected=true},
            new SelectListItem { Text = "USA", Value = "US", Group = NorthAmericaGroup },
            new SelectListItem { Text = "France", Value = "FR", Group = EuropeGroup },
            new SelectListItem { Text = "Spain", Value = "ES", Group = EuropeGroup },
            new SelectListItem { Text = "Germany", Value = "DE", Group = EuropeGroup, Disabled=true},
        };

    }
}
