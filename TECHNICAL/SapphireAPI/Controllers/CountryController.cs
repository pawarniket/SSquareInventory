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

    public class CountryController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public CountryController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }
        [Route("Country/get")]
        [HttpPost]
        public IActionResult Get(Country country)
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
                    {
                        if (country.CountryID != 0)
                        {
                            oDBUtility.AddParameters("@CountryID", DBUtilDBType.Integer, DBUtilDirection.In, 50, country.CountryID);
                        }
                        if (country.CountryName != null)
                        {
                            oDBUtility.AddParameters("@CountryName", DBUtilDBType.Varchar, DBUtilDirection.In, 100, country.CountryName);
                        }


                        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_COUNTRY");

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
