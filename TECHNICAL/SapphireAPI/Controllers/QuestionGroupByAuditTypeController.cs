using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Authorize]

public class QuestionGroupByAuditTypeController : ControllerBase
{
    private readonly IDaeConfigManager _configurationIG;
    private readonly IWebHostEnvironment _env;
    private ServiceRequestProcessor oServiceRequestProcessor;

    public QuestionGroupByAuditTypeController(IDaeConfigManager configuration, IWebHostEnvironment env)
    {
        _configurationIG = configuration;
        _env = env;
    }


    [Route("QuestionGroupByAuditType/get")]
    [HttpPost]
    public IActionResult Get(QuestionGroupByAuditType questiongroupbyaudittype)
    {
        try
        {
            DBUtility oDBUtility = new DBUtility(_configurationIG);
            {

                if (questiongroupbyaudittype.AuditTypeID != null && questiongroupbyaudittype.AuditTypeID != 0)
                {
                    oDBUtility.AddParameters("@AuditTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, questiongroupbyaudittype.AuditTypeID);
                }


                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_Questiongroup_By_AuditType");

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
