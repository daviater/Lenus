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
                    BackgroundColor = Color.White
                };
                ab_Buttons[i].Clicked += Work_Out_Clicked;

                InnerStack.Children.Add(ab_Buttons[i]);
            }
        }

        private void Enter_Button_Clicked(Object sender, EventArgs e)
        {
            if(NewWorkoutEntry.Text != "") {
                Storage.addMenuItem(NewWorkoutEntry.Text);
            }

            foreach (Button b in ab_Buttons)
            {
                InnerStack.Children.Remove(b);
            }

            as_Exercises = Storage.getMenu();

            ab_Buttons = new Button[as_Exercises[0].Length];

            for (int i = 0; i < as_Exercises[0].Length; i++)
            {
                ab_Buttons[i] = new Button
                {
                    Text = as_Exercises[0][i],
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White
                };
                ab_Buttons[i].Clicked += Work_Out_Clicked;

                InnerStack.Children.Add(ab_Buttons[i]);
            }

            AddWorkoutButton.Text = "Add Workout";
            AddWorkoutButton.Clicked -= Cancel_Button_Clicked;
            AddWorkoutButton.Clicked += Add_Button_Clicked;
            Entrybutton.IsEnabled = false;
            Entrybutton.IsVisible = false;
            EnterNameText.IsEnabled = false;
            EnterNameText.IsVisible = false;
            NewWorkoutEntry.IsVisible = false;
            NewWorkoutEntry.IsEnabled = false;
        }

        private void Cancel_Button_Clicked(Object sender, EventArgs e)
        {
            foreach (Button b in ab_Buttons)
            {
                b.IsEnabled = true;
                b.IsVisible = true;
            }

            AddWorkoutButton.Text = "Add Workout";
            AddWorkoutButton.Clicked -= Cancel_Button_Clicked;
            AddWorkoutButton.Clicked += Add_Button_Clicked;
            Entrybutton.IsVisible = false;
            Entrybutton.IsEnabled = false;
            EnterNameText.IsEnabled = false;
            EnterNameText.IsVisible = false;
            NewWorkoutEntry.IsVisible = false;
            NewWorkoutEntry.IsEnabled = false;
        }

        private void Add_Button_Clicked(Object sender, EventArgs e)
        {
            foreach(Button b in ab_Buttons)
            {
                b.IsEnabled = false;
                b.IsVisible = false;
            }

            AddWorkoutButton.Text = "Cancel";
            AddWorkoutButton.Clicked -= Add_Button_Clicked;
            AddWorkoutButton.Clicked += Cancel_Button_Clicked;
            Entrybutton.IsVisible = true;
            Entrybutton.IsEnabled = true;
            EnterNameText.IsEnabled = true;
            EnterNameText.IsVisible = true;
            NewWorkoutEntry.IsVisible = true;
            NewWorkoutEntry.IsEnabled = true;
        }

        private void Delete_Button_Clicked(Object sender, EventArgs e)
        {
            foreach (Button b in ab_Buttons)
            {
                b.Clicked -= Work_Out_Clicked;
                b.Clicked += DeleteThis;
                b.BackgroundColor = Color.Red;
            }

            DeleteWorkoutbutton.Text = "Cancel";
            DeleteWorkoutbutton.Clicked -= Delete_Button_Clicked;
            DeleteWorkoutbutton.Clicked += CancelDelete;
        }

        private void CancelDelete(Object sender, EventArgs e)
        {
            foreach (Button b in ab_Buttons)
            {
                b.Clicked -= DeleteThis;
                b.Clicked += Work_Out_Clicked;
                b.BackgroundColor = Color.White;
            }

            DeleteWorkoutbutton.Text = "Delete Workout";
            DeleteWorkoutbutton.Clicked -= CancelDelete;
            DeleteWorkoutbutton.Clicked += Delete_Button_Clicked;
        }

        private void DeleteThis(Object sender, EventArgs e)
        {
            DeleteWorkoutbutton.Text = "Delete Workout";
            DeleteWorkoutbutton.Clicked -= CancelDelete;
            DeleteWorkoutbutton.Clicked += Delete_Button_Clicked;

            Storage.removeMenuItem(Array.IndexOf(ab_Buttons, (Button)sender));

            foreach(Button b in ab_Buttons)
            {
                InnerStack.Children.Remove(b);
            }

            as_Exercises = Storage.getMenu();

            ab_Buttons = new Button[as_Exercises[0].Length];

            for (int i = 0; i < as_Exercises[0].Length; i++)
            {
                ab_Buttons[i] = new Button
                {
                    Text = as_Exercises[0][i],
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White
                };
                ab_Buttons[i].Clicked += Work_Out_Clicked;

                InnerStack.Children.Add(ab_Buttons[i]);
            }
        }

        private void Work_Out_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkOut(as_Exercises[1][Array.IndexOf(ab_Buttons,(Button)sender)], as_Exercises[0][Array.IndexOf(ab_Buttons, (Button)sender)]));
        }
    }
}
