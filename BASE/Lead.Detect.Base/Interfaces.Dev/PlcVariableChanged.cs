namespace Lead.Detect.Interfaces.Dev
{
    public delegate int PlcVariableChanged(string valName, string valAddr, string valType, object valLast, object valCur);
}