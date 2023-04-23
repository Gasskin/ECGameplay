using System;

namespace ECGameplay
{
    public class GameTimer
    {
        private float maxTime;
        private float time;
        private Action action;

        public bool IsFinished => time >= maxTime;
        public bool IsRunning => time < maxTime;

        public float MaxTime
        {
            get => maxTime;
            set => maxTime = value;
        }

        public GameTimer(float maxTime,Action action)
        {
            if (maxTime <= 0)
                throw new Exception($"_maxTime can not be 0 or negative");
            this.maxTime = maxTime;
            this.action = action;
            time = 0f;
        }

        public void Reset()
        {
            time = 0f;
        }

        public void UpdateAsFinish(float delta)
        {
            if (!IsFinished)
            {
                time += delta;
                if (IsFinished)
                {
                    action?.Invoke();
                }
            }
        }

        public void UpdateAsRepeat(float delta)
        {
            if (delta > maxTime)
                throw new Exception($"_maxTime too small, delta:{delta} > _maxTime:{maxTime}");
            time += delta;
            while (time >= maxTime)
            {
                time -= maxTime;
                action?.Invoke();
            }
        }
    }
}