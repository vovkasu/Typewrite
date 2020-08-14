using System;
using CategoryQuestions.Assets.Scripts.Core.Animations;
using CategoryQuestions.Assets.Scripts.Core.Timer;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.SplashScreen
{
    public class SplashScreenPageBase : MonoBehaviour
    {
        public static Texture2D LoadingTextValue;
        public float TimeToShowSeconds = 3.5f;
        public float TimeToDissolutionSeconds = 0.3f;

        public delegate void AnimationTickEventHandler(object value);

        public AnimationTickEventHandler AnimationTick;

        public event Action StartHideEvent;

        protected virtual void OnStartHideEvent()
        {
            Action handler = StartHideEvent;
            if (handler != null) handler();
        }

        public event Action FinishHideEvent;

        protected virtual void OnFinishHideEvent()
        {
            Action handler = FinishHideEvent;
            if (handler != null) handler();
        }

        public virtual void StartHide()
        {
            var dt = gameObject.AddComponent<DispatcherTimer>();
            dt.Interval = TimeSpan.FromSeconds(TimeToShowSeconds);
            dt.Tick += (sender, args) =>
            {
                Destroy(dt);
                var sb = gameObject.AddComponent<Storyboard>();

                OnStartHideEvent();

                sb.Begin();
                sb.Completed += (o, eventArgs) =>
                {
                    OnFinishHideEvent();
                    FinalizeSpalsh();
                };

            };
            dt.Begin();
        }

        public virtual void StartHide(TimeSpan timeToShow)
        {
            TimeToShowSeconds = (float) timeToShow.TotalSeconds;
            StartHide();
        }

        protected virtual void FinalizeSpalsh()
        {
            Destroy(gameObject);
        }
    }
}