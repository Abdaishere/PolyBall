using UnityEngine;

namespace Player
{
    public class DrawLine : MonoBehaviour
    {
        public int sideNum = -1;
        public Vector3[] points;

        public Vector3[] Points
        {
            set => points = value;
        }

        private LineRenderer _lineRenderer;
        public float width;
        public void DrawLineInit(int num,float w, Vector3[] se)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.startWidth = w;
            _lineRenderer.endWidth = w;
            _lineRenderer.numCapVertices = 70;
            _lineRenderer.SetPositions(points);
        
            _lineRenderer.startColor = Main.UsedColors[num];
            _lineRenderer.endColor = Main.UsedColors[num];

            width = w;
            sideNum = num;
            points = se;
        }
    
        // Update is called once per frame
        private void Update()
        {
            if (sideNum != -1)
                _lineRenderer.SetPositions(points);
        }

        private void OnDestroy()
        {
            Destroy(_lineRenderer);
        }

        public void UpdateColor()
        {
            _lineRenderer.startColor = Main.UsedColors[sideNum];
            _lineRenderer.endColor = Main.UsedColors[sideNum];
        }
    }
}
