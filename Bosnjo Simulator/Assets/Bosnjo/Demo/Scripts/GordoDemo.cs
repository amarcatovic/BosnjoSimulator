// Gordo! Demo Presentation (c)Dynamic Arts http://dynamicarts.com


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DArts;

namespace DArts {


public class GordoDemo : MonoBehaviour {

	public GameObject m_ObjectToAnimate;
	public GameObject m_SkeletonObject;

	private Animator ac;
	private AnimatorControllerParameter[] ac_parms;
	private DASkeleton skel_script;

	// Use this for initialization
	void Start () {
		ac = m_ObjectToAnimate.GetComponent<Animator>();
		ac_parms = ac.parameters;
		skel_script = m_SkeletonObject.GetComponent<DASkeleton>();

		Toggle chk_skel = GameObject.Find("DAChkBox_Skel").GetComponent<Toggle>();
		chk_skel.isOn = skel_script.m_ShowSkeleton;

	}
	


	// NEW Start Specific Animation ==========
	public void clickAnimation(bool is_on) {

		if (is_on) {

			// Selected Toggle
			Toggle my_toggle = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
			
			// Trigger name
			string trig_name = my_toggle.GetComponent<DAChkBox>().param_01;

			// If trigger exists in Animation Controller, Set Trigger
			bool ok = false;
			for (int i=0;  i < ac_parms.Length; i++) if (ac_parms[i].name == trig_name) ok = true;
			if (ok) {
				ac.SetBool(trig_name, true);
				} else {
			}

		}

	}


	
	// Flip Direction ==========
	public void flip() {
		float new_x = -m_ObjectToAnimate.transform.localScale.x;
		m_ObjectToAnimate.transform.localScale = new Vector3(new_x, 1f, 1f);
	}


	// Show or Hide Skeleton ==========
	public void switchSkeleton(bool new_val) {
		skel_script.switchSkeleton(new_val);
	}


	// Show or Hide Character ==========
	public void switchCharacter(bool new_val) {
		skel_script.switchCharacter(new_val);
	}



	public void goVisit() {
		Application.OpenURL ("https://dynamicarts.com");
	}


	void Reset() {
		m_ObjectToAnimate = GameObject.Find("Gordo");
	}

}
}