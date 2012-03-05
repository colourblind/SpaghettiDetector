using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace SpaghettiDetector.UI
{
    class YamlSerialiser
    {
        private object Object
        {
            get;
            set;
        }

        public YamlSerialiser(object o)
        {
            Object = o;
        }

        public void Run(TextWriter writer)
        {
            Serialise(writer, Object, 0);
        }

        private void Serialise(TextWriter writer, object o, int indent)
        {
            if (o is IEnumerable)
            {
                foreach (object element in (IEnumerable)o)
                {
                    writer.WriteLine("{0}-", new String(' ', indent * 2)); 
                    Serialise(writer, element, indent + 1);
                }
            }
            else
            {
                Type t = o.GetType();
                foreach (PropertyInfo p in t.GetProperties())
                {
                    if (p.PropertyType.IsValueType || p.PropertyType == typeof(string))
                        writer.WriteLine("{0}{1} : {2}", new String(' ', indent * 2), p.Name, p.GetValue(o, null));
                    else
                    {
                        writer.WriteLine("{0}{1} :", new String(' ', indent * 2), p.Name);
                        Serialise(writer, p.GetValue(o, null), indent + 1);
                    }
                }
            }
        }
    }
}
