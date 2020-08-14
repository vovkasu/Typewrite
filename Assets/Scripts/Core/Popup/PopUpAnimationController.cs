using System;
using System.Collections;
using System.Collections.Generic;
using CategoryQuestions.Assets.Scripts.Core.Animations;
using CategoryQuestions.Assets.Scripts.Core.Animations.EasingFunction;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Popup
{
    public class PopUpAnimationController : MonoBehaviour
    {
        public List<PopUpAnimation> AnimationElements;

        protected void Show()
        {
            foreach (var animationElement in AnimationElements)
            {
                animationElement.Transform.localScale = Vector3.zero;
            }
            StartCoroutine(PlayAnimations());
        }
        
        private IEnumerator PlayAnimations()
        {
            yield return new WaitForEndOfFrame();
            foreach (var element in AnimationElements)
            {
                Play(element);
                yield return new WaitForSeconds(element.DelayUntilNextAnimation);
            }
        }

        private void Play(PopUpAnimation elem)
        {
            var storyboard = gameObject.AddComponent<Storyboard>();
            
            var scaleAnimation = new Vector2Animation
            {
                From = Vector2.zero,
                To = Vector2.one,
                Duration = TimeSpan.FromSeconds(0.3d),
                EasingFunction = new CurveEase { Curve = elem.Curve }
            };

            scaleAnimation.Tick += value =>
            {
                elem.Transform.localScale = (Vector2)value;
            };
            storyboard.Children.Add(scaleAnimation);
            storyboard.Begin();
        }
    }

    [Serializable]
    public class PopUpAnimation
    {
        public RectTransform Transform;
        public AnimationCurve Curve;
        public float DelayUntilNextAnimation;
    }
}