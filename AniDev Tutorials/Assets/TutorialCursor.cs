using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TutorialCursor : MonoBehaviour {

	public Camera cam;
	public Sprite released;
	public Sprite pressed;

	private SpriteRenderer sprite;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		transform.position = MousePos();
		if (Input.GetMouseButton(0)) {
			sprite.sprite = pressed;
		} else {
			sprite.sprite = released;
		}
	}

	private Vector2 MousePos() {
		return cam.ScreenToWorldPoint(Input.mousePosition);
	}

}
