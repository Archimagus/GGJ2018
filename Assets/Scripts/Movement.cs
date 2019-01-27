using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float JumpForce = 300;
	public float MoveForce = 100;
	public float LowJumpMultiplier = 2f;

	Rigidbody2D _rb;
	CircleCollider2D _collider;
	Vector2 _targetDirection;
	Animator _animator;
	bool _climbing;
	private bool _grounded;
	private Vector2 _direction;
	private Vector2 _groundNormal = Vector2.up;
	private Vector2 _gravity = Vector2.down * 9.81f;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_collider = GetComponent<CircleCollider2D>();
	}
	void Update()
	{
		_targetDirection.x = Input.GetAxis("Horizontal");
		_targetDirection.y = Input.GetAxis("Vertical");
		_targetDirection = (Vector2)transform.right * Vector2.Dot(transform.right, _targetDirection) * MoveForce;
		if (_grounded && Input.GetKeyDown(KeyCode.Space))
		{
			_rb.velocity += (Vector2.up * 20 + -_gravity).normalized * JumpForce;
		}
	}

	private void OnDrawGizmos()
	{
		if (_rb != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_collider.bounds.center, _collider.bounds.center + new Vector3(_direction.x, _direction.y).normalized * 0.3f);
			Gizmos.color = Color.blue;

			Gizmos.DrawLine(_collider.bounds.center, _collider.bounds.center + new Vector3(_gravity.x, _gravity.y).normalized * 0.3f);
		}
	}
	private void LateUpdate()
	{
		_animator.SetFloat("Horizontal", Vector2.Dot(transform.right, _rb.velocity));
		_animator.SetFloat("Vertical", _rb.velocity.y);
		_animator.SetBool("Grounded", _grounded);
		_animator.SetBool("Wall", Mathf.Abs(transform.right.y) > 0.5f);
	}
	private void FixedUpdate()
	{
		if (_grounded)
		{
			if (_targetDirection.SqrMagnitude() < 0.01f && !Input.GetButton("Jump"))
				_rb.velocity *= (Vector2)(transform.right * 0.5f + transform.up);
			if (_rb.velocity.sqrMagnitude < _targetDirection.sqrMagnitude)
				_rb.velocity += _targetDirection * 0.5f;
		}
		else if (_gravity.y < 0)
		{
			if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
				_rb.velocity += _gravity * (LowJumpMultiplier - 1) * Time.fixedDeltaTime;
		}

		_rb.velocity += _gravity * Time.fixedDeltaTime;
		_rb.velocity += _targetDirection * Time.fixedDeltaTime;



		_direction = _rb.velocity;

		_grounded = false;

		var hits = Physics2D.RaycastAll(_collider.bounds.center, _direction, 0.4f, ~(1 << 8));
		if (hits.Any(hit => hit.collider.CompareTag("Teleporter")))
		{
			var tp = hits.First(hit => hit.collider.CompareTag("Teleporter")).collider.GetComponent<Teleporter>();
			transform.position = tp.Destination;
		}
		if (hits.Any(hit => Vector2.Dot(_targetDirection, hit.normal) < 0))
		{
			var h = hits.First(hit => Vector2.Dot(_targetDirection, hit.normal) < 0);
			faceNormal(h.normal);
		}
		else
		{
			var hit = Physics2D.Raycast(_collider.bounds.center, -transform.up, 0.5f, ~((1 << 8) | (1 << 9)));
			if (hit && Vector2.Dot(-transform.up, hit.normal) < 0)
			{
				faceNormal(hit.normal);
			}
			else
			{
				faceNormal(Vector2.up, false);
			}
		}
	}

	private void faceNormal(Vector2 normal, bool grounded = true)
	{
		_groundNormal = normal;
		_grounded = grounded;
		_gravity = -_groundNormal * 9.81f;
		float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg + -90;
		var q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = q;
	}
}
