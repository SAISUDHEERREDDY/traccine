using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using traccine.Helpers;
using traccine.Models;
using Xamarin.Forms;

namespace traccine.ViewModels
{
    public class TimelineViewModel :INotifyPropertyChanged
    {
      
        public ObservableCollection<TimeLineModel> TimeLine { get; set; }
        public ICommand ViewCommand { get; set; }
        public string CurrentDate { get; set; }
        public TimelineViewModel()
        {
            ViewCommand = new Command(ViewCommandasync);
            TimeLine = new ObservableCollection<TimeLineModel>();
            var dateNow = DateTime.Now;
            CurrentDate =String.Format("{0:dddd, MMMM d, yyyy}", dateNow);
            ViewCommandasync(dateNow.Day);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void ViewCommandasync(object id)
        {
            TimeLine.Clear();
            int intObj = Convert.ToInt32(id);
            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, intObj, 4, 5, 6);
            CurrentDate = String.Format("{0:dddd, MMMM d, yyyy}", date);
            var data = await App.Database.GetEventsbyDate(dateNow, date);
            foreach (var _timeline in data)
            {
                TimeLine.Add(_timeline);
            }
          
        }

      
    }
}
