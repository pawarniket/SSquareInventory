using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class CustomQuestionsController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public CustomQuestionsController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("CustomQuestion/get")]
        [HttpPost]
        public IActionResult Get(CustomQuestions customquestion)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (customquestion.CustomQuestionID != null && customquestion.CustomQuestionID != 0)
                    {
                        oDBUtility.AddParameters("@CustomQuestionID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.CustomQuestionID);
                    }

                    if (customquestion.CustomQuestion != null)
                    {
                        oDBUtility.AddParameters("@CustomQuestion", DBUtilDBType.Varchar, DBUtilDirection.In, 500, customquestion.CustomQuestion);
                    }


                    if (customquestion.CustomQuestionDescription != null)
                    {
                        oDBUtility.AddParameters("@CustomQuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, customquestion.CustomQuestionDescription);
                    }

                    if (customquestion.ClientID != null && customquestion.ClientID != 0)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.ClientID);
                    }

                    if (customquestion.AuditTypeID != null && customquestion.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.AuditTypeID);
                    }

                    if (customquestion.QuestionType != null && customquestion.QuestionType != 0)
                    {
                        oDBUtility.AddParameters("@QuestionType", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.QuestionType);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, customquestion.IsActive);



                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CUSTOMQUESTION");

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

        [Route("CustomQuestion/add")]
        [HttpPost]
        public IActionResult Post(CustomQuestions customquestion)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@CustomQuestion", DBUtilDBType.Varchar, DBUtilDirection.In, 500, customquestion.CustomQuestion);
                oDBUtility.AddParameters("@CustomQuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, customquestion.CustomQuestionDescription);
                oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.ClientID);
                oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.AuditTypeID);
                oDBUtility.AddParameters("@QuestionType", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.QuestionType);
                oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, customquestion.IsActive);
                oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CUSTOMQUESTION");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));

            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }



        [Route("CustomQuestion/update")]
        [HttpPost]
        public IActionResult Put(CustomQuestions customquestion)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                    oDBUtility.AddParameters("@CustomQuestionID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.CustomQuestionID);
                   
                    oDBUtility.AddParameters("@CustomQuestion", DBUtilDBType.Varchar, DBUtilDirection.In, 500, customquestion.CustomQuestion);
                    oDBUtility.AddParameters("@CustomQuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, customquestion.CustomQuestionDescription);
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.ClientID);
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.AuditTypeID);
                    oDBUtility.AddParameters("@QuestionType", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.QuestionType);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, customquestion.IsActive);
                    oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, customquestion.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CUSTOMQUESTION");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }

    }
}