using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApp
{
    class Exercise
    {
        #region Basics
        public bool b_setupGoal;
        public bool b_setupLatest;
        public string s_Name;
        public int i_Goal;
        public int i_Current;
        public int i_NumberOfSets = 8;
        public float f_DistanceGoal;
        public float f_DistanceCurrent;
        public float f_TimeGoal;
        public float f_TimeCurrent;
        public int i_Type;
        public int i_day;
        #endregion

        #region Analytics
        public bool b_LatestPreferenceEntered;
        public int[] ai_LatestSet;
        public float[] af_LatestSetTime;
        public float[] af_LatestRestTime;
        public int i_LatestPreference;
        public int[] ai_LatestSetPreference;
        #endregion

        public void reset()
        {
            
        b_setupGoal = false;
        b_setupLatest = false;
        s_Name = "";
        i_Goal = 0;
        i_Current = 0;
        i_NumberOfSets = 8;
        f_DistanceGoal = 0.0f;
        f_DistanceCurrent = 0.0f;
        f_TimeGoal = 0.0f;
        f_TimeCurrent = 0.0f;
        i_Type = 0;
        i_day = 0;
        

        
        b_LatestPreferenceEntered = false;
        ai_LatestSet = new int[]{ 0,0,0,0,0};
        af_LatestSetTime= new float[] { 0.0f, 0.0f, 0.0f, 0.0f };
        af_LatestRestTime =new float[] { 0.0f, 0.0f, 0.0f, 0.0f };
        i_LatestPreference = 0 ;
        ai_LatestSetPreference = new int[] { 0, 0, 0, 0, 0 };

        
        }
}
}
