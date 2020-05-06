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
    [CustomEditor(typeof(Foliage2D_Sprite))]
    public class Foliage2D_SpriteEditor : Editor
    {

        bool showVisuals = true;

        void OnEnable()
        {
            Foliage2D_Sprite sprite = (Foliage2D_Sprite)target;
            if (sprite.GetComponent<MeshFilter>().sharedMesh == null)
                sprite.RebuildMesh();

        }

        // Inspector Fields
        public override void OnInspectorGUI()
        {
            Foliage2D_Sprite foliage2D = (Foliage2D_Sprite)target;
            CustomInspector(foliage2D);
        }

        //------------------------------------------------------------------------------

        private void CustomInspector(Foliage2D_Sprite foliage2D_sprite)
        {
            showVisuals = EditorGUILayout.Foldout(showVisuals, "Visual Properties");

            if (showVisuals)
            {
                EditorGUI.indentLevel = 1;

                foliage2D_sprite.pixelsPerUnit = Mathf.Clamp(EditorGUILayout.FloatField("Pixels Per Unit", foliage2D_sprite.pixelsPerUnit), 1, 768);
                foliage2D_sprite.widthSegments = Mathf.Clamp(EditorGUILayout.IntField("With Segments", foliage2D_sprite.widthSegments), 1, 100);
                foliage2D_sprite.heightSegments = Mathf.Clamp(EditorGUILayout.IntField("Height Segments", foliage2D_sprite.heightSegments), 1, 100);

                Type utility = Type.GetType("UnityEditorInternal.InternalEditorUtility, UnityEditor");
                if (utility != null)
                {
                    PropertyInfo sortingLayerNames = utility.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
                    if (sortingLayerNames != null)
                    {
                        string[] layerNames = sortingLayerNames.GetValue(null, null) as string[];
                        string currName = foliage2D_sprite.GetComponent<Renderer>().sortingLayerName == "" ? "Default" : foliage2D_sprite.GetComponent<Renderer>().sortingLayerName;
                        int nameID = EditorGUILayout.Popup("Sorting Layer", Array.IndexOf(layerNames, currName), layerNames);

                        foliage2D_sprite.GetComponent<Renderer>().sortingLayerName = layerNames[nameID];

                    }
                    else
                    {
                        foliage2D_sprite.GetComponent<Renderer>().sortingLayerID = EditorGUILayout.IntField("Sorting Layer", foliage2D_sprite.GetComponent<Renderer>().sortingLayerID);
                    }
                }
                else
                {
                    foliage2D_sprite.GetComponent<Renderer>().sortingLayerID = EditorGUILayout.IntField("Sorting Layer", foliage2D_sprite.GetComponent<Renderer>().sortingLayerID);
                }
                foliage2D_sprite.GetComponent<Renderer>().sortingOrder = EditorGUILayout.IntField("Order in Layer", foliage2D_sprite.GetComponent<Renderer>().sortingOrder);

            }
            EditorGUI.indentLevel = 0;

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                foliage2D_sprite.RebuildMesh();
            }
        }
    }
}

