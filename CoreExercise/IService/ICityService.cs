using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreExercise.IService
{
    public interface ICityService
    {
        // using Microsoft.AspNetCore.Mvc.Rendering; => SelectListItem
        List<SelectListItem> GetAllCities();
    }
}
