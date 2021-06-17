using System.Timers;
using System;

namespace WebAPI {

    public class RefreshTimer {

        public RefreshTimer() {}

        private Timer timer;

        public void Start()
        {
            // calculate seconds till midnight.
            DateTime now = DateTime.Now;
            int hours = 0, minutes = 0, seconds = 0, secondsTillMidnight = 0;
            hours = (24 - now.Hour) - 1;
            minutes = (60 - now.Minute) - 1;
            seconds = (60 - now.Second - 1);

            secondsTillMidnight = seconds + (minutes * 60) + (hours * 3600);

            // Create a timer with a two second interval.
            timer = new Timer(secondsTillMidnight);

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Stop() { timer.Enabled = false; }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // RESET DAILY TRAINING COUNTS FOR ALL HEROES.
            WebAPI.Services.HeroService.ResetDailyCounts();

            // Set timer interval to 24 hours.
            timer.Interval = 24 * 60 * 60 * 1000;
        }
    }
}