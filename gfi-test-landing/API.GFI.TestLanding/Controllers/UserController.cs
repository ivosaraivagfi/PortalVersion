using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.GFI.TestLanding
{
    public class UserController : ApiController
    {

        private testLandingEntities db = new testLandingEntities();

        [Route("api/GetUser/{idUser}")]
        public UserModel GetUsers(string idUser)
        {
            var UsersList = (from u in db.AspNetUsers
                             where u.Id == idUser
                             select new UserModel
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 UserName = u.UserName,
                                 ImageUrl = u.ImageUrl,
                                 PhoneNumber = u.PhoneNumber

                             }).FirstOrDefault();

            return UsersList;
        }

    }
}
