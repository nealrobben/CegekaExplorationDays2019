using System;
using System.Net.Http;
using System.Text;

namespace MachineLearningClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = new Uri("https://europewest.services.azureml.net/workspaces/a73a318c48744428bd5db2b30f7d604a/services/515622d69b9f4554b7b518ab33aa185d/execute?api-version=2.0&details=true");
            var body = "";

            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

                var result = client.PostAsync(url, content).Result;

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;

                    Console.WriteLine("Request successful...");
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("Error while performing request...");
                    Console.WriteLine(result.StatusCode);
                }
            }

            Console.WriteLine("Done...");
        }
    }
}
