using UnityEngine;

public class Home : MonoBehaviour
{
	public ParticleSystem NewItemParticles;
	public AudioClip NewItemSound;
	public Transform PlayerStartingPosition;
	public GameObject[] Pickups;
	public GameObject[] FinalObjectsToEnable;
	public GameObject[] FinalObjectsToDisable;

	void Start()
	{
		var p = Player.Instance;
		p.transform.position = PlayerStartingPosition.position;
		p.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		if (p.CurrentPickup > -1)
		{
			p.FoundPickups.Add(p.CurrentPickup);
		}
		foreach (var i in p.FoundPickups)
		{
			Pickups[i]?.SetActive(true);
			if (i == p.CurrentPickup)
			{
				Instantiate(NewItemParticles, Pickups[i].transform.position, Quaternion.identity);
				if (NewItemSound != null)
					AudioManager.PlaySound(null, NewItemSound);
			}
		}
		p.CurrentPickup = -1;
		if (Player.Instance.FoundPickups.Count == Pickups.Length)
		{
			Destroy(Player.Instance.gameObject);
			//Game Over;
			foreach (var item in FinalObjectsToDisable)
			{
				item.SetActive(false);
			}
			foreach (var item in FinalObjectsToEnable)
			{
				item.SetActive(true);
			}
		}
	}
}
