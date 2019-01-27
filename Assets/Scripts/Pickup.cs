using UnityEngine;

public class Pickup : MonoBehaviour
{
	public Sprite Sprite;
	public AudioClip Audio;
	public int Id=-1;

	private SpriteRenderer _renderer;
	// Start is called before the first frame update
	void Start()
	{
		if(Player.Instance.CurrentPickup == Id || Player.Instance.FoundPickups.Contains(Id))
		{
			Destroy(gameObject);
			return;
		}
		_renderer = GetComponent<SpriteRenderer>();
		var collider = gameObject.AddComponent<PolygonCollider2D>();
		collider.isTrigger = true;
	}

	private void OnValidate()
	{
		if (_renderer == null)
			_renderer = GetComponent<SpriteRenderer>();
		_renderer.sprite = Sprite;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			if (Player.Instance.CurrentPickup == -1)
				collect();
			else
				AudioManager.PlaySound(null, AudioClips.audiomissing);
		}
	}

	private void collect()
	{
		Player.Instance.CurrentPickup = Id;
		AudioManager.PlaySound(null, Audio, location: transform.position);
		Destroy(gameObject);
	}
}
