using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Animations.EasingFunction
{
    public class CurveEase : EasingFunctionBase
    {
        public AnimationCurve Curve;

        protected override float Function(float timeProgress)
        {
            return Curve.Evaluate(timeProgress);
        }
    }
}