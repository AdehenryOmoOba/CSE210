using System;

namespace maintenance_app
{
    public class ReminderTimer
    {
        private System.Threading.Timer _timer;

        public ReminderTimer(int intervalInMinutes, Action callback)
        {
            _timer = new System.Threading.Timer(_ => callback.Invoke(), null, 0, intervalInMinutes * 60 * 1000);
        }

        public void StopTimer()
        {
            _timer?.Change(System.Threading.Timeout.Infinite, 0);
        }
    }
}