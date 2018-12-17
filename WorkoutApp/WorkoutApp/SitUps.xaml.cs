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
	public partial class SitUps : ContentPage
	{
        Thread timerThread;
        int i_TimeSet = 30;
        float f_TimeFloat;
        static bool b_TimerStarted = false;
        static bool b_TimerFinished = false;

        int i_Highest_Total;//Highest sit ups completed
        int i_Goal;//Sit up goal
        int[] ia_Current_Session;//Current sets to complete
        DateTime dt_Latest_Date;//Date of latest completed workout
        int i_Latest_Total = 0;//Latest_Total
        int i_Current_Set_Number = 0;
        bool b_Setup;//True if a goal has been set
        bool b_Latest_Entry;//True if sit ups have been completed before

		public SitUps ()
		{
			InitializeComponent ();

            //string _goal = null;
            //string _latest = null;

            

            i_Goal = Storage.getGoal("situp");
            i_Latest_Total = Storage.getLatest("situp");

            if(i_Goal == 0)
            {
                b_Setup = false;
            }
            else
            {
                b_Setup = true;
            }

            if (i_Latest_Total == 0){
                b_Latest_Entry = false;
            }
            else{
                b_Latest_Entry = true;
            }
            
            /*if (Application.Current.Properties.ContainsKey("situp_goal"))//Checks if the goal has been entered before
            {
                _goal = (Application.Current.Properties["situp_goal"].ToString());//pulls data from dictionary
            }

            if (Application.Current.Properties.ContainsKey("situps_latest"))//Checks if situps have been completed before
            {
                _latest = (Application.Current.Properties["situp_latest"].ToString());//puls data from dictionary
            }

            
            if (_goal != null)
            {
                i_Goal = int.Parse(_goal);//parses goal to int
                b_Setup = true;
                if(_latest != null)
                {
                    i_Latest_Total = int.Parse(_latest) + 8;//Sets todays sets to 8 higher than the last one
                    b_Latest_Entry = true;
                }
                else
                {
                    b_Latest_Entry = false;
                }
            }
            else
            {
                b_Setup = false;
                b_Latest_Entry = false;
            }*/

            if (b_Setup && b_Latest_Entry)//Sets the Amount of sit ups to complete in the text fields
            {
                setUpStart();
            }
            else if (b_Setup)
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
            setUpSets();
        }

        private void Complete_Button_Clicked(Object sender, EventArgs e)
        {
            if (b_Setup && b_Latest_Entry)
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
                        //timerThread = new Thread(TimerStart);

                        //timerThread.Start();
                    }
                    
                }
                else
                {
                    CurrentSetText.Text = "Completed";
                    Storage.setLatest("situp", i_Latest_Total + 8);
                    //Application.Current.Properties["situp_latest"] = "" + (i_Latest_Total + 8);
                   //Application.Current.SavePropertiesAsync();
                }
            }
            else if (b_Setup)
            {
                if((SitUpAmount.Text != "") && (int.TryParse(SitUpAmount.Text, out int i))){
                    CurrentSetText.Text = "Completed";
                    Storage.setLatest("situp", i);
                    //Application.Current.Properties["situp_latest"] = "" + i;
                    //Application.Current.SavePropertiesAsync();
                    b_Latest_Entry = true;



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
                    Storage.setGoal("situp", i);
                    //Application.Current.Properties["situp_goal"] = "" + i;
                    //Application.Current.SavePropertiesAsync();
                    b_Setup = true;

                    SitUpAmount.Text = "";
                }
            }
        }

        private void Quit_Button_Clicked(Object sender, EventArgs e)
        {

        }

        private void Reset_Button_Clicked(Object sender, EventArgs e)
        {
            b_Setup = false;
            b_Latest_Entry = false;

            Storage.resetValues("situp");

            /*Application.Current.Properties["situp_latest"] = null;
            Application.Current.SavePropertiesAsync();

            Application.Current.Properties["situp_goal"] = null;
            Application.Current.SavePropertiesAsync();*/

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

        private void setUpSets()
        {
            ia_Current_Session = new int[8];
            int i_current_session = i_Latest_Total;

            AllSetsText.Text = "";

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

            CurrentSetText.Text = "" + ia_Current_Session[0];
            CurrentSetText.IsEnabled = true;
            CurrentSetText.IsVisible = true;

            CompleteButton.IsEnabled = true;
            CompleteButton.IsVisible = true;

            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;

            QuitButton.IsEnabled = true;
            QuitButton.IsVisible = true;

            ResetButton.IsVisible = true;
            ResetButton.IsEnabled = true;
        }

        private void setUpStart()
        {
            ia_Current_Session = new int[8];
            int i_current_session = i_Latest_Total;

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

            CurrentSetText.Text = "" + i_Latest_Total;
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