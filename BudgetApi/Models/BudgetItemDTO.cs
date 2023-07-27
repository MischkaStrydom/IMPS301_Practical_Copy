namespace BudgetApi.Models
{
    public class BudgetItemDTO
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public decimal Amount { get; set; }
    }
}
