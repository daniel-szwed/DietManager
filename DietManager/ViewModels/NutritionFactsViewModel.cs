using System.ComponentModel;
using System.Net.Http;
using System.Text;
using DietManager.Commands;
using DietManager.Services;
using Newtonsoft.Json;

namespace DietManager.ViewModels
{
    public class NutritionFactsViewModel : INutritionFactsViewModel, INotifyPropertyChanged
    {
        private string _response;
        public Command GetNutritionFacts { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Response
        {
            get { return _response; }
            set { _response = value; NotifyPropertyChanged(nameof(Response)); }
        }

        public NutritionFactsViewModel()
        {
            GetNutritionFacts = new Command(OnGetNutritionFacts);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void OnGetNutritionFacts(object obj)
        {
            var query = obj as string;
            using(var client = new ApiService())
            {
                var body = new { query = query, timezone = "US/Eastern" };
                var response = await client
                            .SetBaseAddress("https://trackapi.nutritionix.com")
                            .SetMethod(HttpMethod.Post)
                            .AddHeader("x-app-id", "e0f10739")
                            .AddHeader("x-app-key", "05ab0d773ba54b5661b68ae4e7aec65d")
                            .SetStringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                            .SetTimeout(15000)
                            .SendRequestAsync("v2/natural/nutrients");
                dynamic responseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                Response = responseBody.foods[0].nf_calories;
            }
        }
    }
}
