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
    
    public partial class Tools_Step
    {
        public int id { get; set; }
        public Nullable<int> id_tools_test { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string error_message { get; set; }
        public string screenshot { get; set; }
        public string duration { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_start { get; set; }
        public Nullable<System.DateTime> data_end { get; set; }
    
        public virtual Tools_Test Tools_Test { get; set; }
    }
}
