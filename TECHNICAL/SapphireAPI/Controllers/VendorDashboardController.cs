using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Common.EncryptionDecryption;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MS.SSquare.API.Controllers
{
 
    [ApiController]
    [Authorize]

    public class VendorDashboardController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public VendorDashboardController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("VendorDashboard/getVendorDahboard")]
        [HttpPost]
        public IActionResult GetAdminDashboard(VendorDashboard vendordashboard)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2" && userrole != "5")
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
                        if (vendordashboard.VendorID != null)
                        {
                            oDBUtility.AddParameters("@VendorID", DBUtilDBType.Integer, DBUtilDirection.In, 10, vendordashboard.VendorID);
                        }


                        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_VENDOR_DASHBOARD");

                        return new JsonResult(ds);

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
