using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class StateModel
    {
        public bool Result { get; set; }
        public StateData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class StateData
    {
        public int Id { get; set; }
        public string Statename { get; set; }
        public object[] StateList { get; set; }
        public object[] CountryList { get; set; }
        public int CountryId { get; set; }
        public string Countryname { get; set; }
        public object Edit { get; set; }
        public object Delete { get; set; }
        public int StatusId { get; set; }
        public int CreatedById { get; set; }
        public object ModifiedById { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
