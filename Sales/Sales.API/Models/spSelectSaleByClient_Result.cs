//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sales.API.Models
{
    using System;
    
    public partial class spSelectSaleByClient_Result
    {
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public System.DateTime DateSale { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }
    }
}
