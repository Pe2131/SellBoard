
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class Extention
{

    public static string GetErrors(this ModelStateDictionary modelState)
    {
        if (modelState.ErrorCount!=0)
        {
            return string.Join("<br/>", (from item in modelState
                                         where item.Value.Errors.Any()
                                         select item.Value.Errors[0].ErrorMessage).ToList());
        }
        return null;

    }


}
