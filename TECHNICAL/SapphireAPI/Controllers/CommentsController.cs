using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Common.EncryptionDecryption;
using Microsoft.AspNetCore.Authorization;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    //[Authorize]

    public class CommentsController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;
        private readonly IJwtAuth jwtAuth;

        public CommentsController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
            this.jwtAuth = jwtAuth;

        }

        [Route("COMMENT/ADD")]
        [HttpPost]
        public IActionResult Post(Comments comment)
        {
            ConfigHandler oEncrDec;

            oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 50, comment.AuditResponseID);
                oDBUtility.AddParameters("@ClientAuditID", DBUtilDBType.Integer, DBUtilDirection.In, 50, comment.ClientAuditID);
                oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 100, comment.UserID);
                //oDBUtility.AddParameters("@Password", DBUtilDBType.Varchar, DBUtilDirection.In, 255, user.Password);

                oDBUtility.AddParameters("@Message", DBUtilDBType.Varchar, DBUtilDirection.In, 500, comment.Message);
                oDBUtility.AddParameters("@Remark", DBUtilDBType.Varchar, DBUtilDirection.In, 500, comment.Remark);
                oDBUtility.AddParameters("@CommentBy", DBUtilDBType.Integer, DBUtilDirection.In, 100, comment.CommentBy);

                oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 255, comment.CreatedBy);



                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_COMMENTS");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));

            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }


        [Route("COMMENT/get")]
        [HttpPost]
        public IActionResult Get(Comments comment)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (comment.CommentID != 0 && comment.CommentID != null)
                    {
                        oDBUtility.AddParameters("@CommentID", DBUtilDBType.Integer, DBUtilDirection.In, 10, comment.CommentID);
                    }


                    if (comment.AuditResponseID != 0)
                    {
                        oDBUtility.AddParameters("@AuditResponseID", DBUtilDBType.Integer, DBUtilDirection.In, 10, comment.AuditResponseID);
                    }


                    if (comment.ClientAuditID != 0)
                    {
                        oDBUtility.AddParameters("@ClientAuditID", DBUtilDBType.Integer, DBUtilDirection.In, 10, comment.ClientAuditID);
                    }

                    if (comment.UserID != 0)
                    {
                        oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, comment.UserID);
                    }
                    if (comment.Message != null)
                    {
                        oDBUtility.AddParameters("@Message", DBUtilDBType.Varchar, DBUtilDirection.In, 100, comment.Message);
                    }
                    if (comment.Remark != null)
                    {
                        oDBUtility.AddParameters("@Remark", DBUtilDBType.Varchar, DBUtilDirection.In, 100, comment.Remark);
                    }
                    if (comment.CommentBy != 0)
                    {
                        oDBUtility.AddParameters("@CommentBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, comment.CommentBy);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_COMMENTS");

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
