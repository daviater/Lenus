using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkoutApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkOut : ContentPage
	{

        Exercise e_Exercise;

        int i_TimeSet = 30;
        
        static bool b_TimerStarted = false;

        string s_Exercise;
        int i_Highest_Total;//Highest sit ups completed
        //int i_Goal;//Sit up goal
        int[] ia_Current_Session;//Current sets to complete
        DateTime dt_Latest_Date;//Date of latest completed workout
        //int i_Latest_Total = 0;//Latest_Total
        int i_Current_Set_Number = 0;
        //bool b_Setup;//True if a goal has been set
        //bool b_Latest_Entry;//True if sit ups have been completed before

		public WorkOut (String s_ExerciseKey, String s_ExerciseName)
		{
			InitializeComponent ();

            s_Exercise = s_ExerciseKey;
            e_Exercise = Storage.GetExercise(s_ExerciseKey, s_ExerciseName);

            //i_Goal = Storage.getGoal(s_Exercise);
            //i_Latest_Total = Storage.getLatest(s_Exercise);

           /* if(i_Goal == 0)
            {
                b_Setup = false;
            }
            else
            {
                b_Setup = true;
            }*/

            /*if (i_Latest_Total == 0){
                b_Latest_Entry = false;
            }
            else{
                b_Latest_Entry = true;
            }*/
            
            

            if (e_Exercise.b_setupGoal && e_Exercise.b_setupLatest)//Sets the Amount of sit ups to complete in the text fields
            {
                setUpStart();
            }
            else if (e_Exercise.b_setupGoal)
            {
                CurrentSetText.Text = "Please enter the maximum amount of sit ups you can complete";
                CurrentSetText.IsEnabled = true;
                CurrentSetText.IsVisible = true;

                CompleteButton.IsEnabled = true;
                CompleteButton.IsVisible = true;

                QuitButton.IsEnabled = true;
                QuitButton.IsVisible = true;

                SitUpAmount.IsVisible = true;
                SitUpAmount.IsEnabled = true;

                ResetButton.IsVisible = true;
                ResetButton.IsEnabled = true;
            }
            else
            {
                CurrentSetText.Text = "Please enter the amount of sit ups you want to be able to do";
                CurrentSetText.IsEnabled = true;
                CurrentSetText.IsVisible = true;

                CompleteButton.IsEnabled = true;
                CompleteButton.IsVisible = true;

                QuitButton.IsEnabled = true;
                QuitButton.IsVisible = true;

                SitUpAmount.IsVisible = true;
                SitUpAmount.IsEnabled = true;

                ResetButton.IsVisible = true;
                ResetButton.IsEnabled = true;
            }
		}

        private void Start_Button_Clicked(Object sender, EventArgs e)
        {
            //setUpSets();
            CurrentSetText.Text = "" + ia_Current_Session[0];
            CompleteButton.IsEnabled = true;
            CompleteButton.IsVisible = true;
            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;
        }

        private void Complete_Button_Clicked(Object sender, EventArgs e)
        {
            if (e_Exercise.b_setupGoal && e_Exercise.b_setupLatest)
            {
                if(i_Current_Set_Number < 7)// sets next set of sit ups
                {

                    if (b_TimerStarted)
                    {
                        TimerText.TimerStop();
                        TimerText.IsVisible = false;
                        RestText.IsVisible = false;
                        TimerText.Text = "Timer Starting";
                        i_Current_Set_Number++;
                        
                        CurrentSetText.IsVisible = true;
                        CurrentSetText.Text = "" + ia_Current_Session[i_Current_Set_Number];
                        b_TimerStarted = false;
                    }
                    else
                    {
                        RestText.IsVisible = true;
                        
                        TimerText.IsVisible = true;
                        
                        CurrentSetText.IsVisible = false;


                        TimerStart();
                        
                    }
                    
                }
                else
                {
                    CurrentSetText.Text = "Completed, How was your workout?";
                    e_Exercise.i_Current += 8;
                    e_Exercise.b_setupLatest = true;
                    Storage.SetExercise(s_Exercise, e_Exercise);
                    //Storage.setLatest(s_Exercise, e_Exercise.i_Current + 8);

                    EasyButton.IsEnabled = true;
                    EasyButton.IsVisible = true;
                    PerfectButton.IsEnabled = true;
                    PerfectButton.IsVisible = true;
                    HardButton.IsEnabled = true;
                    HardButton.IsEnabled = true;

                }
            }
            else if (e_Exercise.b_setupGoal)
            {
                if((SitUpAmount.Text != "") && (int.TryParse(SitUpAmount.Text, out int i))){
                    CurrentSetText.Text = "Completed";
                    e_Exercise.i_Current = i;
                    e_Exercise.b_setupLatest = true;
                    Storage.SetExercise(s_Exercise, e_Exercise);
                    //Storage.setLatest(s_Exercise, i);
                    
                   // e_Exercise.b_setupLatest = true;



                    CompleteButton.IsEnabled = false;
                    CompleteButton.IsVisible = false;

                    SitUpAmount.IsVisible = false;
                    SitUpAmount.IsEnabled = false;
                }
            }
            else
            {
                if ((SitUpAmount.Text != "") && (int.TryParse(SitUpAmount.Text, out int i))){

                    CurrentSetText.Text = "Please enter the maximum amount of sit ups you can complete";
                    e_Exercise.i_Goal = i;
                    e_Exercise.b_setupGoal = true;
                    Storage.SetExercise(s_Exercise ,e_Exercise);
                    //Storage.setGoal(s_Exercise, i);
                    
                    //e_Exercise.b_setupGoal = true;

                    SitUpAmount.Text = "";
                }
            }
        }

        private void Preference_Button_Clicked(Object sender, EventArgs e)
        {
            if( (Button)sender == EasyButton)
            {
                CurrentSetText.Text = "Great! It'll be harder next time";
                e_Exercise.i_LatestPreference = 1;
                e_Exercise.b_LatestPreferenceEntered = true;
            }
            if((Button)sender == PerfectButton)
            {
                CurrentSetText.Text = "Awesome!";
                e_Exercise.i_LatestPreference = 2;
                e_Exercise.b_LatestPreferenceEntered = true;
            }
            if((Button)sender == HardButton)
            {
                CurrentSetText.Text = "don't worry, It'll be easier next time";
                e_Exercise.i_LatestPreference = 3;
                e_Exercise.b_LatestPreferenceEntered = true;
            }
            Storage.SetExercise(s_Exercise, e_Exercise);

            EasyButton.IsEnabled = false;
            EasyButton.IsVisible = false;
            PerfectButton.IsEnabled = false;
            PerfectButton.IsVisible = false;
            HardButton.IsEnabled = false;
            HardButton.IsEnabled = false;
        }

        private void Quit_Button_Clicked(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Reset_Button_Clicked(Object sender, EventArgs e)
        {
            e_Exercise.b_setupGoal = false;
            e_Exercise.b_setupLatest = false;

            Storage.resetValues(s_Exercise, e_Exercise);

            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;

            CurrentSetText.Text = "Please enter the amount of sit ups you want to be able to do";
            CurrentSetText.IsEnabled = true;
            CurrentSetText.IsVisible = true;

            AllSetsText.Text = "";
            AllSetsText.IsEnabled = false;
            AllSetsText.IsVisible = false;

            CompleteButton.IsEnabled = true;
            CompleteButton.IsVisible = true;

            QuitButton.IsEnabled = true;
            QuitButton.IsVisible = true;

            SitUpAmount.IsVisible = true;
            SitUpAmount.IsEnabled = true;
        }

       

        private void setUpStart()
        {
            ia_Current_Session = new int[8];
            int i_current_session = e_Exercise.i_Current;

            for (int i = 7; i >= 0; i--)
            {
                if ((AllSetsText.Text != null) || (AllSetsText.Text == ""))
                {
                    AllSetsText.Text = " - " + AllSetsText.Text;
                }
                AllSetsText.Text = (i_current_session / (i + 1)) + AllSetsText.Text;
                ia_Current_Session[i] = i_current_session / (i + 1);
                i_current_session -= i_current_session / (i + 1);
            }

            AllSetsText.IsEnabled = true;
            AllSetsText.IsVisible = true;

            CurrentSetText.Text = "" + e_Exercise.i_Current;
            CurrentSetText.IsEnabled = true;
            CurrentSetText.IsVisible = true;


            StartButton.IsEnabled = true;
            StartButton.IsVisible = true;

            QuitButton.IsEnabled = true;
            QuitButton.IsVisible = true;

            ResetButton.IsVisible = true;
            ResetButton.IsEnabled = true;
        }


        private void TimerStart()
        {
            b_TimerStarted = true;
            TimerText.SetValue(CountDownTimer.CountDownSecondsProperty, i_TimeSet);
            TimerText.SetValue(CountDownTimer.CountDownMinutesProperty, 0);
            TimerText.TimerStart();
        }
    }
}