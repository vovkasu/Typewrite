using System;
using System.Collections.Generic;
using System.Linq;
using CategoryQuestions.Assets.Scripts.Core.Loading;
using CategoryQuestions.Assets.Scripts.Core.Options;
using CategoryQuestions.Assets.Scripts.Core.Pages;
using CategoryQuestions.Assets.Scripts.Core.SplashScreen;
using CategoryQuestions.Core.Audio;
using CategoryQuestions.Assets.Scripts.Core.ExtendedMethods;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CategoryQuestions.Assets.Scripts.Core.Application
{
    public abstract class EntryPointBase : MonoBehaviour
    {
        public PageBaseLink StartPage;
        public static NavigationControllerBase NavigationController;
        public static OptionsProviderBase Options;
        public static EntryPointBase Current;

        public string SoundRootPath = "Sounds/";
        public MediaPlayer MediaPlayer;
        
        [HideInInspector] public bool BackNavigationByEscape = true;

        [HideInInspector] public LoadingScreen LoadingScreen;
        public LoadingScreen LoadingScreenPrefab;

        public bool ShowSplashScreen;
        public GameObject SplashScreenPrefab;

        public List<PageBaseLink> PageLinkList = new List<PageBaseLink>();

        [HideInInspector]
        public PageBase AddPageBase;

        protected SplashScreenPageBase SplashScreen;
        protected Navigator[] _allPages;

        public abstract NavigationControllerBase InitNavigationController();
        public abstract void SetStartPage(GameObject startPage);
        public Action AppStarted;

        public virtual void OnAppStarted()
        {
            NavigationController.RunStartPage();
            if (AppStarted != null)
            {
                AppStarted();
            }
        }

        protected virtual void Awake()
        {
#if UNITY_IOS
            UnityEngine.Application.targetFrameRate = 60;
#endif
            Options = new OptionProvider();
            Options.LaunchCount++;

            MediaPlayer = new MediaPlayer(gameObject, SoundRootPath);

            _allPages = gameObject.GetComponentsInChildren<Navigator>(true);
            foreach (var children in _allPages)
            {
                children.gameObject.SetActive(false);
            }

            if (ShowSplashScreen)
            {
                SplashScreen = gameObject.AddChild(SplashScreenPrefab).GetComponent<SplashScreenPageBase>();
                SplashScreen.StartHideEvent += OnFinishHideSplashScreen;
                OnSplashScreenStarted();
            }
        }

        protected virtual void Start()
        {
            if (LoadingScreenPrefab != null)
            {
                LoadingScreen = Instantiate(LoadingScreenPrefab);
                LoadingScreen.transform.SetParent(transform);
                LoadingScreen.name = "Loading Screen";
                LoadingScreen.transform.localScale = Vector3.one;
                LoadingScreen.transform.localPosition = Vector3.zero;
                LoadingScreen.GetComponent<LoadingScreen>().Init();
            }

            InitNavigationController();
            
        }

        protected virtual void Update()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnApplicationFocus(bool focusStatus)
        {
        }

        protected virtual void OnApplicationPause(bool paused)
        {
        }

        protected virtual void OnApplicationQuit()
        {
        }

        protected virtual void OnDestroy()
        {
        }
        

        protected virtual void OnFinishHideSplashScreen()
        {
            OnAppStarted();
        }

        protected virtual void OnSplashScreenStarted()
        {
            SplashScreen.StartHide();
        }

#if UNITY_EDITOR
        protected void OnValidate()
        {
            RegistratePageBase(AddPageBase);
        }

        public void RegistratePageBase(PageBase addPageBase)
        {
            if (addPageBase != null)
            {
                var scene = addPageBase.gameObject.scene;
                var assetPathToGuid = AssetDatabase.AssetPathToGUID(scene.path);

                addPageBase.PageBaseLink = new PageBaseLink
                {
                    SceneGuid = assetPathToGuid,
                    SceneTitle = addPageBase.name,
                    SceneName = scene.name
                };

                EditorSceneManager.MarkSceneDirty(addPageBase.gameObject.scene);
                EditorSceneManager.SaveOpenScenes();

                var oldPageBaseLink = PageLinkList.FirstOrDefault(_ => _.SceneGuid == assetPathToGuid);

                if (oldPageBaseLink == null)
                {
                    var pageBaseLikn = new PageBaseLink();
                    pageBaseLikn.SceneGuid = assetPathToGuid;
                    pageBaseLikn.SceneTitle = addPageBase.name;
                    pageBaseLikn.SceneName = scene.name;
                    AddPageBase = null;
                    PageLinkList.Add(pageBaseLikn);
                }
                else
                {
                    oldPageBaseLink.SceneTitle = addPageBase.PageBaseLink.SceneTitle;
                    oldPageBaseLink.SceneName = addPageBase.PageBaseLink.SceneName;
                    AddPageBase = null;
                }
            }
        }
#endif
    }
}