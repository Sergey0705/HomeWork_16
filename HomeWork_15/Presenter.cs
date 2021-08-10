using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    class Presenter
    {
        private Model model;
        private IView view;

        public Presenter(IView View)
        {
            this.view = View;
            model = new Model();
        }

        public List<Department> GetDepartments()
        {
           return model.GetDepartments();
        }
        public void LoadBank()
        {
            model.LoadBank();
        }

        public List<ClientsOfBank> GetClientsOfBank()
        {
            return model.GetClientsOfBank();
        }

        public void CloseAccount()
        {
            model.CloseAccount(view.Department, view.Client, view.Account);
        }

        public void OpenAccount()
        {
            model.OpenAccount(view.Department, view.Client);
        }

        public void TransferMoney()
        {
            model.TransferMoney(view.Department, view.Client, view.Account, view.Sum);
        }

        public void OpenDepositWithoutCap()
        {
            model.OpenDepositWithoutCap(view.Department, view.Client);
        }

        public void OpenDepositWithCap()
        {
            model.OpenDepositWithCap(view.Department, view.Client);
        }

        public void TransferMonths()
        {
            model.TransferMonths(view.Department, view.TransferMonth);
        }

        public void GiveCredit()
        {
            model.GiveCredit(view.Department, view.Client);
        }

        public void AddClient()
        {
            model.AddClient(view.Department);
        }

        public void DeleteClient()
        {
            model.DeleteClient(view.Department, view.Client);
        }

        public void CreateDataBaseMessages()
        {
            model.CreateDataBaseMessages(view.Department);
        }

        public void ChangePropertyIsCreateDataBaseMessagesCsv()
        {
            model.ChangePropertyIsCreateDataBaseMessagesCsv();
        }

        public void AddDataBaseMessagesToClient()
        {
            model.AddDataBaseMessagesToClient(view.Department, view.Client);
        }

        public bool NavigationOnDataBase()
        {
            return model.NavigationOnDataBase(view.Department, view.Client, view.NumberOfNavigation);
        }
    }
}
