using UnityEngine;

namespace Assets.Scripts
{
    public class Page : ScriptableObject
    {
        public Sprite Image;
        public string Text;
        public string NarratorSoundName;
        public Story Story;
    }
}