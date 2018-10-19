using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lead.Detect.Helper
{
    public class XMLHelper
    {
        public static XmlNode Create(string nodeName)
        {
            var doc = new XmlDocument();
            XmlNode node = doc.CreateElement(nodeName);
            return node;
        }

        public static XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument doc = node.OwnerDocument;
                XmlAttribute attr = doc.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception err)
            {
                string desc = err.Message;
                return null;
            }
        }

        public static XmlNode ObjectToXML(object config)
        {
                var xnd = new XmlDocument();
                if (config != null)
                {
                    //we need the type to serialize
                    Type t = config.GetType();
                    var ser = new XmlSerializer(t);
                    //will hold the xml
                    using (var writer = new StringWriter(CultureInfo.InvariantCulture))
                    {
                        ser.Serialize(writer, config);
                        xnd.LoadXml(writer.ToString());
                        writer.Close();
                    }
                }

                return xnd.DocumentElement;
           
        }

        public static object XMLToObject(XmlNode node, Type objectType)
        {
            object convertedObject = null;
            if (node != null)
            {
                using (var reader = new StringReader(node.OuterXml))
                {
                    var ser = new XmlSerializer(objectType);
                    convertedObject = ser.Deserialize(reader);
                    reader.Close();
                }
            }

            return convertedObject;
        }
    }

    public class XmlSerializerHelper
    {
        /// <summary>
        ///     读取XML文件
        /// </summary>
        /// <param name="XmlFilePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ReadXML(string XmlFilePath, Type type)
        {
            if (!File.Exists(XmlFilePath))
            {
                throw new Exception($"File {XmlFilePath} not Exists");
            }

            using (var fs = new FileStream(XmlFilePath, FileMode.Open))
            {
                return new XmlSerializer(type).Deserialize(fs);
            }

        }

        /// <summary>
        ///     序列化XML文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool WriteXML(object data, string file, Type type)
        {
            using (var fs = new FileStream(file, FileMode.Create))
            {
                new XmlSerializer(type).Serialize(fs, data);
            }

            return true;
        }

        public static string XMLSerialize<T>(T entity)
        {
            var buffer = new StringBuilder();

            var serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, entity);
            }

            return buffer.ToString();
        }

        public static string ByteToString(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }

        public static byte[] StringToByte(string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        public static T DeXMLSerialize<T>(string xmlString)
        {
            T cloneObject = default(T);

            var buffer = new StringBuilder();
            buffer.Append(xmlString);

            var serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(buffer.ToString()))
            {
                Object obj = serializer.Deserialize(reader);
                cloneObject = (T)obj;
            }

            return cloneObject;
        }

        /// <summary>
        ///     把对象序列化为字符串
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
                return null;
            var _memory = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(_memory, pObj);
            _memory.Position = 0;
            var read = new byte[_memory.Length];
            _memory.Read(read, 0, read.Length);
            _memory.Close();
            return Compress(read);
        }

        /// <summary>
        ///     把字节反序列化成相应的对象
        /// </summary>
        /// <param name="pBytes">字节流</param>
        /// <returns>object</returns>
        public static object DeserializeObject(byte[] pBytes)
        {
            object _newOjb = null;
            if (pBytes == null)
                return _newOjb;
            var _memory = new MemoryStream(Decompress(pBytes));
            _memory.Position = 0;
            var formatter = new BinaryFormatter();
            _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return _newOjb;
        }

        /// <summary>
        ///     Write byte[] to file
        /// </summary>
        public static void WriteByteToFile(byte[] dataSource, string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Create);

            //将byte数组写入文件中
            fs.Write(dataSource, 0, dataSource.Length);

            fs.Close();
        }

        /// <summary>
        ///     Read byte from file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReadByteFromFile(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open);

            //获取文件大小
            long size = fs.Length;

            var array = new byte[size];

            //将文件读到byte数组中
            fs.Read(array, 0, array.Length);

            fs.Close();

            return array;
        }

        /// <summary>
        ///     Write object to file
        /// </summary>
        public static void WriteObjectToFile(object dataSource, string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Create);

            byte[] arraysource = SerializeObject(dataSource);
            //将byte数组写入文件中
            fs.Write(arraysource, 0, arraysource.Length);

            fs.Close();
        }

        /// <summary>
        ///     Read object from file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static object ReadObjectFromFile(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open);

            //获取文件大小
            long size = fs.Length;

            var array = new byte[size];

            //将文件读到byte数组中
            fs.Read(array, 0, array.Length);

            fs.Close();

            return DeserializeObject(array);
        }

        /// <summary>
        ///     字符串压缩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            try
            {
                var ms = new MemoryStream();
                var zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                var buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     字符串解压缩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                var ms = new MemoryStream(data);
                var zip = new GZipStream(ms, CompressionMode.Decompress, true);
                var msreader = new MemoryStream();
                var buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }

                    msreader.Write(buffer, 0, reader);
                }

                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     string 压缩
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);
            byte[] compressAfterByte = Compress(compressBeforeByte);
            //compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);  
            compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }

        /// <summary>
        ///     string 解压缩
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecompressString(string str)
        {
            string compressString = "";
            //byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);  
            byte[] compressBeforeByte = Convert.FromBase64String(str);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
            return compressString;
        }
    }
}