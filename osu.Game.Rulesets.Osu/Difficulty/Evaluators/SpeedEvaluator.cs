// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Osu.Difficulty.Preprocessing;

namespace osu.Game.Rulesets.Osu.Difficulty.Evaluators
{
    public static class SpeedEvaluator
    {
        private const double min_speed_bonus = 75; // ~200BPM
        private const double speed_balancing_factor = 40;

        /// <summary>
        /// Evaluates the difficulty of tapping the current object, based on:
        /// <list type="bullet">
        /// <item><description>time between pressing the previous and current object,</description></item>
        /// <item><description>distance between those objects,</description></item>
        /// <item><description>and how easily they can be cheesed.</description></item>
        /// </list>
        /// </summary>
        public static double EvaluateDifficultyOf(DifficultyHitObject current, double greatWindow)
        {
            // derive strainTime for calculation
            var osuCurrObj = (OsuDifficultyHitObject)current;
            var osuNextObj = (OsuDifficultyHitObject)current.Next(0);

            double strainTime = osuCurrObj.StrainTime;
            double greatWindowFull = greatWindow * 2;
            double doubletapness = 1;

            // Nerf doubletappable doubles.
            if (osuNextObj != null)
            {
                double currDeltaTime = Math.Max(1, osuCurrObj.DeltaTime);
                double nextDeltaTime = Math.Max(1, osuNextObj.DeltaTime);
                double deltaDifference = Math.Abs(nextDeltaTime - currDeltaTime);
                double speedRatio = currDeltaTime / Math.Max(currDeltaTime, deltaDifference);
                double windowRatio = Math.Pow(Math.Min(1, currDeltaTime / greatWindowFull), 2);
                doubletapness = Math.Pow(speedRatio, 1 - windowRatio);
            }

            // Cap deltatime to the OD 300 hitwindow.
            // 0.93 is derived from making sure 260bpm OD8 streams aren't nerfed harshly, whilst 0.92 limits the effect of the cap.
            strainTime /= Math.Clamp((strainTime / greatWindowFull) / 0.93, 0.92, 1);

            // derive speedBonus for calculation
            double speedBonus = 1.0;

            if (strainTime < min_speed_bonus)
                speedBonus = 1 + 0.75 * Math.Pow((min_speed_bonus - strainTime) / speed_balancing_factor, 2);

            return speedBonus * doubletapness / strainTime;
        }
    }
}
