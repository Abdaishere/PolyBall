using System;
using UnityEngine;

public class Rotator : MonoBehaviour {
	private const int Speed = 100;
	public bool rotate = true;
	private void Update ()
	{
		if (!rotate) return;
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
			rotate = false;
			PlayerPolygon.SetRotation();
		}
	}
}
