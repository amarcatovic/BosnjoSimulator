using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;

namespace Foliage
{
    [CustomEditor(typeof(Foliage2D_Path))]
    public class Foliage2D_PathEditor : Editor
    {
        private Texture2D texDot;
        private Texture2D texBlueDot;
        private Texture2D texDotSelected;
        private Texture2D texDotPlus;
        private Texture2D texMinus;
        private static float pathScale = 1;
        private List<int> selectedPoints = new List<int>();
        private bool showProperties = true;
        private bool handleSelected = false;
        private bool showFoliagePrefabList = true;
        private int index = 0;


        void OnEnable()
        {
            texDot = GetGizmo("circle.png");
            texBlueDot = GetGizmo("blue_circle.png");
            texDotSelected = GetGizmo("circle_selected.png");
            texDotPlus = GetGizmo("dot-plus.png");
            texMinus = GetGizmo("dot-minus.png");

            selectedPoints.Clear();
        }

        // Inspector Fields
        public override void OnInspectorGUI()
        {
            Foliage2D_Path path = (Foliage2D_Path)target;
            CustomInspector(path);
        }

        private void CustomInspector(Foliage2D_Path path2D)
        {
            Undo.RecordObject(target, "Modified Inspector");

            showProperties = EditorGUILayout.Foldout(showProperties, "Path Properties");

            if (showProperties)
            {
                EditorGUI.indentLevel = 1;
                InspectorBox(8, () =>
                {
                    path2D.foliagePattern = (Foliage2D_Pattern)EditorGUILayout.EnumPopup(new GUIContent("Foliage Pattern", "Describes how the foliage objects should be arranged on  " 
                        + "the path lines. Consecutive option will place the foliage objects in a consecutive order while random, in a random order."), path2D.foliagePattern);
                    path2D.foliageOverlapType = (Foliage2D_OverlappingType)EditorGUILayout.EnumPopup(new GUIContent("Overlapping Type", "This popup menu contains two options for how " 
                        + " the overlapping factor is calculated. The value of the overlapping factor determines which part of a foliage object mesh will be placed to the left of " 
                        + "the right edge of the previous foliage object mesh."), path2D.foliageOverlapType);

                    if (path2D.foliageOverlapType == Foliage2D_OverlappingType.Fixed)
                        path2D.overlappingFactor = EditorGUILayout.Slider(new GUIContent("Overlapping Factor", "The value of the overlapping factor determines which part of a foliage " 
                            + "object mesh will be placed to the left of the right edge of the previous foliage object mesh."), path2D.overlappingFactor, -2, 0.99f);
                    else
                    {
                        path2D.minOverlappingFactor = EditorGUILayout.Slider(new GUIContent("Min Overlapping", "The min value for the overlaping factor."), path2D.minOverlappingFactor, -2, 0.99f);
                        path2D.maxOverlappingFactor = EditorGUILayout.Slider(new GUIContent("Max Overlapping", "The max value for the overlaping factor."), path2D.maxOverlappingFactor, -2, 0.99f);
                    }
                    path2D.firstObjectOffset = EditorGUILayout.FloatField(new GUIContent("First Object Offset", "Offsets the distance from the start of the line, where the first object on the current line will be placed."), path2D.firstObjectOffset);
                    path2D.lastObjectOffset = EditorGUILayout.FloatField(new GUIContent("Last Object Offset", "Offsets the distance from the start of the line, where the last object on the current line will be placed."), path2D.lastObjectOffset);
                    path2D.foliagePathType = (Foliage2D_PathType)EditorGUILayout.EnumPopup(new GUIContent("Path Type", "Describes how the objects will be placed on the foliage path."), path2D.foliagePathType);

                    if (path2D.foliagePathType == Foliage2D_PathType.Smooth)
                    {
                        path2D.uniformValues = EditorGUILayout.Toggle(new GUIContent("Uniform Values", "When set to true the tension and bias will be the same for all path nodes. " 
                            + "Disable this if you want to change the tension and bias of individual path nodes."), path2D.uniformValues);
                        if (path2D.uniformValues)
                        {
                            path2D.bias = EditorGUILayout.FloatField(new GUIContent("Bias", "The bias of the path nodes."), path2D.bias);
                            path2D.tension = EditorGUILayout.FloatField(new GUIContent("Tension", "The tension of the path nodes."), path2D.tension);
                        }
                        else
                        {
                            path2D.biasScale = EditorGUILayout.FloatField(new GUIContent("Bias Scale", "Bias scale"), path2D.biasScale);
                            path2D.tensionScale = EditorGUILayout.FloatField(new GUIContent("Tension Scale", "Tension scale"), path2D.tensionScale);
                        }
                    }

                    path2D.zOffset = EditorGUILayout.FloatField(new GUIContent("Z Offset", "The offset on the Z axis for a foliage object."), path2D.zOffset);
                });
            }
            EditorGUI.indentLevel = 0;

            showFoliagePrefabList = EditorGUILayout.Foldout(showFoliagePrefabList, "Foliage Prefabs");

            if (showProperties)
            {
                EditorGUI.indentLevel = 1;
                InspectorBox(10, () =>
                {
                    path2D.foliagePrefabListSize = Mathf.Clamp(EditorGUILayout.IntField(new GUIContent("Size", "The size of the list that contains the foliage prefabs."), path2D.foliagePrefabListSize), 1, 200);

                    if (path2D.foliagePrefabListSize != path2D.foliagePrefabs.Count)
                    {
                        if (path2D.foliagePrefabListSize > path2D.foliagePrefabs.Count)
                        {
                            while (path2D.foliagePrefabListSize > path2D.foliagePrefabs.Count)
                            {
                                path2D.foliagePrefabs.Add(null);
                            }
                        }
                        else
                        {
                            while (path2D.foliagePrefabListSize < path2D.foliagePrefabs.Count)
                            {
                                int last = path2D.foliagePrefabs.Count - 1;
                                path2D.foliagePrefabs.RemoveAt(last);
                            }
                        }
                    }

                    for (int i = 0; i < path2D.foliagePrefabs.Count; i++)
                    {
                        path2D.foliagePrefabs[i] = EditorGUILayout.ObjectField(new GUIContent("Element " + i), path2D.foliagePrefabs[i], typeof(GameObject), true) as GameObject;
                    }
                });
            }
            EditorGUI.indentLevel = 0;

            if (GUILayout.Button("Clear and Fill"))
                path2D.ClearList();

            if (GUILayout.Button("Center Position"))
                path2D.ReCenterPivotPoint();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                path2D.RecreateFoliage();
            }

