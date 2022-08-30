using System.Collections.Generic;
using UnityEngine;

namespace LineCollisionScript
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class LineCollision : MonoBehaviour
    {
        //The Line Manager Class
        private DrawLine _drawLine;

        //The collider for the line
        private PolygonCollider2D _polygonCollider2D;

        //The points to draw a collision shape between
        private List<Vector2> _colliderPoints = new List<Vector2>();

        private void Start()
        {
            _drawLine = GetComponent<DrawLine>();
            _polygonCollider2D = GetComponent<PolygonCollider2D>();
        }


        private void Update()
        {
            if(_drawLine.points.Length == 0) return;
            _colliderPoints = CalculateColliderPoints();
            _polygonCollider2D.SetPath(0, _colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.black;
            _colliderPoints?.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
        }

        private List<Vector2> CalculateColliderPoints() {
            //Get All positions on the line renderer
            var positions = _drawLine.points;

            //Get the Width of the Line
            var width = _drawLine.width;

            //m = (y2 - y1) / (x2 - x1)
            var m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
            var deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
            var deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

            //Calculate the Offset from each point to the collision vertex
            var offsets = new Vector3[2];
            offsets[0] = new Vector3(-deltaX, deltaY);
            offsets[1] = new Vector3(deltaX, -deltaY);

            //Generate the Colliders Vertices
            var colliderPositions = new List<Vector2>
            {
                positions[0] + offsets[0],
                positions[1] + offsets[0],
                positions[1] + offsets[1],
                positions[0] + offsets[1]
            };

            return colliderPositions;
        }
    }
}
