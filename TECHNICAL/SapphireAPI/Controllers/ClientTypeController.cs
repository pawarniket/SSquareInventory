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

    public class ClientTypeController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public ClientTypeController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }
       



        [Route("ClientType/getclientname")]
        [HttpPost]
        public IActionResult Get(ClientType clienttype)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (clienttype.ClientTypeID != 0)
                    {
                        oDBUtility.AddParameters("@ClientTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clienttype.ClientTypeID);
                    }
                    if (clienttype.ClientTypeName != null)
                    {
                        oDBUtility.AddParameters("@ClientTypeName", DBUtilDBType.Varchar, DBUtilDirection.In, 10, clienttype.ClientTypeName);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CLIENTTYPE");

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

