using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CategoryQuestions.Assets.Scripts.Core.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        public Image Background;

        public Image Balls;
        public Text Text;

        public virtual void Init()
        {
            gameObject.SetActive(false);
        }

        public void FadeIn(Action onComplete)
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeImage(false, onComplete));
        }

        public void FadeOut(Action onComplete)
        {
            if(!gameObject.activeSelf) return;
            StartCoroutine(FadeImage(true, () =>
            {
                if (onComplete != null)
                {
                    onComplete();
                }
                gameObject.SetActive(false);
            }));
        }

        private IEnumerator FadeImage(bool fadeAway, Action callback = null)
        {
            if (fadeAway)
            {
                var i = 1f;
                var j = 1f;

                while (i >= 0)
                {
                    i -= 0.05f;
                    j -= 0.3f;
                    Background.color = new Color(255, 255, 255, i);
                    Balls.color = new Color(255, 255, 255, j);
                    Text.color = new Color(255, 255, 255, j);
                    yield return null;
                }
                Background.gameObject.SetActive(false);
                if (callback != null)
                {
                    callback();
                }
            }
            else
            {
                Background.gameObject.SetActive(true);
                var i = 0f;
                var j = 0f;
                while (i <= 1)
                {
                    i += 0.05f;
                    j += 0.3f;
                    Background.color = new Color(255, 255, 255, i);
                    Balls.color = new Color(255, 255, 255, j);
                    Text.color = new Color(255, 255, 255, j);
                    yield return null;
                }
                if (callback != null)
                {
                    callback();
                }
            }
        }
    }
}