using System.Collections.Generic;

namespace Lead.Detect.Interfaces.Dev
{
    public class DataArea : IDataArea
    {
        private readonly object _wobiLock = new object();

        public DataArea(string srcName)
        {
            DataSrcName = srcName;
        }

        public event DataAreaSingleObjRefresh OnDataAreaSingleObjRefresh;

        public event DataAreaSingleStringRefresh OnDataAreaSingleStringRefresh;

        public event DataAreaMultiObjRefresh OnDataAreaMultiObjRefresh;

        public event DataAreaMultiStringRefresh OnDataAreaMultiStringRefresh;

        public event DataStateChanged OnDataStateChanged;

        public event DataOpLog OnDataOpLog;

        public string DataSrcName { get; set; }

        public Dictionary<string, DataInfo> DataAreaList { get; set; } = new Dictionary<string, DataInfo>();

        public bool DataAreaUptateFlag { get; set; }

        public bool IAddDataInfo(DataInfo item)
        {
            var ret = true;
            lock (_wobiLock)
            {
                DataAreaList.Add(item.DataName, item);
            }

            return ret;
        }

        public bool IClearDataInfo()
        {
            var ret = true;
            lock (_wobiLock)
            {
                DataAreaList.Clear();
            }

            return ret;
        }

        public bool ISetDataVal(string name, object val)
        {
            var ret = true;
            bool result;
            if (!DataAreaList.ContainsKey(name))
            {
                result = false;
            }
            else
            {
                lock (_wobiLock)
                {
                    DataAreaList[name].DataVal = val;
                    DataAreaList[name].DataUpdateFlag = true;
                    DataAreaUptateFlag = true;
                }

                var singleObj = new DataObjInfo();
                singleObj.DataName = name;
                singleObj.Context = DataAreaList[name].DataVal;
                singleObj.Type = DataAreaList[name].DataType;
                if (OnDataAreaSingleObjRefresh != null) OnDataAreaSingleObjRefresh(singleObj);
                result = ret;
            }

            return result;
        }

        public DataInfo IGetDataInfo(string name)
        {
            DataInfo result;
            if (!DataAreaList.ContainsKey(name))
            {
                result = null;
            }
            else
            {
                var info = DataAreaList[name];
                result = info;
            }

            return result;
        }

        protected virtual void OnOnDataAreaSingleStringRefresh(DataStrInfo singlestr)
        {
            OnDataAreaSingleStringRefresh?.Invoke(singlestr);
        }

        protected virtual void OnOnDataAreaMultiObjRefresh(List<DataObjInfo> multiobj)
        {
            OnDataAreaMultiObjRefresh?.Invoke(multiobj);
        }

        protected virtual void OnOnDataAreaMultiStringRefresh(List<DataStrInfo> multistr)
        {
            OnDataAreaMultiStringRefresh?.Invoke(multistr);
        }

        protected virtual void OnOnDataStateChanged(string devname, string context)
        {
            OnDataStateChanged?.Invoke(devname, context);
        }

        protected virtual void OnOnDataOpLog(string devname, string log)
        {
            OnDataOpLog?.Invoke(devname, log);
        }
    }
}