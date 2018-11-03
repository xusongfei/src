using System;

namespace Lead.Detect.Interfaces.Dev
{
    public class DataEnumInfo
    {
        public static DataKeep GetDataKeepByStr(string str)
        {
            DataKeep keep;
            if (str != null)
            {
                if (str == "可保持")
                {
                    keep = DataKeep.Enable;
                    return keep;
                }

                if (str == "不保持")
                {
                    keep = DataKeep.UnEnable;
                    return keep;
                }
            }

            keep = DataKeep.Other;
            return keep;
        }

        public static DataRWProp GetDataRWPropByStr(string str)
        {
            DataRWProp prop;
            if (str != null)
            {
                if (str == "可读写")
                {
                    prop = DataRWProp.ReadWrite;
                    return prop;
                }

                if (str == "只读")
                {
                    prop = DataRWProp.ReadOnly;
                    return prop;
                }

                if (str == "只写")
                {
                    prop = DataRWProp.WriteOnly;
                    return prop;
                }
            }

            prop = DataRWProp.Other;
            return prop;
        }

        public static DataType GetDataTypeByStr(string str)
        {
            DataType type;
            switch (str)
            {
                case "Bool":
                    type = DataType.BOOL;
                    return type;
                case "Byte":
                    type = DataType.BYTE;
                    return type;
                case "Short":
                    type = DataType.SHORT;
                    return type;
                case "UShort":
                    type = DataType.USHORT;
                    return type;
                case "Int":
                    type = DataType.INT;
                    return type;
                case "UInt":
                    type = DataType.UINT;
                    return type;
                case "Float":
                    type = DataType.FLOAT;
                    return type;
                case "Double":
                    type = DataType.DOUBLE;
                    return type;
            }

            type = DataType.Other;
            return type;
        }

        public static DataUse GetDataUse(bool use)
        {
            DataUse useState;
            if (use)
                useState = DataUse.Enable;
            else
                useState = DataUse.UnEnable;
            return useState;
        }

        public static string GetStrByDataKeep(DataKeep keep)
        {
            string str;
            switch (keep)
            {
                case DataKeep.Enable:
                    str = "可保持";
                    break;
                case DataKeep.UnEnable:
                    str = "不保持";
                    break;
                default:
                    str = "";
                    break;
            }

            return str;
        }

        public static string GetStrByDataRWProp(DataRWProp prop)
        {
            string str;
            switch (prop)
            {
                case DataRWProp.ReadWrite:
                    str = "可读写";
                    break;
                case DataRWProp.ReadOnly:
                    str = "只读";
                    break;
                case DataRWProp.WriteOnly:
                    str = "只写";
                    break;
                default:
                    str = "";
                    break;
            }

            return str;
        }

        public static string GetStrByDataType(DataType type)
        {
            string str;
            switch (type)
            {
                case DataType.BOOL:
                    str = "Bool";
                    return str;
                case DataType.BYTE:
                    str = "Byte";
                    return str;
                case DataType.INT:
                    str = "Int";
                    return str;
                case DataType.UINT:
                    str = "UInt";
                    return str;
                case DataType.FLOAT:
                    str = "Float";
                    return str;
                case DataType.DOUBLE:
                    str = "Double";
                    return str;
                case DataType.SHORT:
                    str = "Short";
                    return str;
                case DataType.USHORT:
                    str = "UShort";
                    return str;
            }

            str = "";
            return str;
        }

        public static string GetStrByTCType(TCType type)
        {
            string str;
            switch (type)
            {
                case TCType.Trigger:
                    str = "触发获取";
                    break;
                case TCType.Cycle:
                    str = "循环获取";
                    break;
                default:
                    str = "";
                    break;
            }

            return str;
        }

        public static TCType GetTCTypeByStr(string str)
        {
            TCType type;
            if (str != null)
            {
                if (str == "循环获取")
                {
                    type = TCType.Cycle;
                    return type;
                }

                if (str == "触发获取")
                {
                    type = TCType.Trigger;
                    return type;
                }
            }

            type = TCType.Other;
            return type;
        }

        public static Type GetTypeByDataType(DataType dataType)
        {
            var type = typeof(bool);
            switch (dataType)
            {
                case DataType.BOOL:
                    type = typeof(bool);
                    break;
                case DataType.BYTE:
                    type = typeof(byte);
                    break;
                case DataType.INT:
                    type = typeof(int);
                    break;
                case DataType.UINT:
                    type = typeof(uint);
                    break;
                case DataType.FLOAT:
                    type = typeof(float);
                    break;
                case DataType.DOUBLE:
                    type = typeof(double);
                    break;
                case DataType.SHORT:
                    type = typeof(short);
                    break;
                case DataType.USHORT:
                    type = typeof(ushort);
                    break;
            }

            return type;
        }
    }
}