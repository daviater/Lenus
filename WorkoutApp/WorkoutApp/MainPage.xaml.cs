using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void Sit_Ups_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new SitUps());
        }
    }
}
