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
    [Authorize]


    public class VendorController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public VendorController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("Vendor/getALLDATA")]
        [HttpPost]
        public IActionResult GetClient(Client allclient)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {

                    if (allclient.ClientTypeId != 0)
                    {
                        oDBUtility.AddParameters("@ClientTypeId", DBUtilDBType.Integer, DBUtilDirection.In, 10, allclient.ClientTypeId);
                    }
                    if (allclient.Name != null)
                    {
                        oDBUtility.AddParameters("@Name", DBUtilDBType.Varchar, DBUtilDirection.In, 100, allclient.Name);
                    }
                    if (allclient.ParentClientID != 0)
                    {
                        oDBUtility.AddParameters("@ParentClientID", DBUtilDBType.Integer, DBUtilDirection.In, 50, allclient.ParentClientID);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, allclient.IsActive);



                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_VENDOR");

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
