using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    /// <summary>
    /// 火车信息
    /// </summary>
    public class Train
    {
        private int seatCount;
        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount
        {
            get { return seatCount; }
            set { seatCount = value; }
        }
    }
}
