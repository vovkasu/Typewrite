﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Animations
{
    public class Storyboard : MonoBehaviour
    {
        public List<SimpleAnimationBase> Children = new List<SimpleAnimationBase>();
        public bool IsEnabled;
        public bool IsDestroyOnStop = true;

        public event EventHandler Completed;

        protected virtual void OnCompleted()
        {
            var handler = Completed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Begin()
        {
            IsEnabled = true;
            foreach (var child in Children)
            {
                child.InitAnimation();
            }
        }

        private void Update()
        {
            if (!IsEnabled)
            {
                return;
            }

            bool allAnimationFinished = true;
            foreach (var child in Children)
            {
                if (child.IsEnabled)
                {
                    allAnimationFinished = false;
                    child.OnTick(Time.deltaTime);
                }
            }
            if (allAnimationFinished)
            {
                IsEnabled = false;
                OnCompleted();
                Destroy(this);
            }
        }

        public void Stop()
        {
            IsEnabled = false;
            if (IsDestroyOnStop)
            {
                Destroy(this);
            }
        }
    }
}