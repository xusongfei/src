namespace Lead.Detect.Base.GlobalStation
{
    public class StationFactory
    {
        private StationFactory()
        {
        }

        public static StationFactory Instance { get; } = new StationFactory();

        public IStation Create(string station)
        {
            IStation s = null;

            switch (station)
            {
                default:
                    //s = new Machine.Machine();
                    break;
            }

            return s;
        }

        //public IStation Create(StationType type, XmlNode xmlNode) //创建界面的同时把攻占绑定的信息同步更新进去
        //{
        //    IStation station = null;
        //    switch (type)
        //    {
        //        case StationType.Other:
        //            station = new Machine.Machine(xmlNode);
        //            break;
        //    }

        //    return station;
        //}
    }
}