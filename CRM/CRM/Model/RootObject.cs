using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class RootObject<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string AccessToken { get; set; }
        public T Data { get; set; }
    }
    public class RootObjectList<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string AccessToken { get; set; }
        public List<T> Data { get; set; }
    }

    public class RootObjectSearchList<T>
    {
        public bool Result { get; set; }
        public List<T> List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }
}
