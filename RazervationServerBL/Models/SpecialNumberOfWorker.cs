using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class SpecialNumberOfWorker
    {
        public DateTime SpecialDate { get; set; }
        public int NumWorkers { get; set; }
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
    }
}
