using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MachineLearningClient
{
    public class Program
    {
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {

                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new[] {"leeftijd contact", "geslacht contact", "nationaliteit", "type vervoer", "type locatie", "statecode"},
                                Values = new[,] {  { "0", "value", "value", "value", "value", "0" },  { "0", "value", "value", "value", "value", "0" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                };

                const string apiKey = "WaubxJzn76CdLbGo0JKT0kG/UsDduN8+9plHG34pnOks+LkZRrsdtKvf3RAyItL8fGGbaR4TIivFXw95QWm3EA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://europewest.services.azureml.net/workspaces/a73a318c48744428bd5db2b30f7d604a/services/515622d69b9f4554b7b518ab33aa185d/execute?api-version=2.0&details=true");

                HttpResponseMessage response = client.PostAsJsonAsync("", scoreRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine($"The request failed with status code: {response.StatusCode}");

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                }
            }

            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }
}
