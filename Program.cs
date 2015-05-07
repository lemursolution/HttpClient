using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertHoja();
            // InsertTarea();
            // UploadStream();
            // ShowString();
            // InsertEmail();
        }

        static void InsertHoja()
        {
            // var uriString = "http://localhost:49547/PostHojas.svc/ActualizarHoja";
            var uriString = "http://localhost:49547/PostHojas.svc/GrabarHoja";
            // var uriString = "http://lemursolution-cessav2.dnsalias.com/PostHojas.svc/GrabarHoja";

            WebClient client = new WebClient();
            string postData = getJsonHojaData();

            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            myWebClient.Encoding = Encoding.ASCII;
            myWebClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            myWebClient.Headers.Add("Accept-Encoding", "gzip, deflate");
            // postStream implicitly sets HTTP POST as the request method.
            Console.WriteLine("Uploading to {0} ...", uriString);

            var result = myWebClient.UploadString(uriString, postData);
            if (result.ToString() == "true") { Console.WriteLine("\nSuccessfully posted the new Hoja."); }
            else { Console.WriteLine("\nError in posted the new hoja."); }
            Console.Read();

        }

        static void InsertTarea()
      {
          var uriString = "http://localhost:49547/PostTareas.svc/ActualizarTarea";
          // var uriString = "http://localhost:49547/PostTareas.svc/GrabarTarea";

          WebClient client = new WebClient();
          // var data = client.DownloadString("http://localhost:1140/RESTService.svc/web/getdata?id=0");
          // Console.WriteLine(data);

          string postData = getJsonTareaData();
          // Apply Ascii Encoding to obtain an array of bytes.  
          byte[] postArray = Encoding.ASCII.GetBytes(postData);

          // Create a new WebClient instance.
          WebClient myWebClient = new WebClient();
          myWebClient.Encoding = Encoding.ASCII;
          myWebClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
          myWebClient.Headers.Add("Accept-Encoding", "gzip, deflate");
          // postStream implicitly sets HTTP POST as the request method.
          Console.WriteLine("Uploading to {0} ...", uriString);

          var result = myWebClient.UploadString(uriString, postData);
          Console.WriteLine("\nSuccessfully posted the data.");
          Console.Read();

      }

        static void InsertEmail()
        {
            try
            {
                var uriString = "http://localhost:49547/PostHojas.svc/EnviarEmail";
                // var uriString = "http://lemursolution-cessa.dnsalias.com/PostHojas.svc/EnviarEmail";
                WebClient client = new WebClient();
                string EmailData = getJsonEmailData();

              // Create a new WebClient instance.
              WebClient myWebClient = new WebClient();
              myWebClient.Encoding = Encoding.ASCII;
              myWebClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
              myWebClient.Headers.Add("Accept-Encoding", "gzip, deflate");
              // postStream implicitly sets HTTP POST as the request method.
              Console.WriteLine("Uploading to {0} ...", uriString);

              var result = myWebClient.UploadString(uriString, EmailData);
              if (result.ToString() == "true") { Console.WriteLine("\nSuccessfully posted the EmailData."); }
              else { Console.WriteLine("\nError in posted the EmailData."); }
              Console.Read();
            }
            catch (Exception ex) { string err = ex.ToString(); }

        }

        static void UploadStream()
        {
            // var uriString = "http://localhost:49547/PostFirmas.svc/GrabarFirmaTecnico";
            var uriString = "http://localhost:49547/PostFirmas.svc/GrabarFirmaCliente";
            byte[] data = new byte[100];
            for (int i = 0; i < 100; i++)
            {
                data[i] = (byte)i;
            }
            var fileStream = new FileStream(@"C:\Temp\firmaIrene.png", FileMode.Open);
           // var fileStream = new FileStream(@"C:\Temp\firmaLazaro.png", FileMode.Open);
            var reader = new BinaryReader(fileStream);
            data = reader.ReadBytes((int)fileStream.Length);

            WebClient client = new WebClient();
            client.Headers.Add("codhoja", "3");
            //client.Headers.Add("Content-Type", "image/jpg");
            var result = client.UploadData(uriString, data);
        }


        static string getJsonTareaData()
        {
            // return "{\"cod_tarea\":-9999, \"idhoja\":3, \"titulo\":\"Task 1\", \"idtecnico\":32, \"fecha\":\"05/07/2010 0:00:00\", \"tdFecha\":\"01/02/2014 0:09:00\", \"thFecha\":\"01/02/2014 0:18:00\", \"ddFecha\":\"01/02/2014 0:00:00\", \"dhFecha\":\"01/02/2014 0:00:00\", \"horasviaje\":12, \"kilometros\":50, \"estado\":1}";
            return "{\"codtarea\":16, \"idhoja\":1, \"titulo\":\"Tarea insertada desde HttpClient actualizada\", \"idtecnico\":15, \"fecha\":\"2014-01-12 0:00:00\", \"tdFecha\":\"2014-01-12 9:00:00\", \"thFecha\":\"2014-01-12 19:00:00\", \"ddFecha\":\"2014-01-12 13:00:00\", \"dhFecha\":\"2014-01-12 14:00:00\", \"horasviaje\":0, \"kilometros\":0, \"estado\":2}";
        }

        static string getJsonHojaData()
        {
            return "{\"Id\":-9999,\"codusuario\":15,\"codorden\":\"\",\"expediente\":\"70020216\",\"titulo\":\"Reparación del TBG620V16\",\"codtecnico\":15,\"codplanta\":105,\"codpeticionario\":490,\"refpedido\":\"\",\"codcargoregion\":1,\"tipotrabajo\":4,\"tipohoja\":1,\"fechafinmontaje\":\"2014-02-21 00:00:00\",\"fechahoja\":\"2014-02-18 00:00:00\",\"codmotorplanta\":119,\"numseriemotor\":\"2207363\",\"horasmotor\":\"27000\",\"codestado\":1,\"direccion\":\"\",\"codpos\":\"\",\"pais\":\"\",\"provincia\":\"\",\"ciudad\":\"\",\"telefono\":\"\"}";

        }

        static string getJsonEmailData()
        {
            int cod_hoja = 30;
            int cod_usuario = 16;
            int cod_tecnico = 16;
            int cod_peticionario = 461;
            return "{\"codhoja\":" + cod_hoja + ",\"codusuario\":" + cod_usuario + ",\"asunto\":\"Hoja de Trabajo Caterpillar No :" + cod_hoja + "\",\"emailcliente\":\"cliente@hotmail.com\",\"emailtecnico\":\"tecnico@hotmail.com\",\"codtecnico\":" + cod_tecnico + ",\"codpeticionario\":" + cod_peticionario + ",\"fechahoja\":\"2015-03-13 0:00:00\"}";
        }

        static void ShowString()
        {
            string str = "c:\\Temp\\Clasicword.doc";
            string replacestr = Regex.Replace(str,"\\+",@"\");
           Console.WriteLine(replacestr);
           Console.Read();
        }
    }
}