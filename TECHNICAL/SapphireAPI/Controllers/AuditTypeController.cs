using Microsoft.AspNetCore.Http;
using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Common.EncryptionDecryption;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
    
    [ApiController]
    [Authorize]

    public class AuditTypeController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public AuditTypeController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("AUDITTYPE/ADD")]
        [HttpPost]

        public IActionResult Post(AuditType auditype)
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
                    oDBUtility.AddParameters("@AuditName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditype.AuditName);
                    oDBUtility.AddParameters("@AuditDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditype.AuditDescription);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, auditype.IsActive);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditype.CreatedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_AUDITTYPE");
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

        [Route("AUDITTYPE/get")]
        [HttpPost]
        public IActionResult Get(AuditType audittype)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (audittype.AuditTypeID != null && audittype.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, audittype.AuditTypeID);
                    }

                    if (audittype.AuditName != null)
                    {
                        oDBUtility.AddParameters("@AuditName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, audittype.AuditName);
                    }

                    if (audittype.AuditDescription != null)
                    {
                        oDBUtility.AddParameters("@AuditDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, audittype.AuditDescription);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, audittype.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_AUDITTYPE");

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



        [Route("AUDITTYPE/update")]
        [HttpPost]
        public IActionResult Put(AuditType audittype)
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
                    if (audittype.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, audittype.AuditTypeID);
                    }

                    if (audittype.AuditName != null)
                    {
                        oDBUtility.AddParameters("@AuditName", DBUtilDBType.Varchar, DBUtilDirection.In, 50, audittype.AuditName);
                    }

                    if (audittype.AuditDescription != null)
                    {
                        oDBUtility.AddParameters("@AuditDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, audittype.AuditDescription);
                    }

                    if (audittype.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, audittype.IsActive);
                    }

                    if (audittype.ModifiedBy != 0)
                    {
                        oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, audittype.ModifiedBy);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_AUDITTYPE");
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


        [Route("AUDITTYPE/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(AuditType audittype)
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
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, audittype.AuditTypeID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, audittype.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, audittype.ModifiedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_AUDIT_TYPE_STATUS");
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
