using System.Linq;
using Coditech.DataAccessLayer.DataEntity;
using Coditech.DataAccessLayer.Repository;
using Coditech.ExceptionManager;
using Coditech.Model;
using Coditech.Resources;
using Coditech.Utilities.Helper;
using static Coditech.Utilities.Helper.CoditechHelperUtility;
namespace Coditech.DataAccessLayer
{
    public class UserClientDAL : BaseDataAccessLogic
    {
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        private readonly ICoditechRepository<AdminRoleMaster> _roleMasterRepository;
        private readonly ICoditechRepository<AdminAssociateFormsToRole> _adminAssociateFormsToRoleRepository;
        public UserClientDAL()
        {
            _userMasterRepository = new CoditechRepository<UserMaster>();
            _roleMasterRepository = new CoditechRepository<AdminRoleMaster>();
            _adminAssociateFormsToRoleRepository = new CoditechRepository<AdminAssociateFormsToRole>();
        }

        #region Public Method
        public UserModel CreateUserClient(UserModel userMasterModel)
        {
            if (IsNull(userMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            userMasterModel.UserName = userMasterModel.EmailId;
            userMasterModel.UserType = userMasterModel.AdminRoleMasterId.ToString();
            userMasterModel.MobileNumber = "9876543210";
            string rawPassword = GenerateRandomPassword();
            userMasterModel.Password = MD5Hash(rawPassword);
            UserMaster userModel = userMasterModel.FromModelToEntity<UserMaster>();

            UserMaster userData = _userMasterRepository.Insert(userModel);
            if (userData?.UserMasterId > 0)
            {
                userMasterModel.UserMasterId = userData.UserMasterId;
            }
            else
            {
                userMasterModel.HasError = true;
                userMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return userMasterModel;
        }


        public UserMasterListModel GetUserClientList()
        {
            UserMasterListModel listModel = new UserMasterListModel();
            listModel.UserMasterList = (from user in _userMasterRepository.Table
                                        join role in _roleMasterRepository.Table
                                        on user.AdminRoleMasterId equals role.AdminRoleMasterId
                                        into UserRoleGroup //Performing LINQ Group Join
                                        from userrole in UserRoleGroup.DefaultIfEmpty()
                                        where user.UserType != "SuperAdmin"
                                        select new UserModel
                                        {
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            IsActive = user.IsActive,
                                            AdminRoleMasterId = user.AdminRoleMasterId,
                                            RoleName = userrole.RoleName,
                                            UserMasterId = user.UserMasterId,
                                        }).ToList();
            return listModel;
        }


        //Get UserMaster by UserMaster id.
        public UserModel GetUserClient(int userMasterId)
        {
            if (userMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "userMasterId"));

            //Get the UserMaster Details based on id.
            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserMasterId == userMasterId);
            UserModel userMasterModel = userMasterData.FromEntityToModel<UserModel>();
            return userMasterModel;
        }

        //Update UserMaster.
        public UserModel UpdateUserClient(UserModel userModel)
        {
            if (IsNull(userModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (userModel.UserMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "ProductMasterID"));

            UserMaster userMasterData = _userMasterRepository.Table.Where(x => x.UserMasterId == userModel.UserMasterId)?.FirstOrDefault();
            userMasterData.IsActive = userModel.IsActive;
            userMasterData.AdminRoleMasterId = userModel.AdminRoleMasterId;
            userMasterData.ModifiedBy = userModel.ModifiedBy;
            //Update UserMaster
            bool isUserMasterUpdated = _userMasterRepository.Update(userMasterData);
            if (!isUserMasterUpdated)
            {
                userModel.HasError = true;
                userModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return userModel;
        }

        #endregion
    }
}
