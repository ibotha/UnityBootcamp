using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Unit
{
	public Unit target;
	SpriteRenderer spriteRenderer;
	Vector3 destination;
	Animator animator;
	new Rigidbody2D rigidbody;

	float still_time;
	public float attackDist;

	void Attack(Unit u)
	{
		still_time = 1.0f;
		animator.SetBool("attacking", true);
		u.health -= damage;
		Debug.Log(u.name + " Unit [" + u.health + "/" + u.maxHealth + "] has been attacked");
	}
    // Start is called before the first frame update
    public new void Start()
    {
		base.Start();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		destination = transform.position;
    }

    // Update is called once per frame
    public new void Update()
    {
		animator.SetBool("attacking", false);
		still_time -= Time.deltaTime;
		base.Update();
		if (still_time < 0.0f && !dead)
		{
			if (target)
			{
				destination = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
				RaycastHit2D[] res = Physics2D.RaycastAll(transform.position, destination - transform.position, float.PositiveInfinity, 1 << 8);
				Debug.DrawRay(transform.position, destination - transform.position);
				if (target.faction != this.faction && res[1].transform.gameObject.GetComponent<Unit>() == target && res[1].distance < attackDist)
				{
					Attack(target);
					return;
				}
			}
			float max = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, destination, max);
		}
    }

	public void SetDestination(Vector3 location)
	{
		target = null;
		destination = location;
	}
	public void SetTarget(Unit _target)
	{
		target = _target;
	}

	public void Stop()
	{
		destination = transform.position;
	}

	bool wasdead;
	void FixedUpdate() {
		if (dead)
		{
			if (!wasdead)
			{
				Play("death");
				animator.SetBool("dead", true);
			}
		}
		wasdead = dead;
		Vector2 dir = destination - transform.position;
		if (dir.magnitude < 0.01)
		{
			animator.SetBool("walking", false);
			rigidbody.bodyType = RigidbodyType2D.Static;
			return;
		}
		else
		{
			animator.SetBool("walking", true);
			rigidbody.bodyType = RigidbodyType2D.Dynamic;
			dir.Normalize();
			if (dir.x < 0)
				spriteRenderer.flipX = true;
			else
				spriteRenderer.flipX = false;
			animator.SetFloat("SpeedX", dir.x);
			animator.SetFloat("SpeedY", dir.y);
		}
	}
}
