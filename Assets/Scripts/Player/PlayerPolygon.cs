using System;
using System.Collections.Generic;
using UI;
using UI.Add___Delete_Buttons;
using UI.ScoreSystem;
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
        
        private List<Vector3> _points;
        private List<GameObject> _lines;
        private static List<DrawLine> _linesUpdaters;
    
        private float _width;
        private static int _sides;
    
        [Range(3f, 4.6f)]
        private static float _radius;

        public float rotation;
        public float targetRotation;
        [Range(-1, 1)] private int _rotationDirection;
        
        public static int LineBuffer;
        
        private float _rotationAlpha;
        private float _rotationScale;
        private const float Tau = 2 * Mathf.PI;

        [SerializeField] private AudioSource audioSource;
        private void Start()
        {
            _sides = Main.Difficulty;
            _radius = Ball.MapNum(_sides, 3, 63, 3f, 4.6f, 2);
            _width = Main.LineWidth;
            _linesUpdaters = new List<DrawLine>();
            _lines = new List<GameObject>();
            LineBuffer = 0;

            score = Instantiate(score, transform);
            _score = score.GetComponent<Score>();

            GetPoints();
            InitLines();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateLines();
            
            if(Main.GameOver || MenuManager.GameIsPaused) return;

            if (Main.GameStarted) {
                if (Rotating()) return;
                
                // controls 
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.Play();
                    rotation = targetRotation;
                    targetRotation -= _rotationAlpha;
                    _rotationDirection = -1;
                }

                if (!Input.GetKeyDown(KeyCode.LeftArrow)) return;
                
                audioSource.Play();
                rotation = targetRotation;
                targetRotation += _rotationAlpha;
                _rotationDirection = 1;
                
            }
            else {
                while (LineBuffer != 0) {
                    while (LineBuffer > 0) {
                        _sides++;
                        GetPoints();
                        AddLine(_sides - 1, _sides - 2);
                        LineBuffer--;
                    }

                    while (LineBuffer < 0) {
                        _sides--;
                        _linesUpdaters.RemoveAt(_linesUpdaters.Count - 1);
                        Destroy(_lines[_lines.Count - 1]);
                        _lines.RemoveAt(_lines.Count - 1);
                        LineBuffer++;
                    }

                    HighScore.UpdateHighScore();
                    _radius = Ball.MapNum(_sides, 3, 63, 3f, 4.6f, 2);
                    _score.UpdateScore();
                    ColorsCount.UpdateHighScore();
                }
            }
        }
        private void InitLines()
        {
            AddLine(0, _points.Count - 1);
            for (var i = 1; i < _sides; i++)
                AddLine(i, i -1);
        }
    
        private void UpdateLines()
        {
            _points.Clear();
            GetPoints();

            try {
                _linesUpdaters[0].Points = new[] { _points[0], _points[_points.Count - 1] };
                for (var i = 1; i < _sides; i++)
                    _linesUpdaters[i].Points = new[] { _points[i], _points[i - 1] };
            }
            catch {
                // ignored
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void AddLine(int start, int end)
        {
            var tempLine = Instantiate(liInterface, transform);
            tempLine.GetComponent<DrawLine>().DrawLineInit(start,
                _width, new[]{_points[start], _points[end]});
            _linesUpdaters.Add(tempLine.GetComponent<DrawLine>());
            _lines.Add(tempLine);
        
        }
        
        private bool Rotating()
        {
            switch (_rotationDirection)
            {
                case 0 : return false;
                // rotating clockwise
                case -1 when targetRotation > rotation:
                {
                    if (targetRotation < 0)
                    {
                        targetRotation += 360;
                    }

                    _rotationDirection = 0;
                    rotation = targetRotation;
                    return false;
                }
                // rotating counter clockwise
                case 1 when targetRotation < rotation:
                {
                    if (targetRotation > 360)
                    {
                        targetRotation -= 360;
                    }
                
                    _rotationDirection = 0;
                    rotation = targetRotation;
                    return false;
                }
                default:
                    var delta = Math.Abs(targetRotation - rotation);
                    rotation += Math.Min(_rotationScale * Time.deltaTime * _rotationDirection, delta);
                    return !(delta < _rotationAlpha * 0.25);
            }
        }
        public void StartGame()
        {
            rotation = -90;
            targetRotation = rotation;
            
            _rotationAlpha =  360f / _sides;
            _rotationScale = Ball.MapNum(_sides, 3, 63, 1000, 50, 2);
            _rotationDirection = 0;
            
            _score.InitScore();
        }
        public static void UpdateColors() {
            
            foreach(var line in _linesUpdaters)
                line.UpdateColor();
        }
        private void GetPoints()
        {
            _points = new List<Vector3>();

            for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
            {
                var currentRadian = (float)currentPoint / _sides * Tau;
                var x = Mathf.Cos(currentRadian) * _radius;
                var y = Mathf.Sin(currentRadian) * _radius;
            
                var newPosition = new Vector3(x, y, 0);
                newPosition = Quaternion.Euler(0, 0, rotation) * newPosition;
                newPosition.y -= 5;
            
                _points.Add(newPosition);
            }
        }
    }
}
