using UnityEngine;

public class Home : MonoBehaviour
{
	public ParticleSystem NewItemParticles;
	public AudioClip NewItemSound;
	public Transform PlayerStartingPosition;
	public GameObject[] Pickups;

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
			Pickups[i]?.SetActive(true);
			if (PlayerPrefs.GetInt($"PlayerItems_{i}", 0) == 0)
			{
				PlayerPrefs.SetInt($"PlayerItems_{i}", 1);
				Instantiate(NewItemParticles, Pickups[i].transform.position, Quaternion.identity);
				if(NewItemSound != null)
					AudioManager.PlaySound(null, NewItemSound);
			}
		}
	}
}
