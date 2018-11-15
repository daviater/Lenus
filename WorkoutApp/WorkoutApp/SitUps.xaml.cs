using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SitUps : ContentPage
	{
        int i_Latest_Total;//Latest sit ups completed
        int i_Highest_Total;//Highest sit ups completed
        int i_Goal;//Sit up goal
        int[] ia_Current_Session;//Current sets to complete
        DateTime dt_Latest_Date;//Date of latest completed workout
        int i_Current_Set = 0;//Current set
        int i_Current_Set_Number = 0;
        bool b_Setup;//True if a goal has been set
        bool b_Latest_Entry;//True if sit ups have been completed before

		public SitUps ()
		{
			InitializeComponent ();

            string _goal = null;
            string _latest = null;

            try {
                _goal = (Application.Current.Properties["situp_goal"].ToString());
                _latest = (Application.Current.Properties["situp_latest"].ToString());
            }
            catch(Exception e)
            {
               
            }
            
            if (_goal != null)
            {
                i_Goal = int.Parse(_goal);
                b_Setup = true;
                if(_latest != null)
                {
                    i_Current_Set = int.Parse(_latest) + 8;
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
            }

            if (b_Setup && b_Latest_Entry)
            {
                ia_Current_Session = new int[8];
                int i_current_session = i_Current_Set;
                for(int i = 7; i >= 0; i--)
                {
                    ia_Current_Session[i] = i_current_session/(i + 1);
                    i_current_session -= i_current_session / (i + 1);
                }

                CurrentSetText.Text = "" + ia_Current_Session[0];
                CurrentSetText.IsEnabled = true;
                CurrentSetText.IsVisible = true;

                CompleteButton.IsEnabled = true;
                CompleteButton.IsVisible = true;

                QuitButton.IsEnabled = true;
                QuitButton.IsVisible = true;
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

                SetUpAmount.IsVisible = true;
                SetUpAmount.IsEnabled = true;
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

                SetUpAmount.IsVisible = true;
                SetUpAmount.IsEnabled = true;
            }
		}

        private void Complete_Button_Clicked(Object sender, EventArgs e)
        {
            if (b_Setup && b_Latest_Entry)
            {
                if(i_Current_Set_Number < 7)
                {
                    i_Current_Set_Number++;
                    CurrentSetText.Text = "" + ia_Current_Session[i_Current_Set_Number];
                }
                else
                {
                    CurrentSetText.Text = "Completed";
                    Application.Current.Properties["situp_latest"] = "" + (i_Latest_Total + 8);
                    Application.Current.SavePropertiesAsync();
                }
            }
            else if (b_Setup)
            {
                if((SetUpAmount.Text != "") && (int.TryParse(SetUpAmount.Text, out int i))){
                    CurrentSetText.Text = "Completed";
                    Application.Current.Properties["situp_latest"] = "" + int.Parse(SetUpAmount.Text);
                    Application.Current.SavePropertiesAsync();
                    b_Latest_Entry = true;
                }
            }
            else
            {
                if ((SetUpAmount.Text != "") && (int.TryParse(SetUpAmount.Text, out int i))){
                    CurrentSetText.Text = "Please enter the maximum amount of sit ups you can complete";
                    Application.Current.Properties["situp_goal"] = "" + int.Parse(SetUpAmount.Text);
                    Application.Current.SavePropertiesAsync();
                    b_Setup = true;

                    CompleteButton.IsEnabled = false;
                    CompleteButton.IsVisible = false;
                }
            }
        }

        private void Quit_Button_Clicked(Object sender, EventArgs e)
        {

        }

    }
}