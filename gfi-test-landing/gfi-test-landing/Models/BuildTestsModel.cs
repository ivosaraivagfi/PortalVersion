﻿using System;
using System.Linq;
using System.Web;

namespace gfi_test_landing.Models
{
    public class BuildTestsModel
    {

            public int Id { get; set; }
            public int Project_id { get; set; }
            public int? Report_id { get; set; }
            public DateTime? Date_start { get; set; }
            public DateTime? Date_end { get; set; }
            public string Status { get; set; }
            public string General_message { get; set; }
            public string Error_message { get; set; }
            public string Browser { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public string Duration { get; set; }
            public string Site { get; set; }
            public byte[] Screenshot { get; set; }

    }
}