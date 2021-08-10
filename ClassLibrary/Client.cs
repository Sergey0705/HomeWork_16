using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class Client : IComparable<Client>
    {
        // Реализация перегрузки операции сравнения
        public int CompareTo(Client other)
        {
            if (Convert.ToInt32(this.SumOnFirstAccountValue) > Convert.ToInt32(other.SumOnFirstAccountValue))
            {
                return 1;
            }
            else if (Convert.ToInt32(this.SumOnFirstAccountValue) < Convert.ToInt32(other.SumOnFirstAccountValue))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public static bool operator < (Client c1, Client c2) => c1.CompareTo(c2) < 0;
        public static bool operator > (Client c1, Client c2) => c1.CompareTo(c2) > 0;
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Id отдела
        /// </summary>
        public int DepartamentId { get; set; }
        /// <summary>
        /// Ставка кредита
        /// </summary>
        public double RateOfCredit { get; set; }
        /// <summary>
        /// Минимальная ставка кредита
        /// </summary>
        public double MinRate { get; set; }
        /// <summary>
        /// Счета клиента
        /// </summary>
        public List<int> AccountsClient { get; set; }
        /// <summary>
        /// Суммы на счетах клиента
        /// </summary>
        public List<int> SumsOnAccounts { get; set; }
        /// <summary>
        /// Вклад с капитализацией
        /// </summary>
        public Repository<string> DepositWithCap { get; set; }
        /// <summary>
        /// Вклад без капитализации
        /// </summary>
        public Repository<int> DepositWithoutCap { get; set; }
        /// <summary>
        /// Сумма вклада без капитализации
        /// </summary>
        public Repository<double> SumDepositWithoutCap { get; set; }
        /// <summary>
        /// Начальная сумма вклада без капитализации
        /// </summary>
        public Repository<double> InitialSumDepositWithoutCap { get; set; }
        /// <summary>
        /// Сумма вклада с капитализацией
        /// </summary>
        public Repository<double> SumDepositWithCap { get; set; }
        /// <summary>
        /// Текущий месяц вклада без капитализации
        /// </summary>
        public Repository<int> MonthDepositWithoutCap { get; set; }
        /// <summary>
        /// Текущий месяц вклада c капитализацией
        /// </summary>
        public Repository<int> MonthDepositWithCap { get; set; }
        /// <summary>
        /// Текущий месяц кредита
        /// </summary>
        public Repository<int> MonthCredit { get; set; }
        /// <summary>
        /// Срок вклада с капитализацией в месяцах
        /// </summary>
        public Repository<int> InitialMonthDepositWithCap { get; set; }
        /// <summary>
        /// Срок кредита в месяцах
        /// </summary>
        public Repository<int> InitialMonthCredit { get; set; }
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public Repository<double> Credit { get; set; }
        /// <summary>
        /// Начальная сумма кредита
        /// </summary>
        public Repository<double> InitialSumCredit { get; set; }
        /// <summary>
        /// Процент кредита
        /// </summary>
        public Repository<int> PecentOfCredit { get; set; }
        /// <summary>
        /// Флаг пониженной ставки
        /// </summary>
        public Repository<bool> isRedusedRate { get; set; }
        /// <summary>
        /// Общая база счетов
        /// </summary>
        public static List<int> AccountsDb;

        public bool isCreatedDataDase { get; set; }
        public string FirstBaseMessages { get; set; }
        public string SecondBaseMessages { get; set; }
        public string ThirdBaseMessages { get; set; }
        public string FourtnBaseMessages { get; set; }
        public string FifthBaseMessages { get; set; }
        public string SixthBaseMessages { get; set; }
        public string SeventhBaseMessages { get; set; }
        public string EighthBaseMessages { get; set; }
        public string NinthBaseMessages { get; set; }
        public string TenthBaseMessages { get; set; }
        public string JournalOfClientActions { get; set; }
        public string SecondAccountValue { get; set; }
        public string FirstAccountValue { get; set; }
        public string SumOnFirstAccountValue { get; set; }
        public string SumOnSecondAccountValue { get; set; }
        public string DepositWithoutCapValue { get; set; }
        public string MonthDepositWithoutCapValue { get; set; }
        public string SumDepositWithoutCapValue { get; set; }
        public string DepositWithCapValue { get; set; }
        public string MonthDepositWithCapValue { get; set; }
        public string SumDepositWithCapValue { get; set; }
        public string CreditValue { get; set; }
        public string MonthCreditValue { get; set; }
        public string PecentOfCreditValue { get; set; }
        /// <summary>
        /// Общий id клиентов
        /// </summary>
        static int id { get; set; }

        static Random r;

        static Client()
        {
            id = 0;
            AccountsDb = new List<int>();
            r = new Random();
        }

        public Client()
        {
         
        }

        public Client(string name, int age, int departmentId)
        {
            id = ++id;
            Name = name + id;
            DepartamentId = departmentId;
            Age = age;
            int countOfAccount = r.Next(1, 3);
            AccountsClient = new List<int>();
            DepositWithCap = new Repository<string>();
            DepositWithoutCap = new Repository<int>();
            SumDepositWithoutCap = new Repository<double>();
            InitialSumDepositWithoutCap = new Repository<double>();
            SumDepositWithCap = new Repository<double>();
            MonthDepositWithoutCap = new Repository<int>();
            MonthDepositWithCap = new Repository<int>();
            InitialMonthDepositWithCap = new Repository<int>();
            Credit = new Repository<double>();
            InitialSumCredit = new Repository<double>();
            PecentOfCredit = new Repository<int>();
            MonthCredit = new Repository<int>();
            InitialMonthCredit = new Repository<int>();
            isRedusedRate = new Repository<bool>();
            SumsOnAccounts = new List<int>();
            JournalOfClientActions += $"Журнал действий клиента {Name}:\n";


            for (int i = 0; i < countOfAccount; i++)
            {
                int sum = r.Next(0, 1000_000);
                int accountClient = r.Next(100_000_000, 999_999_999);

                if (!AccountsDb.Contains(accountClient))
                {
                    AccountsDb.Add(accountClient);
                    AccountsClient.Add(accountClient);
                    SumsOnAccounts.Add(sum);
                    JournalOfClientActions += $"Клиент открыл счет: {accountClient} с суммой: {sum};\n";
                }
                else
                {
                    i--;
                }
            }

            if (AccountsClient.Count > 1)
            {
                SecondAccountValue = AccountsClient[1].ToString();
                FirstAccountValue = AccountsClient[0].ToString();
            }
            else if (AccountsClient.Count > 0)
            {
                FirstAccountValue = AccountsClient[0].ToString();
            }

            if (SumsOnAccounts.Count > 1)
            {
                SumOnSecondAccountValue = SumsOnAccounts[1].ToString();
                SumOnFirstAccountValue = SumsOnAccounts[0].ToString();
            }
            else if (SumsOnAccounts.Count > 0)
            {
                SumOnFirstAccountValue = SumsOnAccounts[0].ToString();
            }

            DepositWithoutCapValue = default;
            MonthDepositWithoutCapValue = default;
            SumDepositWithoutCapValue = default;
            DepositWithCapValue = default;
            MonthDepositWithCapValue = default;
            SumDepositWithCapValue = default;
            CreditValue = default;
            MonthCreditValue = default;
            PecentOfCreditValue = default;
            FirstBaseMessages = default;
            SecondBaseMessages = default;
            ThirdBaseMessages = default;
            FourtnBaseMessages = default;
            FifthBaseMessages = default;
            SixthBaseMessages = default;
            SeventhBaseMessages = default;
            EighthBaseMessages = default;
            NinthBaseMessages = default;
            TenthBaseMessages = default;
            isCreatedDataDase = default;
        }
    }
}
