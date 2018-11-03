using System.Collections.Generic;

namespace Lead.Detect.Interfaces.Dev
{
    public interface IDataArea
    {
        string DataSrcName { get; set; }

        Dictionary<string, DataInfo> DataAreaList { get; set; }

        bool DataAreaUptateFlag { get; set; }

        bool IAddDataInfo(DataInfo item);

        bool IClearDataInfo();

        DataInfo IGetDataInfo(string name);

        bool ISetDataVal(string name, object val);

        event DataAreaMultiObjRefresh OnDataAreaMultiObjRefresh;

        event DataAreaMultiStringRefresh OnDataAreaMultiStringRefresh;
        event DataAreaSingleObjRefresh OnDataAreaSingleObjRefresh;

        event DataAreaSingleStringRefresh OnDataAreaSingleStringRefresh;

        event DataOpLog OnDataOpLog;

        event DataStateChanged OnDataStateChanged;
    }
}