using Coditech.BusinessLogicLayer;
using Coditech.Filters;
using Coditech.Resources;
using Coditech.Utilities.Constant;
using Coditech.Utilities.Helper;
using Coditech.ViewModel;
using System.Web.Mvc;
using System.Web.Security;
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
            UserMasterListViewModel list = _userClientBA.GetUserClientList();
            return View($"~/Views/UserClient/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new UserMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(UserMasterViewModel userMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                userMasterViewModel = _userClientBA.CreateUserClient(userMasterViewModel);
                if (!userMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(userMasterViewModel.ErrorMessage));
            return View(createEdit, userMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult EditUserClient(int userMasterId)
        {
            UserMasterViewModel userMasterViewModel = _userClientBA.GetUserClient(userMasterId);
            foreach (var item in _adminRoleMasterBA.GetAdminRoleList()?.AdminRoleMasterList)
            {
                userMasterViewModel.AdminRoleMasterList.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.AdminRoleMasterId.ToString(),
                    Selected = item.AdminRoleMasterId == userMasterViewModel.AdminRoleMasterId
                });
            }

            return ActionView($"~/Views/UserMaster/Edit.cshtml", userMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult EditUserClient(UserMasterViewModel userMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _userClientBA.UpdateUserClient(userMasterViewModel).HasError;
                SetNotificationMessage(status
                    ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    return RedirectToAction<ClientController>(x => x.List());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(userMasterViewModel.ErrorMessage));
            return RedirectToAction<ClientController>(x => x.EditUserClient(userMasterViewModel.UserMasterId));
        }

    }
}