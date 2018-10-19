using System;
using Lead.Detect.Helper;

namespace Lead.Detect.FrameworkExtension.common
{
    public abstract class UserSettings<T> where T : class
    {
        private static object obj = new object();
        public static T Load(string file)
        {
            lock (obj)
            {
                return XmlSerializerHelper.ReadXML(file, typeof(T)) as T;
            }
        }

        public static T Load(string file, Type t)
        {
            lock (obj)
            {
                return XmlSerializerHelper.ReadXML(file, t) as T;
            }
        }

        public virtual void Save(string environment)
        {
            XmlSerializerHelper.WriteXML(this, environment, this.GetType());
        }

        public virtual void SaveAs(string environment)
        {
            XmlSerializerHelper.WriteXML(this, environment, typeof(T));
        }


        public abstract bool CheckIfNormal();
    }
}