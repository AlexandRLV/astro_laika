using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlayerProgress
{
    [JsonObject]
    public class PlayerProgressData
    {
        [JsonProperty("money")] public int Money;
        [JsonProperty("selected_ship")] public int SelectedShip;
        [JsonProperty("bought_ships")] public List<int> BoughtShips;
        [JsonProperty("completed_levels")] public List<int> CompletedLevels;
    }
}