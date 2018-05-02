using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	public Claw claw;
	public Joystick joystick;

	public GameObject startButton;
	public GameObject backButton;
	public GameObject grabButton;
	public GameObject okButton;
	public GameObject youGotText;
	public GameObject celebration;
	public Transform rotateRoot;
	GameObject currentContent;

	public ContentSpawner contentSpawner;

	void Start() {
		joystick.claw = claw;
		BackPress ();
	}

	public void BackPress() {
		startButton.SetActive (true);
		backButton.SetActive (false);
		grabButton.SetActive (false);
		okButton.SetActive (false);
		youGotText.SetActive (false);
		joystick.gameObject.SetActive (false);
		celebration.SetActive (false);
	}
	
	public void StartPress() {
		startButton.SetActive (false);
		backButton.SetActive (true);
		grabButton.SetActive (true);
		okButton.SetActive (false);
		youGotText.SetActive (false);
		joystick.gameObject.SetActive (true);

		contentSpawner.CheckContents ();
	}
	
	public void GrabPress() {
		claw.Grab ();
	}

	public void GetContent(GameObject content) {
		backButton.SetActive (false);
		grabButton.SetActive (false);
		okButton.SetActive (true);
		youGotText.SetActive (true);
		joystick.gameObject.SetActive (false);
		celebration.SetActive (true);
		content.GetComponentInChildren<Rigidbody> ().isKinematic = true;
		content.GetComponentInChildren<Collider> ().enabled = false;
		content.transform.SetParent (rotateRoot);
		content.transform.localPosition = Vector3.zero;
		content.transform.localEulerAngles = Vector3.zero;
		content.transform.localScale = Vector3.one;
		content.transform.GetChild (0).localPosition = Vector3.zero;
		content.transform.GetChild (0).localEulerAngles = new Vector3(-90f,0f,0f);

		currentContent = content;
	}

	public void DestroyContent() {
		backButton.SetActive (true);
		grabButton.SetActive (true);
		okButton.SetActive (false);
		youGotText.SetActive (false);
		joystick.gameObject.SetActive (true);
		celebration.SetActive (false);
		contentSpawner.DestroyContent (currentContent);
	}


	void Update() {
	}
}
