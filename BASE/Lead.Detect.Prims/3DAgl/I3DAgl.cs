using System;
using System.Collections.Generic;
using CommonStruct.Communicate;
using CommonStruct.LC3D;

namespace Lead.Detect.Prim3DAgl
{
    public interface I3DAgl
    {

        List<List<List<PointFS>>> fsPointListD { set; get; }
        int LoadTask(ref string FileName);
        OutputPrimData TaskStart(bool StartCount, Int32 Index, ref List<List<PointFS>> fsPointListD1);
        void RunTaskLine(OutputPrimData fsPointList);
    }
}
