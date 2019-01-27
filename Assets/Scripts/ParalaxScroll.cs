using UnityEngine;

public class ParalaxScroll : MonoBehaviour
{
	public float ParalaxFactor;
	Camera _camera;
	Vector3 _initialCamPosition;
	Vector3 _myInitialPosition;
	void Start()
	{
		_camera = Camera.main;
		_myInitialPosition = transform.position;
		_initialCamPosition = _camera.transform.position;
	}

	void LateUpdate()
	{
		transform.position = _myInitialPosition + Vector3.right * (_initialCamPosition.x - _camera.transform.position.x) * ParalaxFactor;
	}
}
