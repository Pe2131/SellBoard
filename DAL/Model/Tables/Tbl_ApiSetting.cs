using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model.Tables
{
    public class Tbl_ApiSetting
    {
        [Key]
        public int id { get; set; }
        public string BaseUrl { get; set; }
        public string PostUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
    }
}
