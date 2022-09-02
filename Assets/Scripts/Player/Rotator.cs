using UnityEngine;

namespace Player
{
	public class Rotator : MonoBehaviour {
		
		private Rotator _rotator;
		private PlayerPolygon _player;
		
		private void Start()
		{
			_rotator = GetComponent<Rotator>();
			_player = GetComponent<PlayerPolygon>();
		}

		private void Update ()
		{
			if (Main.GameStarted == false)
			{
				_player.rotation -= 350f / Main.Difficulty * Time.deltaTime;
				if (_player.rotation < 0)
				{
					_player.rotation += 360;
				}
			}
			else
			{
				Destroy(_rotator);
			}
		}
	}
}
