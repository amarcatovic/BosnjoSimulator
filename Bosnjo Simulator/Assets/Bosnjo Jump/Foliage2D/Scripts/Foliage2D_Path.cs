using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Foliage
{
    #region Enums
    /// <summary>
    /// Describes how the foliage objects should be arranged on the path lines.
    /// </summary>
    public enum Foliage2D_Pattern
    {
        /// <summary>
        /// When a new object must be instantiated, a random value between 0 
        /// and the max number of foliage prefabs, is generated. This value is 
        /// used to decide which prefab to instantiate.
        /// </summary>
        Random,
        /// <summary>
        /// The objects are instantiated and arranged in a consecutive order.
        /// </summary>
        Consecutive
    }

    /// <summary>
    /// Describes the overlapping type. The value of the overlapping factor determines which part of a 
    /// foliage object mesh will be placed to the left of the right edge of the previous foliage object mesh.
    /// </summary>
    public enum Foliage2D_OverlappingType
    {
        /// <summary>
        /// The overlapping value is constant for all objects.
        /// </summary>
        Fixed,
        /// <summary>
        /// The overlapping value is a random value between a max and min for every object.
        /// </summary>
        Random
    }

    /// <summary>
    /// Describes the path type.
    /// </summary>
    public enum Foliage2D_PathType
    {
        /// <summary>
        /// The foliage objects are arranged in straight line and bottom aligned to the path lines.
        /// </summary>
        Linear,
        /// <summary>
        /// The foliage objects are arranged on a smoothed line.
        /// </summary>
        Smooth
    }
    #endregion

    public class Foliage2D_Path : MonoBehaviour
    {
        #region Private fields
        /// <summary>
        /// Instace to a Foliage2D object script.
        /// </summary>
        private Foliage2D foliage2D;
        /// <summary>
        /// Foliage object position offset. This is what places the foliage object on top of the path line.
        /// </summary>
        private Vector2 posOffset;
        /// <summary>
        /// The normal of a path segment.
        /// </summary>
        private Vector2 lineNormal;
        /// <summary>
        /// A point on the line described by 2 path nodes.
        /// </summary>
        private Vector2 pointOnTheLine;
        /// <summary>
        /// A line described by 2 path nodes.
        /// </summary>
        private Vector2 line;
        /// <summary>
        /// The mesh width of a foliage object.
        /// </summary>
        private float foliagePrefabWidth;
        /// <summary>
        /// How far from the start point of a line described by 2 path nodes, the current object should be placed.
        /// </summary>
        private float distanceFromStart = 0;
        /// <summary>
        /// How far from the start point of a line described by 2 path nodes, the previous object was placed.
        /// </summary>
        private float prevDistanceFromStart = 0;
        /// <summary>
        /// The mesh width of the previous foliage object.
        /// </summary>
        private float previousWidth = 0;
        /// <summary>
        /// The length of the line described by 2 path nodes.
        /// </summary>
        private float lineLength;
        /// <summary>
        /// The angle in radians of a foliage object.
        /// </summary>
        private float angleInRad;
        /// <summary>
        /// The angle in degrees of a foliage object.
        /// </summary>
        private float angleInDeg;
        /// <summary>
        /// The total number of foliage objects that are placed on the current path.
        /// </summary>
        private int foliageCount;
        /// <summary>
        /// The index of the current foliage object.
        /// </summary>
        private int objectIndex;
        /// <summary>
        /// The index of the previous foliage object.
        /// </summary>
        private int prevIndex;
        /// <summary>
        /// The total number of foliage prefabs.
        /// </summary>
        private int maxCount;
        /// <summary>
        /// The index of a foliage prefab. It is used to decide which prefab to instantiate.
        /// </summary>
        private int prefabIndex = -1;
        /// <summary>
        /// It is used to determine if the "prefabIndex" value changed.
        /// </summary>
        private bool updatedPrefabIndex = false;
        #endregion

        #region Public fields
        public Foliage2D_Pattern foliagePattern = Foliage2D_Pattern.Random;
        public Foliage2D_OverlappingType foliageOverlapType = Foliage2D_OverlappingType.Fixed;
        public Foliage2D_PathType foliagePathType = Foliage2D_PathType.Linear;
        /// <summary>
        /// The local positions of the path handles.
        /// </summary>
        public List<Vector2> handlesPosition = new List<Vector2>();
        /// <summary>
        /// The local positions of the handles that control the tension and bias of a path node.
        /// </summary>
        public List<Vector2> handleControlsPos = new List<Vector2>();
        /// <summary>
        /// The list of foliage objects that are placed on the current path.
        /// </summary>
        public List<GameObject> foliageOnPath = new List<GameObject>();
        /// <summary>
        /// The list of foliage prefabs.
        /// </summary>
        public List<GameObject> foliagePrefabs = new List<GameObject>();
        /// <summary>
        /// The list of tensions for each path node.
        /// </summary>
        public List<float> nodeTension = new List<float>();
        /// <summary>
        /// The list of bias for each path node.
        /// </summary>
        public List<float> nodeBias = new List<float>();
        /// <summary>
        /// The value of the overlapping factor determines which part of a foliage object mesh 
        /// will be placed to the left of the right edge of the previous foliage object mesh.
        /// </summary>
        public float overlappingFactor = 0.4f;
        /// <summary>
        /// The min value that the "overlappingFactor" should have.
        /// </summary>
        public float minOverlappingFactor = 0f;
        /// <summary>
        /// The max value that the "overlappingFactor" should have.
        /// </summary>
        public float maxOverlappingFactor = 0.8f;
        /// <summary>
        /// The bias of every path node.
        /// </summary>
        public float bias = 0f;
        /// <summary>
        /// The tension of every path node.
        /// </summary>
        public float tension = 0f;
        /// <summary>
        /// The offset for the first object on a path line.
        /// </summary>
        public float firstObjectOffset = 0f;
        /// <summary>
        /// The offset for the last object on a path line.
        /// </summary>
        public float lastObjectOffset = 0f;
        /// <summary>
        /// The bias scale.
        /// </summary>
        public float biasScale = 4f;
        /// <summary>
        /// The tension scale.
        /// </summary>
        public float tensionScale = 4f;
        /// <summary>
        /// The offset on the Z axis for a foliage object.
        /// </summary>
        public float zOffset = 0.01f;
        /// <summary>
        /// When set to true the tension and bias will be the same for all path nodes.
        /// </summary>
        public bool uniformValues = true;
        /// <summary>
        /// The size of the list that contains the foliage prefabs.
        /// </summary>
        public int foliagePrefabListSize = 1;
        #endregion

        #region Class methods
        /// <summary>
        /// Fills the foliage path with objects.
        /// </summary>
        public void RecreateFoliage()
        {
            foliageCount = foliageOnPath.Count;
            distanceFromStart = 0;
            prevDistanceFromStart = 0;
            maxCount = 0;
            float ZAxisOffset = zOffset;

            List<GameObject> foliageP = new List<GameObject>();
            int len = foliagePrefabs.Count;

            foliageP.Clear();
            // The foliage prefabs are placed in a new list to make sure that there are no null fields.
            for (int k = 0; k < len; k++)
            {
                if (foliagePrefabs[k] != null)
                    foliageP.Add(foliagePrefabs[k]);
            }
            maxCount = foliageP.Count;

            // If the prefab list is empty return.
            if (maxCount == 0)
                return;
            objectIndex = 1;

            // Foliage objects are placed on the line described by the path nodes.
            for (int i = 0; i < handlesPosition.Count - 1; i++)
            {
                line = handlesPosition[i + 1] - handlesPosition[i];
                angleInRad = Mathf.Atan2(line.y, line.x);
                angleInDeg = angleInRad * Mathf.Rad2Deg;
                lineLength = line.magnitude;
                // The vector is normalized so that we can calculate its normal correctly.
                line.Normalize();
                lineNormal = new Vector2(-line.y, line.x);
                distanceFromStart = 0;
                prevDistanceFromStart = 0;

                // This loop is executed until the distance between the start of the line and a 
                // foliage object is smaller than the length of the line.
                while (true)
                {
                    updatedPrefabIndex = false;

                    // If a new object must be instantiated, updates the "prefabIndex" value.
                    // The value of "prefabIndex" determines which prefab will be instantiated
                    // in the current iteration.
                    if (foliageCount < objectIndex)
                    {
                        if (foliagePattern == Foliage2D_Pattern.Random)
                        {
                            // Get a random index.
                            prefabIndex = Random.Range(0, maxCount);
                        }
                        else
                        {
                            // The value of "prefabIndex" is incremented by 1 so that new objects are instantiated consecutively.
                            prefabIndex++;
                            // If the value of "prefab Index" is bigger than "maxCount" or smaller than 0, we reset it to 0.
                            if (prefabIndex >= maxCount || prefabIndex < 0)
                                prefabIndex = 0;
                        }

                        updatedPrefabIndex = true;
                        // We get the Foliage2D component of the prefab with the index of "prefabIndex" 
                        // so that we have access to the width of the correct foliage object when we 
                        // calculate the distance from the start of the line where this object will be placed.
                        foliage2D = foliageP[prefabIndex].GetComponent<Foliage2D>();
                        DistanceFromStart();
                    }
                    else
                    {
                        foliage2D = foliageOnPath[objectIndex - 1].GetComponent<Foliage2D>();
                        DistanceFromStart();
                    }

                    // If the distance from the start of the line where the last object was placed is
                    // bigger than the line length, the position of the object is reset and the execution
                    // of the while loop is terminated.
                    if (distanceFromStart > lineLength)
                    {
                        foliage2D = foliageOnPath[prevIndex].GetComponent<Foliage2D>();
                        distanceFromStart = lineLength - foliage2D.width / 2f + lastObjectOffset;

                        if (foliagePathType == Foliage2D_PathType.Smooth)
                            SmoothPath(i);
                        else
                            pointOnTheLine = Vector2.Lerp(handlesPosition[i], handlesPosition[i + 1], distanceFromStart / lineLength);

                        posOffset = lineNormal * (foliage2D.height / 2f);
                        foliageOnPath[prevIndex].transform.position = transform.TransformPoint(new Vector3(pointOnTheLine.x + posOffset.x, pointOnTheLine.y + posOffset.y, ZAxisOffset));

                        if (updatedPrefabIndex && foliagePattern == Foliage2D_Pattern.Consecutive)
                            RestorePrefabIndex();

                        break;
                    }

                    if (foliagePathType == Foliage2D_PathType.Smooth)
                        SmoothPath(i);
                    else
                        pointOnTheLine = Vector2.Lerp(handlesPosition[i], handlesPosition[i + 1], distanceFromStart / lineLength);

                    if (foliageCount < objectIndex)
                    {
                        // If the index of the current object is bigger than the total number of 
                        // instantiated objects, a new object is instantiated.
                        ZAxisOffset *= -1;
                        Vector3 pos = pointOnTheLine + posOffset;
                        pos.z = zOffset;
                        GameObject obj = Instantiate(foliageP[prefabIndex], transform.TransformPoint(pos), Quaternion.Euler(new Vector3(0, 0, angleInDeg))) as GameObject;
                        obj.transform.parent = transform;
                        foliageOnPath.Add(obj);
                        prevIndex = foliageOnPath.Count - 1;
                        foliageCount = foliageOnPath.Count;
                        Foliage2D objFoliage = obj.GetComponent<Foliage2D>();
                        // The object mesh is recreated so that we don't have 2 objects with the same mesh instance.
                        objFoliage.RebuildMesh();
                        objectIndex++;
                    }
                    else
                    {
                        ZAxisOffset *= -1;
                        prevIndex = objectIndex - 1;
                        foliageOnPath[objectIndex - 1].transform.position = transform.TransformPoint(new Vector3(pointOnTheLine.x + posOffset.x, pointOnTheLine.y + posOffset.y, ZAxisOffset));
                        foliageOnPath[objectIndex - 1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleInDeg));
                        objectIndex++;
                    }
                }
            }

            // If the path shrinked and there is an excess of foliage objects, delete them.
            if (foliageCount + 1 > objectIndex)
            {
                int lenD = foliageCount + 1 - objectIndex;

                for (int i = 0; i < lenD; i++)
                {
                    int last = foliageOnPath.Count - 1;
                    DestroyImmediate(foliageOnPath[last]);
                    foliageOnPath.RemoveAt(last);

                    if (foliagePattern == Foliage2D_Pattern.Consecutive)
                        RestorePrefabIndex();
                }
            }
        }

        /// <summary>
        /// This method is called only when the foliage pattern is set to consecutive.
        /// Restores the prefab index when an object is deleted or this is the last object
        /// on the line.
        /// </summary>
        private void RestorePrefabIndex()
        {
            prefabIndex--;
            if (prefabIndex == -1)
                prefabIndex = maxCount - 1;
        }

        /// <summary>
        /// Calculates the distance from the start of the line, described by 2 path nodes, 
        /// where the current object must be placed
        /// </summary>
        private void DistanceFromStart()
        {
            // This segment of code is executed if this is the first object on the path
            // or the first on the line described by 2 path nodes.
            if (objectIndex == 1 || distanceFromStart == 0)
            {
                distanceFromStart = foliage2D.width / 2f + firstObjectOffset;

                // If the value of "firstObjectOffset" is negative and its absolute value is equal to 
                // half the width of the mesh, we would get stuck in an infinite loop, so we make sure 
                // that "distanceFromStart" never has the value of 0.
                if (distanceFromStart == 0)
                    distanceFromStart = 0.0001f;

                // The distance from the start point of the current line and mesh width of 
                // the current object are saved so that we can determine where the next object 
                // should be placed on the line in relation to current object.
                prevDistanceFromStart = distanceFromStart;
                previousWidth = foliage2D.width;
                // This makes sure that no matter the height of the mesh, all foliage objects are 
                // bottom aligned to the foliage path. 
                posOffset = lineNormal * (foliage2D.height / 2f);
            }
            else
            {
                float currentOverlappingFactor;

                if (foliageOverlapType == Foliage2D_OverlappingType.Fixed)
                    currentOverlappingFactor = overlappingFactor;
                else
                    currentOverlappingFactor = Random.Range(minOverlappingFactor, maxOverlappingFactor);

                // The position on the line for the current object is calculated based on 
                // the value of "currentOverlappingFactor" and the position on the line
                // and mesh width of the previous object.
                if (currentOverlappingFactor <= 0.5f)
                    distanceFromStart = prevDistanceFromStart + (previousWidth / 2f) + ((foliage2D.width / 2f) - (foliage2D.width * currentOverlappingFactor));
                else
                    distanceFromStart = prevDistanceFromStart + (previousWidth / 2f) - ((foliage2D.width * currentOverlappingFactor) - (foliage2D.width / 2f));

                prevDistanceFromStart = distanceFromStart;
                previousWidth = foliage2D.width;
                posOffset = lineNormal * (foliage2D.height / 2f);
            }
        }

        /// <summary>
        /// Smooths a path segment.
        /// </summary>
        /// <param name="index">Index of the path node.</param>
        private void SmoothPath(int index)
        {
            // The first of the 4 path points.
            Vector2 startPoint = Vector2.zero;
            // The last of the 4 path points.
            Vector2 endPoint = Vector2.zero;
            Vector2 tanget;

            // If this is the first path node, an imaginary node is created that has the same slope as the start node.
            if (index == 0)
                startPoint = new Vector2(handlesPosition[index].x - 1 * line.x, handlesPosition[index].y - 1 * line.y);
            else
                startPoint = handlesPosition[index - 1];

            // If this is the last path node minus 2, an imaginary node is created that has the same slope as the last node.
            if (index == handlesPosition.Count - 2)
                endPoint = new Vector2(handlesPosition[index + 1].x - 1 * line.x, handlesPosition[index + 1].y - 1 * line.y);
            else
                endPoint = handlesPosition[index + 2];
            
            // The smoothed position of the object is calculated using the hermite interpolation method.  
            pointOnTheLine = new Vector2(
                HermiteInterpolation(startPoint.x, handlesPosition[index].x, handlesPosition[index + 1].x, endPoint.x, index),
                HermiteInterpolation(startPoint.y, handlesPosition[index].y, handlesPosition[index + 1].y, endPoint.y, index));

            tanget = GetTangent(index, startPoint, endPoint);
            angleInRad = Mathf.Atan2(tanget.y, tanget.x);
            angleInDeg = angleInRad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculates the position of an object using the hermite interpolation method.
        /// </summary>
        /// <param name="p1"> The position on the x or y axis of the first node. </param>
        /// <param name="p2"> The position on the x or y axis of the second node. </param>
        /// <param name="p3"> The position on the x or y axis of the third node. </param>
        /// <param name="p4"> The position on the x or y axis of the fourth node. </param>
        /// <param name="index"> Index of the main path node. </param>
        /// <returns> The interpolated position of an object on the X or Y axis. </returns>
        private float HermiteInterpolation(float p1, float p2, float p3, float p4, int index)
        {
            float m0, m1, mu, mu2, mu3;
            float a0, a1, a2, a3;
            float currentTension;
            float currentBias;

            mu = distanceFromStart / lineLength;

            if (uniformValues)
            {
                currentTension = tension;
                currentBias = bias;
            }
            else
            {
                currentTension = Mathf.Lerp(nodeTension[index], nodeTension[index + 1], mu);
                currentBias = Mathf.Lerp(nodeBias[index], nodeBias[index + 1], mu);
            }

            mu2 = mu * mu;
            mu3 = mu2 * mu;
            m0 = (p2 - p1) * (1 + currentBias) * (1 - currentTension) / 2 + (p3 - p2) * (1 - currentBias) * (1 - currentTension) / 2;
            m1 = (p3 - p2) * (1 + currentBias) * (1 - currentTension) / 2 + (p4 - p3) * (1 - currentBias) * (1 - currentTension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * p2 + a1 * m0 + a2 * m1 + a3 * p3);
        }

        private Vector2 GetTangent(int index, Vector2 startPoint, Vector2 endPoint)
        {
            float mu = distanceFromStart / lineLength;
            return new Vector2(
                HermiteSlope(startPoint.x, handlesPosition[index].x, handlesPosition[index + 1].x, endPoint.x, mu, index),
                HermiteSlope(startPoint.y, handlesPosition[index].y, handlesPosition[index + 1].y, endPoint.y, mu, index));
        }

        private float HermiteSlope(float p1, float p2, float p3, float p4, float mu, int index)
        {
            float m0, m1, mu2;
            float a0, a1, a2, a3;
            float currentTension;
            float currentBias;

            if (uniformValues)
            {
                currentTension = tension;
                currentBias = bias;
            }
            else
            {
                currentTension = Mathf.Lerp(nodeTension[index], nodeTension[index + 1], mu);
                currentBias = Mathf.Lerp(nodeBias[index], nodeBias[index + 1], mu);
            }

            mu2 = mu * mu;
            m0 = ((1 - currentTension) * (currentBias + 1) * (p2 - p1) + (1 - currentBias) * (1 - currentTension) * (p3 - p2)) / 2;
            m1 = ((1 - currentTension) * (currentBias + 1) * (p3 - p2) + (1 - currentBias) * (1 - currentTension) * (p4 - p3)) / 2;
            a0 = (3 * mu2 - 4 * mu + 1);
            a1 = (3 * mu2 - 2 * mu);
            a2 = p2 * (6 * mu2 - 6 * mu);
            a3 = p3 * (6 * mu - 6 * mu2);

            return (a0 * m0 + a1 * m1 + a2 + a3);
        }

        /// <summary>
        /// Moves the object location to the center of the handles. Also offsets the handles locations to match.
        /// </summary>
        public void ReCenterPivotPoint()
        {
            Vector2 center = Vector2.zero;

            for (int i = 0; i < handlesPosition.Count; i++)
            {
                center += handlesPosition[i];
            }

            center = center / handlesPosition.Count + new Vector2(transform.position.x, transform.position.y);
            Vector2 offset = center - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            for (int i = 0; i < handlesPosition.Count; i++)
            {
                handlesPosition[i] -= offset;
            }

            gameObject.transform.position = new Vector3(center.x, center.y, gameObject.transform.position.z);
            RecreateFoliage();
        }

        /// <summary>
        /// Deletes all objects on the current path and instantiates new objects.
        /// </summary>
        public void ClearList()
        {
            int len = foliageOnPath.Count - 1;

            for (int i = len; i >= 0; i--)
            {
                DestroyImmediate(foliageOnPath[i]);
                foliageOnPath.RemoveAt(i);
            }
            prefabIndex = -1;
            foliageOnPath.Clear();
            RecreateFoliage();
        }

        /// <summary>
        /// Adds a node to the foliage path.
        /// </summary>
        /// <param name="hP">Node local position.</param>
        public void AddPathPoint(Vector2 hP)
        {
            handlesPosition.Add(hP);
            handleControlsPos.Add(new Vector2(hP.x + 2f, hP.y));
            nodeTension.Add(0f);
            nodeBias.Add(0f);
        }
        #endregion
    }
}
