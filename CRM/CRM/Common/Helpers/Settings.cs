using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Common.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants
        private const string UserRole = "User_Role";
        private static readonly string DefaultUserRole = string.Empty;

        private const string UserId = "User_Id";
        private static readonly string DefaultUserId = string.Empty;

        private const string CreatedById = "Created_By_Id";
        private static readonly string DefaultCreatedById = string.Empty;

        private const string LoginUserName = "Login_User_Name";
        private static readonly string DefaultLoginUserName = string.Empty;

        private const string ForgotOtp = "Forgot_Otp";
        private static readonly string DefaultForgotOtp = string.Empty;

        private const string CallNo = "Call_No";
        private static readonly string DefaultCallNo = string.Empty;

        private const string WhatsappNo = "Whatsapp_No";
        private static readonly string DefaultWhatsappNo = string.Empty;
        #endregion

        #region Setting Properties
        public static string CRM_UserRole
        {
            get => AppSettings.GetValueOrDefault(nameof(UserRole), DefaultUserRole);
            set => AppSettings.AddOrUpdateValue(nameof(UserRole), value);
        }
        public static string CRM_UserId
        {
            get => AppSettings.GetValueOrDefault(nameof(UserId), DefaultUserId);
            set => AppSettings.AddOrUpdateValue(nameof(UserId), value);
        }
        public static string CRM_CreatedById
        {
            get => AppSettings.GetValueOrDefault(nameof(CreatedById), DefaultCreatedById);
            set => AppSettings.AddOrUpdateValue(nameof(CreatedById), value);
        }
        public static string CRM_LoginUserName
        {
            get => AppSettings.GetValueOrDefault(nameof(LoginUserName), DefaultLoginUserName);
            set => AppSettings.AddOrUpdateValue(nameof(LoginUserName), value);
        }
        public static string CRM_ForgotOtp
        {
            get => AppSettings.GetValueOrDefault(nameof(ForgotOtp), DefaultForgotOtp);
            set => AppSettings.AddOrUpdateValue(nameof(ForgotOtp), value);
        }
        public static string CRM_CallNo
        {
            get => AppSettings.GetValueOrDefault(nameof(CallNo), DefaultCallNo);
            set => AppSettings.AddOrUpdateValue(nameof(CallNo), value);
        }
        public static string CRM_WhatsappNo
        {
            get => AppSettings.GetValueOrDefault(nameof(WhatsappNo), DefaultWhatsappNo);
            set => AppSettings.AddOrUpdateValue(nameof(WhatsappNo), value);
        }
        #endregion
    }
}
