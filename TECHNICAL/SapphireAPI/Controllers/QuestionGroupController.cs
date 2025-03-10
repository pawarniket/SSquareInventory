using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DAE.Common.EncryptionDecryption;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class QuestionGroupController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public QuestionGroupController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("QuestionGroup/add")]
        [HttpPost]

        public IActionResult Post(QuestionGroup questiongroup)
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
                    oDBUtility.AddParameters("@QuestionGroupName", DBUtilDBType.Nvarchar, DBUtilDirection.In, 255, questiongroup.QuestionGroupName);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questiongroup.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_QUESTIONGROUP");
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

        [Route("QuestionGroup/get")]
        [HttpPost]
        public IActionResult Get(QuestionGroup questiongroup)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (questiongroup.QuestionGroupID != null)
                    {
                        oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questiongroup.QuestionGroupID);
                    }

                    if (questiongroup.QuestionGroupName != null)
                    {
                        oDBUtility.AddParameters("@QuestionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 25, questiongroup.QuestionGroupName);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, questiongroup.IsActive);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_QUESTION_GROUP");

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

        [Route("QuestionGroup/update")]
        [HttpPost]
        public IActionResult Put(QuestionGroup questiongroup)
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
                    if (questiongroup.QuestionGroupID != 0)
                    {
                        oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questiongroup.QuestionGroupID);
                    }

                    if (questiongroup.QuestionGroupName != null)
                    {
                        oDBUtility.AddParameters("@QuestionGroupName", DBUtilDBType.Nvarchar, DBUtilDirection.In, 255, questiongroup.QuestionGroupName);
                    }

                    if (questiongroup.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questiongroup.IsActive);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_QUESTIONGROUP");
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


        [Route("QuestionGroup/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(QuestionGroup questiongroup)
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
                    oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questiongroup.QuestionGroupID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questiongroup.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_QUESTION_GROUP_STATUS");
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
