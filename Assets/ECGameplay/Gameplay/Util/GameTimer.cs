using System;

namespace ECGameplay
{
    public interface IGameTimer
    {
        public bool IsFinish { get; }
        public void Update(float delta);
        public void Reset();
    }

    public class DurationTimer : IGameTimer
    {
        public bool IsFinish => time >= maxTime;
        private Action action;
        private readonly float maxTime;
        private float time;

        public DurationTimer(float maxTime, Action action)
        {
            this.maxTime = maxTime;
            this.action = action;
            time = 0;
        }

        public void Update(float delta)
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

    public class IntervalTimer : IGameTimer
    {
        public bool IsFinish => time >= maxTime;

        private readonly float maxTime;
        private float intervalTime;
        private float countTime;
        private float time;

        private Action intervalAction;
        private Action endAction;

        public IntervalTimer(float maxTime, float intervalTime, Action intervalAction, Action endAction)
        {
            this.maxTime = maxTime;
            this.intervalTime = intervalTime;
            this.intervalAction = intervalAction;
            this.endAction = endAction;
            countTime = 0;
            time = 0;
        }

        public void Update(float delta)
        {
            if (!IsFinish)
            {
                time += delta;
                countTime += delta;
                if (countTime > intervalTime)
                {
                    countTime -= intervalTime;
                    intervalAction?.Invoke();
                }

                if (IsFinish)
                {
                    endAction?.Invoke();
                }
            }
        }

        public void Reset()
        {
            time = 0;
            countTime = 0;
        }
    }
}