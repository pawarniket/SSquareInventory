using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.MirajPopExpress.API.Models;
using Microsoft.AspNetCore.Authorization;


namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class StateController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public StateController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }
        [Route("State/get")]
        [HttpPost]
        public IActionResult Get(State state)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (state.StateID != 0)
                    {
                        oDBUtility.AddParameters("@StateID", DBUtilDBType.Integer, DBUtilDirection.In, 50, state.StateID);
                    }
                    if (state.StateName != null)
                    {
                        oDBUtility.AddParameters("@StateName", DBUtilDBType.Varchar, DBUtilDirection.In, 100, state.StateName);
                    }


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_STATE");

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
