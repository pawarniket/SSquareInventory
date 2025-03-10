using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Numerics;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class AuditResponseController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public AuditResponseController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("AuditResponse/get")]
        [HttpPost]
        public IActionResult Get(AuditResponse auditresponse)

                {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (auditresponse.AuditResponseID != null && auditresponse.AuditResponseID != 0)
                    {
                        oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditResponseID);
                    }

                    if (auditresponse.QuetionGroupName != null )
                    {
                        oDBUtility.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditresponse.QuetionGroupName);
                    }

                    if ( auditresponse.QuestionGroupID != null)
                    {
                        oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.QuestionGroupID);
                    }


                    if (auditresponse.ReferenceNo != 0 )
                    {
                        oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.ReferenceNo);
                    }

                    if (auditresponse.ClientID != null && auditresponse.ClientID != 0)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ClientID);
                    }

                    if (auditresponse.AuditTypeID != null && auditresponse.AuditTypeID != 0)
                    {
                        oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditTypeID);
                    }

                    if (auditresponse.Question != null)
                    {
                        oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.Question);
                    }


                    if (auditresponse.QuestionDescription != null)
                    {
                        oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionDescription);
                    }

                    if (auditresponse.ResponseDateTime != null)
                    {
                        oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ResponseDateTime);
                    }

                    if (auditresponse.ResponseAction != null)
                    {
                        oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponse.ResponseAction);
                    }

                    if (auditresponse.ApprovedDate != null)
                    {
                        oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ApprovedDate);
                    }

                    if (auditresponse.Applicability != null)
                    {
                        oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Applicability);
                    }

                    if (auditresponse.Criticality != null)
                    {
                        oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Criticality);
                    }
                    if (auditresponse.Likelihood != 0)
                    {
                        oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.Likelihood);
                    }
                    if (auditresponse.Impact != 0)
                    {
                        oDBUtility.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.Impact);
                    }
                    if (auditresponse.PartnerComment != null)
                    {
                        oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.PartnerComment);
                    }
                    if (auditresponse.ReviewerComment != null)
                    {
                        oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.ReviewerComment);
                    }
                    if (auditresponse.RiskScore != null)
                    {
                        oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.RiskScore);
                    }

                    if (auditresponse.ApprovedBy != null)
                    {
                        oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ApprovedBy);
                    }

                    if (auditresponse.AuditStatusID != null && auditresponse.AuditStatusID != 0)
                    {
                        oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditStatusID);
                    }

                    if (auditresponse.ReviewedBy != null)
                    {
                        oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ReviewedBy);
                    }
                    if (auditresponse.Attachments != null)
                    {
                        oDBUtility.AddParameters("Attachments", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.Attachments);
                    }

                    if (auditresponse.ReviewedDate != null)
                    {
                        oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ReviewedDate);
                    }
                    if (auditresponse.ReviewStatus != null)
                    {
                        oDBUtility.AddParameters("@ReviewStatus", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.ReviewStatus);
                    }
                    if (auditresponse.IsCustom != 0)
                    {
                        oDBUtility.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.IsCustom);
                    }
                  

                    //oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, auditresponse.IsActive);



                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_AUDITRESPONSE");

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

        [Route("AuditResponse/add")]
        [HttpPost]
        public IActionResult Post(AuditResponse auditresponse)
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
                    //var timestamp = DateTime.Now.ToString("yyyyMMdd");

                    //var referenceNo = auditresponse.ClientID.ToString() + '/' +
                    //           auditresponse.AuditTypeID.ToString() + '/' +
                    //           timestamp;

                    DBUtility oDBUtility = new DBUtility(_configurationIG);
                    oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.ReferenceNo);
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ClientID);
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditTypeID);

                    oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionGroupID);

                    oDBUtility.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditresponse.QuestionGroupName);


                    oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.Question);
                    oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionDescription);
                    oDBUtility.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.IsCustom);
                    oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditStatusID);
                    //oDBUtility.AddParameters("@Attachments", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.Attachments);
                    //oDBUtility.AddParameters("@ReviewStatus", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.ReviewStatus);


                    //oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ResponseDateTime);
                    //oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponse.ResponseAction);
                    //oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ApprovedDate);
                    //oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Applicability);
                    //oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Criticality);
                    //oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Likelihood);
                    //oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.PartnerComment);
                    //oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.ReviewerComment);
                    //oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.RiskScore);
                    //oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ApprovedBy);
                    //oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.CreatedBy);
                    //oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditStatusID);
                    //oDBUtility.AddParameters("@CreatedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.CreatedDate);
                    //oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ReviewedBy);
                    //oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ReviewedDate);
                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_AUDITRESPONSE");
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



        [Route("AuditResponse/update")]
        [HttpPost]
        public IActionResult Put(AuditResponse auditresponse)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);


                if (auditresponse.AuditResponseID != null && auditresponse.AuditResponseID != 0)
                {
                    oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditResponseID);
                }

                if (auditresponse.QuetionGroupName != null)
                {
                    oDBUtility.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditresponse.QuetionGroupName);
                }

                if ( auditresponse.QuestionGroupID != null)
                {
                    oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.QuestionGroupID);
                }


                if (auditresponse.ReferenceNo != 0 && auditresponse.ReferenceNo != null)
                {
                    oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.ReferenceNo);
                }

                if (auditresponse.ClientID != null && auditresponse.ClientID != 0)
                {
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ClientID);
                }

                if (auditresponse.AuditTypeID != null && auditresponse.AuditTypeID != 0)
                {
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditTypeID);
                }

                if (auditresponse.Question != null)
                {
                    oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.Question);
                }


                if (auditresponse.QuestionDescription != null)
                {
                    oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionDescription);
                }

                if (auditresponse.ResponseDateTime != null)
                {
                    oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ResponseDateTime);
                }

                if (auditresponse.ResponseAction != null)
                {
                    oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponse.ResponseAction);
                }

                if (auditresponse.ApprovedDate != null)
                {
                    oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ApprovedDate);
                }

                if (auditresponse.Applicability != null)
                {
                    oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Applicability);
                }

                if (auditresponse.Criticality != null)
                {
                    oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Criticality);
                }
                if (auditresponse.Likelihood != 0)
                {
                    oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.Likelihood);
                }
                if (auditresponse.Impact != 0)
                {
                    oDBUtility.AddParameters("@Impact", DBUtilDBType.Integer, DBUtilDirection.In, 50, auditresponse.Impact);
                }
                if (auditresponse.PartnerComment != null)
                {
                    oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.PartnerComment);
                }
                if (auditresponse.ReviewerComment != null)
                {
                    oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.ReviewerComment);
                }
                if (auditresponse.RiskScore != null)
                {
                    oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.RiskScore);
                }

                if (auditresponse.ApprovedBy != null)
                {
                    oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ApprovedBy);
                }

                if (auditresponse.AuditStatusID != null && auditresponse.AuditStatusID != 0)
                {
                    oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditStatusID);
                }

                if (auditresponse.ReviewedBy != null)
                {
                    oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ReviewedBy);
                }
                if (auditresponse.Attachments != null)
                {
                    oDBUtility.AddParameters("@Attachments", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.Attachments);
                }
                if (auditresponse.ReviewedDate != null)
                {
                    oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ReviewedDate);
                }
                if (auditresponse.ReviewStatus != null)
                {
                    oDBUtility.AddParameters("@ReviewStatus", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.ReviewStatus);
                }
                if (auditresponse.IsCustom != 0)
                {
                    oDBUtility.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.IsCustom);
                }


                //oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditResponseID);

                //oDBUtility.AddParameters("@ReferenceNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.ReferenceNo);
                //oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ClientID);
                //oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditTypeID);
                //oDBUtility.AddParameters("@QuestionGroupID", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionGroupID);
                //oDBUtility.AddParameters("@QuetionGroupName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, auditresponse.QuetionGroupName);

                //oDBUtility.AddParameters("@Question", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.Question);
                //oDBUtility.AddParameters("@QuestionDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, auditresponse.QuestionDescription);
                //oDBUtility.AddParameters("@ResponseDateTime", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ResponseDateTime);
                //oDBUtility.AddParameters("@ResponseAction", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, auditresponse.ResponseAction);
                //oDBUtility.AddParameters("@ApprovedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ApprovedDate);
                //oDBUtility.AddParameters("@Applicability", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Applicability);
                //oDBUtility.AddParameters("@Criticality", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Criticality);
                //oDBUtility.AddParameters("@Likelihood", DBUtilDBType.Varchar, DBUtilDirection.In, 50, auditresponse.Likelihood);
                //oDBUtility.AddParameters("@PartnerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.PartnerComment);
                //oDBUtility.AddParameters("@ReviewerComment", DBUtilDBType.Varchar, DBUtilDirection.In, 500, auditresponse.ReviewerComment);
                //oDBUtility.AddParameters("@RiskScore", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.RiskScore);
                //oDBUtility.AddParameters("@ApprovedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ApprovedBy);
                //oDBUtility.AddParameters("@AuditStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.AuditStatusID);
                //oDBUtility.AddParameters("@ReviewedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.ReviewedBy);
                //oDBUtility.AddParameters("@ReviewedDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, auditresponse.ReviewedDate);
                //oDBUtility.AddParameters("@IsCustom", DBUtilDBType.Integer, DBUtilDirection.In, 10, auditresponse.IsCustom);
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_AUDITRESPONSE");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }


        //[Route("AuditResponse/Attachments")]
        //[HttpPost]
        //public JsonResult SaveinnerFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;

        //        // Retrieve the 'currentUser' from the form data
        //        string currentUser = httpRequest["currentuser"];

        //        // Create the physical path for saving the file
        //        var physicalPath = Path.Combine(_env.ContentRootPath, "Images/Response", filename);

        //        // Save the file to the specified location
        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }



        //        // Return both the filename, physical path, and currentUser
        //        return new JsonResult(new
        //        {
        //            Filename = filename,
        //            PhysicalPath = physicalPath,
        //            User = currentUser
        //            // Include currentUser in the response
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        // Return default file name and empty path in case of an error
        //        return new JsonResult(new
        //        {
        //            Filename = "anonymous.png",
        //            PhysicalPath = string.Empty
        //        });
        //    }
        //}



        //[Route("AuditResponse/Attachments")]
        //[HttpPost]
        //public JsonResult SaveinnerFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;


        //        string currentUser = httpRequest["currentuser"];


        //        if (postedFile.Length > 20 * 1024 * 1024)
        //        {
        //            return new JsonResult(new
        //            {
        //                Success = false,
        //                Message = "File size exceeds 20 MB.",
        //                Filename = string.Empty,
        //                PhysicalPath = string.Empty
        //            });
        //        }


        //        var allowedExtensions = new[] { ".xlsx", ".xls", ".jpg", ".jpeg", ".pdf", ".png" };
        //        var fileExtension = Path.GetExtension(filename).ToLower();


        //        if (!allowedExtensions.Contains(fileExtension))
        //        {
        //            return new JsonResult(new
        //            {
        //                Success = false,
        //                Message = "Invalid file type. Only .xlsx, .xls, .jpg, .jpeg, .pdf, .png files are allowed.",
        //                Filename = string.Empty,
        //                PhysicalPath = string.Empty
        //            });
        //        }

        //        var physicalPath = Path.Combine(_env.ContentRootPath, "Images/Response", filename);


        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }


        //        return new JsonResult(new
        //        {
        //            Success = true,
        //            Message = "File uploaded successfully.",
        //            Filename = filename,
        //            PhysicalPath = physicalPath,
        //            User = currentUser
        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //        return new JsonResult(new
        //        {
        //            Success = false,
        //            Message = "An error occurred while uploading the file. " + ex.Message,
        //            Filename = string.Empty,
        //            PhysicalPath = string.Empty
        //        });
        //    }
        //}


        [Route("AuditResponse/Attachments")]
        [HttpPost]
        public JsonResult SaveinnerFile()
        {
            try
            {
                var httpRequest = Request.Form;

                // Ensure a file is included in the request
                if (httpRequest.Files.Count == 0)
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "No file uploaded.",
                        Filename = string.Empty,
                        PhysicalPath = string.Empty
                    });
                }

                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;

                // Retrieve the 'currentUser' from the form data
                string currentUser = httpRequest["currentuser"];

                // Check file size (20 MB = 20 * 1024 * 1024 bytes)
                if (postedFile.Length > 20 * 1024 * 1024)
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "File size exceeds 20 MB.",
                        Filename = string.Empty,
                        PhysicalPath = string.Empty
                    });
                }

                // Allowed file types
                var allowedExtensions = new[] { ".xlsx", ".xls", ".jpg", ".jpeg", ".pdf", ".png" };
                var fileExtension = Path.GetExtension(filename).ToLower();

                // Check if the file extension is allowed
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "Invalid file type. Only .xlsx, .xls, .jpg, .jpeg, .pdf, .png files are allowed.",
                        Filename = string.Empty,
                        PhysicalPath = string.Empty
                    });
                }

                // Create the physical path for saving the file
                var physicalPath = Path.Combine(_env.ContentRootPath, "Images/Response", filename);

                // Save the file to the specified location
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                // Return success response
                return new JsonResult(new
                {
                    Success = true,
                    Message = "File uploaded successfully.",
                    Filename = filename,
                    PhysicalPath = physicalPath,
                    User = currentUser
                });
            }
            catch (Exception ex)
            {
                // Return a controlled error response
                return new JsonResult(new
                {
                    Success = false,
                    Message = "An error occurred while uploading the file. " + ex.Message,
                    Filename = string.Empty,
                    PhysicalPath = string.Empty
                });
            }
        }









        [Route("AuditResponse/delete")]
        [HttpPost]
        public IActionResult Delete(int AuditResponseID)
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

                    // Ensure a valid AuditResponseID is provided
                    if (AuditResponseID > 0)
                    {
                        // Adding the parameter to pass to the stored procedure
                        oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, AuditResponseID);
                    }
                    else
                    {
                        return BadRequest("Invalid AuditResponseID.");
                    }

                    // Execute the stored procedure
                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_DELETE_QUESTION");

                    // Process the result
                    oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch (Exception ex)
            {
                // Handle any errors and return a bad request response
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

    }
}