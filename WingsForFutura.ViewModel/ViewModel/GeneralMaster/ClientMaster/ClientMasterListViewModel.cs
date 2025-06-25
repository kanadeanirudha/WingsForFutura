using System.Collections.Generic;
namespace Coditech.ViewModel
{
    public class ClientMasterListViewModel : BaseViewModel
    {
        public List<ClientMasterViewModel> ClientMasterList { get; set; }

        public ClientMasterListViewModel()
        {
            ClientMasterList = new List<ClientMasterViewModel>();
        }
    }
}
