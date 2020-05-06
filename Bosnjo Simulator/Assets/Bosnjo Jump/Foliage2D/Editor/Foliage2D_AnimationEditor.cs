using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Foliage
{
    [CustomEditor(typeof(Foliage2D_Animation))]
    public class Foliage2D_AnimationEditor : Editor
    {
        private bool showProperties = true;
        private bool showOffsetFactors = true;

        // Inspector Fields
        public override void OnInspectorGUI()
        {
            Foliage2D_Animation anim = (Foliage2D_Animation)target;
            CustomInspector(anim);
        }

        private void CustomInspector(Foliage2D_Animation anim)
        {
            Undo.RecordObject(target, "Modified Inspector");

            showProperties = EditorGUILayout.Foldout(showProperties, "Animation");

            if (showProperties)
            {
                EditorGUI.indentLevel = 1;
                InspectorBox(10, () =>
                {
                    anim.foliageBending = (Foliage2D_MeshBending)EditorGUILayout.EnumPopup(new GUIContent("Mesh Bending ", "Describes the type of bending that should be applied to the foliage mesh."), anim.foliageBending);

                    anim.changeAnimationSpeed = EditorGUILayout.Toggle(new GUIContent("Asynchronous Anim Start", "When set to true, the speed of the animation is changed for a short period of time. " 
                        + "This change takes place only once at the start of the game. This is useful if you don't want the foliage objects to have synchronous animations."), anim.changeAnimationSpeed);
                    if (anim.changeAnimationSpeed)
                    {
                        anim.minSpeed = EditorGUILayout.FloatField(new GUIContent("Min Speed", "The min value for a random number used to change the playback speed of an animation."), anim.minSpeed);
                        anim.maxSpeed = EditorGUILayout.FloatField(new GUIContent("Max Speed", "The max value for a random number used to change the playback speed of an animation."), anim.maxSpeed);

                        anim.minSeconds = EditorGUILayout.FloatField(new GUIContent("Min Seconds", "The min value for a random number. The value of this " 
                            + "number determines how many seconds should the animation speed offset last."), anim.minSeconds);
                        anim.maxSeconds = EditorGUILayout.FloatField(new GUIContent("Max Seconds", "The max value for a random number. The value of this " 
                            + " number determines how many seconds should the animation speed be offset."), anim.maxSeconds);


                    }
                });
            }
            EditorGUI.indentLevel = 0;

            showOffsetFactors = EditorGUILayout.Foldout(showOffsetFactors, "Offset Factors");

            if (showOffsetFactors)
            {
                EditorGUI.indentLevel = 1;
                InspectorBox(10, () =>
                {
                    Foliage2D foliage2D = anim.GetComponent<Foliage2D>();

                    if (anim.offsetFactor.Count != foliage2D.heightSegments + 1)
                    {

                        anim.offsetFactor.Clear();

                        int len = foliage2D.heightSegments + 1;
                        float offset = 1f / foliage2D.heightSegments;

                        for (int i = 0; i < len; i++)
                        {
                            anim.offsetFactor.Add(offset * i);
                        }
                    }


                    for (int i = anim.offsetFactor.Count - 1; i >= 0; i--)
                    {
                        anim.offsetFactor[i] = EditorGUILayout.Slider(new GUIContent("Offset " + i, "Determines which part of the animated variable "
                            + " 'offset' will be used to calculate the new vertex position."), anim.offsetFactor[i], 0, 1);
                    }
                });
            }

            EditorGUI.indentLevel = 0;
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
