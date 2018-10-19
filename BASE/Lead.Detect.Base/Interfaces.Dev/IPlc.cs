namespace Lead.Detect.Interfaces.Dev
{
    public interface IPlc
    {
        event PlcVariableChanged OnPlcVariableChanged;

        //发送UDP指令  dataStr十六进制
        int WriteData(string posStr, int dataSet);

        int ReadData(string posStr, int readNum, out int[] dataRead);

        object ReadVariableFromName(string valName);
        object ReadVariableFromAddr(string valAddr);

        int WriteVariableFromName(string valName, object val);
        int WriteVariableFromAddr(string valAddr, object val);

        int RegVariableName(string valName);
        int RemoveRegVariableName(string valName);
        int ClearRegVariableName();

        int RegVariableAddr(string valAddr);
        int RemoveRegVariableAddr(string valAddr);
        int ClearRegVariableAddr();

        int SetDataChangedNotifyMode(DataChangedNotify mode);
    }
}