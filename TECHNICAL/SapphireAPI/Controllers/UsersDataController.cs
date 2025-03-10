using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DAE.DAL.SQL;
using DAE.Configuration;
using System;
using MS.SSquare.API;
using Microsoft.AspNetCore.Authorization;
using MS.SSquare.API.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;
using DAE.Common.EncryptionDecryption;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    public class UsersDataController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public UsersDataController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("UsersData/get")]
        [HttpPost]

        public IActionResult Get(UsersData usersData)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (usersData.UserID != 0)
                {
                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserID);
                }

                if (usersData.Cinema_strID != null)
                {
                    oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, usersData.Cinema_strID);
                }

                if (usersData.UserRoleID != 0)
                {
                    oDBUtility.AddParameters("@UserRoleID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserRoleID);
                }

                if (usersData.Username != null)
                {
                    oDBUtility.AddParameters("@Username", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.Username);
                }

                if (usersData.Password != null)
                {
                    oDBUtility.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, usersData.Password);
                }

                if (usersData.FirstName != null)
                {
                    oDBUtility.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.FirstName);
                }

                if (usersData.MiddleName != null)
                {
                    oDBUtility.AddParameters("@MiddleName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.MiddleName);
                }

                if (usersData.LastName != null)
                {
                    oDBUtility.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.LastName);
                }

                if (usersData.MobileNumber != null)
                {
                    oDBUtility.AddParameters("@MobileNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 12, usersData.MobileNumber);
                }

                if (usersData.ProfilePicturePath != null)
                {
                    oDBUtility.AddParameters("@ProfilePicturePath", DBUtilDBType.Varchar, DBUtilDirection.In, 500, usersData.ProfilePicturePath);
                }

                if (usersData.DateOfBirth != null)
                {
                    oDBUtility.AddParameters("@DateOfBirth", DBUtilDBType.DateTime, DBUtilDirection.In, 10, usersData.DateOfBirth);
                }

                //if (usersData.IsActive != 0)
                //{
                //    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, usersData.IsActive);
                //}

                if (usersData.CreatedBy != 0)
                {
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, usersData.CreatedBy);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_USERS");

                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }


        [Route("UsersData/insert")]
        [HttpPost]
        public IActionResult usersDataInsert(UsersData usersData)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2" && userrole != "1")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Unauthorized(jsondata);
                }
                else
                {
                    DBUtility oDBUtility = new DBUtility(_configurationIG);

                    ConfigHandler oEncrDec;

                    oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

                    //string encryptedPassword = oEncrDec.Cryptohelper.Encrypt(usersData.Password);

                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserID);
                    oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, usersData.Cinema_strID);
                    oDBUtility.AddParameters("@UserRoleID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserRoleID);
                    oDBUtility.AddParameters("@Username", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.Username);
                    oDBUtility.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, oEncrDec.Cryptohelper.Encrypt(usersData.Password));
                    oDBUtility.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.FirstName);
                    oDBUtility.AddParameters("@MiddleName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.MiddleName);
                    oDBUtility.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.LastName);
                    oDBUtility.AddParameters("@MobileNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 12, usersData.MobileNumber);
                    oDBUtility.AddParameters("@ProfilePicturePath", DBUtilDBType.Varchar, DBUtilDirection.In, 500, usersData.ProfilePicturePath);
                    oDBUtility.AddParameters("@DateOfBirth", DBUtilDBType.DateTime, DBUtilDirection.In, 10, usersData.DateOfBirth);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, usersData.IsActive);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.CreatedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_INSERT_USERS");
                    oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }


        [Route("UsersData/update")]
        [HttpPost]
        public IActionResult Put(UsersData usersData)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2" && userrole != "1")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Unauthorized(jsondata);
                }
                else
                {
                    DBUtility oDBUtility = new DBUtility(_configurationIG);

                    ConfigHandler oEncrDec;

                    oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserID);
                    oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, usersData.Cinema_strID);
                    oDBUtility.AddParameters("@UserRoleID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserRoleID);
                    oDBUtility.AddParameters("@Username", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.Username);
                    if (usersData.Password != null)
                    {
                        oDBUtility.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, oEncrDec.Cryptohelper.Encrypt(usersData.Password));
                    }

                    oDBUtility.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.FirstName);
                    oDBUtility.AddParameters("@MiddleName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.MiddleName);
                    oDBUtility.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, usersData.LastName);
                    oDBUtility.AddParameters("@MobileNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 12, usersData.MobileNumber);
                    oDBUtility.AddParameters("@ProfilePicturePath", DBUtilDBType.Varchar, DBUtilDirection.In, 500, usersData.ProfilePicturePath);
                    oDBUtility.AddParameters("@DateOfBirth", DBUtilDBType.DateTime, DBUtilDirection.In, 10, usersData.DateOfBirth);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, usersData.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.ModifiedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_USERS");
                    oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }

        [Route("UsersData/Isactive")]
        [HttpPost]

        public IActionResult PutIsactive(UsersData usersData)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2" && userrole != "1")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Unauthorized(jsondata);
                }
                else
                {
                    DBUtility oDBUtility = new DBUtility(_configurationIG);

                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 11, usersData.UserID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, usersData.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_USERS_ISACTIVE");
                    oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("ItemMasterItemCode/ProPicImage")]
        [HttpPost]
        public JsonResult SaveFiles()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/profilepic/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }

        }

    }
}
