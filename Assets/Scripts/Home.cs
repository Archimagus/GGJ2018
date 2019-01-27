using UnityEngine;

public class Home : MonoBehaviour
{
	public Transform PlayerStartingPosition;
	public SpriteRenderer[] Pickups;

	void Start()
	{
		var p = Player.Instance;
		p.transform.position = PlayerStartingPosition.position;
		p.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		if (p.CurrentPickup > -1)
		{
			p.FoundPickups.Add(p.CurrentPickup);
			p.CurrentPickup = -1;
		}
		foreach (var i in p.FoundPickups)
		{
			Pickups[i].enabled = true;
			if (PlayerPrefs.GetInt($"PlayerItems_{i}", 0) == 0)
			{
				PlayerPrefs.SetInt($"PlayerItems_{i}", 1);
				// New Item Particles
				// New Item Sound
			}
		}
	}
}
