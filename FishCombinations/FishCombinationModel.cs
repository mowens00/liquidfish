using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FishCombinations
{
    public class FishCombinationModel
    {
        public DateTime Date { get; set; }
        [JsonProperty("fish_caught")]
        public List<string> FishCaught { get; set; }
    }
}
