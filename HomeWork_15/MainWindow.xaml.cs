using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;


namespace HomeWork_15
{
    public partial class MainWindow : Window, IView
    {
        Presenter p;

        bool isValidNavigationNumber;

        public MainWindow()
        {
            InitializeComponent();

            p = new Presenter(this);

            cbDepartment.ItemsSource = p.GetDepartments();

            isValidNavigationNumber = false;

            btnCloseAccount.Click += (s, e) =>
            {
                p.CloseAccount();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnOpenAccount.Click += (s, e) =>
            {
                p.OpenAccount();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnTransferMoney.Click += (s, e) =>
            {
                p.TransferMoney();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnOpenDepositWithoutCap.Click += (s, e) =>
            {
                p.OpenDepositWithoutCap();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnOpenDepositWithCap.Click += (s, e) =>
            {
                p.OpenDepositWithCap();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnTransferMonths.Click += (s, e) =>
            {
                p.TransferMonths();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnGiveCredit.Click += (s, e) =>
            {
                p.GiveCredit();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnAddClient.Click += (s, e) =>
            {
                p.AddClient();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnDeleteClient.Click += (s, e) =>
            {
                p.DeleteClient();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnCreateDataBaseMessages.Click += (s, e) => p.CreateDataBaseMessages();

            btnCreateDataBaseMessagesCsv.Click += (s, e) =>
            {
                p.ChangePropertyIsCreateDataBaseMessagesCsv();
                p.CreateDataBaseMessages();
            };

            btnAddDataBaseMessagesToClient.Click += (s, e) =>
            {
                p.AddDataBaseMessagesToClient();
                lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId);
            };

            btnNavigationOnDataBase.Click += (s, e) =>
            {
                isValidNavigationNumber = p.NavigationOnDataBase();
                if (isValidNavigationNumber)
                {
                    p.AddDataBaseMessagesToClient();
                }     
            };

            this.Loaded += (s, e) => p.LoadBank();        
        }

        public Department Department
        {
            get => cbDepartment.SelectedItem as Department;
        }
        public ClientsOfBank Client
        {
            get => lvClients.SelectedItem as ClientsOfBank;
        }
        public string Account
        {
            get => tbAccount.Text;
        }
        public string Sum
        {
            get => tbSum.Text;
        }
        public string TransferMonth
        {
            get => tbMonth.Text;
        }

        public string NumberOfNavigation
        {
            get => tbNavigation.Text;
        }

        /// <summary>
        /// Изменяет список клиентов в зависимости от выбранного отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvClients.ItemsSource = p.GetClientsOfBank().Where(client => client.DepartmentId == Department.DepartmentId); 
        }
    }
}
