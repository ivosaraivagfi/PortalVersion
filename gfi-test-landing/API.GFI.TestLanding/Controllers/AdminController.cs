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

        //Delete Project 
        [Route("api/DeleteProject/{projectId}")]
        public HttpResponseMessage Delete( int projectId)
        {
            try {  
            var project = db.Project.Find(projectId);
            if(project == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Project with Id = " + projectId + "Not Found");
            }
            db.Project.Remove(project);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


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

        [Route("api/DeleteProject/{UserId}")]
        public HttpResponseMessage DeleteProject(string UserId)
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

        [Route("api/setUserRole")]
        public HttpResponseMessage setUserRole(UserRole userRole)
        {

            try
            {

                var user = db.UserRole.Where(u => u.id_project == userRole.id_project && u.UserId == userRole.UserId).FirstOrDefault();

                db.UserRole.Remove(user);
                db.SaveChanges();

                UserRole ur = new UserRole
                {
                    RoleId = userRole.RoleId,
                    UserId = userRole.UserId,
                    id_project = userRole.id_project
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
