using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace FishCombinations
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient _client = new RestClient("https://liquid.fish/fishes.json");
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = _client.Execute(request);

            List<FishCombinationModel> respModel = JsonConvert.DeserializeObject<List<FishCombinationModel>>(response.Content);

            List<string> combinations = new List<string>();
            foreach (FishCombinationModel item in respModel)
            {
                StringBuilder combo = new StringBuilder();
                foreach (string fish in item.FishCaught)
                {
                    string uppercase = char.ToUpper(fish[0]) + fish.Substring(1);
                    combo.Append(uppercase);
                }
                combinations.Add(combo.ToString());
            }

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string combo in combinations)
            {
                GetCombinations(combo, dict);
            }

            IEnumerable<KeyValuePair<string, int>> top20 = dict.OrderByDescending(pair => pair.Value).Take(20);

            foreach (var item in top20)
            {
                Console.WriteLine(item.Key + " caught " + item.Value + " times");
            }

            Console.ReadLine();
        }

        public static Dictionary<string, int> GetCombinations(string combo, Dictionary<string, int> dict)
        {
            if (dict.ContainsKey(combo))
            {
                dict[combo] = dict[combo] + 1;
            }
            else
            {
                dict.Add(combo, 1);
            }
            return dict;
        }
    }
}
