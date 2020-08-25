using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;

namespace CRM.Common.Helpers
{
    public static class Extensions
    {
        public static string GetDateTime(this ObservableCollection<object> date)
        {
            try
            {
                return date[0] + "-" + date[1] + "-" + date[2];
            }
            catch (Exception)
            {
                int month = Convert.ToInt32(date[0]);
                int day = Convert.ToInt32(date[1]);
                int year = Convert.ToInt32(date[2]);
                if (month != 2)
                    return date[0] + "-" + date[1] + "-" + date[2];
                else if(month == 2 && day <= 28)
                    return date[0] + "-" + date[1] + "-" + date[2];
                else if(month == 2 && day == 29 && year%4 == 0)
                    return date[0] + "-" + date[1] + "-" + date[2];
                else
                    return DateTime.Now.ToString("MM-dd-yyyy");
            }
        }
    }
}
