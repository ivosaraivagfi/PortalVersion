using API.GFI.TestLanding;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Api.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        private testLandingEntities db = new testLandingEntities();
        

        [Route("api/GetProjects")]
        public List<ProjectModel> GetProjects()
        {
            var ProjectList = (from p in db.Project
                               select new ProjectModel
                               {
                                   Id = p.id,
                                   Name = p.name,
                                   Description = p.description,
                                   Logo_url = p.logo_url
                               }).ToList();

            return ProjectList;
        }


        [Route("api/CreateProject")]
        public HttpResponseMessage Post([FromBody]ProjectModel project)
        {
            try
            {
                Project p = new Project
                {
                    name = project.Name,
                    description = project.Description,
                    logo_url = project.Logo_url,
                    projectImage = project.ByteImage
                    
                };
                db.Project.Add(p);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, p);
                message.Headers.Location = new Uri(Request.RequestUri + p.name);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[Route("api/GetProject/{idProject}")]
        //[HttpGet]
        //public IHttpActionResult Get(Project project)
        //{
        //    Project proj = null;

        //    using (var db = new testLandingEntities())
        //    {
        //        proj = db.Project.Where(p => p.id == project.id).FirstOrDefault();
        //    }

        //    if (proj == null )
        //    {
        //        return NotFound();
        //    }

        //    return Ok(project);
        //}


        [Route("api/ProjectEdit")]
        [HttpPut][HttpPost]
        public IHttpActionResult ProjectEdit(ProjectModel project)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var db = new testLandingEntities())
            {
                var existingProject = db.Project.Where(p => p.id == project.Id)
                                                        .FirstOrDefault();

                if (existingProject != null)
                {
                    existingProject.name = project.Name;
                    existingProject.description = project.Description;
                    existingProject.logo_url = project.Logo_url;
                    db.Entry(existingProject).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        //Delete Project 
        [Route("api/DeleteProject/{projectId}")]
        [HttpDelete]
        public HttpResponseMessage DeleteProject(int projectId)
        {
            if (projectId <= 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not a valid student id");

            try {
                
            using (var db = new testLandingEntities())
            {
                var project = db.Project
                    .Where(p => p.id == projectId)
                    .FirstOrDefault();

                db.Entry(project).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.OK, project);
                message.Headers.Location = new Uri(Request.RequestUri + project.name);
                return message;
            }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //public HttpResponseMessage Project( int projectId)
        //{
        //    try {  
        //    var project = db.Project.Find(projectId);
        //    if(project == null)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Project with Id = " + projectId + "Not Found");
        //    }
        //    db.Project.Remove(project);
        //    db.SaveChanges();

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch(Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        // Update Project
        [Route("api/UpdateProject/{idProject, value}")]
        public void Put(int projectId, [FromBody]string value)
        {

        }

        [Route("api/GetRoles")]
        public List<RolesModel> GetRoles()
        {
            var RoleList = (from r in db.AspNetRoles
                            select new RolesModel
                            {
                                Id = r.Id,
                                Name = r.Name

                            }).ToList();

            return RoleList;
        }

        [Route("api/RegisterUser")]
        public HttpResponseMessage SetRegisterUser([FromBody]RegisterViewModel project)
        {

            return null;

        }

        [Route("api/GetUsers")]
        public List<UserModel> GetUsers()
        {
            var UsersList = (from u in db.AspNetUsers
                             select new UserModel
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,


                             }).ToList();

            return UsersList;
        }

        [Route("api/EditUser")]
        public HttpResponseMessage EditUser([FromBody]AspNetUsers user)
        {
            try
            {
                AspNetUsers userRow = db.AspNetUsers.Find(user.Id);
                // userRow.Email = user.Email;
                userRow.PhoneNumber = user.PhoneNumber;
                userRow.ImageUrl = user.ImageUrl;
                userRow.FirstName = user.FirstName;
                userRow.LastName = user.LastName;

                db.Entry(userRow).State = EntityState.Modified;

                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, userRow);
                message.Headers.Location = new Uri(Request.RequestUri + userRow.Email);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/DeleteUser/{UserId}")]
        public HttpResponseMessage DeleteUser(string UserId)
        {
            try
            {

                var user = db.AspNetUsers
                .Where(u => u.Id == UserId)
                .FirstOrDefault();

                //db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.AspNetUsers.Remove(user);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, user);
                message.Headers.Location = new Uri(Request.RequestUri + user.Email);
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [Route("api/RoleProjectByUser/{UserId}")]
        public List<RoleUserByProject> GetRoleProjectByUser(string UserId)
        {
            var UsersList = (from u in db.UserRole
                             where u.UserId == UserId
                             select new RoleUserByProject
                             {
                                 IdUser = UserId,
                                 IdProject = u.id_project,
                                 IdRole = u.RoleId,
                                 NameProject = u.Project.name,
                                 NameRole = u.AspNetRoles.Name

                             }).ToList();



            return UsersList;
        }

        [Route("api/DeleteUserRole")]
        public HttpResponseMessage DeleteUserRole([FromBody] UserRole userrole)
        {
            try
            {

                var usersrole = db.UserRole
                .Where(ur => ur.id_project == userrole.id_project && ur.UserId == userrole.UserId)
                .FirstOrDefault();

                //db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.UserRole.Remove(usersrole);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, usersrole);
               // message.Headers.Location = new Uri(Request.RequestUri + usersrole);
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/setUserRole")]
        public HttpResponseMessage setUserRole(UserRole userRole)
        {

            try
            {

                var user = db.UserRole.Where(u => u.id_project == userRole.id_project && u.UserId == userRole.UserId).FirstOrDefault();

                if (user != null)
                {
                    db.UserRole.Remove(user);
                    db.SaveChanges();
                }

                UserRole ur = new UserRole
                {
                    RoleId = userRole.RoleId,
                    UserId = userRole.UserId,
                    id_project = userRole.id_project,
                    date = DateTime.Now
                };
                db.UserRole.Add(ur);

                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, ur);
                message.Headers.Location = new Uri(Request.RequestUri + ur.UserId);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/RoleUserByProject/{ProjectId}")]
        public List<RoleUserByProject> GetRoleUserByProject(int ProjectId)
        {
           // var ProjectId = int.Parse(ProjId);
            var UsersList = (from u in db.UserRole
                             where u.id_project == ProjectId
                             select new RoleUserByProject
                             {
                                 IdUser = u.UserId,
                                 IdProject = ProjectId,
                                 IdRole = u.RoleId,
                                 NameProject = u.Project.name,
                                 NameRole = u.AspNetRoles.Name,
                                 Username = u.AspNetUsers.UserName
                             }).ToList();



            return UsersList;
        }
    }
}
