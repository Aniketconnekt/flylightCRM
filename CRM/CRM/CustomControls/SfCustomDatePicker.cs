using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CRM.CustomControls
{
    public class SfCustomDatePicker : SfPicker
    {
        #region Public Properties
        public ObservableCollection<string> Headers { get; set; }
        public ObservableCollection<object> Date { get; set; }
        internal ObservableCollection<object> Day { get; set; }
        internal ObservableCollection<object> Month { get; set; }
        internal ObservableCollection<object> Year { get; set; }
        #endregion

        public SfCustomDatePicker()
        {
            Date = new ObservableCollection<object>();
            Day = new ObservableCollection<object>();
            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();
            PopulateDateCollection();
            this.ItemsSource = Date;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
            Headers = new ObservableCollection<string>();
            Headers.Add("Day");
            Headers.Add("Month");
            Headers.Add("Year");
            HeaderText = "Calendar";
            this.ColumnHeaderText = Headers;
            ShowFooter = true;
            ShowHeader = true;
            ShowColumnHeader = true;
        }
        private void CustomDatePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }
        public void UpdateDays(ObservableCollection<object> Date, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                    {
                        if (Date.Count == 3)
                        {
                            bool flag = false;
                            if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == 3 && (e.NewValue as ObservableCollection<object>).Count == 3)
                            {
                                if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                                    flag = true;
                                if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                                    flag = true;
                            }

                            if (flag)
                            {
                                ObservableCollection<object> days = new ObservableCollection<object>();
                        //int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        //int year = int.Parse((e.NewValue as IList)[2].ToString());
                        //for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        //{
                        //    if (j < 10)
                        //        days.Add("0" + j);
                        //    else
                        //        days.Add(j.ToString());
                        //}
                        ObservableCollection<object> PreviousValue = new ObservableCollection<object>();

                                foreach (var item in e.NewValue as IList)
                                    PreviousValue.Add(item);

                                if (days.Count > 0)
                                {
                                    Date.RemoveAt(1);
                                    Date.Insert(1, days);
                                }

                                if ((Date[1] as IList).Contains(PreviousValue[1]))
                                    this.SelectedItem = PreviousValue;
                                else
                                {
                                    PreviousValue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                                    this.SelectedItem = PreviousValue;
                                }
                            }
                        }
                    });
            }
            catch (Exception ex)
            {
            }
        }
        private void PopulateDateCollection()
        {
            try
            {
                //populate months       
                for (int i = 1; i < 13; i++)
                {
                    //if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))
                    //    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    //Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));

                    if (i < 10)
                        Month.Add("0" + i);
                    else
                        Month.Add(i.ToString());
                }
                //populate year
                for (int i = 1900; i < 2050; i++)
                    Year.Add(i.ToString());

                //populate Days
                for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
                {
                    if (i < 10)
                        Day.Add("0" + i);
                    else
                        Day.Add(i.ToString());
                }
                Date.Add(Day);
                Date.Add(Month);
                Date.Add(Year);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
