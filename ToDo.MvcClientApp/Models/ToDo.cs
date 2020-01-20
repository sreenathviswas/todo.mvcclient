using System;
using System.Diagnostics.CodeAnalysis;

namespace ToDo.MvcClientApp
{
    [ExcludeFromCodeCoverage]
    public class ToDo
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
