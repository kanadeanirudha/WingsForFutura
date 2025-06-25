using Coditech.DataAccessLayer;
using Coditech.ExceptionManager;
using Coditech.Model;
using Coditech.Resources;
using Coditech.Utilities.Helper;
using Coditech.ViewModel;
using System;
using System.Linq;
using static Coditech.Utilities.Helper.CoditechHelperUtility;
namespace Coditech.BusinessLogicLayer
{
    public class UserClientBA : BaseBusinessLogic
    {
        UserClientDAL _userClientDAL = null;
        public UserClientBA()
        {
            _userClientDAL = new UserClientDAL();
        }
        public ClientMasterViewModel CreateUserClient(ClientMasterViewModel clientMasterViewModel)
        {
            try
            {
                UserModel userModel = clientMasterViewModel.ToModel<UserModel>();
                UserModel created = _userClientDAL.CreateUserClient(userModel);

                return IsNotNull(created)
                    ? created.ToViewModel<ClientMasterViewModel>()
                    : new ClientMasterViewModel { HasError = true, ErrorMessage = GeneralResources.ErrorFailedToCreate };
            }
            catch (CoditechException ex)
            {
                return (ClientMasterViewModel)GetViewModelWithErrorMessage(clientMasterViewModel, ex.ErrorMessage);
            }
            catch (Exception ex)
            {
                CoditechFileLogging.LogMessage(ex.Message, CoditechComponents.Components.User.ToString());
                return (ClientMasterViewModel)GetViewModelWithErrorMessage(clientMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }
        public ClientMasterListViewModel GetUserClientList()
        {
            ClientMasterListModel clientMasterList = _userClientDAL.GetUserClientList();
            ClientMasterListViewModel listViewModel = new ClientMasterListViewModel { ClientMasterList = clientMasterList?.ClientMasterList?.ToViewModel<ClientMasterViewModel>().ToList() };
            return listViewModel;
        }

        public ClientMasterViewModel GetUserClient(int userMasterId)
            => _userClientDAL.GetUserClient(userMasterId).ToViewModel<ClientMasterViewModel>();

        public ClientMasterViewModel UpdateUserClient(ClientMasterViewModel clientMasterViewModel)
        {
            try
            {
                clientMasterViewModel.ModifiedBy = LoginUserId();
                UserModel userMasterModel = _userClientDAL.UpdateUserClient(clientMasterViewModel.ToModel<UserModel>());
                return IsNotNull(userMasterModel) ? userMasterModel.ToViewModel<ClientMasterViewModel>() : (ClientMasterViewModel)GetViewModelWithErrorMessage(new ClientMasterListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                CoditechFileLogging.LogMessage(ex.Message, CoditechComponents.Components.ClientMaster.ToString());
                return (ClientMasterViewModel)GetViewModelWithErrorMessage(clientMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
    }
}
