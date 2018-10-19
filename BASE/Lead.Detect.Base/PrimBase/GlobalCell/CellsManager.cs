using System;
using System.Collections.Generic;

namespace Lead.Detect.Base.GlobalCell
{
    public class CellsManager
    {
        private CellsManager()
        {
        }

        public static CellsManager Instance { get; } = new CellsManager();

        public List<ICell> Cells { get; } = new List<ICell>();

        public ICell GetDefCellByGuid(Guid id)
        {
            foreach (var item in Cells)
                if (item.CellID == id)
                    return item;

            return null;
        }
    }
}