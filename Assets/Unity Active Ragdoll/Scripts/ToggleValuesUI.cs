using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleValuesUI : MonoBehaviour {

	public Toggle invert;
	public Toggle curl;
	public Toggle jog;
	public Toggle superman;

	public Animator ragAnim;

	public ScreenClick click;

	void Update () {
		if (invert.isOn == click.pickUp)
			invert.isOn = !click.pickUp;
	}

	public void IsJogging () {
		ragAnim.SetBool("Jog", jog.isOn);
	}

	public void IsCurling () {
		ragAnim.SetBool("Curl", curl.isOn);
	}

	public void IsSuperman () {
		ragAnim.SetBool("Superman", superman.isOn);
	}
}
