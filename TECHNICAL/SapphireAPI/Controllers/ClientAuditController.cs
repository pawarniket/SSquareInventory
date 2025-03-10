using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Configuration;
using DAE.DAL.SQL;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.VisualBasic;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class ClientAuditController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private readonly ServiceRequestProcessor _serviceRequestProcessor;
        private readonly EmailController _emailController;

        public ClientAuditController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
            _serviceRequestProcessor = new ServiceRequestProcessor();
            _emailController = new EmailController(configuration);
            //this._emailController = _emailController;

        }

        [Route("ClientAudit/add")]
        [HttpPost]
        public IActionResult Post(ClientAudit clientAudit)
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
                    // Initialize DBUtility
                    DBUtility oDBUtility = new DBUtility(_configurationIG);

                    // Add parameters for USP_ADD_CLIENTAUDIT
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ClientID);

                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.AuditTypeID);
                    oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ApprovedBy);

                    oDBUtility.AddParameters("@ReviewBy", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ReviewBy);
                    oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.AuditStatusID);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.CreatedBy);
                    oDBUtility.AddParameters("@DueDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, clientAudit.DueDate);
                    oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.VendorID);

                    // Execute the first stored procedure
                    DataSet dsClientAudit = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CLIENTAUDIT");

                    // Check if the first operation was successful
                    if (dsClientAudit != null && dsClientAudit.Tables.Count > 0 && dsClientAudit.Tables[0].Rows.Count > 0)
                    {

                        // Retrieve the ClientAuditID from the first procedure's result
                        int ClientAuditID = dsClientAudit.Tables[0].Rows[0]["identity"] != DBNull.Value
                              ? Convert.ToInt32(dsClientAudit.Tables[0].Rows[0]["identity"])
                              : 0;
                        int AuditTypeID = clientAudit.AuditTypeID ?? 0; // or another default value
                        Boolean IsActive = true;
                        // Prepare parameters for USP_GET_QUESTIONMASTER
                        DBUtility oDBUtilityQuestion = new DBUtility(_configurationIG);

                        if (AuditTypeID != 0)
                        {
                            oDBUtilityQuestion.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, AuditTypeID);
                        }

                        oDBUtilityQuestion.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, IsActive);


                        // Execute the second stored procedure
                        DataSet dsQuestionMaster = oDBUtilityQuestion.Execute_StoreProc_DataSet("USP_GET_QUESTIONMASTER");

                        // Check if questions were retrieved
                        if (dsQuestionMaster != null && dsQuestionMaster.Tables.Count > 0 && dsQuestionMaster.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in dsQuestionMaster.Tables[0].Rows)
                            {
                                AuditResponse auditResponse = new AuditResponse
                                {
                                    ClientID = clientAudit.ClientID,
                                    AuditTypeID = clientAudit.AuditTypeID,
                                    VendorID = clientAudit.VendorID,
                                    QuetionGroupName = row["QuestionGroupName"].ToString(),
                                    Question = row["Question_details"].ToString(),
                                    QuestionDescription = row["QuestionDescription"].ToString(),
                                    QuestionGroupID = Convert.ToInt32(row["QuestionGroupID"]),
                                    Likelihood = Convert.ToInt32(row["Likelihood"]),
                                    Impact = Convert.ToInt32(row["Impact"]),
                                    ReferenceNo = ClientAuditID,
                                    IsCustom = 0,
                                    AttachmentRequired = row["AttachmentRequired"] == DBNull.Value ? "No" : row["AttachmentRequired"].ToString(),


                                    AuditStatusID = clientAudit.AuditStatusID,

                                };
                                //IsCustom = Convert.ToInt32(row["IsCustom"]),



                                // Initialize a new DBUtility for each AuditResponse
                                DBUtility oDBUtilityResponse = new DBUtility(_configurationIG);

                                oDBUtilityResponse.AddParameters("@ReferenceNo", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditResponse.ReferenceNo);
                                oDBUtilityResponse.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.ClientID);
                                oDBUtilityResponse.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.VendorID);
                                oDBUtilityResponse.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.AuditTypeID);
                                oDBUtilityResponse.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.QuestionGroupID);
                                oDBUtilityResponse.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditResponse.QuetionGroupName);
                                oDBUtilityResponse.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditResponse.Question);
                                oDBUtilityResponse.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditResponse.QuestionDescription);
                                oDBUtilityResponse.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.Likelihood);
                                oDBUtilityResponse.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.Impact);

                                oDBUtilityResponse.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.IsCustom);
                                oDBUtilityResponse.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.AuditStatusID);
                                oDBUtilityResponse.AddParameters("@AttachmentRequired", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditResponse.AttachmentRequired);

                                // Execute stored procedure to add AuditResponse
                                DataSet dsAuditResponse = oDBUtilityResponse.Execute_StoreProc_DataSet("USP_ADD_AUDITRESPONSE");

                                if (dsAuditResponse == null || dsAuditResponse.Tables.Count == 0 || dsAuditResponse.Tables[0].Rows.Count == 0)
                                {

                                    // Log or handle the case where adding AuditResponse fails for a particular question
                                }
                            }
                        }
                        var jsonData = new
                        {
                            StatusCode = 100,
                            Message = "ClientAudit and AuditResponses were successfully added.",
                            referanceNo = ClientAuditID
                        };

                        return Ok(jsonData);
                    }
                    else
                    {
                        // If the first operation fails, return an error
                        return BadRequest("Failed to add ClientAudit.");
                    }
                }
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }


        [Route("ClientAudit/get")]
        [HttpPost]
        public IActionResult Get(ClientAudit clientAudit)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (clientAudit.ClientAuditID != 0 && clientAudit.ClientAuditID != null)
                    {
                        oDBUtility.AddParameters("@ClientAuditID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ClientAuditID);
                    }
                    if (clientAudit.VendorID != 0 && clientAudit.VendorID != null)
                    {
                        oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.VendorID);
                    }

                    if (clientAudit.ClientID != 0 && clientAudit.ClientID != null)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ClientID);
                    }


                    if (clientAudit.AuditTypeID != 0 && clientAudit.AuditTypeID != null)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.AuditTypeID);
                    }


                    //if (clientAudit.ClientAuditID != 0 && clientAudit.ClientAuditID != null)
                    //{
                    //    oDBUtility.AddParameters("@ClientAuditID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ClientAuditID);
                    //}





                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CLIENTAUDIT");

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

        [Route("ClientAudit/update")]
        [HttpPost]
        public IActionResult Put(ClientAudit clientAudit)
        {

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2" && userrole != "1" && userrole != "5")
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

                    if (clientAudit.ClientAuditID != 0 && clientAudit.ClientAuditID != null)
                    {
                        oDBUtility.AddParameters("@ClientAuditID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ClientAuditID);
                    }

                    if (clientAudit.ClientID != 0 && clientAudit.ClientID != null)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ClientID);
                    }

                    if (clientAudit.AuditTypeID != 0 && clientAudit.AuditTypeID != null)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.AuditTypeID);
                    }

                    if (clientAudit.ApprovedBy != 0 && clientAudit.ApprovedBy != null)
                    {
                        oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ApprovedBy);
                    }

                    if (clientAudit.ReviewBy != 0 && clientAudit.ReviewBy != null)
                    {
                        oDBUtility.AddParameters("@ReviewBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ReviewBy);
                    }

                    if (clientAudit.AuditStatusID != 0 && clientAudit.AuditStatusID != null)
                    {
                        oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.AuditStatusID);
                    }

                    if (clientAudit.ModifiedBy != 0 && clientAudit.ModifiedBy != null)
                    {
                        oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.ModifiedBy);
                    }

                    if (clientAudit.ResponseCompletedDate != null)
                    {
                        oDBUtility.AddParameters("@ResponseCompletedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, clientAudit.ResponseCompletedDate);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CLIENTAUDIT");

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




        [Route("ClientAudit/Assign")]
        [HttpPost]
        public IActionResult AuditAssign(ClientAudit clientAudit)
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
                    // Initialize DBUtility
                    DBUtility oDBUtility = new DBUtility(_configurationIG);

                    // Add parameters for USP_ADD_CLIENTAUDIT
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ClientID);

                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.AuditTypeID);
                    oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ApprovedBy);

                    oDBUtility.AddParameters("@ReviewBy", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.ReviewBy);
                    oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 50, clientAudit.AuditStatusID);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.CreatedBy);
                    oDBUtility.AddParameters("@DueDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, clientAudit.DueDate);
                    oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.VendorID);

                    // Execute the first stored procedure
                    DataSet dsClientAudit = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CLIENTAUDIT");

                    // Check if the first operation was successful
                    if (dsClientAudit != null && dsClientAudit.Tables.Count > 0 && dsClientAudit.Tables[0].Rows.Count > 0)
                    {

                        // Retrieve the ClientAuditID from the first procedure's result
                        int ClientAuditID = dsClientAudit.Tables[0].Rows[0]["identity"] != DBNull.Value
                              ? Convert.ToInt32(dsClientAudit.Tables[0].Rows[0]["identity"])
                              : 0;
                      
                 
                        // Prepare parameters for USP_GET_QUESTIONMASTER
                        DBUtility oDBUtilityQuestion = new DBUtility(_configurationIG);

                        if (clientAudit.Questions != null && clientAudit.Questions.Length > 0)
                        {

                            for (int i = 0; i < clientAudit.Questions.Length; i++)
                            {
                                AuditResponse auditResponse = new AuditResponse
                                {
                                    ClientID = clientAudit.ClientID,
                                    AuditTypeID = clientAudit.AuditTypeID,
                                    VendorID = clientAudit.VendorID,
                                    QuetionGroupName = clientAudit.Questions[i].QuestionGroupName,
                                    Question = clientAudit.Questions[i].Question,
                                    QuestionDescription = clientAudit.Questions[i].QuestionDescription,
                                    QuestionGroupID = clientAudit.Questions[i].QuestionGroupID,
                                    Likelihood = clientAudit.Questions[i].Likelihood,
                                    Impact = clientAudit.Questions[i].Impact,
                                    ReferenceNo = ClientAuditID,
                                    IsCustom = clientAudit.Questions[i].IsCustom,
                                    AttachmentRequired = clientAudit.Questions[i].AttachmentRequired,


                                    AuditStatusID = clientAudit.AuditStatusID,

                                };
                                //IsCustom = Convert.ToInt32(row["IsCustom"]),



                                // Initialize a new DBUtility for each AuditResponse
                                DBUtility oDBUtilityResponse = new DBUtility(_configurationIG);

                                oDBUtilityResponse.AddParameters("@ReferenceNo", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditResponse.ReferenceNo);
                                oDBUtilityResponse.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.ClientID);
                                oDBUtilityResponse.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.VendorID);
                                oDBUtilityResponse.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.AuditTypeID);
                                oDBUtilityResponse.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.QuestionGroupID);
                                oDBUtilityResponse.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditResponse.QuetionGroupName);
                                oDBUtilityResponse.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditResponse.Question);
                                if (auditResponse.QuestionDescription !=null && auditResponse.QuestionDescription !="")
                                {
                                    oDBUtilityResponse.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditResponse.QuestionDescription);
                                }
                                
                                oDBUtilityResponse.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.Likelihood);
                                oDBUtilityResponse.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 1000, auditResponse.Impact);

                                oDBUtilityResponse.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.IsCustom);
                                oDBUtilityResponse.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditResponse.AuditStatusID);
                                oDBUtilityResponse.AddParameters("@AttachmentRequired", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditResponse.AttachmentRequired);

                                // Execute stored procedure to add AuditResponse
                                DataSet dsAuditResponse = oDBUtilityResponse.Execute_StoreProc_DataSet("USP_ADD_AUDITRESPONSE");

                                if (dsAuditResponse == null || dsAuditResponse.Tables.Count == 0 || dsAuditResponse.Tables[0].Rows.Count == 0)
                                {

                                    // Log or handle the case where adding AuditResponse fails for a particular question
                                }
                            }
                        }
                        // DBUtility oDBUtility2 = new DBUtility(_configurationIG);
                        // oDBUtility2.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientAudit.VendorID?.ToString());
                        // DataSet updateResult = oDBUtility2.Execute_StoreProc_DataSet("USP_GET_USER");
                        //string userFName = updateResult.Tables[0].Rows[0]["FirstName"].ToString();  // Assuming UserFirstName is string or nullable
                        //string userLName = updateResult.Tables[0].Rows[0]["LastName"].ToString();    // Same as above
                        string userFName = clientAudit.VendorID?.ToString();
                        string userLName = clientAudit.VendorID?.ToString();
                        string assessmentName = clientAudit.AuditTypeID?.ToString();
                        string clientName = clientAudit.ClientID?.ToString();
                        DateTime ? dueDate = clientAudit.DueDate;

                        // If DueDate is a DateTime, convert it to string in the desired format
                       string dueDate1 = clientAudit.DueDate.ToString();
                        // Call the AssignedAssessmentEmail function from EmailController
                        _emailController.AssignedAssessmentEmail(userFName, userLName, assessmentName, clientName,dueDate1);


                        var jsonData = new
                        {
                            StatusCode = 100,
                            Message = "ClientAudit and AuditResponses were successfully added.",
                            referanceNo = ClientAuditID
                        };
                        // call the function written in EmailController over here
                       
                        return Ok(jsonData);
                    }
                    else
                    {
                        // If the first operation fails, return an error
                        return BadRequest("Failed to add ClientAudit.");
                    }
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
