using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutApp
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            //InitializeComponent();
        }

        private void sitUps_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SitUps());
        }
    }
}
