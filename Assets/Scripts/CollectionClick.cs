using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionClick : MonoBehaviour {

    public GameControl gameControl;

	private void OnMouseDown() {
        if (!transform.IsChildOf(gameControl.viewRoot)) {
            gameControl.ViewContent(gameObject);
        }
	}
}
