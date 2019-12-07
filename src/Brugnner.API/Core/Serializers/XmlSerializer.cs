using System.IO;
using System.Text;
using System.Xml;

namespace Brugnner.API.Core.Serializers
{
    /// <summary>
    /// Serializes and deserializes objects into and from XML documents.
    /// </summary>
    public class XmlSerializer
    {
        /// <summary>
        ///  Deserializes the XML document contained by the specified XML string.
        /// </summary>
        /// <typeparam name="T">Object to be deserialized.</typeparam>
        /// <param name="xml">XML representation of the object.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            T result = default(T);

            using (var reader = new StringReader(xml))
            {
                result = (T)new System.Xml.Serialization.XmlSerializer(typeof(T)).Deserialize(reader);
            }

            return result;
        }

        /// <summary>
        /// Serializes the specified object and returns a XML string representation.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="entity">Object to be serialized.</param>
        /// <returns></returns>
        public static string Serialize<T>(T entity)
        {
            var xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            var namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
            {

                new System.Xml.Serialization.XmlSerializer(typeof(T)).Serialize(xmlWriter, entity, namespaces);
                return stringWriter.ToString();
            }
        }
    }
}
