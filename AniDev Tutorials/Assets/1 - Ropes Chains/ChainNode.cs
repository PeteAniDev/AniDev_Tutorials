using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ChainNode : MonoBehaviour {

	public Camera cam;
	public Rigidbody2D rigid;
	public ChainNode next = null;
	public float nextNodeDistance;
	public bool locked = false;
	public float springPower = 1;
	public float drag = 1;

	private bool softLocked = false;
	private Vector2 previousMouse;

	void FixedUpdate() {
		if (next != null) {
			float springForceMagnitude = Time.fixedDeltaTime * (nextNodeDistance - (next.rigid.position - rigid.position).magnitude);
			Vector2 springForce = springForceMagnitude * (next.rigid.position - rigid.position).normalized;
			if (!locked && !softLocked) {
				rigid.velocity -= springPower * springForce;
			}
			next.rigid.velocity += next.springPower * springForce;
		}

		if (locked || softLocked) {
			rigid.bodyType = RigidbodyType2D.Static;
			rigid.velocity = Vector2.zero;
		} else {
			rigid.bodyType = RigidbodyType2D.Dynamic;
			rigid.velocity -= new Vector2(0, (10 + drag) * Time.fixedDeltaTime);
			rigid.velocity -= rigid.velocity * Time.fixedDeltaTime * drag;
		}

		if (softLocked) {
			rigid.position += MousePos() - previousMouse;
			previousMouse = MousePos();
		}
	}

	private void Update() {
		if (softLocked && Input.GetKeyDown(KeyCode.Space)) {
			locked = !locked;
		}
	}

	private void OnMouseDown() {
		softLocked = true;
		previousMouse = MousePos();
	}

	private void OnMouseUp() {
		softLocked = false;
	}

	private Vector2 MousePos() {
		return cam.ScreenToWorldPoint(Input.mousePosition);
	}

}
