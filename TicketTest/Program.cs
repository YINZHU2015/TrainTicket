using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Route> routes = new List<Route>();

            //5个站
            var S1 = TicketManage.BuildStation("S1",1);
            var S2 = TicketManage.BuildStation("S2", 2);
            var S3 = TicketManage.BuildStation("S3", 3);
            var S4= TicketManage.BuildStation("S4", 4);
            var S5 = TicketManage.BuildStation("S5", 5);

            //车辆 100个座位
            var train = TicketManage.BuildTrain(100);

            Route route = new Route();
            route.RouteCode = "Z17";
            route.Stations = new List<Station>();
            route.Stations.Add(S1);
            route.Stations.Add(S2);
            route.Stations.Add(S3);
            route.Stations.Add(S4);
            route.Stations.Add(S5);
            route.Train = train;
            routes.Add(route);

            TicketManage ticketManage = new TicketManage(routes);
 
            ticketManage.buyTicket(S1, S3, route,Guid.NewGuid().ToString());

            ticketManage.buyTicket(S3, S4, route, Guid.NewGuid().ToString());

            ticketManage.buyTicket(S2, S4, route, Guid.NewGuid().ToString());

            ticketManage.QueryLeftTicketCount(S3,S5, route);

            ticketManage.QueryLeftTicketCount(S4, S5, route);

            ticketManage.QueryLeftTicketCount(S1, S2, route);
 
            Console.ReadKey();
        }
    }
}
