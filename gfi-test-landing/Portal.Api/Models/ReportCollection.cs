﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Api.Models
{
    public class ReportCollection
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int Report_id { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }
        public string Status { get; set; }
        public string General_message { get; set; }
        public string Error_message { get; set; }
        public string Error_type { get; set; }
        public string Logs { get; set; }
        public string Test_name { get; set; }
        public string Author { get; set; }
        public DateTime Duration { get; set; }
        public string Area { get; set; }
        public byte[] Screenshot { get; set; }
    }
}