using System;
using UnityEngine;

namespace CategoryQuestions.Assets.Scripts.Core.Pages
{
    [Serializable]
    public class PageBaseLink : IPageBaseLink
    {
        public string SceneTitle;
        [HideInInspector] public string SceneName;
        [HideInInspector] public string SceneGuid;
    }
}