using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using DietManager.Commands;
using DietManager.Models;
using DietManager.Services;
using DietManager.Utils;
using Newtonsoft.Json;

namespace DietManager.ViewModels
{
    public class NutritionFactsViewModel : INutritionFactsViewModel, INotifyPropertyChanged
    {
        private string _response;
        private Ingredient _ingredient;
        public Command GetNutritionFacts { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Response
        {
            get { return _response; }
            set { _response = value; NotifyPropertyChanged(nameof(Response)); }
        }

        public Ingredient Ingredient
        {
            get { return _ingredient; }
            set { _ingredient = value; NotifyPropertyChanged(nameof(Ingredient)); }
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
                var body = new { query, timezone = "US/Eastern" };
                var response = await client
                            .SetBaseAddress("https://trackapi.nutritionix.com")
                            .SetMethod(HttpMethod.Post)
                            .AddHeader("x-app-id", "e0f10739")
                            .AddHeader("x-app-key", "05ab0d773ba54b5661b68ae4e7aec65d")
                            .SetStringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                            .SetTimeout(15000)
                            .SendRequestAsync("v2/natural/nutrients");
                dynamic responseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                if(DynamicUtil.HasProperty(responseBody, "foods"))
                {
                    var foods = responseBody.foods[0];
                    var ingredient = new Ingredient();
                    var mapping = new Dictionary<string, string>();
                    mapping.Add("name", "food_name");
                    mapping.Add("kcal", "nf_calories");
                    mapping.Add("protein", "nf_protein");
                    mapping.Add("carbohydrates", "nf_total_carbohydrate");
                    mapping.Add("sugar", "nf_sugars");
                    mapping.Add("fat", "nf_total_fat");
                    mapping.Add("saturated", "nf_saturated_fat");
                    DynamicUtil.UpdateModel(ingredient, foods, null, mapping);
                    var serving_weight_grams = (long)foods.serving_weight_grams.Value;
                    Recalc(ingredient, serving_weight_grams);
                    Ingredient = ingredient;
                }
            }
        }

        private void Recalc(Ingredient ingredient, long serving_weight_grams)
        {
            foreach (var propertyInfo in ingredient.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.Name == "Single")
                {
                    float currentValue = (float)propertyInfo.GetValue(ingredient);
                    float newValue = currentValue * (100f / (float)serving_weight_grams);
                    propertyInfo.SetValue(ingredient,  newValue);
                }
            }
        }
    }
}
