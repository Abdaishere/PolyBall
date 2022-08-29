using UnityEngine;

public class Rotator : MonoBehaviour {
	private Rotator _rotator;
	private const int Speed = 110;

	private void Start()
	{
		_rotator = GetComponent<Rotator>();
	}

	private void Update ()
	{
		if (Main.GameStarted == false)
		{
			PlayerPolygon.Rotation += Speed * Time.deltaTime;
			if (PlayerPolygon.Rotation > 360)
			{
				PlayerPolygon.Rotation -= 360;
			}
		}
		else
		{
			PlayerPolygon.SetRotation();
			Destroy(_rotator);
		}
	}
}
