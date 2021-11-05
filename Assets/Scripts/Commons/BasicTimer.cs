using System;

namespace Commons
{
    public class BasicTimer
    {
        public float TimerDuration => timerDuration;
        
        private Action completeCallback;
        private float timerDuration;
        private bool isTimerActive;

        public void StartTimer(float duration, Action callback)
        {
            timerDuration = duration;
            completeCallback = callback;
            isTimerActive = true;
        }

        public void Cancel()
        {
            isTimerActive = false;
            timerDuration = 0;
            completeCallback = null;
        }

        public void UpdateFrame(float deltaTime)
        {
            if (!isTimerActive) return;

            timerDuration -= deltaTime;

            if (timerDuration <= 0)
            {
                completeCallback?.Invoke();
                isTimerActive = false;
            }
        }
    }
}