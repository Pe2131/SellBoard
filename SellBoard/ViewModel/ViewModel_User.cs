using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SellBoard.ViewModel
{
    public class ViewModel_User
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "please fill FullName")]
        public string FullName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "please fill Mobile")]
        public string Mobile { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
    }
}
