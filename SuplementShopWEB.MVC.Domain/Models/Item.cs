using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [AllowNull]
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
