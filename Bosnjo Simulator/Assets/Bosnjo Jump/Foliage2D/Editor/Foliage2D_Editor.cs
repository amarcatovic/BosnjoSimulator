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
    [CustomEditor(typeof(Foliage2D))]
    public class Foliage2D_Editor : Editor
    {
        bool showVisuals = true;

        void OnEnable()
        {
            Foliage2D foliage = (Foliage2D)target;
            if (foliage.GetComponent<MeshFilter>().sharedMesh == null)
                foliage.RebuildMesh();
        }

        // Inspector Fields
        public override void OnInspectorGUI()
        {
            Foliage2D foliage2D = (Foliage2D)target;
            CustomInspector(foliage2D);
        }

        /// <summary>
        /// Custom inspector.
        /// </summary>
        private void CustomInspector(Foliage2D foliage2D)
        {
            Undo.RecordObject(target, "Modified Inspector");
            showVisuals = EditorGUILayout.Foldout(showVisuals, "Visual Properties");

            if (showVisuals)
            {
                EditorGUI.indentLevel = 1;
                InspectorBox(10, () =>
                {
                    foliage2D.pixelsPerUnit = Mathf.Clamp(EditorGUILayout.FloatField(new GUIContent("Pixels To Units", "The number of pixels in 1 Unity unit."), foliage2D.pixelsPerUnit), 1, 768);
                    foliage2D.widthSegments = Mathf.Clamp(EditorGUILayout.IntField(new GUIContent("With Segments", "The number of columns the mesh has."), foliage2D.widthSegments), 1, 100);
                    foliage2D.heightSegments = Mathf.Clamp(EditorGUILayout.IntField(new GUIContent("Height Segments", "The number of rows the mesh has."), foliage2D.heightSegments), 1, 100);

                    Type utility = Type.GetType("UnityEditorInternal.InternalEditorUtility, UnityEditor");
                    if (utility != null)
                    {
                        PropertyInfo sortingLayerNames = utility.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
                        if (sortingLayerNames != null)
                        {
                            string[] layerNames = sortingLayerNames.GetValue(null, null) as string[];
                            string currName = foliage2D.GetComponent<Renderer>().sortingLayerName == "" ? "Default" : foliage2D.GetComponent<Renderer>().sortingLayerName;
                            int nameID = EditorGUILayout.Popup("Sorting Layer", Array.IndexOf(layerNames, currName), layerNames);

                            foliage2D.GetComponent<Renderer>().sortingLayerName = layerNames[nameID];

                        }
                        else
                        {
                            foliage2D.GetComponent<Renderer>().sortingLayerID = EditorGUILayout.IntField("Sorting Layer", foliage2D.GetComponent<Renderer>().sortingLayerID);
                        }
                    }
                    else
                    {
                        foliage2D.GetComponent<Renderer>().sortingLayerID = EditorGUILayout.IntField("Sorting Layer", foliage2D.GetComponent<Renderer>().sortingLayerID);
                    }
                    foliage2D.GetComponent<Renderer>().sortingOrder = EditorGUILayout.IntField("Order in Layer", foliage2D.GetComponent<Renderer>().sortingOrder);
                });
            }
            EditorGUI.indentLevel = 0;

            if (GUILayout.Button("Rebuild Mesh"))
                foliage2D.RebuildMesh();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                foliage2D.RebuildMesh();
            }

            if (Event.current.type == EventType.ValidateCommand)
            {
                switch (Event.current.commandName)
                {
                    case "UndoRedoPerformed":
                        foliage2D.RebuildMesh();
                        break;
                }
            }
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
