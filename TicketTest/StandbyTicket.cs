using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    public class StandbyTicket
    {
        private Station startStation;
        private Station endStation;
        private int leftTicketCount;

        /// <summary>
        /// 余票数量
        /// </summary>
        public int LeftTicketCount
        {
            get { return leftTicketCount; }
            set { leftTicketCount = value; }
        }

        /// <summary>
        /// 起始站
        /// </summary>
        public Station StartStation
        {
            get { return startStation; }
            set { startStation = value; }
        }

        /// <summary>
        /// 结束站
        /// </summary>
        public Station EndStation
        {
            get { return endStation; }
            set { endStation = value; }
        }
    }
}
