using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour {

	public Vector3 spawnPosition;
	
	public float downForce = 5f;
	public float smashForce = 1000f;
	public float speedUp = 10f;
	
	public Rigidbody2D rb;
	public SpriteRenderer sr;
	public Material smallBallMaterial;
	public int currentColor;
	private void Start ()
	{
		SpawnBall();
		
		var sides = Main.Difficulty;
		if (sides <= 12) return;
		if (sides <= 64)
			BallSize(1/64f);
		else throw new UnauthorizedAccessException();

	}
	
	private void Update ()
	{
		// TODO make smash button
		if (Input.GetButtonDown("Jump"))
		{
			rb.velocity = Vector2.down * smashForce;
		}
		
		rb.velocity = Vector2.down * downForce;
		// TODO make speed up feature to make the game tougher through time 
	}

	private void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag("Goal"))
		{
			SpawnBall();
			return;
		}

		if (col.gameObject.GetComponent<DrawLine>().sideNum == currentColor) return;
		Debug.Log("GAME OVER!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void SpawnBall ()
	{
		transform.position = spawnPosition;
		var index = Random.Range(0, Main.UsedColors.Count - 1);
		while (Main.UsedColors[index].ColorIndex == currentColor)
		{
			index = Random.Range(0, Main.UsedColors.Count - 1);
		}
		
		currentColor = Main.UsedColors[index].ColorIndex;
		sr.color = Main.UsedColors[index].Value;
	}

	private void BallSize(float newSize)
	{
		if (newSize > 0.1f)
			transform.localScale = new Vector3(newSize, newSize);
		else
			sr.material = smallBallMaterial;
	}
}
