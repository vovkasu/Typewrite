using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Story", menuName = "Typewrite/Story", order = 2)]
    public class Story : ScriptableObject
    {
        public List<Page> Pages = new List<Page>();

#if UNITY_EDITOR
        [MenuItem("Assets/Story/Add Page", false, 400)]
        private static void AddCategoryVariantContextMenu()
        {
            var story = Selection.activeObject as Story;
            var page = ScriptableObject.CreateInstance<Page>();
            story.Pages.Add(page);
            page.Story = story;
            RenamePages(story);
            AssetDatabase.AddObjectToAsset(page, story);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(story));
        }

        [MenuItem("Assets/Story/Add Page", true)]
        private static bool AddCategoryVariantContextMenuValidation()
        {
            return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(Story);
        }

        [MenuItem("Assets/Story/Delete Page", false, 400)]
        private static void DeleteCategoryVariantContextMenu()
        {
            var page = Selection.activeObject as Page;
            DeleteAction(page);
        }

        [MenuItem("Assets/Story/Delete Page", true)]
        private static bool DeleteCategoryVariantContextMenuValidation()
        {
            return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(Page);
        }

        public static void DeleteAction(Page page)
        {
            var story = page.Story;
            story.Pages.Remove(page);
            Object.DestroyImmediate(page, true);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(story));
            AssetDatabase.SaveAssets();
        }

        public static void RenamePages(Story story)
        {
            for (int i = 0; i < story.Pages.Count; i++)
            {
                story.Pages[i].name = "Page" + (i + 1);
            }
        }
#endif
    }
}