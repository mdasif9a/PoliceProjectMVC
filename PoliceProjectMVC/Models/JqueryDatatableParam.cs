﻿using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class JqueryDatatableParam
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string search { get; set; }
    }
}