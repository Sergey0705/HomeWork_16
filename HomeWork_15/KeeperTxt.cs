using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork_15
{
    class KeeperTxt : IDataMessageSave
    {
        private string nameOfFile;
        public KeeperTxt(string NameOfFile)
        {
            this.nameOfFile = NameOfFile;
        }

        public void DataMessageSave(int start)
        {
            int limit = start + 1_000_000;
            string pathToFile = "DataBase/" + this.nameOfFile + ".txt";

            using (StreamWriter sw = new StreamWriter(pathToFile))
            {
                for (int i = start + 1; i <= limit; i++)
                {
                    sw.Write($"№ {i} - {Guid.NewGuid().ToString().Substring(0, 10)}\n");
                }
            }
        }
    }
}
