namespace Juan.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}
