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
        int[] ia_Current_Session;//Current sets to complete

        int i_Current_Set_Number = 0;
      

		public WorkOut (String s_ExerciseKey, String s_ExerciseName)
		{
			InitializeComponent ();

            s_Exercise = s_ExerciseKey;
            e_Exercise = Storage.GetExercise(s_ExerciseKey, s_ExerciseName);


            TitleText.Text = s_ExerciseName;

            ExercisNowText.Text = s_ExerciseName + " now";

            ToCompleteText.Text = s_ExerciseName + " to complete";

            if (e_Exercise.b_setupGoal && e_Exercise.b_setupLatest)//Sets the Amount of sit ups to complete in the text fields
            {
                setUpStart();
            }
            else if (e_Exercise.b_setupGoal)
            {
                CurrentSetText.Text = "Please enter the maximum amount of "+ e_Exercise.s_Name + " you can complete";
                CurrentSetText.IsEnabled = true;
                CurrentSetText.IsVisible = true;

                CompleteButton.IsEnabled = true;
                CompleteButton.IsVisible = true;

                QuitButton.IsEnabled = true;
                QuitButton.IsVisible = true;

                SetAmount.IsVisible = true;
                SetAmount.IsEnabled = true;

                ResetButton.IsVisible = true;
                ResetButton.IsEnabled = true;
            }
            else
            {
                CurrentSetText.Text = "Please enter the amount of " + e_Exercise.s_Name + " you want to be able to do";
                CurrentSetText.IsEnabled = true;
                CurrentSetText.IsVisible = true;

                CompleteButton.IsEnabled = true;
                CompleteButton.IsVisible = true;

                QuitButton.IsEnabled = true;
                QuitButton.IsVisible = true;

                SetAmount.IsVisible = true;
                SetAmount.IsEnabled = true;

                ResetButton.IsVisible = true;
                ResetButton.IsEnabled = true;
            }
		}

        private void Start_Button_Clicked(Object sender, EventArgs e)
        {
            CompleteText.IsEnabled = true;
            CompleteText.IsVisible = true;
            ExercisNowText.IsEnabled = true;
            ExercisNowText.IsVisible = true;
            CurrentSetText.Text = "" + ia_Current_Session[0];
            CompleteButton.IsEnabled = true;
            CompleteButton.IsVisible = true;
            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;
            ToCompleteText.IsVisible = false;
            SessionText.IsVisible = false;
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
                        SecondsText.IsVisible = false;
                        TimerText.Text = "Timer Starting";
                        i_Current_Set_Number++;
                        
                        CurrentSetText.IsVisible = true;
                        CurrentSetText.Text = "" + ia_Current_Session[i_Current_Set_Number];
                        b_TimerStarted = false;

                        CompleteText.IsEnabled = true;
                        CompleteText.IsVisible = true;
                        ExercisNowText.IsEnabled = true;
                        ExercisNowText.IsVisible = true;
                    }
                    else
                    {
                        RestText.IsVisible = true;
                        
                        TimerText.IsVisible = true;
                        
                        CurrentSetText.IsVisible = false;
                        SecondsText.IsVisible = true;

                        TimerStart();
                        CompleteText.IsEnabled = false;
                        CompleteText.IsVisible = false;
                        ExercisNowText.IsEnabled = false;
                        ExercisNowText.IsVisible = false;
                    }
                    
                }
                else
                {
                    if(e_Exercise.i_Current == e_Exercise.i_Goal)
                    {
                        CurrentSetText.Text = "Congratulations!, You've completed your goal";
                    }
                    else
                    {
                        CompleteText.IsEnabled = false;
                        CompleteText.IsVisible = false;
                        ExercisNowText.IsEnabled = false;
                        ExercisNowText.IsVisible = false;
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
                        HardButton.IsVisible = true;
                    }
                    CompleteButton.IsEnabled = false;
                    CompleteButton.IsVisible = false;

                }
            }
            else if (e_Exercise.b_setupGoal)
            {
                if((SetAmount.Text != "") && (int.TryParse(SetAmount.Text, out int i))){
                    CurrentSetText.Text = "Completed";
                    e_Exercise.i_Current = i;
                    e_Exercise.b_setupLatest = true;
                    Storage.SetExercise(s_Exercise, e_Exercise);
                    //Storage.setLatest(s_Exercise, i);
                    
                   // e_Exercise.b_setupLatest = true;



                    CompleteButton.IsEnabled = false;
                    CompleteButton.IsVisible = false;

                    SetAmount.IsVisible = false;
                    SetAmount.IsEnabled = false;
                }
            }
            else
            {
                if ((SetAmount.Text != "") && (int.TryParse(SetAmount.Text, out int i))){

                    CurrentSetText.Text = "Please enter the maximum amount of " + e_Exercise.s_Name + " you can complete";
                    e_Exercise.i_Goal = i;
                    e_Exercise.b_setupGoal = true;
                    Storage.SetExercise(s_Exercise ,e_Exercise);
                    //Storage.setGoal(s_Exercise, i);
                    
                    //e_Exercise.b_setupGoal = true;

                    SetAmount.Text = "";
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
            HardButton.IsVisible = false;
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

            CurrentSetText.Text = "Please enter the amount of " + e_Exercise.s_Name + " you want to be able to do";
            CurrentSetText.IsEnabled = true;
            CurrentSetText.IsVisible = true;

            AllSetsText.Text = "";
            AllSetsText.IsEnabled = false;
            AllSetsText.IsVisible = false;

            CompleteButton.IsEnabled = true;
            CompleteButton.IsVisible = true;

            QuitButton.IsEnabled = true;
            QuitButton.IsVisible = true;

            SetAmount.IsVisible = true;
            SetAmount.IsEnabled = true;
        }

       

        private void setUpStart()
        {
            if (e_Exercise.b_LatestPreferenceEntered)
            {
                if(e_Exercise.i_LatestPreference == 3)
                {
                    e_Exercise.i_Current += 2;
                }
                else if(e_Exercise.i_LatestPreference == 1)
                {
                    e_Exercise.i_Current -= 2;
                }
                e_Exercise.b_LatestPreferenceEntered = false;
            }
            if (e_Exercise.i_Current > e_Exercise.i_Goal)
            {
                e_Exercise.i_Current = e_Exercise.i_Goal;
                e_Exercise.i_NumberOfSets = 1;
            }
            else if (((float)e_Exercise.i_Current / (float)e_Exercise.i_NumberOfSets) >= ((float)e_Exercise.i_Goal / (float)e_Exercise.i_NumberOfSets) * 0.9f)
            {
                e_Exercise.i_NumberOfSets--;
            }
            
            ia_Current_Session = new int[e_Exercise.i_NumberOfSets];
            int i_current_session = e_Exercise.i_Current;

            for (int i = e_Exercise.i_NumberOfSets-1; i >= 0; i--)
            {
                if ((AllSetsText.Text != null) || (AllSetsText.Text == ""))
                {
                    AllSetsText.Text = " - " + AllSetsText.Text;
                }
                AllSetsText.Text = (i_current_session / (i + 1)) + AllSetsText.Text;
                ia_Current_Session[i] = i_current_session / (i + 1);
                i_current_session -= i_current_session / (i + 1);
            }

            ToCompleteText.IsVisible = true;
            SessionText.IsVisible = true;

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