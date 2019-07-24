using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class GetModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DataModel> data { get; set; }
    }
}
