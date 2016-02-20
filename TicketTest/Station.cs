using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    public class Station
    {
        private string name;
        private string code;
        private int id;

        /// <summary>
        /// 站点ID
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
