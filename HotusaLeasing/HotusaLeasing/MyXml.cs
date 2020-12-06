using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HotusaLeasing
{
    public class MyXml
    {
        public string ficConfiguracion = System.Environment.CurrentDirectory + "\\configuracion\\configuracion.xml";
        /// <summary>
        /// Lee el fichero de xml y devuelve la fecha
        /// </summary>
        public DateTime leerXml()
        {
            DateTime res = new DateTime();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ficConfiguracion);

            XmlNodeList xConfiguracion = xDoc.GetElementsByTagName("configuracion");
            //XmlNodeList xLista = ((XmlElement)xPersonas[0]).GetElementsByTagName("nombre");
            try
            {
                foreach (XmlElement nodo in xConfiguracion)
                {
                    //string xFecha = nodo.GetAttribute("ultima_fecha");
                    string xNombre = nodo.InnerText;
                    if (xNombre != "")
                    {
                        res = Convert.ToDateTime(xNombre);
                    }
                }
            }catch(Exception e)
            {
                return res;
            }
            return res;
        }

        public Boolean actualizarXml(string nodo, string valor)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ficConfiguracion);
            XmlNode root = doc.DocumentElement;
            //Create a new title element.
            XmlElement elem = doc.CreateElement(nodo);
            elem.InnerText = valor;
            //Replace the title element.
            root.ReplaceChild(elem, root.FirstChild);

            doc.Save(ficConfiguracion);
            return true;
        }

        public void actualizarXmlLinq(string elemento1, string elemento2, string valor)
        {
            XDocument xmlFile = XDocument.Load(ficConfiguracion);
            var query = from c in xmlFile.Elements(elemento1).Elements(elemento2)
                        select c;
            foreach (XElement book in query)
            {
                book.Attribute("attr1").Value = valor;
            }
            xmlFile.Save(ficConfiguracion);
        }
    }
}
