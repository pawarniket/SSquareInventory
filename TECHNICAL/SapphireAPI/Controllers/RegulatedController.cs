using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class RegulatedController : ControllerBase
    {

        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public RegulatedController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("Regulated/add")]
        [HttpPost]

        public IActionResult Post(Regulated regulated)
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
                    oDBUtility.AddParameters("@RegulatedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, regulated.RegulatedName);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, regulated.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_REGULATED");
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

        [Route("Regulated/get")]
        [HttpPost]
        public IActionResult Get(Regulated regulated)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (regulated.RegulatedID != null && regulated.RegulatedID != 0)
                    {
                        oDBUtility.AddParameters("@RegulatedID", DBUtilDBType.Integer, DBUtilDirection.In, 50, regulated.RegulatedID);
                    }
                    if (regulated.RegulatedName != null)
                    {
                        oDBUtility.AddParameters("@RegulatedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, regulated.RegulatedName);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, regulated.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_REGULATED");

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

        [Route("Regulated/update")]
        [HttpPost]
        public IActionResult Put(Regulated regulated)
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
                    if (regulated.RegulatedID != 0)
                    {
                        oDBUtility.AddParameters("@RegulatedID", DBUtilDBType.Integer, DBUtilDirection.In, 10, regulated.RegulatedID);
                    }

                    if (regulated.RegulatedName != null)
                    {
                        oDBUtility.AddParameters("@RegulatedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, regulated.RegulatedName);
                    }

                    if (regulated.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, regulated.IsActive);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_REGULATED");
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


        [Route("Regulated/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(Regulated regulated)
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
                    oDBUtility.AddParameters("@RegulatedID", DBUtilDBType.Integer, DBUtilDirection.In, 10, regulated.RegulatedID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, regulated.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_REGULATED_STATUS");
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
