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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace MS.SSquare.API.Controllers
{

    [ApiController]
    [Authorize]

    public class BannersController : ControllerBase
    {
        private readonly IDaeConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public BannersController(IDaeConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }
        [Route("Banners/get")]
        [HttpPost]
        public IActionResult Get(Banners banners)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);


                if (banners.BannerID != 0)
                {
                    oDBUtility.AddParameters("@BannerID", DBUtilDBType.Integer, DBUtilDirection.In, 10, banners.BannerID);
                }
                if (banners.Cinema_strID != null)
                {
                    oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Varchar, DBUtilDirection.In, 10, banners.Cinema_strID);
                }
                if (banners.BannerImagePath != null)
                {
                    oDBUtility.AddParameters("@BannerImagePath", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, banners.BannerImagePath);
                }
                if (banners.BannerType != null)
                {
                    oDBUtility.AddParameters("@BannerType", DBUtilDBType.Varchar, DBUtilDirection.In, 1, banners.BannerType);
                }
                if (banners.IsActive != null)
                {
                    oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, banners.IsActive);
                }
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_DAE_Banners");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Banners/insert")]
        [HttpPost]
        public IActionResult Post(Banners banners)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Integer, DBUtilDirection.In, 50, banners.Cinema_strID);
                oDBUtility.AddParameters("@BannerImagePath", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, banners.BannerImagePath);
                oDBUtility.AddParameters("@BannerType", DBUtilDBType.Varchar, DBUtilDirection.In, 1, banners.BannerType);
                oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, banners.IsActive);
                oDBUtility.AddParameters("@CreatedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, banners.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_INSERT_DAE_Banners");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));

            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }
        [Route("Banners/update")]
        [HttpPost]
        public IActionResult Put(Banners banners)
        {

            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@BannerID", DBUtilDBType.Integer, DBUtilDirection.In, 10, banners.BannerID);
                oDBUtility.AddParameters("@Cinema_strID", DBUtilDBType.Integer, DBUtilDirection.In, 50, banners.Cinema_strID);
                oDBUtility.AddParameters("@BannerImagePath", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, banners.BannerImagePath);
                oDBUtility.AddParameters("@BannerType", DBUtilDBType.Varchar, DBUtilDirection.In, 1, banners.BannerType);
                oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 5, banners.IsActive);
                oDBUtility.AddParameters("@ModifiedBy", DBUtilDBType.Integer, DBUtilDirection.In, 10, banners.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPADTE_DAE_Banners");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }

        }
        [Route("Banners/home")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/banner/home/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }

        }
        [Route("Banners/Inner")]
        [HttpPost]
        public JsonResult SaveinnerFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/banner/inner/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }

        }

        [Route("Banners/Isactive")]
        [HttpPost]
        public IActionResult PutIsactive(Banners banners)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@BannerID", DBUtilDBType.Integer, DBUtilDirection.In, 10, banners.BannerID);
                oDBUtility.AddParameters("@IsActive", DBUtilDBType.Boolean, DBUtilDirection.In, 10, banners.IsActive);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_UPDATE_DAE_Banners_ISACTIVE");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        //[Route("Banners/home")]
        //[HttpPost]
        //public JsonResult SaveFile(int id)
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "Images/banner/home/" + id + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }
        //        return new JsonResult(id + filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}
        //[Route("Banners/inner")]
        //[HttpPost]
        //public JsonResult SaveFiles(int id)
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "/banner/inner/" + id + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }
        //        return new JsonResult(id + filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}
    }
}
