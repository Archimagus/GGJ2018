using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float JumpForce = 300;
	public float MoveForce = 100;
	public float FallMultiplier = 2.5f;
	public float LowJumpMultiplier = 2f;

	Rigidbody2D _rb;
	Vector2 _targetDirection;
	Animator _animator;
	bool _climbing;
	private bool _grounded;
	private Vector2 _groundNormal;
	private Vector2 _direction;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}
	void Update()
	{
		_targetDirection.x = Input.GetAxis("Horizontal");
		_targetDirection.y = Input.GetAxis("Vertical");
		_targetDirection = (Vector2)transform.right * Vector2.Dot(transform.right, _targetDirection);
		if (_grounded && Input.GetKeyDown(KeyCode.Space))
		{
			_rb.AddForce((Vector2.up *20 + -Physics2D.gravity).normalized * JumpForce, ForceMode2D.Force);
		}
	}

	private void OnDrawGizmos()
	{
		if (_rb != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position + transform.up * 0.1f, transform.position + transform.up * 0.1f + new Vector3(_direction.x, _direction.y).normalized * 0.5f);
			Gizmos.color = Color.blue;

			Gizmos.DrawLine(transform.position + transform.up * 0.1f, transform.position + transform.up * 0.1f + new Vector3(Physics2D.gravity.x, Physics2D.gravity.y).normalized * 0.2f);
		}
	}
	private void LateUpdate()
	{		
		_animator.SetFloat("Horizontal", Vector2.Dot(transform.right, _rb.velocity));
		_animator.SetFloat("Vertical", _rb.velocity.y);
	}
	private void FixedUpdate()
	{
		_rb.AddForce(_targetDirection * MoveForce, ForceMode2D.Impulse);
		_direction = _rb.velocity;
		if (_direction.sqrMagnitude < 0.01f)
			_direction = _targetDirection;
		RaycastHit2D hit = Physics2D.Raycast(transform.position+transform.up*0.1f, _direction, 0.5f, ~(1 << 8));
		_grounded = false;
		if(hit)
		{
			faceNormal(hit.normal);
		}
		else
		{
			hit = Physics2D.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.2f, ~(1 << 8));
			if (hit)
			{
				faceNormal(hit.normal);
			}
			else
			{
				faceNormal(Vector2.up);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//var normal = collision.contacts[0].normal;
		//faceNormal(normal);
	}

	private void faceNormal(Vector2 normal)
	{
		_groundNormal = normal;
		_grounded = true;
		Physics2D.gravity = -_groundNormal*9.81f;
		float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg + -90;
		var q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = q;
	}
}
