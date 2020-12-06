
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace HotusaLeasing
{
   
    public class Program
    {


        public static string _ficheroEntrada = string.Empty;
        

        public static Logger log = new Logger("Log\\logger_" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "_"));


        static void Main(string[] args)
        {
            log.EscribirCabecera();
            //Logger log = new Logger("Log\\logger_" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "_"));
            //comprobamos los parámetros de entrada
            if (args.Count() > 2 || args.Count() == 1)
            {
                log.EscribirLog("Los argumentos de la linea de comandos son:"
    + "HotusaLeasing.exe <fecha_inicial> <fecha_final>. Se finaliza la ejecución");
                log.EscribirLog("o bien: HotusaLeasing.exe");
                Environment.Exit(1);
            }

            int tipo = 0;

            String strfecha1 = "";
            String strfecha2 = "";

            DateTime dtfecha1 = new DateTime();
            DateTime dtfecha2 = new DateTime();


            if (args.Count() == 0)
            {
                tipo = 1;//si es tipo == 0 se recoge la fecha que hay en el xml y se le suma 1 para hacer la consulta
            }
            else
            {
                strfecha1 = args[0];
                strfecha2 = args[1];
                try
                {
                    dtfecha1 = Convert.ToDateTime(strfecha1);
                    dtfecha2 = Convert.ToDateTime(strfecha2);
                }
                catch(Exception e)
                {
                    log.EscribirLog("los parámetros de entrada deben de ser fechas");
                    Environment.Exit(2);
                }

                if(dtfecha2 <= dtfecha1)
                {
                    log.EscribirLog("la fecha final no puede ser menor a la fecha inicial");
                    Environment.Exit(2);
                }
                tipo = 2;// se utilizan las fechas de los parámetros de entrada
            }

            //leemos fichero de configuración
            if(!File.Exists(System.Environment.CurrentDirectory + "\\configuracion\\configuracion.xml"))
            {
                log.EscribirLog("falta el fichero de configuración:" + System.Environment.CurrentDirectory + "\\configuracion\\configuracion.xml");
                Environment.Exit(3);
            }

            //comproabamos que está relleno el xml con la una fecha
            MyXml xml = new MyXml();
            
            if(tipo == 1)//Se utiliza la fecha del fichero xml
            {
                dtfecha1 = xml.leerXml();
            }

            //Leemos la info de la conexión a Tesorería

            XRTLIBLib.XlDatasource rr = new XRTLIBLib.XlDatasource();
            string res = rr.get("U2");
            string conres = string.Empty;


            if (res.IndexOf("Provider") >= 0)
            {
                int d = res.IndexOf(";");
                conres = res.Substring(d + 1, res.Length - d - 1);
            }else
            {
                log.EscribirLog("No se encuentra conexión a XRTLib.dll");
                Environment.Exit(4);
            }

            //Empieza el desarrollo
            string iswhere = "";
            if (tipo == 1)
            {
                iswhere = "where f_inicial >=" + dtfecha1;
            }else
            {
                iswhere = "where f_inicial >='" + dtfecha1.ToShortDateString() + "' and f_inicial <='" + dtfecha2.ToShortDateString() + "'";
            }

            SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(conres);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string num = "0";
            int num_reg = 0;
            string updte = "select * from REC_ACC where EXTRACT_FLAG=" + num;
            log.EscribirLog("select:" + updte);
            cmd.CommandText = updte;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            string fileName = "salida.txt";
            // int result = command.ExecuteNonQuery();
            string campo1 = string.Empty;
            File.Delete("salida.txt");
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter("salida.txt", true))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        num_reg++;
                        campo1 = rellenarCampo(reader["REC_ACC_MVT_ID"].ToString(), 20);
                        string campo2 = rellenarCampo(reader["ACC_CODE"].ToString(), 20);
                        string campo3 = rellenarCampo(reader["ABK_FLOW_CODE"].ToString(), 20);
                        string campo4 = rellenarCampo(reader["ABK_CUR_CODE"].ToString(), 20);
                        string campo5 = rellenarCampo(reader["ABK_CUR_AMOUNT"].ToString(), 20);
                        string campo6 = rellenarCampo(reader["TRN_CUR"].ToString(), 20);
                        string campo7 = rellenarCampo(reader["TRN_AMOUNT"].ToString(), 20);
                        string campo8 = rellenarCampo(reader["CHEQUE_NB"].ToString(), 20);
                        string campo9 = rellenarCampo(reader["DOCUMENT_NB"].ToString(), 20);
                        string campo10 = rellenarCampo(reader["BOOK_DATE"].ToString(), 20);
                        string campo11 = rellenarCampo(reader["VALUE_DATE"].ToString(), 20);
                        string campo12 = rellenarCampo(reader["DESCRIPTION"].ToString(), 20);
                        string campo13 = rellenarCampo(reader["SENSE_FLAG"].ToString(), 20);
                        //string campo14 = rellenarCampo(reader["MVT_TYPE_FLAG"].ToString(),20);

                        //string campo15 = rellenarCampo(reader["EXTRACT_FLAG"].ToString(),20);
                        //string campo16 = rellenarCampo(reader["IMPORT_PROCESS_LOG_ID"].ToString(),20);
                        //string campo17 = rellenarCampo(reader["IMPORT_DATE"].ToString(),20);
                        //string campo18 = rellenarCampo(reader["HISTORY_ID"].ToString(),20);
                        //string campo19 = rellenarCampo(reader["REC_DATE"].ToString(),20);
                        //string campo20 = rellenarCampo(reader["REC_TYPE_FLAG"].ToString(),20);
                        //string campo21 = rellenarCampo(reader["RECONCILIATION_ID"].ToString(),20);
                        //string campo22 = rellenarCampo(reader["PART_REC_ID"].ToString(),20);
                        //string campo23 = rellenarCampo(reader["REC_TIME_FLAG"].ToString(),20);
                        string campo24 = rellenarCampo(reader["ZU_01"].ToString(), 20);
                        string campo25 = rellenarCampo(reader["ZU_02"].ToString(), 20);
                        string campo26 = rellenarCampo(reader["ZU_03"].ToString(), 20);
                        string campo27 = rellenarCampo(reader["ZU_04"].ToString(), 20);
                        string campo28 = rellenarCampo(reader["ZU_05"].ToString(), 20);
                        string campo29 = rellenarCampo(reader["ZU_06"].ToString(), 20);
                        string campo30 = rellenarCampo(reader["ZU_07"].ToString(), 20);
                        string campo31 = rellenarCampo(reader["ZU_08"].ToString(), 20);
                        string campo32 = rellenarCampo(reader["ZU_09"].ToString(), 20);
                        string campo33 = rellenarCampo(reader["ZU_10"].ToString(), 20);
                        string campo34 = "";// rellenarCampo(reader["ROWVERSION"].ToString(), 20);
                        string linea =
                            //campo1 + 
                            campo2 + campo3 + campo4 + campo5 + campo6 + campo7 + campo8 + campo9 + campo10
                            + campo11 + campo12 + campo13 //+ campo14 + campo15 + campo16 + campo17 + campo18 + campo19 + campo20 + campo21 + campo22
                                                          //+ campo23 
                            + campo24 + campo25 + campo26 + campo27 + campo28 + campo29 + campo30 + campo31 + campo32 + campo33 + campo34;

                        file.WriteLine(linea);
                        log.EscribirLog("linea:" + linea);
                        actualizarDato(conres, campo1, "1");

                    }
                }
            }
            sqlConnection1.Close();
            log.EscribirLog("num_Reg actualizados:" + num_reg);
        }

        public static bool actualizarDato(string con, string valor, string num)
        {
            bool res = false;
            SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(con);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string updte = "Update REC_ACC SET EXTRACT_FLAG=" + num + " where REC_ACC_MVT_ID = '" + valor.Trim() + "'";
            cmd.CommandText = updte;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            log.EscribirLog("update:" + updte);
            return res;
        }

        public static string rellenarCampo(string dato, int tam)
        {
            string res = dato.Trim();
            for (int i = dato.Trim().Length; i < tam; i++)
            {
                res = res + " ";
            }
            return res;

        }





    }
}
