using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClassLibrary;

namespace HomeWork_15
{
    class Bank
    {
        /// <summary>
        /// Тип делегата
        /// </summary>
        /// <param name="msg"></param>
        public delegate void MessageHandler(string msg);
        /// <summary>
        /// Переменная сообщений валидации для делегата 
        /// </summary>
        public MessageHandler handlerOfValidationMessage;
        /// <summary>
        /// Переменная информационных сообщений для делегата 
        /// </summary>
        public MessageHandler handlerOfInformationMessage;

        static Random r;
        static Bank() { r = new Random(); }
        /// <summary>
        /// Список департаментов банка
        /// </summary>
        public List<Department> DepartmentsDb { get; set; }
        /// <summary>
        /// Список клиентов банка
        /// </summary>
        public List<Client> ClientsDb { get; set; }

        public Bank()
        {
            DepartmentsDb = new List<Department>();
            ClientsDb = new List<Client>();

            DepartmentsDb.Add(new Department("Отдел работы с обычными клиентами", 1));
            DepartmentsDb.Add(new Department("Отдел работы с VIP-клиентами", 2));
            DepartmentsDb.Add(new Department("Отдел работы с юридическими лицами", 3));

            int countStanardClient = r.Next(7, 11);

            for (int i = 0; i < countStanardClient; i++)
            {
                ClientsDb.Add(new StandardClient("Обычный клиент_", r.Next(18, 80), 1));
            }

            int countVipClient = r.Next(7, 11);

            for (int i = 0; i < countVipClient; i++)
            {
                ClientsDb.Add(new VipClient("VIP-клиент_", r.Next(18, 80), 2));
            }

            int countLegalClient = r.Next(7, 11);

            for (int i = 0; i < countLegalClient; i++)
            {
                ClientsDb.Add(new LegalClient("Юридичесткое лицо_", r.Next(18, 80), 3));
            }
        }
        /// <summary>
        /// Метод обработки сообщения валидации
        /// </summary>
        /// <param name="msg"></param>
        public static void OnValidationMessageEvent(string msg)
        {
            msg = "Сообщение валидации: " + msg;
            MessageBox.Show(msg);
        }
        /// <summary>
        /// Регистрационная функция для вызывающего кода
        /// </summary>
        /// <param name="methodToCall"></param>
        public void RegisterWithValidationMessage(MessageHandler methodToCall)
        {
            handlerOfValidationMessage = methodToCall;
        }
        /// <summary>
        /// Метод обработки информационного сообщения
        /// </summary>
        /// <param name="msg"></param>
        public static void OnInformationMessageEvent(string msg)
        {
            msg = "Информационное сообщение: " + msg;
            MessageBox.Show(msg);
        }
        /// <summary>
        /// Регистрационная функция для вызывающего кода
        /// </summary>
        /// <param name="methodToCall"></param>
        public void RegisterWithInformationMessage(MessageHandler methodToCall)
        {
            handlerOfInformationMessage = methodToCall;
        }
    }
}
