using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DAE.DAL.SQL;
using DAE.Configuration;
using System;
using MS.SSquare.API;
using Microsoft.AspNetCore.Authorization;
using MS.SSquare.API.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;



namespace MS.SSquare.API.Controllers
{
    //[ApiController]
    //public class DashboardController : ControllerBase
    //{
    //}
    [ApiController]
    [Authorize]

    public class DashboardController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public DashboardController(IDaeConfigManager configuration)
        {
            this._configurationIG = configuration;
        }
        [Route("dashboard/get")]
        [HttpPost]
        public JsonResult Get(Dashboard dashboard)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                if (dashboard.Cinema_strID != "0")
                {
                    oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, dashboard.Cinema_strID);
                }
                if (dashboard.OrderStatusID != 0)
                {
                    oDBUtility.AddParameters("@OrderStatusID", DBUtilDBType.Integer, DBUtilDirection.In, 10, dashboard.OrderStatusID);
                }
                if (dashboard.OrderID != 0)
                {
                    oDBUtility.AddParameters("@OrderID", DBUtilDBType.Integer, DBUtilDirection.In, 10, dashboard.OrderID);
                }
                if (dashboard.UserID != 0)
                {
                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, dashboard.UserID);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_DASHBOARD");

                return new JsonResult(ds);

            }
            catch (Exception ex)
            {
                return new JsonResult(null);
            }
        }


        //[Route("dashboard/CinemawiseStatusWise")]
        //[HttpPost]
        //public JsonResult CinemawiseStatusWise(Dashboard dashboard)
        //{

        //    try
        //    {
        //        DBUtility oDBUtility = new DBUtility(_configurationIG);
                    
        //        DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_REPORT_CinemaWiseStatusWise");

        //        return new JsonResult(ds);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(null);
        //    }
        //}


    }
}
