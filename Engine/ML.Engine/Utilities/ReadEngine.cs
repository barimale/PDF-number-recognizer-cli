using System.Xml.Serialization;
using System.Xml;

namespace ML.Engine.Utilities
{
    public static class ReadEngine
    {
        public static T? ReadXml<T>(string path)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));

                using (XmlReader reader = XmlReader.Create(path))
                {
                    return (T)xsSubmit.Deserialize(reader);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return default;
        }
    }
}
