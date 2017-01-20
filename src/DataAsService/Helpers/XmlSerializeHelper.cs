using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DataAsService.Helpers
{
    public static class XmlSerializeHelper
    {
        #region Private Members
        private const string DocumentHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<dataset xmlns=\"http://developer.cognos.com/schemas/xmldata/1/\"\r\n xmlns:xs=\"http://www.w3.org/2001/XMLSchema-instance\">";
        private const string DocumentFooter = "</dataset>";
        private const string DefaultPropertyTypeName = "string";

        private static string CreateMetaData(PropertyInfo[] propertyInfos)
        {
            var metadataSb = new StringBuilder("<metadata>");

            if (propertyInfos != null)
            {
                foreach (var prop in propertyInfos)
                {
                    if (prop != null)
                    {
                        metadataSb.Append($"<item name=\"{prop.Name}\" type=\"xs:{ConvertTypeToXmlType(prop.PropertyType)}\"/>");
                    }
                }
            }

            metadataSb.Append("</metadata>");
            return metadataSb.ToString();
        }

        /// <summary>
        /// This is based on : http://www.ibm.com/support/knowledgecenter/en/SSCRDM_10.1.0/com.ibm.swg.im.cognos.ug_fm.10.1.0.doc/ug_fm_id34461SupportedXMLDataTypes.html#SupportedXMLDataTypes
        /// </summary>
        /// <param name="propType"></param>
        /// <returns></returns>
        private static string ConvertTypeToXmlType(Type propType)
        {
            /* Valid Types : boolean, byte, date, dateTime, decimal, double, ENTITIES, ENTITY, float, ID, IDREF, int
                , integer, language, long, Name, NCName, negativeInteger, NMTOKEN, NMTOKENS, nonNegativeInteger
                , NonPositiveInteger, NOTATION, positiveInteger, QName, short, string, time, token, unsignedLong
                , unsignedInt, unsignedShort, unsignedByte*/

            if (propType == null)
            {
                return null;
            }

            if (propType == typeof(int))
            {
                return "int";
            }
            
            if (propType == typeof(bool))
            {
                return "boolean";
            }

            if (propType == typeof(byte))
            {
                return "byte";
            }

            if (propType == typeof(DateTime))
            {
                return "dateTime";
            }

            if (propType == typeof(decimal))
            {
                return "decimal";
            }

            if (propType == typeof(double))
            {
                return "double";
            }

            if (propType == typeof(float))
            {
                return "float";
            }

            if (propType == typeof(long))
            {
                return "long";
            }

            if (propType == typeof(short))
            {
                return "short";
            }

            return propType == typeof(string) ? "string" : DefaultPropertyTypeName;
        }

        private static string CreateData<T>(IList<T> collection, PropertyInfo[] propertyInfos)
        {
            var metadataSb = new StringBuilder("<data>");

            if (propertyInfos != null && collection != null)
            {
                foreach (var item in collection)
                {
                    metadataSb.Append("<row>");

                    foreach (var prop in propertyInfos)
                    {
                        if (prop == null)
                        {
                            continue;
                        }

                        var value = prop.GetValue(item);

                        if (value is string)
                        {
                            value = ReplaceInValidXmlChars((string)value);
                        }

                        metadataSb.Append($"<value>{value}</value>");
                    }

                    metadataSb.Append("</row>");
                }
            }

            metadataSb.Append("</data>");
            return metadataSb.ToString();
        }

        /// <summary>
        /// The following characters are replaced : "&lt;" to "&amp;lt;", "&gt;" to "&amp;gt;", "\"" to "&amp;quot;", "\'" to "&amp;apos;", "&amp;" to "&amp;amp;"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ReplaceInValidXmlChars(string input) => input?.Replace("<", "&lt;")?.Replace(">", "&gt;")?.Replace("\"", "&quot;")?.Replace("\'", "&apos;")?.Replace("&", "&amp;");

        #endregion

        public static string SerializeToString<T>(IList<T> collection)
        {
            if (collection == null)
            {
                return null;
            }
            var firstItem = collection[0];

            var propertyInfos = firstItem?.GetType().GetProperties();

            if (propertyInfos == null)
            {
                return null;
            }

            return new StringBuilder(DocumentHeader)
                .Append(CreateMetaData(propertyInfos))
                ?.Append(CreateData(collection, propertyInfos))
                ?.Append(DocumentFooter)
                ?.ToString();
        }
    }
}