using System.Linq;
using CategoryQuestions.Assets.Scripts.Core.Application;
using CategoryQuestions.Assets.Scripts.Core.Pages;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts
{
    public class Application : EntryPointBase
    {
        protected override void Awake()
        {
            Current = this;
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }


        public override NavigationControllerBase InitNavigationController()
        {
            NavigationController = new NavigationController(StartPage);
            return NavigationController;
        }

        public override void SetStartPage(GameObject startPage)
        {
            StartPage = startPage.GetComponent<PageBaseLink>();
        }

        public static PageBaseLink GetLinkBySceneName(string sceneName)
        {
            return Current.PageLinkList.First(_ => _.SceneName == sceneName);
        }

        protected override void OnApplicationQuit()
        {
        }
    }
}