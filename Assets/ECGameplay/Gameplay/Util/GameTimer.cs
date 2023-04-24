using System;

namespace ECGameplay
{
    public interface IGameTimer
    {
        public void Update(float delta,Action action);
        public void Reset();
    }

    public class DurationTimer : IGameTimer
    {
        public bool IsFinish => time >= targetTime;
        
        private float targetTime;
        private float time;
        
        public DurationTimer(float targetTime)
        {
            this.targetTime = targetTime;
            time = 0;
        }
        
        public void Update(float delta, Action action)
        {
            if (!IsFinish)
            {
                time += delta;
                if (IsFinish)
                {
                    action?.Invoke();
                }
            }
        }

        public void Reset()
        {
            time = 0;
        }
    }
    
    public class GameTimer
    {
        private float maxTime;
        private float time;
        private float interval;
        private float intervalTime;
        private Action action;

        public bool IsFinished => time >= maxTime;

        public float MaxTime
        {
            get => maxTime;
            set => maxTime = value;
        }

        public GameTimer(float maxTime, Action action)
        {
            if (maxTime <= 0)
                throw new Exception($"_maxTime can not be 0 or negative");
            this.maxTime = maxTime;
            this.action = action;
            time = 0f;
        }

        public GameTimer(float maxTime, float interval, Action action)
        {
            if (maxTime <= 0)
                throw new Exception($"_maxTime can not be 0 or negative");
            if (interval <= 0)
                throw new Exception($"interval can not be 0 or negative");
            this.maxTime = maxTime;
            this.interval = interval;
            intervalTime = 0;
            time = 0;
            this.action = action;
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

        public void UpdateAsInterval(float delta)
        {
            if (delta > maxTime)
                throw new Exception($"_maxTime too small, delta:{delta} > _maxTime:{maxTime}");
            if (!IsFinished)
            {
                time += delta;
                intervalTime += delta;
                while (intervalTime >= interval)
                {
                    intervalTime -= interval;
                    action?.Invoke();
                }
            }
        }
    }
}