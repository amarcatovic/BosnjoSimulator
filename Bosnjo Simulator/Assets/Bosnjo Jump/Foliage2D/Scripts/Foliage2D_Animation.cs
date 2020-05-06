using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Foliage
{
    #region Enums
    /// <summary>
    /// Describes the type of bending that should be applied to the foliage mesh.
    /// </summary>
    public enum Foliage2D_MeshBending
    {
        /// <summary>
        /// Applies a simple bending to the foliage mesh by offsetting
        /// the position of the mesh vertices.
        /// </summary>
        Simple,
        /// <summary>
        /// Applies a smart bending to the foliage mesh by rotating the vertices around a point.
        /// </summary>
        Smart
    }
    #endregion

    public class Foliage2D_Animation : MonoBehaviour
    {
        #region Private fields
        // List of objects that have a 2D collider and are staying in the current trigger collider.
        private List<Collider2D> collider2DObject;
        // List of objects that have a 3D collider and are staying in the current trigger collider.
        private List<Collider> collider3DObject;
        // The distance between the foliage object and an object that entered its
        // trigger collider.
        private List<float> enterOffset;
        /// <summary>
        /// The initial local position of the mesh vertices.
        /// </summary>
        private Vector3[] initialVertexPos;
        /// <summary>
        /// The final local position of the mesh vertices.
        /// </summary>
        private Vector3[] finalVertexPos;
        /// <summary>
        /// The amount by which to change the initial position of a vertex.
        /// The change in position for the vertices on the same row is the same.
        /// </summary>
        private Vector3[] posOffset;
        /// <summary>
        /// A series of points around which to rotate the mesh vertices.
        /// </summary>
        private Vector2[] centerLinePoints;
        /// <summary>
        /// Reference to the Foliage2D component of this object.
        /// </summary>
        private Foliage2D foliage2D;
        /// <summary>
        /// Reference to the Animator component of this object.
        /// </summary>
        private Animator animator;
        /// <summary>
        /// Reference to the Mesh component of this object.
        /// </summary>
        private Mesh meshFilter;
        /// <summary>
        /// Did an object bend this foliage object?.
        /// </summary>
        private bool isBending = false;
        /// <summary>
        /// The number of verices a horizontal row has.
        /// </summary>
        private int horizontalVerts;
        /// <summary>
        /// The amount in degrees by which to rotate a vertex around a point.
        /// </summary>
        private float[] anglesInDeg;
        #endregion

        #region Public fields
        public Foliage2D_MeshBending foliageBending = Foliage2D_MeshBending.Simple;
        /// <summary>
        /// A list of values that define which part of the "offset" value
        /// should be used to calculate the new vertex position.
        /// </summary>
        public List<float> offsetFactor = new List<float>();
        /// <summary>
        /// This is the value that should be changed to animate the mesh.
        /// </summary>
        public Vector3 offset;
        /// <summary>
        /// Should we change the animation speed at the start?. 
        /// </summary>
        public bool changeAnimationSpeed = false;
        /// <summary>
        /// The min value for a random animation speed.
        /// </summary>
        public float minSpeed = 0.5f;
        /// <summary>
        /// The max value for a random animation speed.
        /// </summary>
        public float maxSpeed = 2.0f;
        /// <summary>
        /// The min value for a random number.
        /// </summary>
        public float minSeconds = 0;
        /// <summary>
        /// The max value for a random number.
        /// </summary>
        public float maxSeconds = 1.5f;
        #endregion

        #region Class methods
        void Start()
        {
            animator = GetComponent<Animator>();
            foliage2D = GetComponent<Foliage2D>();
            meshFilter = GetComponent<MeshFilter>().sharedMesh;
            initialVertexPos = meshFilter.vertices;
            horizontalVerts = foliage2D.widthSegments + 1;
            finalVertexPos = meshFilter.vertices;
            collider2DObject = new List<Collider2D>();
            collider3DObject = new List<Collider>();
            enterOffset = new List<float>();
            anglesInDeg = new float[foliage2D.heightSegments + 1];
            posOffset = new Vector3[foliage2D.heightSegments + 1];
            centerLinePoints = new Vector2[foliage2D.heightSegments + 1];

            // We change the default speed of the animation and then reset it 
            // back so that the motion of different foliage objects isn't synchronized. 
            // This will produce a more realistic behaviour.
            if (animator != null && changeAnimationSpeed)
            {
                StartCoroutine(SetAnimationSpeed());
            }

            // This is a safeguard to eliminate the "Index out of Range Exception" errors
            // that may be generated when the object in the scene is a prefab instance 
            // and the user forgot to save the changes he made to it.
            if (offsetFactor.Count != foliage2D.heightSegments + 1)
            {
                offsetFactor.Clear();
                int len = foliage2D.heightSegments + 1;
                float offset = 1f / foliage2D.heightSegments;

                for (int i = 0; i < len; i++)
                {
                    offsetFactor.Add(offset * i);
                }
            }
        }

        /// <summary>
        /// Changes the default speed of the animation.
        /// </summary>
        IEnumerator SetAnimationSpeed()
        {
            animator.speed = Random.Range(minSpeed, maxSpeed);
            // This suspends the coroutine execution for a random amount of seconds.
            // As a result the animation speed will be set to its default value after a small delay.
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));

            animator.speed = 1f;
        }

        void Update()
        {
            UpdateVertsPos();

            int len = collider2DObject.Count;
            // The loop checks all the objects that are are currently staying in
            // this collider and determines if a bending animations must be triggered.
            for (int i = 0; i < len; i++)
            {
                float offset = collider2DObject[i].transform.position.x - transform.position.x;

                // We want an object to trigger a foliage animation only when this object is past 
                // the middle point of the foliage object. For this purpose we compare the sign of 
                // the difference between the current position of the moving object and the foliage 
                // object, with the value of "enterOffset", of the current moving object.
                if ((Mathf.Sign(enterOffset[i]) != Mathf.Sign(offset)) && !isBending && animator != null)
                {
                    // Which animation to trigger is determined by comparing the position on the X 
                    // axis of the moving object with that of the foliage object.
                    if (collider2DObject[i].transform.position.x > transform.position.x)
                        animator.SetTrigger("tRight");
                    else
                        animator.SetTrigger("tLeft");

                    // We set "isBending" to true so that on the next frame the animators trigger is not set again.
                    isBending = true;
                }
            }

            // This updates the mesh vertices local position.  
            meshFilter.vertices = finalVertexPos;
        }

        /// <summary>
        /// Updates the local position of the mesh vertices.
        /// </summary>
        private void UpdateVertsPos()
        {
            if (foliageBending == Foliage2D_MeshBending.Simple)   
                SimpleMeshBending();  
            else
                SmartMeshBending();  
        }

        /// <summary>
        /// Applies a simple bending to the mesh.
        /// </summary>
        private void SimpleMeshBending()
        {
            int hLen = foliage2D.heightSegments + 1;
            int wLen = foliage2D.widthSegments + 1;

            // This for loop is responsible for the mesh animation.
            // The final position of the mesh vertices is calculated based on 
            // their initial position plus the offset for a given horizontal row.
            for (int i = 0; i < hLen; i++)
            {
                posOffset[i] = new Vector3(offset.x * offsetFactor[i], offset.y * offsetFactor[i], offset.z * offsetFactor[i]);

                for (int j = 0; j < wLen; j++)
                {
                    // The index of the vertex whose final local position must be calculated.
                    int vertIndex = horizontalVerts * i + j;

                    finalVertexPos[vertIndex] = new Vector3(
                        initialVertexPos[vertIndex].x + posOffset[i].x,
                        initialVertexPos[vertIndex].y + posOffset[i].y,
                        initialVertexPos[vertIndex].z + posOffset[i].z);
                }
            }
        }

        /// <summary>
        /// Applies a more realistic bending to the mesh.
        /// </summary>
        private void SmartMeshBending()
        { 
            // The value of -1 means the vertex will be moved to the left of it's
            // initial position on the X axis while 1 to the right.
            int dir = offset.x < 0 ? -1 : 1;
            // The height of a horizontal mesh segment.
            float heightOfSegment = foliage2D.height / foliage2D.heightSegments;
            centerLinePoints[0] = new Vector2(offset.x * offsetFactor[0], initialVertexPos[0].y);

            int hLen = foliage2D.heightSegments + 1;
            int wLen = foliage2D.widthSegments + 1;

            // You could say this is the backbone of the mesh. Uncomment the segment below this loop to see it in the scene view.
            // Calculates the points around which to rotate the mesh vertices.
            for (int i = 1; i < hLen; i++)
            {
                float xOffset = offset.x * offsetFactor[i];

                if (Mathf.Abs(xOffset) < heightOfSegment)
                    centerLinePoints[i] = new Vector2(centerLinePoints[i - 1].x + xOffset, centerLinePoints[i - 1].y + Mathf.Sqrt((heightOfSegment * heightOfSegment) - (xOffset * xOffset)));
                else
                {
                    float y = Mathf.Abs(xOffset) - heightOfSegment;
                    centerLinePoints[i] = new Vector2(centerLinePoints[i - 1].x + dir * Mathf.Sqrt((heightOfSegment * heightOfSegment) - (y * y)), centerLinePoints[i - 1].y - y);
                }
            }

            //// Uncomment this segment to see the mesh center line in the scene view.
            //for (int i = 0; i < hLen - 1; i++)
            //{
            //    Debug.DrawLine(transform.TransformPoint(centerLinePoints[i]), transform.TransformPoint(centerLinePoints[i + 1]), Color.blue);
            //}

            for (int i = 1; i < hLen; i++)
            {
                Vector2 line = centerLinePoints[i] - centerLinePoints[i - 1];
                anglesInDeg[i] = Mathf.Atan2(line.y, line.x) * Mathf.Rad2Deg - 90f;

                // The amount in radians by which to rotate a vertex around a point.
                float angleInRadians = anglesInDeg[i] * Mathf.Deg2Rad;

                for (int j = 0; j < wLen; j++)
                {
                    // The index of the vertex whose final local position must be calculated.
                    int vertIndex = horizontalVerts * i + j;

                    // The initial position of a vertex is offset based on the values of "centerLinePoints".
                    finalVertexPos[vertIndex] = new Vector3(
                        initialVertexPos[vertIndex].x + centerLinePoints[i].x,
                        centerLinePoints[i].y,
                        initialVertexPos[vertIndex].z);

                    // The Current position of the vertex.
                    Vector3 vertCurPos = finalVertexPos[vertIndex];

                    // Calculates the final position of the vertex after it is rotated.
                    finalVertexPos[vertIndex] = new Vector3(
                        (vertCurPos.x - centerLinePoints[i].x) * Mathf.Cos(angleInRadians) - (vertCurPos.y - centerLinePoints[i].y) * Mathf.Sin(angleInRadians) + centerLinePoints[i].x,
                        (vertCurPos.x - centerLinePoints[i].x) * Mathf.Sin(angleInRadians) + (vertCurPos.y - centerLinePoints[i].y) * Mathf.Cos(angleInRadians) + centerLinePoints[i].y,
                        0.0f);     
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!collider3DObject.Contains(other))
            {
                collider3DObject.Add(other);

                // We save the difference between the moving object when it entered the trigger 
                // and the foliage object to a list so that we ca use it to determine when the 
                // moving object has passed the middle point of the foliage object.
                enterOffset.Add(other.transform.position.x - transform.position.x);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // If a collider exited the foliage trigger, we don't need a reference to it anymore.
            if (collider3DObject.Contains(other))
            {
                int index = collider3DObject.IndexOf(other);
                collider3DObject.Remove(other);
                enterOffset.RemoveAt(index);
            }

            isBending = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!collider2DObject.Contains(other))
            {
                collider2DObject.Add(other);

                // We save the difference between the moving object when it entered the trigger 
                // and the foliage object to a list so that we ca use it to determine when the 
                // moving object has passed the middle point of the foliage object.
                enterOffset.Add(other.transform.position.x - transform.position.x);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            // If a collider exited the foliage trigger, we don't need a reference to it anymore.
            if (collider2DObject.Contains(other))
            {
                int index = collider2DObject.IndexOf(other);
                collider2DObject.Remove(other);
                enterOffset.RemoveAt(index);
            }

            isBending = false;
        }
        #endregion
    }
}
