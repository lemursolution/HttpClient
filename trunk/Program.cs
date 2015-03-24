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
            // UseStringHojas();
            // UseStringTareas();
            // UploadStream();
            ShowString();
        }

        static void UseStringHojas()
        {
            var uriString = "http://localhost:49549/PostHojas.svc/ActualizarHoja";
            // var uriString = "http://localhost:49549/PostHojas.svc/GrabarHoja";

            WebClient client = new WebClient();
            // var data = client.DownloadString("http://localhost:1140/RESTService.svc/web/getdata?id=0");
            // Console.WriteLine(data);

            string postData = getJsonHojaData();
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

        static void UseStringTareas()
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
            return "{\"codcargoregion\":2,\"codorden\":-9999,\"direccion\":\"\",\"tipohoja\":1,\"ciudad\":\"\",\"pais\":\"España\",\"codusuario\":30,\"numseriemotor\":\"2207363\",\"telefono\":\"\",\"codpeticionario\":841,\"expediente\":\"7019489\",\"tipotrabajo\":4,\"provincia\":\"\",\"codtecnico\":30,\"horasmotor\":0,\"id\":40,\"titulo\":\"Trabajos especiales en reduccion de corriente en el TCG2016V12C\",\"fechahoja\":\"2014-08-14 00:00:00\",\"codplanta\":223,\"codmotorplanta\":389,\"codestado\":1,\"fechafinmontaje\":\"2014-08-14 00:00:00\",\"refpedido\":\"\",\"codpos\":\"\"}";

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