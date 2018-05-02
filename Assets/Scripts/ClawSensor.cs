using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawSensor : MonoBehaviour {

	public Transform currentContent;

	void Start() {
		currentContent = null;
	}

	void OnTriggerEnter(Collider other) {
		if ((other.transform.parent != null) && (other.transform.parent.gameObject.tag == "Content")) {
			currentContent = other.transform.parent;
		}
	}
	void OnTriggerStay(Collider other) {
		if ((other.transform.parent != null) && (other.transform.parent.gameObject.tag == "Content")) {
			currentContent = other.transform.parent;
		}
	}
	void OnTriggerExit(Collider other) {
		if ((other.transform.parent != null) && (other.transform.parent.gameObject.tag == "Content")) {
			currentContent = null;
		}
	}
}
