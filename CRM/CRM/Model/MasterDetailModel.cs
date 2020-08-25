using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRM.Model
{
    public class MasterDetailModel : BaseViewModel
    {
        //public string icon { get; set; }
        //public bool seperator_visible { get; set; }
        //public string title { get; set; }

        private string _roll;
        public string roll
        {
            get => _roll;
            set
            {
                _roll = value;
                OnPropertyChanged("roll");
            }
        }

        private int _id;
        public int id
        {
            get => _id;
            set
            {
                _id= value;
                OnPropertyChanged("id");
            }
        }

        private string _icon;
        public string icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged("icon");
            }
        }

        private string _title;
        public string title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("title ");
            }
        }

        private bool _seperator_visible;
        public bool seperator_visible
        {
            get => _seperator_visible;
            set
            {
                _seperator_visible = value;
                OnPropertyChanged("seperator_visible");
            }
        }

        private string _title_text_color;
        public string title_text_color
        {
            get => _title_text_color;
            set
            {
                _title_text_color = value;
                OnPropertyChanged("title_text_color");
            }
        }
        
        private string _seperator_color;

        public MasterDetailModel(INavigation navigation) : base(navigation)
        {
        }

        public string seperator_color
        {
            get => _seperator_color;
            set
            {
                _seperator_color = value;
                OnPropertyChanged("title_text_color");
            }
        }

    }
}
