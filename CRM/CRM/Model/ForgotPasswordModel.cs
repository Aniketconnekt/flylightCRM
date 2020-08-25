using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class ForgotPasswordModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Comment { get; set; }
        public int Id { get; set; }
    }
}
