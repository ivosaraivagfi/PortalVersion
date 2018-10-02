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
    
    public partial class Report
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_start { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public string status { get; set; }
        public string general_message { get; set; }
        public string error_message { get; set; }
        public string warning_message { get; set; }
        public string error_type { get; set; }
        public string logs { get; set; }
        public Nullable<int> id_batteryTest { get; set; }
        public Nullable<int> id_machine { get; set; }
        public Nullable<int> pass_tests { get; set; }
        public string duration { get; set; }
        public Nullable<int> total_tests { get; set; }
        public Nullable<int> failed_tests { get; set; }
        public Nullable<int> skipped_tests { get; set; }
    
        public virtual BatteryTest BatteryTest { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
