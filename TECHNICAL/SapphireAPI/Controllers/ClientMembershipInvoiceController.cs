using DAE.Configuration;
using DAE.DAL.SQL;
using MS.SSquare.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MS.SSquare.API.Controllers
{
    [ApiController]
    [Authorize]

    public class ClientMembershipInvoiceController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public ClientMembershipInvoiceController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("ClientMembershipInvoice/add")]
        [HttpPost]

        public IActionResult Post(ClientMembershipInvoice cminvoice)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                string invoiceNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
                cminvoice.InvoiceNumber = invoiceNumber;
                oDBUtility.AddParameters("@InvoiceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.InvoiceNumber);
                //oDBUtility.AddParameters("@InvoiceDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.InvoiceDate);
                oDBUtility.AddParameters("@MemberShipStartDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipStartDate);
                oDBUtility.AddParameters("@MemberShipExpiryDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipExpiryDate);
                oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.ClientID);
                oDBUtility.AddParameters("@MemberShipID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.MemberShipID);
                oDBUtility.AddParameters("@InvoiceAmount", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceAmount);
                oDBUtility.AddParameters("@SGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.SGST);
                oDBUtility.AddParameters("@CGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.CGST);
                oDBUtility.AddParameters("@IGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.IGST);
                oDBUtility.AddParameters("@InvoiceTotal", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceTotal);
                oDBUtility.AddParameters("@PaymentMode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.PaymentMode);
                oDBUtility.AddParameters("@ReferenceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 100, cminvoice.ReferenceNumber);
                oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_ADD_CLIENT_MEMBERSHIP_INVOICE");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));

            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("ClientMembershipInvoice/get")]
        [HttpPost]
        public IActionResult Get(ClientMembershipInvoice cminvoice)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                {
                    if (cminvoice.ClientMembershipID != null && cminvoice.ClientMembershipID != 0)
                    {
                        oDBUtility.AddParameters("@ClientMembershipID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.ClientMembershipID);
                    }

                    if (cminvoice.InvoiceNumber != null)
                    {
                        oDBUtility.AddParameters("@InvoiceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.InvoiceNumber);
                    }

                    if (cminvoice.InvoiceDate != null)
                    {
                        oDBUtility.AddParameters("@InvoiceDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.InvoiceDate);
                    }
                    if (cminvoice.MemberShipStartDate != null)
                    {
                        oDBUtility.AddParameters("@MemberShipStartDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipStartDate);
                    }
                    if (cminvoice.MemberShipExpiryDate != null)
                    {
                        oDBUtility.AddParameters("@MemberShipExpiryDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipExpiryDate);
                    }
                    if (cminvoice.@ClientID != null && cminvoice.ClientID != 0)
                    {
                        oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.ClientID);
                    }
                    if (cminvoice.MemberShipID != null && cminvoice.MemberShipID != 0)
                    {
                        oDBUtility.AddParameters("@MemberShipID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.MemberShipID);
                    }
                    if (cminvoice.InvoiceAmount != null)
                    {
                        oDBUtility.AddParameters("@InvoiceAmount", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceAmount);
                    }
                    if (cminvoice.SGST != null)
                    {
                        oDBUtility.AddParameters("@SGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.SGST);
                    }
                    if (cminvoice.CGST != null)
                    {
                        oDBUtility.AddParameters("@CGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.CGST);
                    }
                    if (cminvoice.IGST != null)
                    {
                        oDBUtility.AddParameters("@IGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.IGST);
                    }
                    if (cminvoice.InvoiceTotal != null)
                    {
                        oDBUtility.AddParameters("@InvoiceTotal", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceTotal);
                    }
                    if (cminvoice.PaymentMode != null)
                    {
                        oDBUtility.AddParameters("@PaymentMode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.PaymentMode);
                    }
                    if (cminvoice.ReferenceNumber != null)
                    {
                        oDBUtility.AddParameters("@ReferenceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 100, cminvoice.ReferenceNumber);
                    }


                    DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_CLIENT_MEMBERSHIP_INVOICE");

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



        [Route("ClientMembershipInvoice/update")]
        [HttpPost]
        public IActionResult Put(ClientMembershipInvoice cminvoice)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                if (cminvoice.ClientMembershipID != null && cminvoice.ClientMembershipID != 0)
                {
                    oDBUtility.AddParameters("@ClientMembershipID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.ClientMembershipID);
                }
                if (cminvoice.InvoiceNumber != null)
                {
                    oDBUtility.AddParameters("@InvoiceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.InvoiceNumber);
                }

                if (cminvoice.InvoiceDate != null)
                {
                    oDBUtility.AddParameters("@InvoiceDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.InvoiceDate);
                }
                if (cminvoice.MemberShipStartDate != null)
                {
                    oDBUtility.AddParameters("@MemberShipStartDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipStartDate);
                }
                if (cminvoice.MemberShipExpiryDate != null)
                {
                    oDBUtility.AddParameters("@MemberShipExpiryDate", DBUtilDBType.DateTime, DBUtilDirection.In, 10, cminvoice.MemberShipExpiryDate);
                }
                if (cminvoice.@ClientID != null && cminvoice.ClientID != 0)
                {
                    oDBUtility.AddParameters("@ClientID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.ClientID);
                }
                if (cminvoice.MemberShipID != null && cminvoice.MemberShipID != 0)
                {
                    oDBUtility.AddParameters("@MemberShipID", DBUtilDBType.Integer, DBUtilDirection.In, 10, cminvoice.MemberShipID);
                }
                if (cminvoice.InvoiceAmount != null)
                {
                    oDBUtility.AddParameters("@InvoiceAmount", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceAmount);
                }
                if (cminvoice.SGST != null)
                {
                    oDBUtility.AddParameters("@SGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.SGST);
                }
                if (cminvoice.CGST != null)
                {
                    oDBUtility.AddParameters("@CGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.CGST);
                }
                if (cminvoice.IGST != null)
                {
                    oDBUtility.AddParameters("@IGST", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.IGST);
                }
                if (cminvoice.InvoiceTotal != null)
                {
                    oDBUtility.AddParameters("@InvoiceTotal", DBUtilDBType.Decimal, DBUtilDirection.In, 1000, cminvoice.InvoiceTotal);
                }
                if (cminvoice.PaymentMode != null)
                {
                    oDBUtility.AddParameters("@PaymentMode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cminvoice.PaymentMode);
                }
                if (cminvoice.ReferenceNumber != null)
                {
                    oDBUtility.AddParameters("@ReferenceNumber", DBUtilDBType.Varchar, DBUtilDirection.In, 100, cminvoice.ReferenceNumber);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_CLIENT_MEMBERSHIP_INVOICE");
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
