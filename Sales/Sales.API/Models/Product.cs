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
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.SaleProducts = new HashSet<SaleProduct>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ManufacturerId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public virtual Manufacturer Manufacturer { get; set; }
        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
