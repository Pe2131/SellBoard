using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model.Tables
{
    public class Tbl_User : IdentityUser
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
    }
}
