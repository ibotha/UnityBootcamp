using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex00 : MonoBehaviour
{
	public float jump_force;
	public float speed;
	Rigidbody2D Rigidbody2D;
	Camera_ex00 cam;
	BoxCollider2D collider2;
    // Start is called before the first frame update
    void Start()
    {
		Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		cam = Camera.main.GetComponent<Camera_ex00>();
		collider2 = gameObject.GetComponent<BoxCollider2D>();
    }
	bool IsGrounded() {
		return Physics2D.Raycast(transform.position - collider2.bounds.extents - new Vector3(0.0f, 0.1f, 0.0f), Vector2.right, 2 * collider2.bounds.extents.x);
	}

    // Update is called once per frame
    void Update()
    {
		Rigidbody2D.velocity = new Vector2(0.0f, Rigidbody2D.velocity.y);
        if (this.gameObject == cam.selected)
		{
			if (Input.GetKey(KeyCode.RightArrow))
				Rigidbody2D.velocity += new Vector2(speed, 0.0f);
			if (Input.GetKey(KeyCode.LeftArrow))
				Rigidbody2D.velocity += new Vector2(-speed, 0.0f);
			if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
				Rigidbody2D.AddForce(new Vector2(0.0f, jump_force), ForceMode2D.Impulse);
		}
    }
}
