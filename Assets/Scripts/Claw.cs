using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClawState {
	IDLE,
	DOWNGRAB,
	GRABBING,
	UPGRAB,
	RETURN,
	RELEASE
}


public class Claw : MonoBehaviour {
	public Transform clawPos;
	public Transform hookPos;
	Vector3 newPos;
	Vector3 oriPos;

	public Transform contentRoot;
	public Transform startPos;
	public Transform downGrabPos;
	public Transform upGrabPos;

	public Transform[] clawHand;
	public Vector3[] startAngle;
	public Vector3[] endAngle;

	public ClawSensor[] clawSensor;
	public ClawState clawState;
	public float dirSpeed;
	public float areaThreshold;
	float timerCountDown;

	public Transform currentPrize;

	// Use this for initialization
	void Start () {
		InitClaw ();
	}

	void InitClaw () {
		clawState = ClawState.IDLE;
		newPos = clawPos.localPosition;
		timerCountDown = 0f;
		currentPrize = null;
	}

	// Update is called once per frame
	void Update () {
		if (clawState == ClawState.IDLE) {
			if (!clawPos.localPosition.Equals(newPos)) {
				clawPos.localPosition = Vector3.Lerp(clawPos.localPosition,newPos,0.1f);
			}
		} else if (clawState == ClawState.DOWNGRAB) {
			if (timerCountDown<1f) {
				timerCountDown += Time.deltaTime * 0.3f;
				hookPos.localPosition = Vector3.Lerp(upGrabPos.localPosition,downGrabPos.localPosition,timerCountDown);
			} else { 
				clawState = ClawState.GRABBING;
				timerCountDown = 0f;
			}
		} else if (clawState == ClawState.GRABBING) {
			if ((timerCountDown<35f) && (!AllSensorsGrabObject())) {
				timerCountDown += (Time.deltaTime * 35f);
				for (int i=0;i<clawHand.Length;i++) {
					clawHand[i].localRotation = Quaternion.Euler(endAngle[i].x,endAngle[i].y,startAngle[i].z+timerCountDown);
				}
			} else {
				if (AllSensorsGrabObject()) 
					SetCurrentPrize();
				clawState = ClawState.UPGRAB;
				timerCountDown = 0f;
			}

		} else if (clawState == ClawState.UPGRAB) {
			if (timerCountDown<1f) {
				timerCountDown += Time.deltaTime * 0.3f;
				hookPos.localPosition = Vector3.Lerp(downGrabPos.localPosition,upGrabPos.localPosition,timerCountDown);
			} else { 
				clawState = ClawState.RETURN;
				timerCountDown = 0f;
				oriPos = clawPos.localPosition;
			}
		} else if (clawState == ClawState.RETURN) {
			if (timerCountDown<1f) {
				timerCountDown += (Time.deltaTime * 0.5f);
				clawPos.localPosition = Vector3.Lerp(oriPos,startPos.localPosition,timerCountDown);
			} else {
				clawState = ClawState.RELEASE;
				timerCountDown = 0f;
				if (currentPrize!=null) 
					ReleaseCurrentPrize();
			}
		} else if (clawState == ClawState.RELEASE) {
			if (timerCountDown<35f) {
				timerCountDown += (Time.deltaTime * 35f);
				for (int i=0;i<clawHand.Length;i++) {
					clawHand[i].localRotation = Quaternion.Euler(startAngle[i].x,startAngle[i].y,endAngle[i].z-timerCountDown);
				}
			} else {
				InitClaw();
			}
			
		}
		
	}

	bool AllSensorsGrabObject() {
		if ((clawSensor [0].currentContent != null) && (clawSensor [0].currentContent == clawSensor [1].currentContent) && (clawSensor [0].currentContent == clawSensor [2].currentContent))
			return true;
		else 
			return false;
	}

	void SetCurrentPrize() {
		currentPrize = clawSensor[0].currentContent;
		currentPrize.SetParent (hookPos);
		currentPrize.GetComponentInChildren<Rigidbody> ().isKinematic = true;
		currentPrize.GetComponentInChildren<Collider> ().enabled = false;
	}
	void ReleaseCurrentPrize() {
		currentPrize.SetParent (contentRoot);
		currentPrize.GetComponentInChildren<Rigidbody> ().isKinematic = false;
		currentPrize.GetComponentInChildren<Collider> ().enabled = true;
	}

	public void Grab() {
		if (clawState == ClawState.IDLE) {
			clawState = ClawState.DOWNGRAB;
			timerCountDown = 0f;
		}
	}

	public void Move(Vector3 gotoPos) {
		if (clawState == ClawState.IDLE) {
			newPos = gotoPos;
		}
	}
	
	public void MoveDir(Vector2 clawDir) {
		if (clawState == ClawState.IDLE) {
			float newX = newPos.x + (-clawDir.x * dirSpeed);
			float newY = newPos.y + (clawDir.y * dirSpeed);
			if (newX> areaThreshold) 
				newX = areaThreshold;
			if (newX< -areaThreshold) 
				newX = -areaThreshold;
			if (newY> areaThreshold) 
				newY = areaThreshold;
			if (newY< -areaThreshold) 
				newY = -areaThreshold;
			newPos = new Vector3(newX,newY,newPos.z);
//			Debug.Log("NewPos: "+newPos);
		}
	}
	
	public void DebugMove(GameObject target) {
		if (clawState == ClawState.IDLE) {
			newPos = target.transform.localPosition;
		}
	}
}
