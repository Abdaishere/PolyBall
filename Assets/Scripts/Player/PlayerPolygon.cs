using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerPolygon : MonoBehaviour
    {
        [SerializeField]
        private GameObject liInterface;
        
        [SerializeField]
        private GameObject score;
        private Score _score;
        
        public List<Vector3> points;
        public List<GameObject> lines;
        public List<DrawLine> linesUpdaters;
    
        private float _width;
        private static int _sides;
    
        [Range(3f, 4.6f)]
        private static float _radius;
        public static float Radius => _radius;
        
        public float rotation;
        public static int LineBuffer;
        private float _rotationAlpha;
        private const float Tau = 2 * Mathf.PI;
        private void Start()
        {
            _sides = Main.Difficulty;
            _radius = Ball.MapNum(_sides, 3, 63, 3f, 4.6f, 2);
            _width = Main.LineWidth;
            linesUpdaters = new List<DrawLine>();
            lines = new List<GameObject>();
            LineBuffer = 0;

            score = Instantiate(score, transform);
            _score = score.GetComponent<Score>();

            GetPoints();
            InitLines();
        }

        // Update is called once per frame
        private void Update()
        {
            if(Main.GameOver) return;
            if (Main.GameStarted)
            {
                // controls 
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(1))
                {
                    rotation -= _rotationAlpha;
                    if (rotation < 0)
                    {
                        rotation += 360;
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
                {
                    rotation += _rotationAlpha;
                    if (rotation > 360)
                    {
                        rotation -= 360;
                    }
                }
            }
            else
            {
                while (LineBuffer != 0)
                {
                    while (LineBuffer > 0) {
                        _sides++;
                        GetPoints();
                        AddLine(_sides - 1, _sides - 2);
                        LineBuffer--;
                    }

                    while (LineBuffer < 0) {
                        _sides--;
                        linesUpdaters.RemoveAt(linesUpdaters.Count - 1);
                        Destroy(lines[lines.Count - 1]);
                        lines.RemoveAt(lines.Count - 1);
                        LineBuffer++;
                    }

                    HighScore.UpdateHighScore();
                    _radius = Ball.MapNum(_sides, 3, 63, 3f, 4.6f, 2);
                    _score.UpdateScore();
                    ColorsCount.UpdateHighScore();
                }
            }
            UpdateLines();
        }
    
        private void InitLines()
        {
            AddLine(0, points.Count - 1);
        
            for (var i = 1; i < _sides; i++)
            {
                AddLine(i, i -1);
            }
        
        }
    
        private void UpdateLines()
        {
            points.Clear();
            GetPoints();
            linesUpdaters[0].UpdatePoints(new[]{points[0], points[points.Count - 1]});
            for (var i = 1; i < _sides; i++)
            {
                linesUpdaters[i].UpdatePoints(new[]{points[i], points[i - 1]});
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void AddLine(int start, int end)
        {
            var tempLine = Instantiate(liInterface, transform);
            tempLine.GetComponent<DrawLine>().DrawLineInit(start,
                _width, new[]{points[start], points[end]});
            linesUpdaters.Add(tempLine.GetComponent<DrawLine>());
            lines.Add(tempLine);
        
        }
        public void StartGame()
        {
            rotation = -90;
            _rotationAlpha =  360f / _sides;
            _score.InitScore();
        }
        private void GetPoints()
        {
            points = new List<Vector3>();

            for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
            {
                var currentRadian = (float)currentPoint / _sides * Tau;
                var x = Mathf.Cos(currentRadian) * _radius;
                var y = Mathf.Sin(currentRadian) * _radius;
            
                var newPosition = new Vector3(x, y, 0);
                newPosition = Quaternion.Euler(0, 0, rotation) * newPosition;
                newPosition.y -= 5;
            
                points.Add(newPosition);
            }
        }
    }
}
