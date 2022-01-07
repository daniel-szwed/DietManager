using ArbreSoft.Utils;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Text.Json;
using ArbreSoft.DietManager.Presentation.Models;

namespace DietManager.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IMapper _mapper;

        public IngredientService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<NutritionFact> SearchIngredientAsync(string name)
        {
            using (var client = new ApiClient())
            {
                var body = new { query = name, timezone = "US/Eastern" };
                var request = new RequestBuilder()
                    .SetUri("https://trackapi.nutritionix.com/v2/natural/nutrients")
                    .SetMethod(HttpMethod.Post)
                    .AddHeader("x-app-id", "e0f10739")
                    .AddHeader("x-app-key", "05ab0d773ba54b5661b68ae4e7aec65d")
                    .SetStringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                    .Build();
                var response = await client
                    .SetTimeout(15000)
                    .SendRequestAsync(request);
                var responseBody = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());

                if (responseBody.TryGetProperty("foods", out JsonElement foods))
                {
                    var apiResponse = new Nutritionix();
                    if (foods.ValueKind == JsonValueKind.Array && foods.GetArrayLength() > 0)
                    {
                        DynamicUtil.UpdateModel(apiResponse, foods[0]);
                        var ingredient = _mapper.Map<NutritionFact>(apiResponse);
                        var serving_weight_grams = apiResponse.serving_weight_grams;
                        Recalc(ingredient, serving_weight_grams);

                        return ingredient;
                    }
                }

                return null;
            }
        }

        private void Recalc(NutritionFact ingredient, long serving_weight_grams)
        {
            foreach (var propertyInfo in ingredient.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.Name == "Single")
                {
                    float currentValue = (float)propertyInfo.GetValue(ingredient);
                    float newValue = currentValue * (100f / serving_weight_grams);
                    var rounded = Math.Round(newValue, 2);
                    propertyInfo.SetValue(ingredient, (float)rounded);
                }
            }
        }

        
    }
}