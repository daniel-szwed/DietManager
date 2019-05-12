namespace zadanie3.Models
{
    public class ChildMovie : Movie
    {
        public override Gener Gener { get => Gener.Animated; set => base.Gener = value; }
    }
}
