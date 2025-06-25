using System.Collections.Generic;
namespace Coditech.Model
{
    public class ClientMasterListModel : BaseListModel
    {
        public List<ClientMasterModel> ClientMasterList { get; set; }
        public ClientMasterListModel()
        {
            ClientMasterList = new List<ClientMasterModel>();
        }
    }
}
