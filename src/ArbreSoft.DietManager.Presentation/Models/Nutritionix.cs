namespace ArbreSoft.DietManager.Presentation.Models
{
    public class Nutritionix
    {
        public string food_name { get; set; }
        public float nf_calories { get; set; }
        public float nf_protein { get; set; }
        public float nf_total_carbohydreate { get; set; }
        public float nf_sugar { get; set; }
        public float nf_total_fat { get; set; }
        public float nf_saturated_fat { get; set; }
        public int serving_weight_grams { get; set; }
    }
}
