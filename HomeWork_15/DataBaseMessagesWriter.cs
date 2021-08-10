using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    public class DataBaseMessagesWriter
    {
        public IDataMessageSave Mode { get; set; }

        public void Save(int start)
        {
            Mode.DataMessageSave(start);
        }
    }
}
