// Build Skeleton for Rig (c)Dynamic Arts http://dynamicarts.com


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DArts;


namespace DArts {

public class DASkeleton : MonoBehaviour {

public GameObject m_ObjectToAnimate;
public bool m_ShowSkeleton;
public Color m_JointColor;
public Vector3 m_SkeletonOffsetPosition;
public Color m_JointGlowColor;
public Color m_BoneFGColor;
public Color m_BoneBGColor;
private GameObject rig;

// Use this for initialization
void Start () {
	if (m_ObjectToAnimate!=null) {

		rig = m_ObjectToAnimate.transform.Find("Rig").gameObject;
		transform.parent = m_ObjectToAnimate.transform;
		transform.position = rig.transform.localPosition + m_SkeletonOffsetPosition;
		buildSkeleton(rig, transform.gameObject);
		switchSkeleton(m_ShowSkeleton);


	}


}


void Update() {
	transform.localPosition = rig.transform.localPosition + m_SkeletonOffsetPosition;
}


// Build/Show Sleleton: Recursive Traversal of game object ==========
private void buildSkeleton(GameObject parent_go, GameObject parent_limb) {
	foreach (Transform tr in parent_go.transform) {
		GameObject child_go = tr.gameObject;
		if (child_go.name != "Img") {
			GameObject limb = addLimb(parent_go, child_go, parent_limb, transform.gameObject);
			buildSkeleton(child_go, limb); 
		}	
	}
}


// Add Limb ==========
private GameObject addLimb(GameObject parent_go, GameObject child_go, GameObject parent_limb, GameObject skel_root) {
	GameObject limb = Instantiate(Resources.Load("DALimb")) as GameObject;
	limb.transform.parent = parent_limb.transform;
	limb.GetComponent<DALimb>().goInit(child_go, skel_root);
	limb.name = child_go.name + "_limb";
	return limb;
}



// Flip Show/Hide Skeleton (External) ==========
	public void switchSkeleton(bool new_val) {
	m_ShowSkeleton = new_val;
	gameObject.SetActive(m_ShowSkeleton);
}



// Flip Show/Hide Character (External) ==========
	public void switchCharacter(bool new_val) {
	rig.SetActive(new_val);
}



// Reset ==========
public void Reset() {
	m_ShowSkeleton = true;
	m_SkeletonOffsetPosition = new Vector3(0,0,-1f);
	m_JointColor = hexStr2Color("#0000FF"); 
	m_JointGlowColor = hexStr2Color("#FFFFFF"); 
	m_BoneFGColor = hexStr2Color("#FF0000");
	m_BoneBGColor = hexStr2Color("#FFFFFF");
} 


// Convert hex string to color ==========
public static Color hexStr2Color(string hex_str) {
	Color clr = new Color(0, 0, 0, 1f);
	if (hex_str != null && hex_str.Length > 0) {
		if (hex_str.StartsWith("#")) hex_str = hex_str.Substring(1);
		if (hex_str.StartsWith("0x")) hex_str = hex_str.Substring(2);
		clr.r = (float)System.Int32.Parse(hex_str.Substring(0, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
		clr.g = (float)System.Int32.Parse(hex_str.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
		clr.b = (float)System.Int32.Parse(hex_str.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
		if (hex_str.Length == 8) clr.a = System.Int32.Parse(hex_str.Substring(6, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
		else clr.a = 1.0f;
	}
	return clr;
}





}
}