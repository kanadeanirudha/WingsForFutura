using Coditech.BusinessLogicLayer;
using Coditech.Filters;
using Coditech.Resources;
using Coditech.ViewModel;
using System.Web.Mvc;
namespace Coditech.Controllers
{
    [SessionTimeoutAttribute]
    public class ClientController : BaseController
    {
        private const string createEdit = "~/Views/Client/Create.cshtml";
        UserClientBA _userClientBA = null;
        AdminRoleMasterBA _adminRoleMasterBA = null;
        public ClientController()
        {
            _userClientBA = new UserClientBA();
            _adminRoleMasterBA = new AdminRoleMasterBA();
        }

        public ActionResult List()
        {
            ClientMasterListViewModel list = _userClientBA.GetUserClientList();
            return View($"~/Views/Client/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new ClientMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(ClientMasterViewModel clientMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                clientMasterViewModel = _userClientBA.CreateUserClient(clientMasterViewModel);
                if (!clientMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }

            SetNotificationMessage(GetErrorNotificationMessage(clientMasterViewModel.ErrorMessage));
            return View(createEdit, clientMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult EditUserClient(int userMasterId)
        {
            ClientMasterViewModel clientMasterViewModel = _userClientBA.GetUserClient(userMasterId);
            foreach (var item in _adminRoleMasterBA.GetAdminRoleList()?.AdminRoleMasterList)
            {
                clientMasterViewModel.ClientMasterList.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.AdminRoleMasterId.ToString(),
                    Selected = item.AdminRoleMasterId == clientMasterViewModel.AdminRoleMasterId
                });
            }

            return ActionView($"~/Views/Client/Create.cshtml", clientMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult EditUserClient(ClientMasterViewModel clientMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _userClientBA.UpdateUserClient(clientMasterViewModel).HasError;
                SetNotificationMessage(status
                    ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    return RedirectToAction<ClientController>(x => x.List());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(clientMasterViewModel.ErrorMessage));
            return RedirectToAction<ClientController>(x => x.EditUserClient(clientMasterViewModel.UserMasterId));
        }
    }
}