using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex01 : MonoBehaviour
{
	public float jump_force;
	public float speed;
	Rigidbody2D Rigidbody2D;
	Camera_ex01 cam;
	BoxCollider2D collider2;
	LayerMask layers;

	public string target_name;

	float check_timer;
	public bool winning = false;
    // Start is called before the first frame update
    void Start()
    {
		Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		cam = Camera.main.GetComponent<Camera_ex01>();
		collider2 = gameObject.GetComponent<BoxCollider2D>();
    }
	bool IsGrounded() {
		return Physics2D.Raycast(transform.position - collider2.bounds.extents - new Vector3(0.0f, 0.1f, 0.0f), Vector2.right, 2 * collider2.bounds.extents.x);
	}

	void OnPlatform() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position - collider2.bounds.extents - new Vector3(0.0f, 0.1f, 0.0f), Vector2.right, 2 * collider2.bounds.extents.x);//Physics2D.BoxCast(transform.position - new Vector3(0.0f, collider2.bounds.extents.y + 0.05f, 0.0f), new Vector2(1.9f * collider2.bounds.extents.x, 0.05f), 0.0f, Vector2.zero);
		if (hit)
		{
			if (!hit.transform.gameObject.GetComponent<PlatformEffector2D>())
				transform.parent = hit.transform;
			else if (((hit.transform.gameObject.GetComponent<PlatformEffector2D>().colliderMask) & (1 << gameObject.layer)) != 0)
			{
				//if (transform.parent != hit.transform)
					transform.parent = hit.transform;
			}
		}
		else
		{
			transform.parent = null;
		}

	}

    // Update is called once per frame
    void Update()
    {
		
		Rigidbody2D.velocity = new Vector2(0.0f, Rigidbody2D.velocity.y);
		OnPlatform();

        if (this.gameObject == cam.selected)
		{
			if (Input.GetKey(KeyCode.RightArrow))
				Rigidbody2D.velocity += new Vector2(speed, 0.0f);
			if (Input.GetKey(KeyCode.LeftArrow))
				Rigidbody2D.velocity += new Vector2(-speed, 0.0f);
			if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
			{
				transform.parent = null;
				Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				Rigidbody2D.AddForce(new Vector2(0.0f, jump_force), ForceMode2D.Impulse);
			}
		}
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == target_name)
		{
			winning = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == target_name)
		{
			winning = false;
		}
	}
}
