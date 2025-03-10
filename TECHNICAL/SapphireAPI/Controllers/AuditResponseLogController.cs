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

    public class AuditResponseLogController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;
        public AuditResponseLogController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("AuditResponseLog/get")]
        [HttpPost]
        public IActionResult Get(AuditResponseLog auditresponselog)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (auditresponselog.AuditResponseLogID != null && auditresponselog.AuditResponseLogID != 0)
                    {
                        oDBUtility.AddParameters("@AuditResponseLogID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditResponseLogID);
                    }
                  

                    if (auditresponselog.ReferenceNo != null)
                    {
                        oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.ReferenceNo);
                    }
                    if (auditresponselog.AuditResponseID != null && auditresponselog.AuditResponseID != 0)
                    {
                        oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditResponseID);
                    }

                    if (auditresponselog.ClientID != null && auditresponselog.ClientID != 0)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ClientID);
                    }

                    if (auditresponselog.AuditTypeID != null && auditresponselog.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditTypeID);
                    }

                    if (auditresponselog.Question != null)
                    {
                        oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.Question);
                    }


                    if (auditresponselog.QuestionDescription != null)
                    {
                        oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponselog.QuestionDescription);
                    }

                    if (auditresponselog.ResponseDateTime != null)
                    {
                        oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ResponseDateTime);
                    }

                    if (auditresponselog.ResponseAction != null)
                    {
                        oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponselog.ResponseAction);
                    }
                    if (auditresponselog.AuditStatusID != null && auditresponselog.AuditStatusID != 0)
                    {
                        oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditStatusID);
                    }
                    if (auditresponselog.Applicability != null)
                    {
                        oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Applicability);
                    }

                    if (auditresponselog.Criticality != null)
                    {
                        oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Criticality);
                    }
                    if (auditresponselog.Likelihood != null)
                    {
                        oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Likelihood);
                    }
                    if (auditresponselog.PartnerComment != null)
                    {
                        oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.PartnerComment);
                    }
                    if (auditresponselog.ReviewerComment != null)
                    {
                        oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.ReviewerComment);
                    }
                    if (auditresponselog.RiskScore != null)
                    {
                        oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.RiskScore);
                    }

                    if (auditresponselog.ApprovedBy != null)
                    {
                        oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ApprovedBy);
                    }
                    if (auditresponselog.ApprovedDate != null)
                    {
                        oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ApprovedDate);
                    }
                    if (auditresponselog.ReviewedBy != null)
                    {
                        oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ReviewedBy);
                    }

                    if (auditresponselog.ReviewedDate != null)
                    {
                        oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ReviewedDate);
                    }

                    //oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, auditresponse.IsActive);



                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_AUDITRESPONSELOG");

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



        [Route("AuditResponseLog/update")]
        [HttpPost]
        public IActionResult Put(AuditResponseLog auditresponselog)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                oDBUtility.AddParameters("@AuditResponseLogID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditResponseLogID);
                oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.ReferenceNo);
                oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditResponseID);
                oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ClientID);
                oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditTypeID);
                oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.Question);
                oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponselog.QuestionDescription);
                oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ResponseDateTime);
                oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponselog.ResponseAction);
                oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.AuditStatusID);
                oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Applicability);
                oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Criticality);
                oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponselog.Likelihood);
                oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.PartnerComment);
                oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponselog.ReviewerComment);
                oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ApprovedDate);
                oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.RiskScore);
                oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ApprovedBy);
                oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponselog.ReviewedBy);
                oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponselog.ReviewedDate);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_AUDITRESPONSELOG");
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
