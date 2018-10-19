using System.Xml;

namespace Lead.Detect.Base.GlobalCell
{
    public class CellsFactory
    {
        private CellsFactory()
        {
        }

        public static CellsFactory Instance { get; } = new CellsFactory();


        public ICell CreateCell()
        {
            return new Cell();
            ;
        }

        public ICell CreateCell(XmlNode cellConfigXml)
        {
            return new Cell(cellConfigXml);
        }
    }
}