            if (Event.current.type == EventType.ValidateCommand)
            {
                switch (Event.current.commandName)
                {
                    case "UndoRedoPerformed":
                        path2D.RecreateFoliage();
                        break;
                }
            }
        }

        void OnSceneGUI()
        {
            Foliage2D_Path path = (Foliage2D_Path)target;

            GUIStyle iconStyle = new GUIStyle();
            iconStyle.alignment = TextAnchor.MiddleCenter;

            // draw the path line
            if (Event.current.type == EventType.Repaint)
            {
                DrawPath(path);
                if (handleSelected && !path.uniformValues && path.foliagePathType == Foliage2D_PathType.Smooth)
                {
                    DrawTensionBiasLine(index, path);
                }
            }

            // draw and interact with all the path handles
            UpdateHandles(path, iconStyle);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                path.RecreateFoliage();
            }

        }

        private void UpdateHandles(Foliage2D_Path path2D, GUIStyle iconStyle)
        {
            Quaternion inv = Quaternion.Inverse(path2D.transform.rotation);

            Handles.color = new Color(1, 1, 1, 0);
            Vector3 global, tGlobal = Vector3.zero;
            handleSelected = false;

            for (int i = 0; i < path2D.handlesPosition.Count; i++)
            {
                // global position of a path point
                Vector3 pos = path2D.transform.position + Vector3.Scale(new Vector3(path2D.handlesPosition[i].x, path2D.handlesPosition[i].y, 0), path2D.transform.localScale);
                Vector3 tPos = path2D.transform.position + Vector3.Scale(new Vector3(path2D.handleControlsPos[i].x, path2D.handleControlsPos[i].y, 0), path2D.transform.localScale);
                bool isSelected = selectedPoints.Contains(i);

                if (!handleSelected)
                    handleSelected = selectedPoints.Contains(i);

                Texture2D tex = null;
                tex = isSelected ? texDotSelected : texDot;

                if (IsVisible(pos))
                {
                    SetScale(pos, tex, ref iconStyle);
                    Handles.Label(pos, new GUIContent(tex), iconStyle);
                }

                if (!path2D.uniformValues && path2D.foliagePathType == Foliage2D_PathType.Smooth && IsVisible(tPos) && selectedPoints.Contains(i))
                {
                    SetScale(tPos, texBlueDot, ref iconStyle);
                    Handles.Label(tPos, new GUIContent(texBlueDot), iconStyle);
                }

                global = Handles.FreeMoveHandle(pos, Quaternion.identity, HandleScale(pos), Vector3.zero, Handles.CircleHandleCap);

                if (!path2D.uniformValues && path2D.foliagePathType == Foliage2D_PathType.Smooth)
                    tGlobal = Handles.FreeMoveHandle(tPos, Quaternion.identity, HandleScale(tPos), Vector3.zero, Handles.CircleHandleCap);

                if (global != pos)
                {
                    selectedPoints.Clear();
                    selectedPoints.Add(i);
                    isSelected = true;

                    Vector3 local = inv * (global - path2D.transform.position);

                    Vector2 relative = new Vector2(
                        local.x / path2D.transform.localScale.x,
                        local.y / path2D.transform.localScale.y) - path2D.handlesPosition[i];

                    path2D.handlesPosition[selectedPoints[0]] += relative;
                    path2D.handleControlsPos[selectedPoints[0]] += relative;

                    index = i;
                }

                if (tGlobal != tPos && !path2D.uniformValues && path2D.foliagePathType == Foliage2D_PathType.Smooth)
                {
                    Vector3 local = inv * (tGlobal - path2D.transform.position);

                    Vector2 relative = new Vector2(
                        local.x / path2D.transform.localScale.x,
                        local.y / path2D.transform.localScale.y) - path2D.handleControlsPos[i];

                    path2D.handleControlsPos[selectedPoints[0]] += relative;
                    path2D.nodeTension[i] = -Vector2.Distance(path2D.handleControlsPos[i], path2D.handlesPosition[i]) + 2f;

                    Vector2 line = path2D.handleControlsPos[i] - path2D.handlesPosition[i];
                    float angleInDeg = Mathf.Atan2(line.y, line.x) * Mathf.Rad2Deg;

                    if (angleInDeg > 0)
                        path2D.nodeBias[i] = -4 * angleInDeg / 180f;
                    else
                        path2D.nodeBias[i] = -4 * angleInDeg / 180f;
                }

                // make sure we can add new point at the midpoints!
                if (i + 1 < path2D.handlesPosition.Count)
                {
                    int index = i + 1;
                    Vector3 pos2 = path2D.transform.position + path2D.transform.rotation * Vector3.Scale(new Vector3(path2D.handlesPosition[index].x, path2D.handlesPosition[index].y, 0), path2D.transform.localScale);
                    Vector3 mid = (pos + pos2) / 2;
                    float handleScale = HandleScale(mid);

                    if (Handles.Button(mid, SceneView.lastActiveSceneView.camera.transform.rotation, handleScale, handleScale, Handles.CircleHandleCap))
                    {
                        Vector2 pt = inv * new Vector2((mid.x - path2D.transform.position.x) / path2D.transform.localScale.x, (mid.y - path2D.transform.position.y) / path2D.transform.localScale.y);

                        path2D.handlesPosition.Insert(index, pt);
                        path2D.handleControlsPos.Insert(index, new Vector2(pt.x + 2f, pt.y));
                        path2D.nodeTension.Insert(index, 0f);
                        path2D.nodeBias.Insert(index, 0f);
                    }
                    if (IsVisible(mid))
                    {
                        SetScale(mid, texDotPlus, ref iconStyle);
                        Handles.Label(mid, new GUIContent(texDotPlus), iconStyle);
                    }
                }

                // check if we want to remove points
                if (Event.current.alt && path2D.handlesPosition.Count > 2)
                {
                    float handleScale = HandleScale(pos);
                    if (IsVisible(pos))
                    {
                        SetScale(pos, texMinus, ref iconStyle);
                        Handles.Label(pos, new GUIContent(texMinus), iconStyle);
                    }

                    if (Handles.Button(pos, SceneView.lastActiveSceneView.camera.transform.rotation, handleScale, handleScale, Handles.CircleHandleCap))
                    {
                        if (!isSelected)
                        {
                            selectedPoints.Clear();
                            selectedPoints.Add(i);
                        }
                        for (int s = 0; s < selectedPoints.Count; s++)
                        {
                            path2D.handlesPosition.RemoveAt(selectedPoints[s]);
                            path2D.handleControlsPos.RemoveAt(selectedPoints[s]);
                            path2D.nodeTension.RemoveAt(selectedPoints[s]);
                            path2D.nodeBias.RemoveAt(selectedPoints[s]);

                            if (selectedPoints[s] <= i) i--;

                            for (int u = 0; u < selectedPoints.Count; u++)
                            {
                                if (selectedPoints[u] > selectedPoints[s]) selectedPoints[u] -= 1;
                            }
                        }
                        selectedPoints.Clear();
                        GUI.changed = true;
                    }
                }
            }
        }

        private void DrawPath(Foliage2D_Path path)
        {
            Handles.color = Color.white;
            List<Vector2> verts = path.handlesPosition;
            for (int i = 0; i < verts.Count - 1; i++)
            {
                Vector3 pos = path.transform.position + path.transform.rotation * Vector3.Scale(new Vector3(verts[i].x, verts[i].y, 0), path.transform.localScale);
                Vector3 pos2 = path.transform.position + path.transform.rotation * Vector3.Scale(new Vector3(verts[i + 1].x, verts[i + 1].y, 0), path.transform.localScale);
                Handles.DrawLine(pos, pos2);
            }
        }

        private void DrawTensionBiasLine(int index, Foliage2D_Path path)
        {
            Handles.color = Color.blue;

            Vector3 pos = path.transform.position + path.transform.rotation * Vector3.Scale(new Vector3(path.handlesPosition[index].x, path.handlesPosition[index].y, 0), path.transform.localScale);
            Vector3 pos2 = path.transform.position + path.transform.rotation * Vector3.Scale(new Vector3(path.handleControlsPos[index].x, path.handleControlsPos[index].y, 0), path.transform.localScale);
            Handles.DrawLine(pos, pos2);
        }

        private static float GetCameraDist(Vector3 aPt)
        {
            return Vector3.Distance(SceneView.lastActiveSceneView.camera.transform.position, aPt);
        }

        private bool IsVisible(Vector3 aPos)
        {
            Transform t = SceneView.lastActiveSceneView.camera.transform;
            if (Vector3.Dot(t.forward, aPos - t.position) > 0)
                return true;
            return false;
        }

        private void SetScale(Vector3 aPos, Texture aIcon, ref GUIStyle aStyle, float aScaleOverride = 1)
        {
            float max = (Screen.width + Screen.height) / 2;
            float dist = SceneView.lastActiveSceneView.camera.orthographic ? SceneView.lastActiveSceneView.camera.orthographicSize / 0.5f : GetCameraDist(aPos);

            float div = (dist / (max / 160));
            float mul = pathScale * aScaleOverride;

            aStyle.fixedWidth = (aIcon.width / div) * mul;
            aStyle.fixedHeight = (aIcon.height / div) * mul;
        }

        private static float HandleScale(Vector3 aPos)
        {
            float dist = SceneView.lastActiveSceneView.camera.orthographic ? SceneView.lastActiveSceneView.camera.orthographicSize / 0.45f : GetCameraDist(aPos);
            return Mathf.Min(0.4f * pathScale, (dist / 5.0f) * 0.4f * pathScale);
        }

        private static Texture2D GetGizmo(string aFileName)
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath("Assets/Foliage2D/Gizmos/" + aFileName, typeof(Texture2D)) as Texture2D;
            if (tex == null)
            {
                tex = EditorGUIUtility.whiteTexture;
                Debug.Log("Couldn't load Gizmo tex " + aFileName);
            }
            return tex;
        }

        public void InspectorBox(int aBorder, System.Action inside, int aWidthOverride = 0, int aHeightOverride = 0)
        {
            Rect r = EditorGUILayout.BeginHorizontal(GUILayout.Width(aWidthOverride));
            if (aWidthOverride != 0)
            {
                r.width = aWidthOverride;
            }
            GUI.Box(r, GUIContent.none);
            GUILayout.Space(aBorder);
            if (aHeightOverride != 0)
                EditorGUILayout.BeginVertical(GUILayout.Height(aHeightOverride));
            else
                EditorGUILayout.BeginVertical();
            GUILayout.Space(aBorder);
            inside();
            GUILayout.Space(aBorder);
            EditorGUILayout.EndVertical();
            GUILayout.Space(aBorder);
            EditorGUILayout.EndHorizontal();
        }
    }
}
