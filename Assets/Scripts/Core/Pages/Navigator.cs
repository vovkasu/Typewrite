using System;
using System.Collections.Generic;
using CategoryQuestions.Assets.Scripts.Core.Application;
using CategoryQuestions.Assets.Scripts.Core.Popup;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Pages
{
    public class Navigator : MonoBehaviour
    {
        public readonly List<PopUpBase> PopUps = new List<PopUpBase>();
        [HideInInspector]
        public bool IsActiv;
        public float WaitTimeLoadScreen = 1.0f;
        public event EventHandler OnPageLoaded;
        
        public virtual void OnNavigatedTo(object arg)
        {
            IsActiv = true;
            if (WaitTimeLoadScreen < 0)
            {
                WaitTimeLoadScreen = 1.0f;
            }
        }

        public virtual void OnNavigatedToCompleted()
        {
        }

        public virtual void OnNavigatedFrom()
        {
            IsActiv = false;
        }

        public virtual void OnNavigatedFromCompleted()
        {
        }

        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
        }

        protected virtual void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (EntryPointBase.Current.BackNavigationByEscape)
                {
                    GoBackLoadingType();
                }
            }
        }

        private void GoBackLoadingType()
        {
            EntryPointBase.NavigationController.GoBack();
        }

        protected virtual void OnDestroy()
        {
        }
    }
}