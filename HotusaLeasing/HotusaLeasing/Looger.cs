using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotusaLeasing
{
    public class Logger
    {
                private string fileName;

        // si pasamos la ruta de un archivo, su utilizará ese para hacer el log
        public Logger(string file) {

                fileName = file;

        }

        // caso contrario se utiliza uno por defecto
        public Logger()
        {
            fileName = @"C:/log.txt";
        }

        public void EscribirLog(string logText)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine(DateTime.Now.ToString() + " - " + logText);
                }
            }
            catch { }
        }

        public void EscribirCabecera()
        {
            //if (!File.Exists(fileName))
            //{
                try
                {
                    using (StreamWriter w = File.AppendText(fileName))
                    {
                        w.WriteLine("--------------------------------------------------------------------------------");
                        w.WriteLine(DateTime.Now.ToString() + " - Fecha Log");
                        w.WriteLine("--------------------------------------------------------------------------------");
                    }
                }
                catch { }
            //}
        }
    }
}
