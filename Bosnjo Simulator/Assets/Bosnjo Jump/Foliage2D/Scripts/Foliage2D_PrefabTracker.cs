using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Foliage {
#if UNITY_EDITOR
    public partial class Foliage2D_PrefabTracker : AssetPostprocessor {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            for (int i = 0; i < importedAssets.Length; i++)
            {
                if (importedAssets[i].EndsWith(".prefab"))
                {
                    GameObject o = AssetDatabase.LoadAssetAtPath(importedAssets[i], typeof(UnityEngine.Object)) as GameObject;
                    if (o == null) continue;

                    Foliage2D[] foliageObjects = o.GetComponentsInChildren<Foliage2D>();
           
                    for (int t = 0; t < foliageObjects.Length; t++)
                    {
                        MeshFilter filter = foliageObjects[t].gameObject.GetComponent<MeshFilter>();

                        if (filter.sharedMesh == null)
                        {
                            foliageObjects[t].prefabInstanceIsCreated = true;
                            foliageObjects[t].RebuildMesh();
                        }
                    }
                }
            }
        }
    }
#endif
}
