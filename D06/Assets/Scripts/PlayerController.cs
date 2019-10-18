using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
	GameObject Viewer;
	Vector2 rotation;
	public float jumpForce;
	public float speed;
	public float sensitivity;
	Rigidbody rb;
	Vector3 last;
	public LayerMask a;
	public LayerMask raycastMask;
	LevelTrack levelTrack;
    // Start is called before the first frame update
    void Start()
    {
		levelTrack = GetComponent<LevelTrack>();
        rb = GetComponent<Rigidbody>();
		last = Input.mousePosition;
		Viewer = GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 0.1f, a))
			rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
		Vector2 mouseDelta = Input.mousePosition - last;
		last = Input.mousePosition;
		mouseDelta *= sensitivity;
		rotation.x = Mathf.Clamp(rotation.x - mouseDelta.y, -70, 80);
		rotation.y = rotation.y + mouseDelta.x;
		transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
		Viewer.transform.localRotation = Quaternion.Euler(rotation.x, 0.0f, 0.0f);
		Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		movement.Normalize();
		movement *= speed;
        rb.velocity = (Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f) * new Vector3(movement.x, rb.velocity.y, movement.y));
		RaycastHit[] r = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 3.0f, raycastMask);
		float closest = Mathf.Infinity;
		Interactable i = null;
		foreach (RaycastHit h in r)
		{
			if (h.distance < closest)
			{
				closest = h.distance;
				i = h.transform.gameObject.GetComponentInParent<Interactable>();
			}
		}
		if (i != null)
		{
			levelTrack.Info.text = "Right Click To Interact";
			levelTrack.fadeoutTime = 1.0f;
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				i.Interact(this.gameObject);
			}
		}
    }
}
