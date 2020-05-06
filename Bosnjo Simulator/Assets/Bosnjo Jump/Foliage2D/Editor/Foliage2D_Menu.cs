using UnityEngine;
using UnityEditor;

namespace Foliage
{
    public class Foliage2D_Menu
    {
        [MenuItem("GameObject/2D Foliage/Create Foliage Object", false, 0)]
        static void AddGrassPatch()
        {
            GameObject obj = new GameObject("New Foliage2D_Object");
            Foliage2D path = obj.AddComponent<Foliage2D>();
            obj.AddComponent<Foliage2D_Animation>();

            path.SetDefaultMaterial();
            path.RebuildMesh();

            obj.transform.position = GetSpawnPos();

            Selection.activeGameObject = obj;
            EditorGUIUtility.PingObject(obj);
        }

        [MenuItem("GameObject/2D Foliage/Create Foliage Path", false, 0)]
        static void AddPath()
        {
            GameObject obj = new GameObject("New Foliage2D_Path");
            Foliage2D_Path path = obj.AddComponent<Foliage2D_Path>();

            path.AddPathPoint(new Vector2(-3, 0));
            path.AddPathPoint(new Vector2(3f, 0));

            path.foliagePrefabs.Add(null);

            obj.transform.position = GetSpawnPos();

            Selection.activeGameObject = obj;
            EditorGUIUtility.PingObject(obj);
        }

        static Vector3 GetSpawnPos()
        {
            Plane plane = new Plane(new Vector3(0, 0, -1), 0);
            float dist = 0;
            Vector3 result = new Vector3(0, 0, 0);
            Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1.0f));
            if (plane.Raycast(ray, out dist))
            {
                result = ray.GetPoint(dist);
            }
            return new Vector3(result.x, result.y, 0);
        }
    }
}
