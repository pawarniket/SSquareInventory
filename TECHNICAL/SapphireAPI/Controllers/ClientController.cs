using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Common.EncryptionDecryption;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class ClientController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public ClientController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("Client/get")]
        [HttpPost]
        public IActionResult Get(Client client)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole == "5")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Unauthorized(jsondata);
                }
                else
                {
                    if (userrole == "2" && (client.ClientID == 0 || client.ClientID == null))
                    {
                        var jsondata = new
                        {
                            Error = "Client ID is Required"
                        };
                        return Ok(jsondata);
                    }
                    else
                    {
                        DBUtility oDBUtility = new DBUtility(_configurationIG);
                        {
                            if (client.ClientID != 0 && client.ClientID != null)
                            {
                                oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, client.ClientID);
                            }
                            if (client.Name != null)
                            {
                                oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 10, client.Name);
                            }
                            oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, client.IsActive);


                            DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CLIENT");

                            oServiceRequestProcessor = new ServiceRequestProcessor();
                            return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        //[Route("Client/add")]
        //[HttpPost]
        //public IActionResult Post(Client Client)
        //{
        //    try
        //    {
        //        DBUtility oDBUtility = new DBUtility(_configurationIG);
        //        oDBUtility.AddParameters("@ClientTypeId", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.ClientTypeId);


        //        oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Name);
        //        oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Client.Email);
        //        oDBUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 225, Client.Address);
        //        oDBUtility.AddParameters("@City", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.City);
        //        oDBUtility.AddParameters("@State", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.State);
        //        oDBUtility.AddParameters("@Country", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.Country);
        //        oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.IndustryID);
        //        oDBUtility.AddParameters("@ParentClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.ParentClientID);
        //        oDBUtility.AddParameters("@Regulated", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Regulated);
        //        oDBUtility.AddParameters("@Certified", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Certified);
        //        oDBUtility.AddParameters("@Pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Pincode);
        //        oDBUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.GSTNo);
        //        oDBUtility.AddParameters("@ContactPersonname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersonname);
        //        oDBUtility.AddParameters("@ContactPersondesignation", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersondesignation);
        //        oDBUtility.AddParameters("@ContactPersonmobile", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.@ContactPersonmobile);
        //        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, Client.IsActive);
        //        //oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 5, Client.CreatedBy);
        //        oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, Client.CreatedBy);




        //        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CLIENT");

        //        string statusCode = ds.Tables[0].Rows[0]["status_Code"].ToString();

        //        // Check if the status code is "100"
        //        if (statusCode == "100")
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                int ClientID = ds.Tables[0].Rows[0]["identity"] != DBNull.Value
        //                    ? Convert.ToInt32(ds.Tables[0].Rows[0]["identity"])
        //                    : 0;

        //                ConfigHandler oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);
        //                string encryptedPassword = oEncrDec.Cryptohelper.Encrypt("123456"); // Using mobile as password example





        //                DBUtility oDBUtility1 = new DBUtility(_configurationIG);
        //                oDBUtility1.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Name);
        //                oDBUtility1.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Name);
        //                oDBUtility1.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Client.Email);


        //                oDBUtility1.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, encryptedPassword);
        //                oDBUtility1.AddParameters("@RoleID", DBUtilDBType.Integer, DBUtilDirection.In, 255, 2); // Use a specific RoleID
        //                oDBUtility1.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, ClientID);
        //                oDBUtility1.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 255, 0);
        //                oDBUtility1.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, true);
        //                oDBUtility1.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, Client.CreatedBy);

        //                DataSet dsUser = oDBUtility1.Execute_StoreProc_DataSet("USP_ADD_USER");

        //                // Check if user insertion was successful
        //                if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
        //                {
        //                    return Ok(new { statusCode = 100, Message = "Vendor successfully added." });


        //                }
        //                else
        //                {
        //                    return BadRequest(new { message = "User addition failed" });
        //                }
        //            }


        //            else if (statusCode == "200")
        //            {
        //                return BadRequest(new { statusCode = 300 });
        //            }
        //            else
        //            {
        //                return BadRequest(new { statusCode = 300 });
        //            }
        //        }



        //        else
        //        {
        //            return BadRequest(new { statusCode = 300 });
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        oServiceRequestProcessor = new ServiceRequestProcessor();
        //        return BadRequest(oServiceRequestProcessor.onError(ex.Message));
        //    }

        //}


        [Route("Client/add")]
        [HttpPost]
        public IActionResult Post(Client client)
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
                    DBUtility dbUtility = new DBUtility(_configurationIG);

                    // Adding parameters for the client
                    dbUtility.AddParameters("@ClientTypeId", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.ClientTypeId);
                    dbUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 100, client.Name);
                    dbUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, client.Email);
                    dbUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 225, client.Address);
                    dbUtility.AddParameters("@City", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.City);
                    dbUtility.AddParameters("@State", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.State);
                    dbUtility.AddParameters("@Country", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.Country);
                    dbUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.IndustryID);
                    dbUtility.AddParameters("@ParentClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.ParentClientID);
                    dbUtility.AddParameters("@Regulated", DBUtilDBType.Varchar, DBUtilDirection.In,5000,  client.Regulated);
                    dbUtility.AddParameters("@Certified", DBUtilDBType.Varchar, DBUtilDirection.In,5000,  client.Certified);
                    dbUtility.AddParameters("@Pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 10, client.Pincode);
                    dbUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.GSTNo);
                    dbUtility.AddParameters("@ContactPersonname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersonname);
                    dbUtility.AddParameters("@ContactPersonlastname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersonlastname);

                    dbUtility.AddParameters("@ContactPersondesignation", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersondesignation);
                    dbUtility.AddParameters("@ContactPersonmobile", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersonmobile);

                    dbUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, client.IsActive);
                    dbUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, client.CreatedBy);
                    dbUtility.AddParameters("@OfficeAddress", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, client.OfficeAddress);
                    DataSet ds = dbUtility.Execute_StoreProc_DataSet("USP_ADD_CLIENT");

                    // Check the status code
                    string statusCode = ds.Tables[0].Rows[0]["status_Code"].ToString();
                    string message = ds.Tables[0].Rows[0]["message"].ToString();

                    if (statusCode == "100" && ds.Tables[0].Rows.Count > 0)
                    {
                        int clientID = Convert.ToInt32(ds.Tables[0].Rows[0]["identity"] ?? 0);

                        ConfigHandler configHandler = new ConfigHandler(_configurationIG.EncryptionDecryptionAlgorithm, _configurationIG.EncryptionDecryptionKey);
                        string encryptedPassword = configHandler.Cryptohelper.Encrypt("123456"); // Using mobile as password example

                        // Add user details
                        DBUtility dbUtility1 = new DBUtility(_configurationIG);
                        dbUtility1.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersonname);
                        dbUtility1.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, client.ContactPersonlastname);
                        dbUtility1.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, client.Email);
                        dbUtility1.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, encryptedPassword);
                        dbUtility1.AddParameters("@RoleID", DBUtilDBType.Integer, DBUtilDirection.In, 255, 2); // Use a specific RoleID
                        dbUtility1.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientID);
                        dbUtility1.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 255, 0);
                        dbUtility1.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, true);
                        dbUtility1.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, client.CreatedBy);

                        DataSet dsUser = dbUtility1.Execute_StoreProc_DataSet("USP_ADD_USER");

                        if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                        {
                            return Ok(new { statusCode = 100, message });
                        }
                        else
                        {
                            return BadRequest(new { message = "User addition failed." });
                        }
                    }
                    else
                    {
                        // Handle the case when the status code is 200
                        return Ok(new { statusCode, message });
                    }

                }
            }
            catch (Exception ex)
            {
                // Logging can be added here if necessary
                return BadRequest(new { message = ex.Message });
            }
        }

        [Route("Client/getALLDATA")]
        [HttpPost]
        public IActionResult GetClient(Client allclient)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                
                    if (allclient.ClientTypeId != 0 && allclient.ClientTypeId != null)
                    {
                        oDBUtility.AddParameters("@ClientTypeId", DBUtilDBType.Integer, DBUtilDirection.In, 10, allclient.ClientTypeId);
                    }
                    if (allclient.Name != null)
                    {
                        oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 100, allclient.Name);
                    }
                    if (allclient.ClientID != 0 && allclient.ClientID != null)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, allclient.ClientID);
                    }
                    if (allclient.ParentClientID != 0 && allclient.ParentClientID != null)
                    {
                        oDBUtility.AddParameters("@ParentClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, allclient.ParentClientID);
                    }

                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, allclient.IsActive);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_ALLCLIENT");

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

        [Route("Client/update")]
        [HttpPost]
        public IActionResult Put(Client Client)
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
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.ClientID);

                    oDBUtility.AddParameters("@ClientTypeId", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.ClientTypeId);
                    oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Name);
                    oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Client.Email);
                    oDBUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 225, Client.Address);
                    oDBUtility.AddParameters("@City", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.City);
                    oDBUtility.AddParameters("@State", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.State);
                    oDBUtility.AddParameters("@Country", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.Country);
                    oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.IndustryID);
                    oDBUtility.AddParameters("@ParentClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, Client.ParentClientID);
                    oDBUtility.AddParameters("@Regulated", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, Client.Regulated);
                    oDBUtility.AddParameters("@Certified", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, Client.Certified);
                    oDBUtility.AddParameters("@Pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.Pincode);
                    oDBUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.GSTNo);
                    oDBUtility.AddParameters("@ContactPersonname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersonname);
                    oDBUtility.AddParameters("@ContactPersonlastname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersonlastname);

                    oDBUtility.AddParameters("@ContactPersondesignation", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersondesignation);
                    oDBUtility.AddParameters("@ContactPersonmobile", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.ContactPersonmobile);

                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, Client.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, Client.ModifiedBy);
                    oDBUtility.AddParameters("@OfficeAddress", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Client.OfficeAddress);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CLIENT");
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


        [Route("Client/updatestatus")]
        [HttpPost]
        public IActionResult UpdateClientStatus(Client client)
        {
            ConfigHandler oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

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
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, client.ClientID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, client.IsActive);
                    //oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, user.ModifiedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CLIENT_STATUS");
                    ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }
    }
}