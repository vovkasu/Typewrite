using System.Collections.Generic;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Pages
{
    public abstract class NavigationControllerBase
    {
        protected NavigationControllerBase(IPageBaseLink startPageLink)
        {
            _startPageLink = startPageLink;
        }

        public static readonly List<IPageBaseLink> Pages = new List<IPageBaseLink>();
        public static int CurrentPageIndex;
        protected IPageBaseLink _startPageLink;

        public abstract Navigator ActivPage();
        public abstract bool CanGoBack();

        public virtual void NavitageTo(IPageBaseLink nextPage)
        {
            NavitageTo(nextPage, null);
        }

        public abstract void NavitageTo(IPageBaseLink nextPage, object newPageArgs);

        public virtual void GoBack()
        {
            Debug.Log("Go back");
            GoBack(null);
        }

        protected virtual void QuitApplication()
        {
            Debug.Log("quit");
            UnityEngine.Application.Quit();
        }

        public void InitStartPage(Navigator startPage)
        {
            startPage.OnNavigatedTo(null);
        }

        public virtual void RunStartPage()
        {
            ActivPage().OnNavigatedToCompleted();
        }

        public abstract void GoToStartPage();
        public abstract void GoBack(object prevPageArgs);
    }
}