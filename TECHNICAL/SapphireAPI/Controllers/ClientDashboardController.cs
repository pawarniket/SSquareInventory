﻿using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using DAE.Common.EncryptionDecryption;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]


    public class ClientDashboard : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public ClientDashboard(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }


        [Route("ClientDashboard/getClientDashboard")]
        [HttpPost]
        public IActionResult GetAdminDashboard(Clientdashboards clientdashboard)
            {
            if (clientdashboard == null)
            {
                return BadRequest("Invalid data.");
            }
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Role);
                var userrole = userIdClaim.Value;
                if (userrole == null || userrole == "" || userrole != "2")
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
                        if (clientdashboard.ClientID != 0 && clientdashboard.ClientID != null)
                        {
                            oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, clientdashboard.ClientID);
                        }
                        var parameters = new Dictionary<string, object>
                        {
                            { "@ClientID", clientdashboard.ClientID },
                            { "@IsActive", clientdashboard.IsActive }
                        };

                        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CLIENTDASHBOARD");

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
