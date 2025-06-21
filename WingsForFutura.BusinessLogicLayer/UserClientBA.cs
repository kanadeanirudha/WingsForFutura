using Coditech.DataAccessLayer;
using Coditech.ExceptionManager;
using Coditech.Model;
using Coditech.Resources;
using Coditech.Utilities.Constant;
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
        public UserMasterViewModel CreateUserClient(UserMasterViewModel userMasterViewModel)
        {
            try
            {
                UserModel userMasterModel = _userClientDAL.CreateUserClient(userMasterViewModel.ToModel<UserModel>());
                return IsNotNull(userMasterModel) ? userMasterModel.ToViewModel<UserMasterViewModel>() : new UserMasterViewModel();
            }
            catch (CoditechException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (UserMasterViewModel)GetViewModelWithErrorMessage(userMasterViewModel, ex.ErrorMessage);
                    default:
                        return (UserMasterViewModel)GetViewModelWithErrorMessage(userMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                CoditechFileLogging.LogMessage(ex.Message, CoditechComponents.Components.User.ToString());
                return (UserMasterViewModel)GetViewModelWithErrorMessage(userMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }
        public UserMasterListViewModel GetUserClientList()
        {
            UserMasterListModel userMasterList = _userClientDAL.GetUserClientList();
            UserMasterListViewModel listViewModel = new UserMasterListViewModel { UserMasterList = userMasterList?.UserMasterList?.ToViewModel<UserMasterViewModel>().ToList() };
            return listViewModel;
        }

        //Get ProductMaster by ProductMaster id.
        public UserMasterViewModel GetUserClient(int userMasterId)
            => _userClientDAL.GetUserClient(userMasterId).ToViewModel<UserMasterViewModel>();

        //Update ProductMaster.
        public UserMasterViewModel UpdateUserClient(UserMasterViewModel userMasterViewModel)
        {
            try
            {
                userMasterViewModel.ModifiedBy = LoginUserId();
                UserModel userMasterModel = _userClientDAL.UpdateUserClient(userMasterViewModel.ToModel<UserModel>());
                return IsNotNull(userMasterModel) ? userMasterModel.ToViewModel<UserMasterViewModel>() : (UserMasterViewModel)GetViewModelWithErrorMessage(new UserMasterListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                CoditechFileLogging.LogMessage(ex.Message, CoditechComponents.Components.ClientMaster.ToString());
                return (UserMasterViewModel)GetViewModelWithErrorMessage(userMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
    }
}
