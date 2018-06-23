using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Repo.Models;
using Repo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LabController : Controller
    {
        private readonly IRepo_Labs _labRepo;

        public LabController(IRepo_Labs labRepo)
        {
            _labRepo = labRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult labws_Read([DataSourceRequest] DataSourceRequest request)
        {
            var model = _labRepo.GetAllLabws();
            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult labws_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<labw> labws)
        {
            foreach (var labw in labws)
            {
                if (labw != null && ModelState.IsValid)
                {
                    _labRepo.AddLabw(labw);
                }
            }

            return Json(labws.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult labws_BatchUpdate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<labw> labws)
        {
            foreach (var labw in labws)
            {
                if (labw != null)
                {
                    _labRepo.EditLabw(labw);
                }
            }

            return Json(labws.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult labws_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<labw> labws)
        {
            foreach (var labw in labws)
            {
                if (labw != null && ModelState.IsValid)
                {
                    _labRepo.DeleteLabw(labw.ID);
                }
            }

            return Json(labws.ToDataSourceResult(request, ModelState));
        }

    }
}