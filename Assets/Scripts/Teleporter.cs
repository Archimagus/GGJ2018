using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public Color EditorHandleColor = Color.white;
	public Vector2 Destination;
	private void OnValidate()
	{
		if (Destination == Vector2.zero)
			Destination = transform.position;
	}
}
