﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Api.Models
{
    public class Bug_Reporting_Collection
    {
        public int Id { get; set; }
        public int Step_id { get; set; }
        public int Bug_report_id { get; set; }
    }
}