//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.GFI.TestLanding
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportCollection
    {
        public int id { get; set; }
        public Nullable<int> project_id { get; set; }
        public Nullable<int> report_id { get; set; }
        public Nullable<System.DateTime> date_start { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public string status { get; set; }
        public string general_message { get; set; }
        public string error_message { get; set; }
        public string error_type { get; set; }
        public string logs { get; set; }
        public string test_name { get; set; }
        public string author { get; set; }
        public string duration { get; set; }
        public string area { get; set; }
        public byte[] screenshot { get; set; }
    }
}
