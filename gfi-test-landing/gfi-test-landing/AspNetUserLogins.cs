
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
    
public partial class AspNetUserLogins
{

    public string LoginProvider { get; set; }

    public string ProviderKey { get; set; }

    public string UserId { get; set; }

    public Nullable<int> id_project { get; set; }



    public virtual Project Project { get; set; }

    public virtual AspNetUsers AspNetUsers { get; set; }

}

}
