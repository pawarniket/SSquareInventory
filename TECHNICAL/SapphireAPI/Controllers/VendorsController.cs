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
    public class VendorsController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public VendorsController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("Vendor/get")]
        [HttpPost]
        public IActionResult Get(Vendor vendor)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" )
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Unauthorized(jsondata);

                }
                else
                {
                    if (userrole == "5" && vendor.VendorID == 0) {
                        var jsondata = new
                        {
                            Error = "Vendor ID is Required"
                        };
                        return Ok(jsondata);
                    }
                    else if (userrole == "2" && (vendor.ClientID == 0 || vendor.ClientID == null)) {
                        var jsondata = new
                        {
                            Error = "Client ID is Required"
                        };
                        return Ok(jsondata);
                    }
                    else { 
                    DBUtility oDBUtility = new DBUtility(_configurationIG);
                    {
                        if (vendor.VendorID != null && vendor.VendorID != 0)
                        {
                            oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.VendorID);
                        }
                        if (vendor.ClientID != 0 && vendor.ClientID != null)
                        {
                            oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ClientID);
                        }
                        if (vendor.VendorName != null)
                        {
                            oDBUtility.AddParameters("@VendorName", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.VendorName);
                        }
                        if (vendor.IsCritical != null)
                        {
                            oDBUtility.AddParameters("@IsCritical", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsCritical);
                        }

                            oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsActive);




                        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_VENDORS");

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


        [Route("Vendor/add")]
        [HttpPost]
        public IActionResult Post(Vendor vendor)
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
                    if (vendor.ClientID != 0 && vendor.ClientID != null)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ClientID);
                    }
                    if (vendor.VendorName != null)
                    {
                        oDBUtility.AddParameters("@VendorName", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.VendorName);
                    }
                    if (vendor.IndustryID != 0)
                    {
                        oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.IndustryID);
                    }
                    if (vendor.Email != null)
                    {
                        oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 150, vendor.Email);
                    }
                    if (vendor.Address != null)
                    {
                        oDBUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.Address);
                    }
                    if (vendor.City != null)
                    {
                        oDBUtility.AddParameters("@City", DBUtilDBType.Varchar, DBUtilDirection.In, 100, vendor.City);
                    }
                    if (vendor.State != null)
                    {
                        oDBUtility.AddParameters("@State", DBUtilDBType.Integer, DBUtilDirection.In, 50, vendor.State);
                    }
                    if (vendor.Country != null)
                    {
                        oDBUtility.AddParameters("@Country", DBUtilDBType.Integer, DBUtilDirection.In, 50, vendor.Country);
                    }
                    if (vendor.Pincode != null)
                    {
                        oDBUtility.AddParameters("@Pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.Pincode);
                    }
                    if (vendor.GSTNo != null)
                    {
                        oDBUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.GSTNo);
                    }
                    if (vendor.Regulated != null)
                    {
                        oDBUtility.AddParameters("@Regulated", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, vendor.Regulated);
                    }
                    if (vendor.Certified != null)
                    {
                        oDBUtility.AddParameters("@Certified", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, vendor.Certified);
                    }
                    if (vendor.ContactPerson_name != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_name", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.ContactPerson_name);
                    }

                    if (vendor.ContactPerson_Lastname != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_Lastname", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.ContactPerson_Lastname);
                    }
                    if (vendor.ContactPerson_designation != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_designation", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.ContactPerson_designation);
                    }
                    if (vendor.ContactPerson_mobile != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_mobile", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.ContactPerson_mobile);
                    }
                    if (vendor.IsCritical != null)
                    {
                        oDBUtility.AddParameters("@IsCritical", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsCritical);
                    }
                    if (vendor.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsActive);
                    }
                    if (vendor.OfficeAddress != null)
                    {
                        oDBUtility.AddParameters("@OfficeAddress", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, vendor.OfficeAddress);
                    }
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.CreatedBy);




                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_VENDOR");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("identity"))
                    {
                        int vendorID = ds.Tables[0].Columns.Contains("identity") && ds.Tables[0].Rows[0]["identity"] != DBNull.Value
                        ? Convert.ToInt32(ds.Tables[0].Rows[0]["identity"])
                            : 0;

                        ConfigHandler oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);
                        string encryptedPassword = oEncrDec.Cryptohelper.Encrypt("123456"); // Using mobile as password example

                        DBUtility oDBUtility1 = new DBUtility(_configurationIG);
                        oDBUtility1.AddParameters("@FirstName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.ContactPerson_name);
                        oDBUtility1.AddParameters("@LastName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.ContactPerson_Lastname);
                        oDBUtility1.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, vendor.Email);
                        oDBUtility1.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, encryptedPassword);
                        oDBUtility1.AddParameters("@RoleID", DBUtilDBType.Integer, DBUtilDirection.In, 255, 5); // Use a specific RoleID
                        oDBUtility1.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ClientID);
                        oDBUtility1.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 255, vendorID);
                        oDBUtility1.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, true);
                        oDBUtility1.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.CreatedBy);

                        DataSet dsUser = oDBUtility1.Execute_StoreProc_DataSet("USP_ADD_USER");

                        // Check if user insertion was successful
                        if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                        {
                            return Ok(new { status_code = 100, Message = "Vendor successfully added." });


                        }
                        else
                        {
                            return BadRequest(new { message = "User addition failed" });
                        }
                    }




                    else
                    {
                        //return BadRequest(new { message = "Vendor addition failed" });
                        return Ok(new { status_code = ds.Tables[0].Rows[0]["status_code"], message = ds.Tables[0].Rows[0]["message"] });
                    }
                }
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }


        [Route("Vendor/update")]
        [HttpPost]
        public IActionResult Put(Vendor vendor)
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
                    if (vendor.VendorID != 0)
                    {
                        oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.VendorID);
                    }
                    if (vendor.ClientID != 0 && vendor.ClientID != null)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ClientID);
                    }
                    if (vendor.VendorName != null)
                    {
                        oDBUtility.AddParameters("@VendorName", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.VendorName);
                    }
                    if (vendor.IndustryID != 0 && vendor.IndustryID != null)
                    {
                        oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.IndustryID);
                    }
                    if (vendor.Email != null)
                    {
                        oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 150, vendor.Email);
                    }
                    if (vendor.Address != null)
                    {
                        oDBUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.Address);
                    }
                    if (vendor.City != null)
                    {
                        oDBUtility.AddParameters("@City", DBUtilDBType.Varchar, DBUtilDirection.In, 100, vendor.City);
                    }
                    if (vendor.State != null)
                    {
                        oDBUtility.AddParameters("@State", DBUtilDBType.Integer, DBUtilDirection.In, 50, vendor.State);
                    }
                    if (vendor.Country != null)
                    {
                        oDBUtility.AddParameters("@Country", DBUtilDBType.Integer, DBUtilDirection.In, 50, vendor.Country);
                    }
                    if (vendor.Pincode != null)
                    {
                        oDBUtility.AddParameters("@Pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.Pincode);
                    }
                    if (vendor.GSTNo != null)
                    {
                        oDBUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.GSTNo);
                    }
                    if (vendor.Regulated != null)
                    {
                        oDBUtility.AddParameters("@Regulated", DBUtilDBType.Varchar, DBUtilDirection.In,5000, vendor.Regulated);
                    }
                    if (vendor.Certified != null)
                    {
                        oDBUtility.AddParameters("@Certified", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, vendor.Certified);
                    }
                    if (vendor.ContactPerson_name != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_name", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.ContactPerson_name);
                    }
                    if (vendor.ContactPerson_Lastname != null)
                    {
                        oDBUtility.AddParameters("@ContactPersonlastname", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.ContactPerson_Lastname);
                    }
                    if (vendor.ContactPerson_designation != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_designation", DBUtilDBType.Varchar, DBUtilDirection.In, 250, vendor.ContactPerson_designation);
                    }
                    if (vendor.ContactPerson_mobile != null)
                    {
                        oDBUtility.AddParameters("@ContactPerson_mobile", DBUtilDBType.Varchar, DBUtilDirection.In, 50, vendor.ContactPerson_mobile);
                    }
                    if (vendor.IsCritical != null)
                    {
                        oDBUtility.AddParameters("@IsCritical", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsCritical);
                    }
                    if (vendor.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, vendor.IsActive);
                    }
                    if (vendor.OfficeAddress != null)
                    {
                        oDBUtility.AddParameters("@OfficeAddress", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, vendor.OfficeAddress);
                    }
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ModifiedBy);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_VENDOR");
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

        [Route("Vendor/updatestatus")]
        [HttpPost]
        public IActionResult UpdateVendorStatus(Vendor vendor)
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
                    oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.VendorID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, vendor.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendor.ModifiedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_VENDOR_STATUS");
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

