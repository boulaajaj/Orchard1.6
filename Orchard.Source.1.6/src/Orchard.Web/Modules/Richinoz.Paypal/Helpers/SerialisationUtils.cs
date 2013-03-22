using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using Orchard;

namespace Richinoz.Paypal.Helpers
{
    public interface ISerialisation:IDependency {
        string SerializeToXml<T>(T obj);
        T DeserializeFromXml<T>(string data) where T : class;
        T Deserialize<T>(string data) where T : class;
    }

    public class Serialisation:ISerialisation {
        public string SerializeToXml<T>(T obj) {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var serializer = new XmlSerializer(obj.GetType());

            serializer.Serialize(sw, obj);

            return sb.ToString();
        }

        public T DeserializeFromXml<T>(string data) where T : class {
            if (data == null) return null;

            var serializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(data))
            {
                using (var xmlTextReader = new XmlTextReader(stringReader))
                {
                    if (!serializer.CanDeserialize(xmlTextReader))
                        throw new ApplicationException(@"Cannot deserialize xml string using " + typeof(T).FullName);

                    return (T)serializer.Deserialize(xmlTextReader);
                }
            }
        }

        public T Deserialize<T>(string data) where T : class {
            return data == null ? null : (T)new LosFormatter().Deserialize(data);
        }
    }

    //public static class SerialisationUtils {
    //    private static ISerialisation _instance;

    //    public static string SerializeToXml<T>(T obj)
    //    {
    //        DepdendencyResolver 
    //        //var sb = new StringBuilder();
    //        //var sw = new StringWriter(sb);
    //        //var serializer = new XmlSerializer(obj.GetType());

    //        //serializer.Serialize(sw, obj);

    //        //return sb.ToString();
    //    }

    //    public static T DeserializeFromXml<T>(string data) where T : class
    //    {
    //        if (data == null) return null;

    //        var serializer = new XmlSerializer(typeof(T));

    //        using (var stringReader = new StringReader(data))
    //        {
    //            using (var xmlTextReader = new XmlTextReader(stringReader))
    //            {
    //                if (!serializer.CanDeserialize(xmlTextReader))
    //                    throw new ApplicationException(@"Cannot deserialize xml string using " + typeof(T).FullName);

    //                return (T)serializer.Deserialize(xmlTextReader);
    //            }
    //        }
    //    }

    //    public static T Deserialize<T>(string data) where T : class
    //    {
    //        return data == null ? null : (T)new LosFormatter().Deserialize(data);
    //    }

    //    //public static string SerializeToJson<T>(T obj)
    //    //{
    //    //    return JsonConvert.SerializeObject(obj);
    //    //}

    //    //public static T DeSerializeFromJson<T>(string data) where T : class
    //    //{
    //    //    return data == null ? null : JsonConvert.DeserializeObject<T>(data);
    //    //}
    //}
}