using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonStruct.Communicate;
using CommonStruct.LC3D;

namespace Lead.Detect.Interfaces
{
    public interface I3DAgl
    {

        List<List<List<PointFS>>> fsPointListD { set; get; }
        int LoadTask(ref string FileName);
        OutputPrimData TaskStart(bool StartCount, Int32 Index, ref List<List<PointFS>> fsPointListD1);
        void RunTaskLine(OutputPrimData fsPointList);
    }
}
