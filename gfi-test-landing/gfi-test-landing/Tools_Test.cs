
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace gfi_test_landing
{

using System;
    using System.Collections.Generic;
    
public partial class Tools_Test
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Tools_Test()
    {

        this.Tools_Step = new HashSet<Tools_Step>();

    }


    public int id { get; set; }

    public int id_build { get; set; }

    public int id_project { get; set; }

    public string name { get; set; }

    public string status { get; set; }

    public Nullable<System.DateTime> date_start { get; set; }

    public Nullable<System.DateTime> date_end { get; set; }

    public string duration { get; set; }

    public string browser { get; set; }

    public string site { get; set; }

    public string general_message { get; set; }

    public string description { get; set; }

    public string error_message { get; set; }



    public virtual Build Build { get; set; }

    public virtual Project Project { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Tools_Step> Tools_Step { get; set; }

}

}
