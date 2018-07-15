using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Repo.Repos;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProcessFlowController : Controller
    {
        public IRepo_PFlow _irepoPFlow; 

        public ProcessFlowController(IRepo_PFlow irepoPFlow)
        {
            _irepoPFlow = irepoPFlow;
        }

        public JsonResult Read_GetAllFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.AllFlowQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetOPDFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetOPDQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetConsultingFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetConsultingQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetConsultingFlowQueuesByRoomID([DataSourceRequest] DataSourceRequest request, int conRoomID)
        {
            var model = _irepoPFlow.GetConsultingQueuesByRoomID(conRoomID);
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetLabFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetLabQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetAdmissionFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetAdmissionQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetPharmacyFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetPharmacyQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult Read_GetAccountsFlowQueues([DataSourceRequest] DataSourceRequest request)
        {
            var model = _irepoPFlow.GetAccountsQueues();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}