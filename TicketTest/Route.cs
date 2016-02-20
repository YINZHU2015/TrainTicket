using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest
{
    public class Route
    {
        private string routeCode;

        public string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }

        private List<Station> stations;

        private Train train;

        public Train Train
        {
            get { return train; }
            set { train = value; }
        }

        public List<Station> Stations
        {
            get { return stations; }
            set { stations = value; }
        }

        /// <summary>
        /// 计算出行程经过的路段 如 S1 到 S3  产生 S1_S2 S2_S3 两个区段信息
        /// </summary>
        /// <param name="startStation"></param>
        /// <param name="endStation"></param>
        /// <returns></returns>
        public List<string> CaculateRoute(Station startStation, Station endStation)
        {
            bool hasStart = false;
            bool hasEnd = false;

            List<string> routeStations = new List<string>();

            Station lastStation = null;

            foreach (Station station in stations)
            {
                if (station.Code == startStation.Code)
                {
                    hasStart = true;
                }
 
                if (!hasStart || hasEnd)
                {
                    lastStation = station;
                    continue;
                }

                if (lastStation != null && station.Code != startStation.Code)
                {
                    string areaCode = lastStation.Code + "-" + station.Code;
                    routeStations.Add(areaCode);
                }
 
                if (station.Code == endStation.Code)
                {
                    hasEnd = true;
                }

                lastStation = station;
            }

            return routeStations;
        }
    }
}
