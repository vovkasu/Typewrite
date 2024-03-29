﻿using System;
using CategoryQuestions.Assets.Scripts.Core.Application;
using UnityEngine.UI;

namespace CategoryQuestions.Assets.Scripts.Core.Popup
{
    public abstract class PopUpBase : PopUpAnimationController
    {
        public Button CloseButton;
        public event Action ClosedEvent;
        
        private Action _onPopupDestroy;

        protected virtual void OnClosedEvent()
        {
            Action handler = ClosedEvent;
            if (handler != null) handler();
        }

        protected virtual void Awake()
        {
            if (CloseButton != null) CloseButton.onClick.AddListener(EntryPointBase.NavigationController.GoBack);
            Show();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
        }

        protected virtual void OnDestroy()
        {
            OnClosedEvent();
        }

        private void OnDisable()
        {
            if (_onPopupDestroy != null)
            {
                _onPopupDestroy();
            }
            _onPopupDestroy = null;
        }

        public virtual void Close(Action onComplete)
        {
            _onPopupDestroy = onComplete;
            if (onComplete != null) onComplete();
        }
    }
}