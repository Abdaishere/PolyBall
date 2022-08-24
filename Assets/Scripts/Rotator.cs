using UnityEngine;

public class Rotator : MonoBehaviour {

	public float speed = 250f;
	
	private void Update () {
		transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}
}
