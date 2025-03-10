using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class CompanyController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public CompanyController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }
        [Route("Company/add")]
        [HttpPost]
        public IActionResult Post(Company company)
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
                    oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, company.Name);
                    oDBUtility.AddParameters("@Email", DBUtilDBType.Varchar, DBUtilDirection.In, 50, company.Email);
                    oDBUtility.AddParameters("@Address", DBUtilDBType.Varchar, DBUtilDirection.In, 225, company.Address);
                    oDBUtility.AddParameters("@GSTNo", DBUtilDBType.Varchar, DBUtilDirection.In, 50, company.GSTNo);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, company.IsActive);


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_COMPANY");
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
