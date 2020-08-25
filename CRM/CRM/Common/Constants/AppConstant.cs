using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Common.Constants
{
    public class AppConstant
    {
        public const string EMPTY_STRING = "";
        public const string NETWORK_FAILURE = "No Network Connection found! Please try again.";
        public const string LOGIN_FAILURE = "Login Failed! Please try again.";
        public const string REGISTRATION_FAILURE = "Registration Failed! Please try again.";
        public const string SOMETHING_WRONG = "Something went wrong.";
        public const string WRONG_Mobile = "Mobile number already exist in database.";
        public const string OTP_Not_Send = "Otp not send on mobile.";

        /// <summary>
        /// The email address
        /// </summary>
        public const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        /// <summary>
        /// The GST Number
        /// </summary>
        public const string gstRegex = "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}";

        /// <summary>
        /// The PAN Number
        /// </summary>
        public const string panRegex = "^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$";

        /// <summary>
        /// The Aadhaar Number
        /// </summary>
        public const string aadhaarRegex = "^[0-9]{12}$";

        // Storage Keys
        //public const string UserRole = "User_Role";
        //public const string UserId = "User_Id";
        //public const string CreatedById = "Created_By_Id";
        //public const string LoginUserName = "Login_User_Name";
        //public const string ForgotOtp = "Forgot_Otp";
    }
}
