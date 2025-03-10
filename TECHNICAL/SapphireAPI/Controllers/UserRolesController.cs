using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DAE.DAL.SQL;
using DAE.Configuration;
using System;
using MS.SSquare.API;
using Microsoft.AspNetCore.Authorization;
using MS.SSquare.API.Models;


namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class UserRolesController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public UserRolesController(IDaeConfigManager configuration)
        {
            _configurationIG = configuration;
        }
        [Route("userroles/get")]
        [HttpPost]
        public IActionResult GetUserRoles(UserRoles userRoles)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_USER_ROLES");
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
