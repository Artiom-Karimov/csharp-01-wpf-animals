using System.Windows.Threading;

namespace csharp_01
{
    internal class GameTimer
    {
        public event EventHandler<GameTimerEventArgs> Tick = delegate { };

        private DispatcherTimer timer = new DispatcherTimer();
        private int tenthsOfSecondsElapsed = 0;

        public GameTimer()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(.1);
        }

        public void Start()
        {
            tenthsOfSecondsElapsed = 0;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed += 1;
            Tick.Invoke(this, new GameTimerEventArgs(tenthsOfSecondsElapsed));
        }
    }

    internal class GameTimerEventArgs : EventArgs
    {
        public int TenthsOfSecondsElapsed { get; set; }

        public GameTimerEventArgs(int tenthsOfSecondsElapsed)
        {
            this.TenthsOfSecondsElapsed = tenthsOfSecondsElapsed;
        }
    }
}
