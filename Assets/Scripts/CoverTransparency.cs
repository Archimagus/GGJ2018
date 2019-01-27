using UnityEngine;

public class CoverTransparency : MonoBehaviour
{
	public Color TriggerColor;
	private Color _originalColor;
	SpriteRenderer _renderer;
	private void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_originalColor = _renderer.color;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			_renderer.color = TriggerColor;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_renderer.color = _originalColor;
		}
	}
}
