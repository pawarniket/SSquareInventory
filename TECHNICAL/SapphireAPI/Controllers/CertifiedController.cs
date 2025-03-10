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
    public class CertifiedController : ControllerBase
    {

        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public CertifiedController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("Certified/add")]
        [HttpPost]

        public IActionResult Post(Certified certified)
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
                    oDBUtility.AddParameters("@CertifiedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, certified.CertifiedName);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, certified.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CERTIFIED");
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

        [Route("Certified/get")]
        [HttpPost]
        public IActionResult Get(Certified certified)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (certified.CertifiedID != null && certified.CertifiedID != 0)
                    {
                        oDBUtility.AddParameters("@CertifiedID", DBUtilDBType.Integer, DBUtilDirection.In, 50, certified.CertifiedID);
                    }
                    if (certified.CertifiedName != null)
                    {
                        oDBUtility.AddParameters("@CertifiedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, certified.CertifiedName);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, certified.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CERTIFIED");

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

        [Route("Certified/update")]
        [HttpPost]
        public IActionResult Put(Certified certified)
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
                    if (certified.CertifiedID != 0)
                    {
                        oDBUtility.AddParameters("@CertifiedID", DBUtilDBType.Integer, DBUtilDirection.In, 10, certified.CertifiedID);
                    }

                    if (certified.CertifiedName != null)
                    {
                        oDBUtility.AddParameters("@CertifiedName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, certified.CertifiedName);
                    }

                    if (certified.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, certified.IsActive);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CERTIFIED");
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


        [Route("Certified/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(Certified certified)
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
                    oDBUtility.AddParameters("@CertifiedID", DBUtilDBType.Integer, DBUtilDirection.In, 10, certified.CertifiedID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, certified.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CERTIFIED_STATUS");
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
