using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.IO;

namespace HomeWork_15
{
    class Model
    {
        Bank bank;

        ClientsOfBank client;

        MSSQLLocalEntities db;

        bool isCreatingDataBaseMessages;
        bool isCreateDataBaseMessagesCsv;
        bool isAddingDataBaseMessages;
        int NavigationNumber;

        DataBaseMessagesWriter dataBaseMessagesWriter;

        static Random r;

        static Model()
        {
            r = new Random();
        }

        public Model()
        {
            bank = new Bank();
            client = new ClientsOfBank();
            db = new MSSQLLocalEntities();
            bank.RegisterWithInformationMessage(Bank.OnInformationMessageEvent);
            bank.RegisterWithValidationMessage(Bank.OnValidationMessageEvent);
            isCreatingDataBaseMessages = false;
            isCreateDataBaseMessagesCsv = false;
            isAddingDataBaseMessages = false;
            dataBaseMessagesWriter = new DataBaseMessagesWriter();
            NavigationNumber = 99999;
        }
        /// <summary>
        /// Метод получения департаментов банка
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartments()
        {
            return bank.DepartmentsDb;
        }
        /// <summary>
        /// Метод получения списка клиентов банка
        /// </summary>
        /// <returns></returns>
        public List<ClientsOfBank> GetClientsOfBank()
        {
            using (MSSQLLocalEntities db = new MSSQLLocalEntities())
            {
                return db.ClientsOfBanks.ToList();
            }
        }
        /// <summary>
        /// Метод загрузки банка
        /// </summary>
        public void LoadBank()
        {
            using (MSSQLLocalEntities db = new MSSQLLocalEntities())
            {
                var clients = db.ClientsOfBanks.ToList();

                if (clients.Count == 0)
                {
                    FillClientsOfBankTable();
                }
            }
        }
        /// <summary>
        /// Метод  заполнения клиентами таблицы ClientsOfBank в БД
        /// </summary>
        private void FillClientsOfBankTable()
        {
            using (MSSQLLocalEntities db = new MSSQLLocalEntities())
            {
                for (int i = 0; i < bank.ClientsDb.Count; i++)
                {
                    client.Name = bank.ClientsDb[i].Name;
                    client.Age = bank.ClientsDb[i].Age;
                    client.DepartmentId = bank.ClientsDb[i].DepartamentId;
                    client.RateOfCredit = bank.ClientsDb[i].RateOfCredit;
                    client.MinRate = bank.ClientsDb[i].MinRate;
                    client.FirstAccountValue = bank.ClientsDb[i].FirstAccountValue;
                    client.SumOnFirstAccountValue = bank.ClientsDb[i].SumOnFirstAccountValue;
                    client.SecondAccountValue = bank.ClientsDb[i]?.SecondAccountValue ?? "";
                    client.SumOnSecondAccountValue = bank.ClientsDb[i]?.SumOnSecondAccountValue ?? "";
                    client.JournalOfClientActions = bank.ClientsDb[i].JournalOfClientActions;
                    client.DepositWithoutCapValue = "";
                    client.MonthDepositWithoutCapValue = "";
                    client.SumDepositWithoutCapValue = "";
                    client.DepositWithCapValue = "";
                    client.MonthDepositWithCapValue = "";
                    client.SumDepositWithCapValue = "";
                    client.CreditValue = "";
                    client.MonthCreditValue = "";
                    client.PecentOfCreditValue = "";
                    client.FirstBaseMessages = "";
                    client.SecondBaseMessages = "";
                    client.ThirdBaseMessages = "";
                    client.FourtnBaseMessages = "";
                    client.FifthBaseMessages = "";
                    client.SixthBaseMessages = "";
                    client.SeventhBaseMessages = "";
                    client.EighthBaseMessages = "";
                    client.NinthBaseMessages = "";
                    client.TenthBaseMessages = "";
                    client.InitialMonthDepositWithCap = 12;

                    db.ClientsOfBanks.Add(client);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Метод закрытия счета клиента
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        /// <param name="account"></param>
        public void CloseAccount(Department department, ClientsOfBank chooseClient, string account)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue == "" && clientsOfBank[i].SecondAccountValue == "")
                        {
                            bank.handlerOfValidationMessage("У клиента нет ни одного счета");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && account == "")
                        {
                            bank.handlerOfValidationMessage("Не введен номер счета");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue != "" && clientsOfBank[i].SecondAccountValue != "" && clientsOfBank[i].FirstAccountValue == account)
                        {
                            bank.handlerOfValidationMessage("Закртытие первого(основного) счета не может идти перед закрытием второго(вспомогательного)");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue != "" && clientsOfBank[i].SecondAccountValue == account)
                        {
                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.SecondAccountValue = "";
                                client.SumOnSecondAccountValue = "";
                                client.JournalOfClientActions += $"Клиент закрыл счет {account};\n";
                                bank.handlerOfInformationMessage($"Клиент {chooseClient.Name} закрыл счет {account}");
                                db.SaveChanges();  
                                break;
                            }
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue == account)
                        {
                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.FirstAccountValue = "";
                                client.SumOnFirstAccountValue = "";
                                client.JournalOfClientActions += $"Клиент закрыл счет {account};\n";
                                bank.handlerOfInformationMessage($"Клиент {chooseClient.Name} закрыл счет {account}");
                                db.SaveChanges();
                                break;
                            }
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            bank.handlerOfValidationMessage("Введен неверный номер счета");
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод открытия счета клиенту
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        public void OpenAccount(Department department, ClientsOfBank chooseClient)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue != "" && clientsOfBank[i].SecondAccountValue != "")
                        {
                            bank.handlerOfValidationMessage("Больше добавить счет нельзя");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue != "" && clientsOfBank[i].SecondAccountValue == "")
                        {
                            int sum = r.Next(0, 1_000_000);
                            int accountClient = r.Next(100_000_000, 999_999_999);

                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.SecondAccountValue = accountClient.ToString();
                                client.SumOnSecondAccountValue = sum.ToString();
                                client.JournalOfClientActions += $"Клиент открыл счет: {accountClient} с суммой: {sum};\n";
                                bank.handlerOfInformationMessage($"Клиент открыл счет: {accountClient} с суммой: {sum}");
                                db.SaveChanges();
                                break;
                            }
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            int sum = r.Next(0, 1_000_000);
                            int accountClient = r.Next(100_000_000, 999_999_999);

                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.FirstAccountValue = accountClient.ToString();
                                client.SumOnFirstAccountValue = sum.ToString();
                                client.JournalOfClientActions += $"Клиент открыл счет: {accountClient} с суммой: {sum};\n";
                                bank.handlerOfInformationMessage($"Клиент {chooseClient.Name} открыл счет: {accountClient} с суммой: {sum}");
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод перевода денег между счетами клиента
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        /// <param name="account"></param>
        /// <param name="sum"></param>
        public void TransferMoney(Department department, ClientsOfBank chooseClient, string account, string sum)
        {
            if (department != null && chooseClient != null)
            {
                bool result = int.TryParse(sum, out int sumOfTransfer);
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue == "" && clientsOfBank[i].SecondAccountValue == "")
                        {
                            bank.handlerOfValidationMessage("У клиента нет ни одного счета");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue != "" && clientsOfBank[i].SecondAccountValue == "")
                        {
                            bank.handlerOfValidationMessage("Нет счета для перевода");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && account == "" && sum == "")
                        {
                            bank.handlerOfValidationMessage("Не введен номер счета и сумма перевода");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && account == "")
                        {
                            bank.handlerOfValidationMessage("Не введен номер счета");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && sum == "")
                        {
                            bank.handlerOfValidationMessage("Не введена сумма перевода");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && result == false)
                        {
                            bank.handlerOfValidationMessage("Сумма должна быть введена в виде целого числа");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && ((Convert.ToInt32(clientsOfBank[i].SumOnFirstAccountValue) < sumOfTransfer && clientsOfBank[i].FirstAccountValue == account) || (Convert.ToInt32(clientsOfBank[i].SumOnSecondAccountValue) < sumOfTransfer && clientsOfBank[i].SecondAccountValue == account)))
                        {
                            bank.handlerOfValidationMessage("Такой суммы нет на счете");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].FirstAccountValue == account)
                        {
                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.SumOnFirstAccountValue = (Convert.ToInt32(client.SumOnFirstAccountValue) - sumOfTransfer).ToString();
                                client.SumOnSecondAccountValue = (Convert.ToInt32(client.SumOnSecondAccountValue) + sumOfTransfer).ToString();
                                client.JournalOfClientActions += $"Клиент перевел со счета: {client.FirstAccountValue} сумму: {sum} на счет: {client.SecondAccountValue};\n";
                                bank.handlerOfInformationMessage($"Клиент перевел со счета: {client.FirstAccountValue} сумму: {sum} на счет: {client.SecondAccountValue}");
                                db.SaveChanges();
                                break;
                            }
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].SecondAccountValue == account)
                        {
                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.SumOnSecondAccountValue = (Convert.ToInt32(client.SumOnSecondAccountValue) - sumOfTransfer).ToString();
                                client.SumOnFirstAccountValue = (Convert.ToInt32(client.SumOnFirstAccountValue) + sumOfTransfer).ToString();
                                client.JournalOfClientActions += $"Клиент перевел со счета: {client.SecondAccountValue} сумму: {sum} на счет: {client.FirstAccountValue};\n";
                                bank.handlerOfInformationMessage($"Клиент перевел со счета: {client.SecondAccountValue} сумму: {sum} на счет: {client.FirstAccountValue}");
                                db.SaveChanges();
                                break;
                            }
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            bank.handlerOfValidationMessage("Введен неверный номер счета");
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод открытия вклада без капитализации
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        public void OpenDepositWithoutCap(Department department, ClientsOfBank chooseClient)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].DepositWithoutCapValue != "")
                        {
                            bank.handlerOfValidationMessage("Вклад без капитализации уже открыт");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            int account = r.Next(10_000_000, 99_999_999);
                            int sum = 100;
                            int month = 0;

                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.DepositWithoutCapValue = account.ToString();
                                client.SumDepositWithoutCapValue = sum.ToString();
                                client.MonthDepositWithoutCapValue = month.ToString();
                                client.JournalOfClientActions += $"Клиент открыл вклад без капитализации: {account} в размере: 100;\n";
                                bank.handlerOfInformationMessage($"Клиент {chooseClient.Name} открыл вклад без капитализации: {account} в размере: 100");
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод открытия вклада с капитализацией
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        public void OpenDepositWithCap(Department department, ClientsOfBank chooseClient)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].DepositWithCapValue != "")
                        {
                            bank.handlerOfValidationMessage("Вклад с капитализацией уже открыт");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            string account = Guid.NewGuid().ToString().Substring(0, 8);
                            int sum = 100;
                            int month = 0;

                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client != null)
                            {
                                client.DepositWithCapValue = account.ToString();
                                client.SumDepositWithCapValue = sum.ToString();
                                client.MonthDepositWithCapValue = month.ToString();
                                client.JournalOfClientActions += $"Клиент открыл вклад с капитализацией: {account} в размере: 100;\n";
                                bank.handlerOfInformationMessage($"Клиент {chooseClient.Name} открыл вклад с капитализацией: {account} в размере: 100");
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод перевода месяцев
        /// </summary>
        /// <param name="department"></param>
        /// <param name="month"></param>
        public void TransferMonths(Department department, string month)
        {
            if (department != null)
            {
                bool result = int.TryParse(month, out int numberOfMonth);

                if (result == false || numberOfMonth < 1 || numberOfMonth > 12)
                {
                    bank.handlerOfValidationMessage("Должно быть целое число от 1 до 12");
                    return;
                }
                else
                {
                    using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                    {
                        var clientsOfBank = db.ClientsOfBanks.ToList();

                        for (int i = 0; i < clientsOfBank.Count; i++)
                        {
                            if (clientsOfBank[i].SumDepositWithoutCapValue != "")
                            {
                                string name = clientsOfBank[i].Name;
                                var client = db.ClientsOfBanks.Where(ex => ex.Name == name).FirstOrDefault();

                                if (client != null)
                                {
                                    client.MonthDepositWithoutCapValue = (Convert.ToInt32(client.MonthDepositWithoutCapValue) + numberOfMonth).ToString();
                                }

                                if (Convert.ToInt32(client.MonthDepositWithoutCapValue) < 12)
                                {
                                    client.JournalOfClientActions += $"Состояние вклада без капитализации: {client.DepositWithoutCapValue} - месяц: {client.MonthDepositWithoutCapValue}, сумма: 100;\n";
                                }
                                else
                                {
                                    if (Convert.ToInt32(client.SumDepositWithoutCapValue) == 100)
                                    {
                                        client.SumDepositWithoutCapValue = (Convert.ToInt32(client.SumDepositWithoutCapValue) * 1.12).ToString();
                                        client.JournalOfClientActions += $"Состояние вклада без капитализации: {client.DepositWithoutCapValue} - месяц: 12, сумма: {client.SumDepositWithoutCapValue};\n";
                                    }
                                    client.MonthDepositWithoutCapValue = 12.ToString();
                                }
                                db.SaveChanges();
                            }
                            
                            if (clientsOfBank[i].SumDepositWithCapValue != "")
                            {
                                string name = clientsOfBank[i].Name;
                                var client = db.ClientsOfBanks.Where(ex => ex.Name == name).FirstOrDefault();

                                if (client != null)
                                {
                                    client.MonthDepositWithCapValue = (Convert.ToInt32(client.MonthDepositWithCapValue) + numberOfMonth).ToString();
                                }

                                if (client.InitialMonthDepositWithCap > 0)
                                {
                                    for (int j = 0; j < numberOfMonth && client.InitialMonthDepositWithCap > 0; j++)
                                    {
                                        client.SumDepositWithCapValue = (Convert.ToDouble(client.SumDepositWithCapValue) * 1.01).ToString();
                                        client.InitialMonthDepositWithCap -= 1;
                                    }

                                    if (Convert.ToInt32(client.MonthDepositWithCapValue) < 12)
                                    {
                                        client.JournalOfClientActions += $"Состояние вклада с капитализацией: {client.DepositWithCapValue} - месяц: {client.MonthDepositWithCapValue}, сумма: {client.SumDepositWithCapValue};\n";
                                    }
                                }

                                if (Convert.ToInt32(client.MonthDepositWithCapValue) >= 12)
                                {
                                    client.MonthDepositWithCapValue = 12.ToString();
                                    if (!client.JournalOfClientActions.Contains($"Состояние вклада с капитализацией: {client.DepositWithCapValue} - месяц: {client.MonthDepositWithCapValue}, сумма: {client.SumDepositWithCapValue};\n"))
                                    {
                                        client.JournalOfClientActions += $"Состояние вклада с капитализацией: {client.DepositWithCapValue} - месяц: {client.MonthDepositWithCapValue}, сумма: {client.SumDepositWithCapValue};\n";
                                    }
                                }
                                db.SaveChanges();
                            }

                            if (clientsOfBank[i].CreditValue != "")
                            {
                                string name = clientsOfBank[i].Name;
                                var client = db.ClientsOfBanks.Where(ex => ex.Name == name).FirstOrDefault();

                                if (client.InitialMonthCredit > 0)
                                {
                                    client.MonthCreditValue = (Convert.ToInt32(client.MonthCreditValue) + numberOfMonth).ToString();

                                    for (int j = 0; j < numberOfMonth && client.InitialMonthCredit > 0; j++)
                                    {
                                        client.CreditValue = (Convert.ToDouble(client.CreditValue) - (Convert.ToDouble(client.InitialSumCredit) / 12)).ToString();
                                        client.InitialMonthCredit -= 1;

                                        if (client.InitialMonthCredit == 0)
                                        {
                                            bank.handlerOfInformationMessage($"Клиент {client.Name} погасил задолженность по кредиту, ему доступна пониженная процентная ставка");
                                            client.JournalOfClientActions += "Кредит погашен;\n";
                                            client.PecentOfCreditValue = 0.ToString();
                                            client.CreditValue = "";
                                            client.MonthCreditValue = 0.ToString();
                                            client.isRedusedRate = true;
                                        }
                                    }
                                    if (client.InitialMonthCredit > 0)
                                    {
                                        client.JournalOfClientActions += $"Состояние выплаты кредита: месяц - {client.MonthCreditValue}, задолженность - {client.CreditValue};\n";
                                    }
                                }
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод выдачи кредита
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        public void GiveCredit(Department department, ClientsOfBank chooseClient)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name && clientsOfBank[i].CreditValue != "")
                        {
                            bank.handlerOfValidationMessage("Этому клиенту кредит уже выдан");
                            break;
                        }
                        else if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                            if (client.isRedusedRate == true)
                            {
                                client.RateOfCredit /= 2;
                                if (client.RateOfCredit < client.MinRate)
                                {
                                    client.RateOfCredit = client.MinRate;
                                }
                                client.PecentOfCreditValue = (client.RateOfCredit * 100).ToString();
                            }
                            else
                            {
                                client.PecentOfCreditValue = (client.RateOfCredit * 100).ToString();
                            }

                            client.CreditValue = (100 * (client.RateOfCredit + 1)).ToString();
                            client.JournalOfClientActions += $"Клиенту выдан кредит в размере: {client.CreditValue} под процент: {client.PecentOfCreditValue};\n";
                            client.InitialSumCredit = Convert.ToInt32(100 * (client.RateOfCredit + 1));
                            client.InitialMonthCredit = 12;
                            client.MonthCreditValue = 0.ToString();
                            bank.handlerOfInformationMessage($"Клиенту выдан кредит в размере: {client.CreditValue} под процент: {client.PecentOfCreditValue}");
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод добавления клиента отделу
        /// </summary>
        /// <param name="department"></param>
        public void AddClient(Department department)
        {
            if (department != null)
            {     
                Client newClient = null;

                if (department.DepartmentId == 1)
                {
                    newClient = ClientFactory.GetClient("Обычный клиент_", r.Next(18, 80), 1);
                    newClient = new StandardClient("Обычный клиент_", r.Next(18, 80), 1);
                }
                else if (department.DepartmentId == 2)
                {
                    newClient = ClientFactory.GetClient("VIP-клиент_", r.Next(18, 80), 2);
                    newClient = new VipClient("VIP-клиент_", r.Next(18, 80), 2);
                }
                else if (department.DepartmentId == 3)
                {
                    newClient = ClientFactory.GetClient("Юридичесткое лицо_", r.Next(18, 80), 3);
                    newClient = new LegalClient("Юридичесткое лицо_", r.Next(18, 80), 3);
                }

                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    client.Name = newClient.Name;
                    client.Age = newClient.Age;
                    client.DepartmentId = newClient.DepartamentId;
                    client.RateOfCredit = newClient.RateOfCredit;
                    client.MinRate = newClient.MinRate;
                    client.FirstAccountValue = newClient.FirstAccountValue;
                    client.SumOnFirstAccountValue = newClient.SumOnFirstAccountValue;
                    client.SecondAccountValue = newClient?.SecondAccountValue ?? "";
                    client.SumOnSecondAccountValue = newClient?.SumOnSecondAccountValue ?? "";
                    client.JournalOfClientActions = newClient.JournalOfClientActions;
                    client.DepositWithoutCapValue = "";
                    client.MonthDepositWithoutCapValue = "";
                    client.SumDepositWithoutCapValue = "";
                    client.DepositWithCapValue = "";
                    client.MonthDepositWithCapValue = "";
                    client.SumDepositWithCapValue = "";
                    client.CreditValue = "";
                    client.MonthCreditValue = "";
                    client.PecentOfCreditValue = "";
                    client.FirstBaseMessages = "";
                    client.SecondBaseMessages = "";
                    client.ThirdBaseMessages = "";
                    client.FourtnBaseMessages = "";
                    client.FifthBaseMessages = "";
                    client.SixthBaseMessages = "";
                    client.SeventhBaseMessages = "";
                    client.EighthBaseMessages = "";
                    client.NinthBaseMessages = "";
                    client.TenthBaseMessages = "";
                    client.InitialMonthDepositWithCap = 12;

                    db.ClientsOfBanks.Add(client);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Метод удаления клиента из отдела
        /// </summary>
        /// <param name="department"></param>
        /// <param name="chooseClient"></param>
        public void DeleteClient(Department department, ClientsOfBank chooseClient)
        {
            if (department != null && chooseClient != null)
            {
                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                    if (client != null)
                    {
                        db.ClientsOfBanks.Remove(client);
                        db.SaveChanges();
                    }
                }
            }
        }
        public async void CreateDataBaseMessages(Department department)
        {
            if (department != null)
            {
                if (File.Exists("DataBase/data1.txt") || File.Exists("DataBase/data1.csv"))
                {
                    bank.handlerOfValidationMessage("База данных сообщений уже создана");
                    return;
                }
                else if (isCreatingDataBaseMessages)
                {
                    bank.handlerOfValidationMessage("Новая база данных сообщений уже создается");
                    return;
                }

                isCreatingDataBaseMessages = true;

                DirectoryInfo dir = new DirectoryInfo(".");
                dir.CreateSubdirectory("DataBase");

                if (isCreateDataBaseMessagesCsv)
                {
                    var saveToCsv1 = new KeeperCsv("data1");
                    dataBaseMessagesWriter.Mode = saveToCsv1;

                    var t11 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(0));
                    await t11;

                    var saveToCsv2 = new KeeperCsv("data2");
                    dataBaseMessagesWriter.Mode = saveToCsv2;

                    var t12 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(1_000_000));
                    await t12;

                    var saveToCsv3 = new KeeperCsv("data3");
                    dataBaseMessagesWriter.Mode = saveToCsv3;

                    var t13 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(2_000_000));
                    await t13;

                    var saveToCsv4 = new KeeperCsv("data4");
                    dataBaseMessagesWriter.Mode = saveToCsv4;

                    var t14 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(3_000_000));
                    await t14;

                    var saveToCsv5 = new KeeperCsv("data5");
                    dataBaseMessagesWriter.Mode = saveToCsv5;

                    var t15 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(4_000_000));
                    await t15;

                    var saveToCsv6 = new KeeperCsv("data6");
                    dataBaseMessagesWriter.Mode = saveToCsv6;

                    var t16 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(5_000_000));
                    await t16;

                    var saveToCsv7 = new KeeperCsv("data7");
                    dataBaseMessagesWriter.Mode = saveToCsv7;

                    var t17 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(6_000_000));
                    await t17;

                    var saveToCsv8 = new KeeperCsv("data8");
                    dataBaseMessagesWriter.Mode = saveToCsv8;

                    var t18 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(7_000_000));
                    await t18;

                    var saveToCsv9 = new KeeperCsv("data9");
                    dataBaseMessagesWriter.Mode = saveToCsv9;

                    var t19 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(8_000_000));
                    await t19;

                    var saveToCsv10 = new KeeperCsv("data10");
                    dataBaseMessagesWriter.Mode = saveToCsv10;

                    var t20 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(9_000_000));
                    await t20;
                }
                else
                {
                    var saveToTxt1 = new KeeperTxt("data1");
                    dataBaseMessagesWriter.Mode = saveToTxt1;

                    var t1 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(0));
                    await t1;

                    var saveToTxt2 = new KeeperTxt("data2");
                    dataBaseMessagesWriter.Mode = saveToTxt2;

                    var t2 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(1_000_000));
                    await t2;

                    var saveToTxt3 = new KeeperTxt("data3");
                    dataBaseMessagesWriter.Mode = saveToTxt3;

                    var t3 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(2_000_000));
                    await t3;

                    var saveToTxt4 = new KeeperTxt("data4");
                    dataBaseMessagesWriter.Mode = saveToTxt4;

                    var t4 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(3_000_000));
                    await t4;

                    var saveToTxt5 = new KeeperTxt("data5");
                    dataBaseMessagesWriter.Mode = saveToTxt5;

                    var t5 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(4_000_000));
                    await t5;

                    var saveToTxt6 = new KeeperTxt("data6");
                    dataBaseMessagesWriter.Mode = saveToTxt6;

                    var t6 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(5_000_000));
                    await t6;

                    var saveToTxt7 = new KeeperTxt("data7");
                    dataBaseMessagesWriter.Mode = saveToTxt7;

                    var t7 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(6_000_000));
                    await t7;

                    var saveToTxt8 = new KeeperTxt("data8");
                    dataBaseMessagesWriter.Mode = saveToTxt8;

                    var t8 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(7_000_000));
                    await t8;

                    var saveToTxt9 = new KeeperTxt("data9");
                    dataBaseMessagesWriter.Mode = saveToTxt9;

                    var t9 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(8_000_000));
                    await t9;

                    var saveToTxt10 = new KeeperTxt("data10");
                    dataBaseMessagesWriter.Mode = saveToTxt10;

                    var t10 = Task.Factory.StartNew(() => dataBaseMessagesWriter.Save(9_000_000));
                    await t10;
                }
                bank.handlerOfInformationMessage("База данных сообщений создана в папке DataBase");
                isCreatingDataBaseMessages = false;
            }
        }

        public void ChangePropertyIsCreateDataBaseMessagesCsv()
        {
            isCreateDataBaseMessagesCsv = true;
        }

        public async void AddDataBaseMessagesToClient(Department department, ClientsOfBank chooseClient)
        {
            if (chooseClient != null && department != null)
            {
                DirectoryInfo dir = new DirectoryInfo("DataBase");
                if (!dir.Exists)
                {
                    bank.handlerOfValidationMessage("База данных не создана");
                    return;
                }

                Task<string> task11 = default;

                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();
                    var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (isCreatingDataBaseMessages)
                        {
                            bank.handlerOfValidationMessage("База данных создается дождитесь завершения загрузки");
                            break;
                        }

                        if (isAddingDataBaseMessages)
                        {
                            bank.handlerOfValidationMessage("База данных добавляется клиенту дождитесь завершения загрузки");
                            break;
                        }

                        if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            if (client.isCreatedDataDase != true)
                            {
                                isAddingDataBaseMessages = true;

                                Task<string> task1 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data1"));
                                Task<string> task2 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data2"));
                                Task<string> task3 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data3"));
                                Task<string> task4 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data4"));
                                Task<string> task5 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data5"));
                                Task<string> task6 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data6"));
                                Task<string> task7 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data7"));
                                Task<string> task8 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data8"));
                                Task<string> task9 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data9"));
                                Task<string> task10 = Task<string>.Factory.StartNew(() => ReadDataFromFile("data10"));

                                client.FirstBaseMessages = await task1;
                                client.SecondBaseMessages = await task2;
                                client.ThirdBaseMessages = await task3;
                                client.FourtnBaseMessages = await task4;
                                client.FifthBaseMessages = await task5;
                                client.SixthBaseMessages = await task6;
                                client.SeventhBaseMessages = await task7;
                                client.EighthBaseMessages = await task8;
                                client.NinthBaseMessages = await task9;
                                client.TenthBaseMessages = await task10;

                                bank.handlerOfInformationMessage($"База данных добавлена клиенту {client.Name}");
                                client.isCreatedDataDase = true;
                            }
                            string[] array = client.JournalOfClientActions.Split('\n');

                            if (array.Length > 100)
                            {
                                client.JournalOfClientActions = null;

                                for (int j = 0; j < array.Length - 101; j++)
                                {
                                    client.JournalOfClientActions += $"{array[j]}\n";
                                }
                            }

                            if (NavigationNumber >= 0 && NavigationNumber < 10000)
                            {
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.FirstBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 10000 && NavigationNumber < 20000)
                            {
                                NavigationNumber = NavigationNumber - 10000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.SecondBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 20000 && NavigationNumber < 30000)
                            {
                                NavigationNumber = NavigationNumber - 20000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.ThirdBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 30000 && NavigationNumber < 40000)
                            {
                                NavigationNumber = NavigationNumber - 30000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.FourtnBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 40000 && NavigationNumber < 50000)
                            {
                                NavigationNumber = NavigationNumber - 40000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.FifthBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 50000 && NavigationNumber < 60000)
                            {
                                NavigationNumber = NavigationNumber - 50000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.SixthBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 60000 && NavigationNumber < 70000)
                            {
                                NavigationNumber = NavigationNumber - 60000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.SeventhBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 70000 && NavigationNumber < 80000)
                            {
                                NavigationNumber = NavigationNumber - 70000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.EighthBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 80000 && NavigationNumber < 90000)
                            {
                                NavigationNumber = NavigationNumber - 80000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.NinthBaseMessages, NavigationNumber));
                            }
                            else if (NavigationNumber >= 90000 && NavigationNumber < 100000)
                            {
                                NavigationNumber = NavigationNumber - 90000;
                                task11 = Task<string>.Factory.StartNew(() => GetChunkOfDataBase(client.TenthBaseMessages, NavigationNumber));
                            }

                            client.JournalOfClientActions += await task11;

                            isAddingDataBaseMessages = false;

                            db.SaveChanges();                        
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод считывает данные из файла
        /// </summary>
        /// <param name="nameOfFile"></param>
        /// <returns></returns>
        private string ReadDataFromFile(string nameOfFile)
        {
            string pathToFile = null;

            if (File.Exists("DataBase/data1.txt"))
            {
                pathToFile = "DataBase/" + nameOfFile + ".txt";
            }
            else if (File.Exists("DataBase/data1.csv"))
            {
                pathToFile = "DataBase/" + nameOfFile + ".csv";
            }

            string data = default;

            using (StreamReader sr = new StreamReader(pathToFile))
            {
                data = sr.ReadToEnd();
            }
            return data;
        }
        /// <summary>
        /// Метод возвращает часть данных базы сообщений для отображения их TextBox
        /// </summary>
        /// <param name="partOfMessageBase"></param>
        /// <param name="numberOfNavigation"></param>
        /// <returns></returns>
        private static string GetChunkOfDataBase(string partOfMessageBase, int numberOfNavigation)
        {
            string[] array = partOfMessageBase.Split('\n');

            string chunk = default;

            int start = numberOfNavigation * 100;
            int limit = start + 100;

            for (int i = start; i < limit; i++)
            {
                chunk += $"{array[i]}\n";
            }

            return chunk;
        }

        public bool NavigationOnDataBase(Department department, ClientsOfBank chooseClient, string number)
        {
            if (chooseClient != null && department != null)
            {
                bool result = int.TryParse(number, out int numberOfNavigation);

                using (MSSQLLocalEntities db = new MSSQLLocalEntities())
                {
                    var clientsOfBank = db.ClientsOfBanks.ToList();
                    var client = db.ClientsOfBanks.Where(ex => ex.Name == chooseClient.Name).FirstOrDefault();

                    for (int i = 0; i < clientsOfBank.Count; i++)
                    {
                        if (clientsOfBank[i].Name == chooseClient.Name)
                        {
                            if (client.isCreatedDataDase != true)
                            {
                                bank.handlerOfValidationMessage($"База данных не добавлена клиенту {bank.ClientsDb[i].Name}");
                                return false;
                            }
                            else if (result == false || numberOfNavigation < 0 || numberOfNavigation > 99999)
                            {
                                bank.handlerOfValidationMessage("Должно быть целое число от 0 до 99999");
                                return false;
                            }
                            else
                            {
                                NavigationNumber = numberOfNavigation;
                                return true;
                            }
                        }                       
                    }      
                }           
            }
            return false;
        }
    }
}
