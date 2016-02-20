using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    public class Ticket
    {
        private Station startStation;
        private Station endStation;
        private Route route;
        private string userID;
        private HashSet<string> routeArea;

        public HashSet<string> RouteArea
        {
            get { return routeArea; }
            set { routeArea = value; }
        }

        public Ticket()
        {
            this.routeArea = new HashSet<string>();
        }

        public string UserId
        {
            get { return userID; }
            set { userID = value; }
        }


        public Station StartStation
        {
            get { return startStation; }
            set { startStation = value; }
        }

        public Station EndStation
        {
            get { return endStation; }
            set { endStation = value; }
        }

        public Route Route
        {
            get { return route; }
            set { route = value; }
        }

        /// <summary>
        /// 重新计算车票区段信息
        /// </summary>
        public void CaculateRouteRouteArea()
        {
            this.routeArea = new HashSet<string>();

            List<string> routeAreas = this.route.CaculateRoute(this.startStation, this.endStation);

            foreach (string area in routeAreas)
            {
                this.routeArea.Add(area);
            }
        }
    }
}
