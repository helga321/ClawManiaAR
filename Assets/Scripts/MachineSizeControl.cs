﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineSizeControl : MonoBehaviour {

	public Transform machine;
//	public Text sizeText;
	public Slider sizeSlider;

	void Start() {
        sizeSlider.maxValue = 0.2f;
		sizeSlider.value = 0.1f;
		SliderChange ();
	}

	public void SliderChange() {
		Vector3 newSize = new Vector3 (sizeSlider.value,sizeSlider.value,sizeSlider.value);
//		sizeText.text = sizeSlider.value.ToString ("N2");
        //Vector3 machineSize = new Vector3(machine.localScale.x * newSize.x, machine.localScale.y * newSize.y, machine.localScale.z * newSize.z);
        machine.localScale = newSize;
	}
}
