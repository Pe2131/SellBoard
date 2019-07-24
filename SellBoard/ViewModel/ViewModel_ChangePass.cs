using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SellBoard.ViewModel
{
    public class ViewModel_ChangePass
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Error in authentication")]
        public string UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Error in authentication")]
        public string code { get; set; }
    }
}
