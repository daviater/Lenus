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
        string[][] as_Exercises;
        Button[] ab_Buttons;

        public MainPage()
        {
            InitializeComponent();

            as_Exercises = Storage.getMenu();

            ab_Buttons = new Button[as_Exercises[0].Length];

            for(int i = 0; i < as_Exercises[0].Length; i++)
            {
                ab_Buttons[i] = new Button
                {
                    Text = as_Exercises[0][i],
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    
                };
                ab_Buttons[i].Clicked += Work_Out_Clicked;

                MainLayout.Children.Add(ab_Buttons[i]);
            }
        }

        public void Work_Out_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkOut(as_Exercises[1][Array.IndexOf(ab_Buttons,(Button)sender)], as_Exercises[0][Array.IndexOf(ab_Buttons, (Button)sender)]));
        }
    }
}
