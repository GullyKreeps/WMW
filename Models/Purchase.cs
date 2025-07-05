namespace WillMyWay.Models
{
    public class Purchase {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AudioId { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}