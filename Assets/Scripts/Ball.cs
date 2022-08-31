using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour {

	public Vector3 spawnPosition;
	
	private int _downForce = 4;
	private const int SmashForce = 30;
	private float _timer;
	private int _speedUpTime = 5;
	private Rigidbody2D _rb;
	private SpriteRenderer _sr;
	private int _currentColor;
	private void Start ()
	{
		_rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
		
		SpawnBall();
		
		var sides = Main.Difficulty;
		if (sides < 12) return;

		if (sides < 24)
			BallSize(MapNum(sides, 11, 24,  1f,0.5f, 2));
		else if (sides < 32)
			BallSize(MapNum(sides, 23, 32, 0.5f,0.3f,  2));
		else if (sides < 64)
			BallSize(MapNum(sides, 31, 64, 0.3f, 0.1f, 2));
		else throw new UnauthorizedAccessException();

	}
	
	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))
		{
			_rb.velocity = Vector2.down * SmashForce;
			return;
		}
		
		_timer += Time.deltaTime;
		if (_timer > _speedUpTime && _downForce <= 15)
		{
			_speedUpTime *= 2;
			_downForce += 1;
			_timer = 0;
			_rb.velocity += Vector2.down * 1;
		}
	}

	private void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag("Goal"))
		{
			SpawnBall();
			return;
		}

		if (col.gameObject.GetComponent<DrawLine>().sideNum == _currentColor) return;
		enabled = false;
		_rb.velocity = Vector2.down * 0.8f;
		Debug.Log($"Ball color was {_currentColor} and touched {col.gameObject.GetComponent<DrawLine>().sideNum}");
		StartCoroutine(GameOver());
	}
	private IEnumerator GameOver()
	{
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 1;
		
		Main.GameStarted = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	private void SpawnBall ()
	{
		_rb.velocity = Vector2.down * _downForce;
		transform.position = spawnPosition;
		
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
		var offset = (negA * scale) + toA;
		var finalNumber = (sourceNumber * scale) + offset;
		var calcScale = (int) Math.Pow(10, decimalPrecision);
		return (float) Math.Round(finalNumber * calcScale) / calcScale;
	}
}
