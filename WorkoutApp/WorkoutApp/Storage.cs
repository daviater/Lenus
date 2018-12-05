using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp
{
    class Storage
    {

        static String GOAL = "_goal";
        static String LATEST = "_latest";

        public static int getGoal(String address)
        {
            String _goal = null;
            int i_Goal = 0;
            if (Application.Current.Properties.ContainsKey(address + GOAL))//Checks if the goal has been entered before
            {
                _goal = (Application.Current.Properties[address + GOAL].ToString());//pulls data from dictionary
            }

            if (_goal != null)
            {
                i_Goal = int.Parse(_goal);//parses goal to int  
            }

            return i_Goal;
        }

        public static int getLatest(String address)
        {
            String _latest = null;
            int i_Latest = 0;
            if (Application.Current.Properties.ContainsKey(address + LATEST))//Checks if the goal has been entered before
            {
                _latest = (Application.Current.Properties[address + LATEST].ToString());//pulls data from dictionary
            }

            if (_latest != null)
            {
                i_Latest = int.Parse(_latest);//parses goal to int  
            }

            return i_Latest;
        }

        public static void setGoal(String address, int amount)
        {
            Application.Current.Properties[address + GOAL] = "" + amount;
            Application.Current.SavePropertiesAsync();
        }

        public static void setLatest(String address, int amount)
        {
            Application.Current.Properties[address + LATEST] = "" + amount;
            Application.Current.SavePropertiesAsync();
        }

        public static void resetValues(String address)
        {
            Application.Current.Properties[address + GOAL] = null;
            Application.Current.Properties[address + LATEST] = null;
            Application.Current.SavePropertiesAsync();
        }
    }
}
