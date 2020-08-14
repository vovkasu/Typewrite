using UnityEngine;

namespace Assets.Scripts
{
    public class Page : ScriptableObject
    {
        public Sprite Image;
        [TextArea]
        public string Text;
        public string NarratorSoundName;
        public Story Story;
    }
}