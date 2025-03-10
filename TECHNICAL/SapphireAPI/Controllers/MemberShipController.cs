using Microsoft.AspNetCore.Http;
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

    public class MemberShipController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public MemberShipController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("MEMBERSHIP/add")]
        [HttpPost]

        public IActionResult Post(MemberShip membership)
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
                    oDBUtility.AddParameters("@MemberShipName", DBUtilDBType.Varchar, DBUtilDirection.In, 500, membership.MemberShipName);
                    oDBUtility.AddParameters("@MembershipFees", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, membership.MembershipFees);
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, membership.IsActive);
                    oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, membership.CreatedBy);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_MEMBERSHIP");
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

        [Route("MEMBERSHIP/get")]
        [HttpPost]
        public IActionResult Get(MemberShip membership)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (membership.MemberShipTypeID != null && membership.MemberShipTypeID != 0)
                    {
                        oDBUtility.AddParameters("@MemberShipTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, membership.MemberShipTypeID);
                    }

                    if (membership.MemberShipName != null)
                    {
                        oDBUtility.AddParameters("@MemberShipName", DBUtilDBType.Varchar, DBUtilDirection.In, 500, membership.MemberShipName);
                    }

                    if (membership.MembershipFees != null)
                    {
                        oDBUtility.AddParameters("@MembershipFees", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, membership.MembershipFees);
                    }
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, membership.IsActive);

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_MEMBERSHIP");

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



        [Route("MEMBERSHIP/update")]
        [HttpPost]
        public IActionResult Put(MemberShip membership)
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
                    if (membership.MemberShipTypeID != 0)
                    {
                        oDBUtility.AddParameters("@MemberShipTypeID", DBUtilDBType.Integer, DBUtilDirection.In, 10, membership.MemberShipTypeID);
                    }

                    if (membership.MemberShipName != null)
                    {
                        oDBUtility.AddParameters("@MemberShipName", DBUtilDBType.Varchar, DBUtilDirection.In, 500, membership.MemberShipName);
                    }

                    if (membership.MembershipFees != null)
                    {
                        oDBUtility.AddParameters("@MembershipFees", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, membership.MembershipFees);
                    }

                    if (membership.IsActive != null)
                    {
                        oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 1, membership.IsActive);
                    }

                    if (membership.ModifiedBy != 0)
                    {
                        oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, membership.ModifiedBy);
                    }

                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_MEMBERSHIP");
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
