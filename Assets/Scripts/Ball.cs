using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

	public Vector3 spawnPosition;
	
	public float downForce = 5f;
	public float smashForce = 1000f;
	public float speedUp = 10f;
	
	public Rigidbody2D rb;
	public SpriteRenderer sr;

	public string currentColor;
	private void Start ()
	{
		SpawnBall();
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

		if (col.CompareTag("Untagged") || col.CompareTag(currentColor)) return;
		Debug.Log("GAME OVER!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void SpawnBall ()
	{
		transform.position = spawnPosition;
		var index = Random.Range(0, Main.UsedColors.Count - 1);
		while (Main.UsedColors[index].ColorName == currentColor)
		{
			index = Random.Range(0, Main.UsedColors.Count - 1);
		}
		
		currentColor = Main.UsedColors[index].ColorName;
		sr.color = Main.UsedColors[index].Value;
	}
}
