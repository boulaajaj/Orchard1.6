﻿using Orchard.ContentManagement.Records;

namespace Richinoz.Paypal.Models {
    public class OrderPartRecord:ContentPartRecord {
        
        public virtual string Details { get; set; }
    }
}