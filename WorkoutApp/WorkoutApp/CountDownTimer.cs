using System;
using System.Threading;
using Xamarin.Forms;

namespace WorkoutApp
{
    public class CountDownTimer : Label
    {
        CancellationTokenSource _CancellationTokenSource;
        public CountDownTimer()
        {
            _CancellationTokenSource = new CancellationTokenSource();
            FontFamily = "sans-serif-light";
            FontSize = 45;
            TextColor = Color.Black;
        }



        public void TimerStart()
        {
            int Min = CountDownMinutes;
            int Sec = CountDownSeconds;
            int TotalSec = (Min * 60) + Sec;
            
            CancellationTokenSource CTS = _CancellationTokenSource;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (CTS.IsCancellationRequested)
                {
                    return false;
                }
                else
                {
                    if (TotalSec == 0)
                    {
                        return false;
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        
                        TotalSec = TotalSec - 1;
                        TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                        Text = string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);

                        if(TotalSec < 1)
                        {
                            Text = "Rest Over";
                        }
                    });
                    return true;
                }
            });
        }

        public void TimerStop()
        {
            Interlocked.Exchange(ref _CancellationTokenSource, new CancellationTokenSource()).Cancel();
        }

        static void OnTimerCancelChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CountDownTimer)bindable).TimerStop();
        }

        static void OnTimerTimeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CountDownTimer)bindable).TimerStop();
            ((CountDownTimer)bindable).TimerStart();
        }

        public static readonly BindableProperty CountDownMinutesProperty = BindableProperty.Create("CountDownMinutes", typeof(int), typeof(CountDownTimer), 0, BindingMode.TwoWay, null, OnTimerTimeChanged);
        public int CountDownMinutes
        {
            get { return (int)base.GetValue(CountDownMinutesProperty); }
            set { base.SetValue(CountDownMinutesProperty, value); }
        }

        public static readonly BindableProperty CountDownSecondsProperty = BindableProperty.Create("CountDownSeconds", typeof(int), typeof(CountDownTimer), 0, BindingMode.TwoWay, null, OnTimerTimeChanged);
        public int CountDownSeconds
        {
            get { return (int)base.GetValue(CountDownSecondsProperty); }
            set { base.SetValue(CountDownSecondsProperty, value); }
        }

        public static readonly BindableProperty TimerCancelProperty = BindableProperty.Create("TimerCancel", typeof(bool), typeof(CountDownTimer), false, BindingMode.TwoWay, null, OnTimerCancelChanged);
        public bool TimerCancel
        {
            get { return (bool)base.GetValue(TimerCancelProperty); }
            set { base.SetValue(TimerCancelProperty, value); }
        }

    }
}