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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using DAE.Common.EncryptionDecryption;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;



namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class UserControllerController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;
        private readonly IJwtAuth jwtAuth;


        public UserControllerController(IDaeConfigManager configuration, IWebHostEnvironment env, IJwtAuth jwtAuth)
        {
            _configurationIG = configuration;
            this._env = env;
            this.jwtAuth = jwtAuth;
        }
        //[AllowAnonymous]
        //[Route("user/login")]
        //[HttpPost]

        //public IActionResult Login(UserController users)
        // {
        //    try
        //    {


        //        ConfigHandler oEncrDec;
        //        oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

        //        DBUtility oDBUtility = new DBUtility(_configurationIG);
        //        oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 50, users.Email);

        //        //oDBUtility.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, users.Password);

        //        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_LOGIN");


          
              
        //        int status_code = Convert.ToInt32(ds.Tables[0].Rows[0]["status_code"].ToString());

        //        if (status_code == 100)
        //        {
        
        //            string decryptedPassword = oEncrDec.Cryptohelper.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
          
        //            if (users.Password == decryptedPassword)
        //            {


        //                bool isFirstLogin = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFirstLogin"]);


        //                if (isFirstLogin)
        //                {
        //                    oServiceRequestProcessor = new ServiceRequestProcessor();
        //                    var jsonData = new
        //                    {
        //                        status_code = 500,
        //                        Message = "You've logged in for first time! Kindly change your password.",
        //                        data = oServiceRequestProcessor.ProcessRequest(ds)
        //                    };

        //                    return Ok(jsonData);
        //                }
        //                else
        //                {
        //                    oServiceRequestProcessor = new ServiceRequestProcessor();
        //                    var username = ds.Tables[0].Rows[0]["Email"].ToString();
        //                    var userID = ds.Tables[0].Rows[0]["UserID"].ToString();
        //                    var roleID = ds.Tables[0].Rows[0]["RoleID"].ToString();
        //                    var token = jwtAuth.Authentication(username, userID, roleID);

        //                    System.Data.DataColumn newColumn = new System.Data.DataColumn("Token", typeof(System.String));
        //                    newColumn.DefaultValue = token.ToString();
        //                    ds.Tables[0].Columns.Add(newColumn);
        //                    ds.Tables[0].Columns.Remove("password");
        //                    ds.Tables[0].Columns.Remove("Email");
        //                    ds.Tables[0].Columns.Remove("UserID");
        //                    ds.Tables[0].Columns.Remove("RoleID");

        //                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
        //                }


        //            }
        //            else
        //            {
        //                DBUtility oDBUtility1 = new DBUtility(_configurationIG);
        //                string userID = ds.Tables[0].Rows[0]["UserID"].ToString();
        //                // Handle IsLocked when it's NULL
        //                int isLocked = ds.Tables[0].Rows[0]["IsLocked"] == DBNull.Value? 0: Convert.ToInt32(ds.Tables[0].Rows[0]["IsLocked"]);
        //                // Calculate remaining attempts
        //                int remainingAttempts = 5 - isLocked;
        //                if (isLocked < 5)
        //                {
        //                    isLocked++;  // Increment the IsLocked count if it's less than 5
        //                    remainingAttempts = 5 - isLocked; // Recalculate remaining attempts

        //                }
        //                else
        //                {
        //                    // If IsLocked is 5 or more, don't increment and set the response to indicate the account is locked
        //                    oServiceRequestProcessor = new ServiceRequestProcessor();
        //                    var jsonData = new
        //                    {
        //                        status_code = 403,
        //                        Message = "Account is locked due to multiple failed login attempts.",

        //                        data = oServiceRequestProcessor.ProcessRequest(ds)


        //                };
        //                    return Ok(jsonData);

        //                }


             

        //                oDBUtility1.AddParameters("@USERID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userID);

        //                oDBUtility1.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 50, users.Email);
        //                oDBUtility1.AddParameters("@IsLocked", DBUtilDBType.Integer, DBUtilDirection.In, 10, isLocked);

        //                oDBUtility1.Execute_StoreProc_DataSet("USP_UPDATE_USERS");

        //                // If attempts remaining > 0, show how many attempts are left
        //                if (remainingAttempts > 0)
        //                {
        //                    var jsonData = new
        //                    {
        //                        status_code = 403,
        //                        Message = $"{remainingAttempts} attempt{(remainingAttempts > 1 ? "s" : "")} remaining. Please try again."
        //                    };
        //                    return Ok(jsonData);
        //                }
        //                else
        //                {
        //                    // If remaining attempts == 0, show account is locked message
        //                    var jsonData = new
        //                    {
        //                        status_code = 403,
        //                        Message = "Account is locked due to multiple failed login attempts. Please try again later."
        //                    };
        //                    return Ok(jsonData);
        //                }




        //                oServiceRequestProcessor = new ServiceRequestProcessor();
        //                return Ok(oServiceRequestProcessor.onUserNotFound());
        //            }
               
        //        }
        //        else
        //        {
        //            oServiceRequestProcessor = new ServiceRequestProcessor();
        //            return Ok(oServiceRequestProcessor.ProcessRequest(ds));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        oServiceRequestProcessor = new ServiceRequestProcessor();
        //        return BadRequest(oServiceRequestProcessor.onError(ex.Message));
        //    }
        //}


    }
}
