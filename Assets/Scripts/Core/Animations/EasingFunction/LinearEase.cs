namespace CategoryQuestions.Assets.Scripts.Core.Animations.EasingFunction
{
    public class Linear : EasingFunctionBase
    {
        protected override float Function(float timeProgress)
        {
            return timeProgress;
        }
    }
}