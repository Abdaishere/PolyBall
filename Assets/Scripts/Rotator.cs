using UnityEngine;

public class Rotator : MonoBehaviour {
	private Rotator _rotator;
	private void Start()
	{
		_rotator = GetComponent<Rotator>();
	}

	private void Update ()
	{
		if (Main.GameStarted == false)
		{
			PlayerPolygon.Rotation -= 350f / Main.Difficulty * Time.deltaTime;
			if (PlayerPolygon.Rotation < 0)
			{
				PlayerPolygon.Rotation += 360;
			}
		}
		else
		{
			PlayerPolygon.SetRotation();
			Destroy(_rotator);
		}
	}
}
