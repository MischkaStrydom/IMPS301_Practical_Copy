namespace BudgetApi.Models
{
    public class BudgetItem
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public decimal Amount { get; set; }

        public string? Secret { get; set; }
    }
}
