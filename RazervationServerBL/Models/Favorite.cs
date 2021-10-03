﻿using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Favorite
    {
        public int ClientId { get; set; }
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
    }
}
