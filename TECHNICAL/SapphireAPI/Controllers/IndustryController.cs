using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DAE.Common.EncryptionDecryption;


namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class IndustryController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public IndustryController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("Industry/add")]
        [HttpPost]

        public IActionResult Post(Industry industry)
        {
            try
            {

                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "1")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Ok(jsondata);
                }
                else
                {

                    DBUtility oDBUtility = new DBUtility(_configurationIG);
                    oDBUtility.AddParameters("@IndustryName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, industry.IndustryName);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, industry.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_INDUSTRY");
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

        [Route("Industry/get")]
        [HttpPost]
        public IActionResult Get(Industry industry)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (industry.IndustryID != null && industry.IndustryID != 0)
                    {
                        oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 50, industry.IndustryID);
                    }
                    if (industry.IndustryName != null)
                    {
                        oDBUtility.AddParameters("@IndustryName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, industry.IndustryName);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, industry.IsActive);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_INDUSTRY");

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

        [Route("Industry/update")]
        [HttpPost]
        public IActionResult Put(Industry industry)
        {

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "1")
                {
                    var jsondata = new
                    {
                        Message = "UnAuthorized Access"
                    };
                    return Ok(jsondata);
                }
                else
                {
                    DBUtility oDBUtility = new DBUtility(_configurationIG);
                    if (industry.IndustryID != 0)
                    {
                        oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 10, industry.IndustryID);
                    }

                    if (industry.IndustryName != null)
                    {
                        oDBUtility.AddParameters("@IndustryName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, industry.IndustryName);
                    }

                    if (industry.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, industry.IsActive);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_INDUSTRY");
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

        [Route("Industry/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(Industry industry)
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
                    oDBUtility.AddParameters("@IndustryID", DBUtilDBType.Integer, DBUtilDirection.In, 10, industry.IndustryID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, industry.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_INDUSTRY_STATUS");
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
    }
}
