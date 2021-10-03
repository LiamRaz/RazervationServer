using System;
using System.Collections.Generic;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class Comment
    {
        public int BusinessId { get; set; }
        public string CommentText { get; set; }
        public int ClientId { get; set; }
        public int AutoCommentId { get; set; }
        public int Rating { get; set; }

        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
    }
}
