using System;
using System.Collections.Generic;

namespace Lead.Detect.Base.GlobalStation
{
    public class StationMgr
    {
        private StationMgr()
        {
        }

        public static StationMgr Instance { get; } = new StationMgr();

        public List<IStation> Stations { get; } = new List<IStation>();

        public IStation GetStationByGUID(Guid id)
        {
            var count = Stations.Count;
            IStation result = null;
            if (count < 0)
                return null;
            for (var i = 0; i < count; i++)
                if (Stations[i].StationId == id)
                {
                    result = Stations[i];
                    return result;
                }

            return null;
        }

        public int RemoveStationByGUID(Guid id)
        {
            var count = Stations.Count;
            int result;
            if (count < 0)
            {
                result = 0;
            }
            else
            {
                for (var i = 0; i < count; i++)
                    if (Stations[i].StationId == id)
                    {
                        Stations.RemoveAt(i);
                        result = 0;
                        return result;
                    }

                result = 0;
            }

            return result;
        }
    }
}