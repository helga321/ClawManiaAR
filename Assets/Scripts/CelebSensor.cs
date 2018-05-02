using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebSensor : MonoBehaviour {

	public GameControl gameControl;

	void OnTriggerEnter(Collider other) {
		if ((other.transform.parent != null) && (other.transform.parent.gameObject.tag == "Content")) {
			gameControl.GetContent(other.transform.parent.gameObject);
		}
	}
}
