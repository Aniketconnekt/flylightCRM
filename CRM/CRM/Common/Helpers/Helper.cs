using CRM.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.Common.Helpers
{
    public class Helper
    {
        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                Regex regex = new Regex(AppConstant.emailRegex);
                Match match = regex.Match(emailaddress);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsGSTPanAadhaarValid(string gst)
        {
            try
            {
                Regex regex = new Regex(AppConstant.gstRegex);
                Match match = regex.Match(gst);
                if (match.Success)
                    return true;
                else
                {
                    Regex panregex = new Regex(AppConstant.panRegex);
                    Match panMatch = panregex.Match(gst);
                    if (panMatch.Success) return true;
                    else
                    {
                        Regex aadhaarregex = new Regex(AppConstant.aadhaarRegex);
                        Match aadhaarMatch = aadhaarregex.Match(gst);
                        if (aadhaarMatch.Success) return true;
                        else return false;
                    }
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
