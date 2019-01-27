using UnityEngine;

public class HoleBlocker : MonoBehaviour
{
	EdgeCollider2D _edge;
	private void Start()
	{
		_edge = GetComponent<EdgeCollider2D>();
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			var dot = Mathf.Abs(Vector2.Dot(Player.Instance.transform.right, transform.up));
			if(dot > 0.5f)
				_edge.enabled = false;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			_edge.enabled = true;
		}
	}
}
