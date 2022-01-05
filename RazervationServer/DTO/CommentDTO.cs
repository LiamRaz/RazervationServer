using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazervationServer.DTO
{
    public class CommentDTO
    {
        public string CommentText { get; set; }

        public int Rating { get; set; }

        public int ClientId { get; set; }

        public int BusinessId { get; set; }

        public DateTime Cdate { get; set; }

    }
}
