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

    public class UserRoleFormsController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public UserRoleFormsController(IDaeConfigManager configuration)
        {
            _configurationIG = configuration;
        }
        [Route("UserRoleForms/get")]
        [HttpPost]
        public IActionResult Get(UserRoleForms userroleforms)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (userroleforms.UserRoleFormID != 0)
                {
                    oDBUtility.AddParameters("@UserRoleFormID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userroleforms.UserRoleFormID);
                }
                if (userroleforms.IsActive != null)
                {
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, userroleforms.IsActive);
                }
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_USERROLEFORMS");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }
        //[Route("menu/get")]
        //[HttpPost]
        //public IActionResult Gets(UserRoleForms userroleforms)
        //{

        //    try
        //    {
        //        DBUtility oDBUtility = new DBUtility(_configurationIG);

        //        if (userroleforms.UserRoleID != 0)
        //        {
        //            oDBUtility.AddParameters("@UserRoleID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userroleforms.UserRoleID);
        //        }
        //        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_MENU_AS_PER_ROLE");
        //        oServiceRequestProcessor = new ServiceRequestProcessor();
        //        return Ok(oServiceRequestProcessor.ProcessRequest(ds));
        //    }
        //    catch (Exception ex)
        //    {
        //        oServiceRequestProcessor = new ServiceRequestProcessor();
        //        return BadRequest(oServiceRequestProcessor.onError(ex.Message));
        //    }
        //}
    }
}
