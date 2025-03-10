using DAE.Configuration;
using DAE.DAL.SQL;
using DAE.MirajPopExpress.API.Models;
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

    public class RoleController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public RoleController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("Roles/get")]
        [HttpPost]
        public IActionResult Get(Roles roles)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "1" && userrole != "2")
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
                    {
                        if (roles.RoleID != 0)
                        {
                            oDBUtility.AddParameters("@RoleID", DBUtilDBType.Integer, DBUtilDirection.In, 10, roles.RoleID);
                        }
                        if (roles.RoleName != null)
                        {
                            oDBUtility.AddParameters("@RoleName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, roles.RoleName);
                        }

                        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_ROLES");

                        oServiceRequestProcessor = new ServiceRequestProcessor();
                        return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                    }
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
