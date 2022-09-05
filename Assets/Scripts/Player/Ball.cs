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
		private Rigidbody2D _rb;
		private SpriteRenderer _sr;
		private int _currentColor;
		private void Start ()
		{
			_spawnPosition = transform.position;
			_rb = GetComponent<Rigidbody2D>();
			_sr = GetComponent<SpriteRenderer>();
		
			SpawnBall();
		
			var sides = Main.Difficulty;
			BallSize(MapNum(sides, 3, 63, 1f, 0.3f, 2));
		}
	
		private void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
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
			if (col.CompareTag("Goal"))
			{
				SpawnBall();
				return;
			}

			if (col.gameObject.GetComponent<DrawLine>().sideNum == _currentColor)
			{
				// TODO point added animation and sound
				_rb.velocity = Vector2.down * 30;
				return;
			}
			enabled = false;
			Main.GameOver = true;
			_rb.velocity = Vector2.down * 0.8f;
			
			StartCoroutine(GameOver());
		}
		private static IEnumerator GameOver()
		{
			Time.timeScale = 0.1f;
			yield return new WaitForSeconds(0.1f);
			Time.timeScale = 1;
			
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	
		private void SpawnBall ()
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
		}

		private void BallSize(float newSize)
		{
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
