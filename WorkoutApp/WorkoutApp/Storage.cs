using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp
{
    class Storage
    {
        static String SETUPGOAL = "_setupgoal";
        static String SETUPLATEST = "_setuplatest";
        static String GOAL = "_goal";
        static String LATEST = "_latest";
        static String DAY = "_day";
        static String NUMBEROFSETS = "_numberofsets";
        static String TYPE = "_type";
        static String NAME = "_name";
        static String LATESTPREFERENCE = "_latestPreference";
        static String LATESTSET = "_latestset";
        static String DISTANCEGOAL = "_distancegoal";
        static String DISTANCELATEST = "_distancelatest";
        static String TIMEGOAL = "_timegoal";
        static String TIMELATEST = "_timelatest";
        static String LATESTSETTIME = "_latestsettime";
        static String LATESTRESTTIME = "_latestresttime";
        static String LATESTSETPREFERENCE = "_latestsetpreference";
        static String LATESTPREFERENCEENTERED = "_latestpreferenceentered";


        public static Exercise GetExercise(string s_Exercise, string s_Name)
        {
            Exercise e_Exercise = new Exercise();


            e_Exercise.b_setupGoal = get<bool>(s_Exercise, SETUPGOAL);
            e_Exercise.b_setupLatest = get<bool>(s_Exercise, SETUPLATEST);
            e_Exercise.i_Goal = get<int>(s_Exercise, GOAL);
            e_Exercise.i_Current = get<int>(s_Exercise, LATEST);

            e_Exercise.i_day = get<int>(s_Exercise, DAY);
            e_Exercise.i_NumberOfSets = get<int>(s_Exercise, NUMBEROFSETS);
            e_Exercise.i_Type = get<int>(s_Exercise, TYPE);
            e_Exercise.s_Name = get<string>(s_Exercise, NAME);
            e_Exercise.i_LatestPreference = get<int>(s_Exercise, LATESTPREFERENCE);
            e_Exercise.ai_LatestSet = getArray<int>(s_Exercise, LATESTSET);
            e_Exercise.af_LatestRestTime = getArray<float>(s_Exercise, LATESTRESTTIME);
            e_Exercise.ai_LatestSetPreference = getArray<int>(s_Exercise, LATESTSETPREFERENCE);
            e_Exercise.af_LatestSetTime = getArray<float>(s_Exercise, LATESTSETTIME);
            e_Exercise.b_LatestPreferenceEntered = get<bool>(s_Exercise, LATESTPREFERENCEENTERED);

            return e_Exercise;
        }

        public static void SetExercise(string s_Exercise, Exercise e_Exercise)
        { 
            set<int>(s_Exercise, GOAL, e_Exercise.i_Goal);
            set<bool>(s_Exercise, SETUPGOAL, e_Exercise.b_setupGoal);
            set<int>(s_Exercise, LATEST, e_Exercise.i_Current);
            set<bool>(s_Exercise, SETUPLATEST, e_Exercise.b_setupLatest);
            set<int>(s_Exercise, DAY, e_Exercise.i_day);
            set<int>(s_Exercise, NUMBEROFSETS, e_Exercise.i_NumberOfSets);
            set<int>(s_Exercise, TYPE, e_Exercise.i_Type);
            set<string>(s_Exercise, NAME, e_Exercise.s_Name);
            set<int>(s_Exercise, LATESTPREFERENCE, e_Exercise.i_LatestPreference);
            setArray<int>(s_Exercise, LATESTSET, e_Exercise.ai_LatestSet);
            setArray<float>(s_Exercise, LATESTRESTTIME, e_Exercise.af_LatestRestTime);
            setArray<int>(s_Exercise, LATESTSETPREFERENCE, e_Exercise.ai_LatestSetPreference);
            setArray<float>(s_Exercise, LATESTSETTIME, e_Exercise.af_LatestSetTime);
            set<bool>(s_Exercise, LATESTPREFERENCEENTERED, e_Exercise.b_LatestPreferenceEntered);

            
           
        }

        private static T get<T>(String address, String type)
        {
            T value ;


            String _value = null;
           
            if (Application.Current.Properties.ContainsKey(address + type))
            {
                Console.WriteLine(_value);
                _value = ((String)Application.Current.Properties[address + type]);
            }

            if (_value != null)
            {
                value = (T)Convert.ChangeType(_value, typeof(T));  
            }
            else
            {
                value = (T)Convert.ChangeType(0, typeof(T));
            }

            return value;
        }

        private static T[] getArray<T>(String address, String type)
        {
            T[] value;


            
            String _amount = null;
            int amount;

            if (Application.Current.Properties.ContainsKey(address + type + "amount"))
            {
                _amount = ((String)Application.Current.Properties[address + type + "amount"]);
            }

            if (_amount != null)
            {
                amount = int.Parse(_amount);
                value = new T[amount];
                for(int i = 0; i < amount; i++)
                {
                    String _value = null;
                    if (Application.Current.Properties.ContainsKey(address + type + i))
                    {
                        _value = ((String)Application.Current.Properties[address + type + i]);
                    }
                    if(_value != null)
                    {
                        value[i] = (T)Convert.ChangeType(_value, typeof(T));
                    }
                    else
                    {
                        value[i] = (T)Convert.ChangeType(0, typeof(T));
                    }
                }
            }
            else
            {
                amount = 0;
                value = new T[] { (T)Convert.ChangeType(0, typeof(T)), (T)Convert.ChangeType(0, typeof(T)), (T)Convert.ChangeType(0, typeof(T)), (T)Convert.ChangeType(0, typeof(T)) };
            }


            return value;
        }

        private static void set<T>(String address, String type, T item)
        {
            Application.Current.Properties[address + type] = "" + item.ToString();
            Application.Current.SavePropertiesAsync();
        }

        private static void setArray<T>(String address, String type, T[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Application.Current.Properties[address + type + i] = "" + array[i].ToString();
                Application.Current.SavePropertiesAsync();
            }
        }
        

        public static void resetValues(String address, Exercise e_Exercise)
        {
            e_Exercise.reset();
            SetExercise(address, e_Exercise);
        }

        public static string[][] setUpMenu()
        {
            string[] as_MenuNames = { "Sit Ups", "Push Ups", "Squats", "Lunges" };
            string[] as_MenuKeys = { "situps", "pushups", "squats", "lunges" };
            string[][] as_MenuItems = { as_MenuNames, as_MenuKeys };


            Application.Current.Properties["MenuAmount"] = "" + as_MenuKeys.Length;

            for(int i = 0; i < as_MenuKeys.Length; i++)
            {
                Application.Current.Properties["MenuName"+i] = "" + as_MenuNames[i];
                Application.Current.Properties["MenuKey"+i] = "" + as_MenuKeys[i];
            }

            Application.Current.SavePropertiesAsync();

            return as_MenuItems;
        }

        public static string[][] getMenu()
        {
            string[] as_MenuNames;
            string[] as_MenuKeys;
            string[][] as_MenuItems;
            int i_MenuAmount;
            string s_MenuAmount = null;

            if (Application.Current.Properties.ContainsKey("MenuAmount"))
            {
                s_MenuAmount = ((String)Application.Current.Properties["MenuAmount"]);
            }

            if (s_MenuAmount != null)
            {
                i_MenuAmount = int.Parse(s_MenuAmount);

                as_MenuNames = new string[i_MenuAmount];
                as_MenuKeys = new string[i_MenuAmount];
                for(int i = 0; i < i_MenuAmount; i++)
                {
                    if (Application.Current.Properties.ContainsKey("MenuName"+i))
                    {
                        as_MenuNames[i] = ((String)Application.Current.Properties["MenuName"+i]);
                    }
                    if (Application.Current.Properties.ContainsKey("MenuKey" + i))
                    {
                        as_MenuKeys[i] = ((String)Application.Current.Properties["MenuKey" + i]);
                    }
                }
                as_MenuItems = new string[2][]{ as_MenuNames, as_MenuKeys };
            }
            else
            {
                as_MenuItems = setUpMenu();
            }

            return as_MenuItems;
        }
    }
}
