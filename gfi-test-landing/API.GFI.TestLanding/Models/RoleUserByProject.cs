using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.GFI.TestLanding
{


    public class RoleUserByProject
    {
        public string IdUser { get; set; }
        public string IdRole { get; set; }
        public int IdProject { get; set; }
        public string NameRole { get; set; }
        public string NameProject { get; set; }
        public string Username { get; set; }

    }
}