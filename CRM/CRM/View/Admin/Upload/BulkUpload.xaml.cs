using CRM.ViewModel.AdminViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Upload
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulkUpload : ContentPage
    {
        BulkUploadViewModel _bulkUploadViewModel;
        public BulkUpload()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _bulkUploadViewModel = new BulkUploadViewModel(Navigation);
            _bulkUploadViewModel.Initialize();
        }
    }
}