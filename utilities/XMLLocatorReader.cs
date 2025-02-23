using System.Xml;

namespace MarvelSelenium.utilities
{
    internal class XMLLocatorReader
    {
        public static string GetLocatorValue(string pageName, string elementName, string locatorType)
        {
            string locatorValue = null;

            /// <summary>
            /// Load XML File
            /// </summary>
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\locators.xml");

            /// <summary>
            /// Get the root element
            /// </summary>
            XmlElement root = xmlDoc.DocumentElement;

            /// <summary>
            /// Constract XPATH eexpression to select the specified element under the specified page with the given locator type
            /// </summary>
            string xpath = $"/Locators/{pageName}/{elementName}[LocatorType = '{locatorType}']/LocatorValue";

            /// <summary>
            /// Select the locator value node
            /// </summary>
            XmlNode locatorValueNode = root.SelectSingleNode(xpath);

            if (locatorValueNode != null)
            {

                locatorValue = locatorValueNode.InnerText;
            }

            return locatorValue;
        }
    }
}