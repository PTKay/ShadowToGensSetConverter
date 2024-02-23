using ShadowToGensSetConverter.Light;
using ShadowToGensSetConverter.SetObjects.Gens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter
{
    internal class XmlSerialiser
    {

        public static string GenerateLightXml(SonicLightXml light)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                // Open the parent element
                stringWriter.Write($"<?xml version=\"1.0\" ?>");
                stringWriter.WriteLine();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SonicLightXml));
                stringWriter.WriteLine("  " + SerializeObject(xmlSerializer, light));

                // Close the parent element
                stringWriter.Write($"");
            }

            // Remove all unnecessary XML comments and return
            String xmlString = stringBuilder.ToString();

            return PostProcess(xmlString);
        }

        public static string GenerateGensXml(List<SetObjectGens> setData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                // Open the parent element
                stringWriter.Write($"<?xml version=\"1.0\" ?>\n<SetObject>");
                stringWriter.WriteLine();

                // Serialize each object in the list
                foreach (SetObjectGens obj in setData)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                    stringWriter.WriteLine("  " + SerializeObject(xmlSerializer, obj).Replace("\n", "\n  "));
                }

                // Close the parent element
                stringWriter.Write($"</SetObject>");
            }

            // Remove all unnecessary XML comments and return
            String xmlString = stringBuilder.ToString();

            return PostProcess(xmlString);
        }

        private static string PostProcess(string xmlString)
        {
            return xmlString
                .Replace("  <?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", "");
        }

        private static string SerializeObject<T>(XmlSerializer serializer, T obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                // Remove xsi:type
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(stringWriter, obj, namespaces);
            }
            return stringBuilder.ToString();
        }
    }
}
