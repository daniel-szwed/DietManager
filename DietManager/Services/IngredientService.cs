using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DietManager.Models;
using DietManager.Utils;
using Newtonsoft.Json;

namespace DietManager.Services
{
    public class IngredientService : IIngredientService
    {
        public async Task<IngredientBase> SearchIngredientAsync(string name)
        {
            using (var client = new ApiService())
            {
                var body = new { query = name, timezone = "US/Eastern" };
                var response = await client
                            .SetBaseAddress("https://trackapi.nutritionix.com")
                            .SetMethod(HttpMethod.Post)
                            .AddHeader("x-app-id", "e0f10739")
                            .AddHeader("x-app-key", "05ab0d773ba54b5661b68ae4e7aec65d")
                            .SetStringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                            .SetTimeout(15000)
                            .SendRequestAsync("v2/natural/nutrients");
                dynamic responseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                if (DynamicUtil.HasProperty(responseBody, "foods"))
                {
                    var foods = responseBody.foods[0];
                    var ingredient = new IngredientBase();
                    var mapping = new Dictionary<string, string>();
                    mapping.Add("name", "food_name");
                    mapping.Add("kcal", "nf_calories");
                    mapping.Add("protein", "nf_protein");
                    mapping.Add("carbohydrates", "nf_total_carbohydrate");
                    mapping.Add("sugar", "nf_sugars");
                    mapping.Add("fat", "nf_total_fat");
                    mapping.Add("saturated", "nf_saturated_fat");
                    DynamicUtil.UpdateModel(ingredient, foods, mapping: mapping);
                    var serving_weight_grams = (long)foods.serving_weight_grams.Value;
                    Recalc(ingredient, serving_weight_grams);
                    return ingredient;
                }
                else
                    return null;
            }
        }

        private void Recalc(IngredientBase ingredient, long serving_weight_grams)
        {
            foreach (var propertyInfo in ingredient.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.Name == "Single")
                {
                    float currentValue = (float)propertyInfo.GetValue(ingredient);
                    float newValue = currentValue * (100f / (float)serving_weight_grams);
                    var rounded = Math.Round(newValue, 2);
                    propertyInfo.SetValue(ingredient, (float)rounded);
                }
            }
        }

        public IngredientBase GetSum(IEnumerable<Ingredient> ingregients)
        {
            return GetSum(ingregients, true);
        }

        public IngredientBase GetSum(IEnumerable<IngredientBase> ingregients)
        {
            return GetSum(ingregients, false);
        }

        private IngredientBase GetSum(IEnumerable<IngredientBase> ingredients, bool dependOnAmount)
        {
            var sumIngredient = new Ingredient();
            ingredients.ToList().ForEach(i => {
                foreach (var propertyInfo in i.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType.Name == "Single")
                    {
                        float value;
                        if(dependOnAmount)
                            value = GetValueForAmmount((Ingredient)i, propertyInfo.Name);
                        else
                            value = (float)propertyInfo.GetValue(i);
                        float sumValue = (float)propertyInfo.GetValue(sumIngredient);
                        var rounded = Math.Round(value, 2);
                        propertyInfo.SetValue(sumIngredient, sumValue + (float)rounded);
                    }
                }
            });
            return sumIngredient;
        }

        private float GetValueForAmmount(Ingredient ingredient, string propName)
        {
            float tableValue = (float)typeof(Ingredient).GetProperty(propName).GetValue(ingredient);
            return tableValue * (ingredient.Amount / 100f);
        }
    }
}