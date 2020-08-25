using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class List
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public int StatusId { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IList<string> ActionList { get; set; }

    }
    public class LadActionModel
    {
        public bool Result { get; set; }
        public List<List> List { get; set; }
        public int Count { get; set; }
        public IList<object> IList { get; set; }
        public string Message { get; set; }

    }
}
