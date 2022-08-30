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
		if (sides <= 12) return;
		if (sides <= 64)
			BallSize(2f/sides);
		else throw new UnauthorizedAccessException();

	}
	
	private void Update ()
	{
		if (!Main.GameStarted) return;
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
		Main.GameStarted = false;
		_rb.velocity = Vector2.down * 1;
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
}
