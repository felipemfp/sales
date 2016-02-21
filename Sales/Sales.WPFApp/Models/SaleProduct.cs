namespace Sales.WPFApp.Models
{
    class SaleProduct
    {
        public int SaleId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
    }
}
