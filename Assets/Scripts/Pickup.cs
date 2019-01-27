using UnityEngine;

public class Pickup : MonoBehaviour
{
	public Sprite Sprite;

	private SpriteRenderer _renderer;
	// Start is called before the first frame update
	void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
		var collider = gameObject.AddComponent<PolygonCollider2D>();
	}

	private void OnValidate()
	{
		if (_renderer == null)
			_renderer = GetComponent<SpriteRenderer>();
		_renderer.sprite = Sprite;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{

		}
	}
}
