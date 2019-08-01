using System;
using System.Collections.Generic;

namespace ArmoryDb.DataAccess.Entities
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }

        public virtual Item ItemNavigation { get; set; }
        public virtual Orders Order { get; set; }
    }
}
