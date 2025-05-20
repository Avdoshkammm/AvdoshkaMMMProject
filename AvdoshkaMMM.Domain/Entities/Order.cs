namespace AvdoshkaMMM.Domain.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
