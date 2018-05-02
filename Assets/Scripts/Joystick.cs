using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : EventTrigger {

	public Claw claw;
	float threshold = 100f;
	Vector2 originPos;
	Vector2 clawDir;
	bool dragging = false;

	public override void OnBeginDrag(PointerEventData data) {
		originPos = data.position;
		dragging = true;
	}
	public override void OnDrag(PointerEventData data) {
		Vector2 newPos = data.position;
		float x = newPos.x - originPos.x;
		if (x > threshold)
			x = threshold;
		if (x < -threshold)
			x = -threshold;
		x = x / threshold;
		float y = newPos.y - originPos.y;
		if (y > threshold)
			y = threshold;
		if (y < -threshold)
			y = -threshold;
		y = y / threshold;

		clawDir = new Vector2 (x,y);
//		Debug.Log ("ClawDir: "+clawDir);
	}
	public override void OnEndDrag(PointerEventData data) {
		dragging = false;
	}

	void Update() {
		if (dragging)
			claw.MoveDir (clawDir);
	}
}
