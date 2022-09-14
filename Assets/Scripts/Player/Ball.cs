using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Player
{
	public class Ball : MonoBehaviour {

		private Vector3 _spawnPosition;
	
		private int _downForce = 4;
		private const int SmashForce = 30;
		private float _timer;
		private int _speedUpTime = 5;
		private static Rigidbody2D _rb;
		private SpriteRenderer _sr;
		private int _currentColor;

		private static AudioSource _gameOver;

		[SerializeField] private TrailRenderer trailRenderer;
		private void Start ()
		{
			_gameOver = GetComponent<AudioSource>();
			_spawnPosition = transform.position;
			_rb = GetComponent<Rigidbody2D>();
			_sr = GetComponent<SpriteRenderer>();
		
			SpawnBall();
			BallSize(MapNum(Main.Difficulty, 3, 63, 1f, 0.3f, 2));
		}
	
		private void Update ()
		{
			if (Main.GameOver) return;
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				trailRenderer.colorGradient = new Gradient
				{
					colorKeys = new []{new GradientColorKey(_sr.color, 0), new GradientColorKey(Color.white, 1)},
					alphaKeys = new []{new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1)}
				};
				
				_rb.velocity = Vector2.down * SmashForce;
				return;
			}
		
			_timer += Time.deltaTime;
			
			if (!(_timer > _speedUpTime) || _downForce > 15) return;
			
			_speedUpTime *= 2;
			_downForce += 1;
			_timer = 0;
			_rb.velocity += Vector2.down * 1;
		}

		private void OnTriggerEnter2D (Collider2D col)
		{
			if (Main.GameOver) return;
			
			if (col.CompareTag("Goal"))
			{
				SpawnBall();
				return;
			}

			if (col.gameObject.GetComponent<DrawLine>().sideNum != _currentColor)
			{
				_rb.velocity = Vector2.down * 0f;
				Main.GameOver = true;
				StartCoroutine(GameOver());
			}
			else
			{
				_rb.velocity = Vector2.down * 30;
			}
		}
		private static IEnumerator GameOver()
		{
			_rb.velocity = Vector2.down * 1f;
			_gameOver.Play();
			Time.timeScale = 0.1f;
			yield return new WaitForSeconds(0.12f);
			Time.timeScale = 0.15f;
			yield return new WaitForSeconds(0.04f);
			_rb.velocity = Vector2.down * 0.3f;
			yield return new WaitForSeconds(0.02f);
			Time.timeScale = 1;
			yield return new WaitForSeconds(0.01f);
			
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	
		private void SpawnBall()
		{
			_rb.velocity = Vector2.down * _downForce;
			transform.position = _spawnPosition;
		
			var index = Random.Range(0, Main.UsedColors.Count);
			while (index == _currentColor)
			{
				index = Random.Range(0, Main.UsedColors.Count);
			}
			
			_currentColor = index;
			_sr.color = Main.UsedColors[index];
			trailRenderer.colorGradient = new Gradient
			{
				colorKeys = new []{new GradientColorKey(_sr.color, 0), new GradientColorKey(Color.white, 1)},
				alphaKeys = new []{new GradientAlphaKey(MapNum(Main.Difficulty, 3, 63, 0, 0.5f, 2), 0), new GradientAlphaKey(0, 1)}
			};
		}

		private void BallSize(float newSize)
		{
			trailRenderer.startWidth = newSize * 0.9f;
			trailRenderer.endWidth = newSize * 0.4f;
			
			transform.localScale = new Vector3(newSize, newSize);
		}

		public static float MapNum(int sourceNumber, double fromA, double fromB, double toA, double toB, int decimalPrecision ) {
			var deltaA = fromB - fromA;
			var deltaB = toB - toA;
			var scale  = deltaB / deltaA;
			var negA   = -1 * fromA;
			var offset = negA * scale + toA;
			var finalNumber = sourceNumber * scale + offset;
			var calcScale = (int) Math.Pow(10, decimalPrecision);
			return (float) Math.Round(finalNumber * calcScale) / calcScale;
		}
	}
}
