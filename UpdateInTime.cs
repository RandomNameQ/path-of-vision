using System;
using System.Threading;
using Timer = System.Threading.Timer;

namespace Poe_show_buff
{
    public class UpdateInTime
    {
        private MakeScreenShot _makeScreenShot = new MakeScreenShot();
        private float _updateTime = 0.1f;
        private Timer _timer;
        private object _lock = new object();
        private bool _isRunning = false;

        public void Start()
        {
            _timer = new Timer(TakeScreen, null, TimeSpan.Zero, TimeSpan.FromSeconds(_updateTime));
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        private void TakeScreen(object state)
        {
            if (!_isRunning)
            {
                lock (_lock)
                {
                    if (!_isRunning)
                    {
                        _isRunning = true;
                        _makeScreenShot.MakeShoot();
                        _isRunning = false;
                    }
                }
            }
        }
    }
}
