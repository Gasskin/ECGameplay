using System;

namespace ECGameplay
{
    public static class MathUtil
    {
        private static Random random = new Random();

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="probability">概率（0-1）</param>
        /// <returns>是否抽中</returns>
        public static bool PrizeDraw(float probability)
        {
            probability = Math.Clamp(probability, 0, 1);
            if (probability == 0)
                return false;
            if (Math.Abs(probability - 1) < 0.00001f)
                return true;
            return (float)random.NextDouble() <= probability;
        }
    }
}