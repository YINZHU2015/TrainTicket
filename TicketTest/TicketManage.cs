using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{

    /// <summary>
    /// 票据管理类
    /// </summary>
    public class TicketManage
    {

        private List<Route> routes;
        /// <summary>
        /// 所有线路
        /// </summary>
        public List<Route> Routes
        {
            get { return routes; }
            set { routes = value; }
        }

        private List<Ticket> _saleTickets;
        /// <summary>
        /// 所有已售出票据
        /// </summary>
        public List<Ticket> SaleTickets
        {
            get { return _saleTickets; }
            set { _saleTickets = value; }
        }

        private static List<StandbyTicket> standbyTicketCache =  new List<StandbyTicket>();

        public TicketManage(List<Route> routes)
        {
            this.routes = routes;
            this._saleTickets = new List<Ticket>();
        }

        /// <summary>
        /// 快速创建站点
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public static Station BuildStation(string stationCode,int id)
        {
            return new Station() { Name = stationCode, Code = stationCode,Id=id };
        }

        /// <summary>
        /// 快速构建列车
        /// </summary>
        /// <param name="seatCount"></param>
        /// <returns></returns>
        public static Train BuildTrain(int seatCount)
        {
            return new Train() { SeatCount = seatCount };
        }
        
        /// <summary>
        /// 查询余票数量
        /// </summary>
        /// <param name="startStation"></param>
        /// <param name="endStation"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public int QueryLeftTicketCount(Station startStation, Station endStation, Route route)
        {

            #region OLD算法
            //查出该路段总共销售了多少票
            //List<Ticket> saleRouteTickets = _saleTickets.FindAll(p => (p.Route.RouteCode.Equals(route.RouteCode)));

            ////查询所有票上售出多少路段
            //List<HashSet<string>> saleRouteAreas = new List<HashSet<string>>();

            //foreach (Ticket saleRouteTicket in saleRouteTickets)
            //{
            //    saleRouteAreas.Add(saleRouteTicket.RouteArea);
            //}

            ////计算出查询区段所有路段
            //List<string> routeStations = route.CaculateRoute(startStation, endStation);

            ////售出的票按照最多的一个路段总数算
            //int salecount = 0;

            //for (int i = 0; i < routeStations.Count; i++)
            //{
            //    salecount = Math.Max(salecount, saleRouteAreas.Count(p => (p.Contains(routeStations[i]))));
            //}

            //int maxticket = route.Train.SeatCount;

            ////余票为最大售票数减去已售出的数据
            //int leftTicket =  maxticket - salecount;

            //Console.WriteLine(string.Format("{0}车次{1}开往{2}余票：{3}张", route.RouteCode,startStation.Code,endStation.Code,leftTicket));

            //return leftTicket;
            #endregion

            return NewQueryLeftTicketCount(startStation,endStation,route);
        }

        public int NewQueryLeftTicketCount(Station startStation, Station endStation, Route route)
        {
            //如果在0票的区间
            if(standbyTicketCache !=null && standbyTicketCache.Count>0)
            {
                int count = standbyTicketCache.Count(m=>(m.StartStation.Id <= startStation.Id && startStation.Id < m.EndStation.Id) || (m.StartStation.Id < endStation.Id && endStation.Id <= m.EndStation.Id) || (m.StartStation.Id == startStation.Id && m.EndStation.Id == endStation.Id));
                if(count > 0)
                {
                    return 0;
                }
            }
             //查出该路段总共销售了多少票
            List<Ticket> saleRouteTickets = _saleTickets.FindAll(p => (p.Route.RouteCode.Equals(route.RouteCode)));
            int maxticket = route.Train.SeatCount;
            int leftTicket=0;
            if (saleRouteTickets != null && saleRouteTickets.Count > 0)
            {
                int salecount = saleRouteTickets.Count(m=> (m.StartStation.Id <= startStation.Id && startStation.Id < m.EndStation.Id) || (m.StartStation.Id < endStation.Id && endStation.Id <= m.EndStation.Id) || (m.StartStation.Id == startStation.Id && m.EndStation.Id == endStation.Id));
                leftTicket = maxticket - salecount;
            }
            else
            {
                leftTicket= maxticket;
            }
            //余票为最大售票数减去已售出的数据
            Console.WriteLine(string.Format("{0}车次{1}开往{2}余票：{3}张", route.RouteCode, startStation.Code, endStation.Code, leftTicket));
            if(leftTicket==0)
            {
                //可以缓存余票已经为0的结果。提高速度
                standbyTicketCache.Add(new StandbyTicket() {
                    StartStation = startStation,
                    EndStation = endStation,
                    LeftTicketCount = leftTicket
                });
            }
                
            return leftTicket;
        }

        /// <summary>
        /// 购票
        /// </summary>
        /// <param name="ticket"></param>
        private void buyTicket(Ticket ticket)
        {
            int leftTicketCount = QueryLeftTicketCount(ticket.StartStation, ticket.EndStation, ticket.Route);
            //余票不足不允许售票
            if (leftTicketCount <= 0)
            {
                throw new Exception("没有余票，无法购票");
            }

            ticket.CaculateRouteRouteArea();

            this._saleTickets.Add(ticket);
        }
        /// <summary>
        /// 售票
        /// </summary>
        /// <param name="startStationCode"></param>
        /// <param name="endStationCode"></param>
        /// <param name="route"></param>
        /// <param name="userID"></param>
        public void buyTicket(Station startStationCode, Station endStationCode, Route route, string userID)
        {
            Ticket ticket = new Ticket()
            {
                StartStation = startStationCode,
                EndStation = endStationCode,
                Route = route,
                UserId = userID
            };
            ticket.CaculateRouteRouteArea();
 
            buyTicket(ticket);

            Console.WriteLine(string.Format("用户“{0}”购买{3}车次{1}开往{2}车票一张", userID, startStationCode.Name, endStationCode.Name, route.RouteCode));
        }
    }
}
