namespace Juan.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

      
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

        public string Image { get; set; }
       

    }
}
