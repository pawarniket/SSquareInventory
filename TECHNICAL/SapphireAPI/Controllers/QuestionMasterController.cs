using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Numerics;
using DAE.Common.EncryptionDecryption;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class QuestionMasterController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public QuestionMasterController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("QuestionMaster/get")]
        [HttpPost]
        public IActionResult Get(QuestionMaster questionmaster)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (questionmaster.QuestionMasterID != null && questionmaster.QuestionMasterID != 0)
                    {
                        oDBUtility.AddParameters("@QuestionMasterID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.QuestionMasterID);
                    }

                    if (questionmaster.Question_details != null  )
                    {
                        oDBUtility.AddParameters("@Question_details", DBUtilDBType.Varchar, DBUtilDirection.In, 500, questionmaster.Question_details);
                    }


                    if (questionmaster.QuestionDescription != null  )
                    {
                        oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, questionmaster.QuestionDescription);
                    }


                    if (questionmaster.AuditTypeID != null && questionmaster.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.AuditTypeID);
                    }

                    if (questionmaster.QuestionGroupID != null && questionmaster.QuestionGroupID != 0)
                    {
                        oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, questionmaster.QuestionGroupID);
                    }
                    if (questionmaster.OrderBy != 0)
                    {
                        oDBUtility.AddParameters("@OrderBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.OrderBy);
                    }
                    if (questionmaster.Impact != 0 && questionmaster.Impact != null)
                    {
                        oDBUtility.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Impact);
                    }
                    if (questionmaster.Likelihood != 0 && questionmaster.Likelihood != null)
                    {
                        oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Likelihood);
                    }
                    if (questionmaster.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, questionmaster.IsActive);
                    }
                    if (questionmaster.AttachmentRequired != null)
                    {
                        oDBUtility.AddParameters("@AttachmentRequired", DBUtilDBType.Varchar, DBUtilDirection.In, 50, questionmaster.AttachmentRequired);
                    }



                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_QUESTIONMASTER");

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

        [Route("QuestionMaster/add")]
        [HttpPost]
        public IActionResult Post(QuestionMaster questionmaster)
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
                    oDBUtility.AddParameters("@Question_details", DBUtilDBType.Varchar, DBUtilDirection.In, 500, questionmaster.Question_details);
                    if (questionmaster.QuestionDescription != null)
                    {
                        oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, questionmaster.QuestionDescription);
                    }
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.AuditTypeID);
                    oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.QuestionGroupID);
                    oDBUtility.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Impact);
                    oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Likelihood);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questionmaster.IsActive);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.CreatedBy);
                    oDBUtility.AddParameters("@AttachmentRequired", DBUtilDBType.Varchar, DBUtilDirection.In, 50, questionmaster.AttachmentRequired);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_QUESTIONMASTER");
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



        [Route("QuestionMaster/update")]
        [HttpPost]
        public IActionResult Put(QuestionMaster questionmaster)
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
                    if (questionmaster.QuestionMasterID != 0)
                    {
                        oDBUtility.AddParameters("@QuestionMasterID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.QuestionMasterID);
                    }

                    if (questionmaster.Question_details != null)
                    {
                        oDBUtility.AddParameters("@Question_details", DBUtilDBType.Varchar, DBUtilDirection.In, 500, questionmaster.Question_details);
                    }


                    if (questionmaster.QuestionDescription != null)
                    {
                        oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, questionmaster.QuestionDescription);
                    }


                    if (questionmaster.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.AuditTypeID);
                    }

                    if (questionmaster.QuestionGroupID != 0)
                    {
                        oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, questionmaster.QuestionGroupID);
                    }
                    if (questionmaster.OrderBy != 0)
                    {
                        oDBUtility.AddParameters("@OrderBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.OrderBy);
                    }
                    if (questionmaster.Impact != 0)
                    {
                        oDBUtility.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Impact);
                    }
                    if (questionmaster.Likelihood != 0)
                    {
                        oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 50, questionmaster.Likelihood);
                    }
                    if (questionmaster.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questionmaster.IsActive);
                    }
                    if (questionmaster.AttachmentRequired != null)
                    {
                        oDBUtility.AddParameters("@AttachmentRequired", DBUtilDBType.Varchar, DBUtilDirection.In, 50, questionmaster.AttachmentRequired);
                    }

                    if (questionmaster.ModifiedBy != 0)
                    {
                        oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.ModifiedBy);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_QUESTIONMASTER");
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

        [Route("QuestionMaster/updatestatus")]
        [HttpPost]
        public IActionResult UpdateQuestionMasterStatus(QuestionMaster questionmaster)
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
                    oDBUtility.AddParameters("@QuestionMasterID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.QuestionMasterID);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, questionmaster.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, questionmaster.ModifiedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_QUESTION_MASTER_STATUS");
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