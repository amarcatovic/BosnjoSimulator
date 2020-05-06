// Skeleton Limb (c)Dynamic Arts http://dynamicarts.com


using UnityEngine;
using System.Collections;
using DArts;


namespace DArts {


public class DALimb : MonoBehaviour {

private DASkeleton parent_script;
public GameObject parent_go;
public GameObject child_go;
public GameObject skel_root;
private LineRenderer lr_fg;
private LineRenderer lr_bg;


// Use this for initialization
void Start () {
}


public void goInit(GameObject child_in, GameObject skel_root_in) {

	skel_root = skel_root_in;
	parent_script = skel_root.GetComponent<DASkeleton>();

	child_go = child_in;

	transform.localPosition = child_go.transform.localPosition;


	// Add Bone Line Renderer (Background)
	lr_bg = transform.Find("Line_BG").gameObject.AddComponent<LineRenderer>();
	lr_bg.material = new Material(Shader.Find("Sprites/Default"));
	lr_bg.useWorldSpace = true;
	lr_bg.positionCount = 2;
	lr_bg.startWidth = .3f;
	lr_bg.endWidth = .13f;
	lr_bg.sortingOrder = 990;
	lr_bg.startColor = parent_script.m_BoneBGColor;
	lr_bg.endColor = parent_script.m_BoneBGColor;

	// Add Bone Line Renderer (Foregound)
	lr_fg = transform.Find("Line_FG").gameObject.AddComponent<LineRenderer>();
	lr_fg.material = new Material(Shader.Find("Sprites/Default"));
	lr_fg.useWorldSpace = true;
	lr_fg.positionCount = 2;
	lr_fg.startWidth = .2f;
	lr_fg.endWidth = .03f;
	lr_fg.sortingOrder = 991;
	lr_fg.startColor = parent_script.m_BoneFGColor;
	lr_fg.endColor = parent_script.m_BoneFGColor;


	// Set Joint color
	transform.Find("Joint/Sphere").gameObject.GetComponent<Renderer>().material.color = parent_script.m_JointColor;

	// Set Glow color
	transform.Find("Joint/Glow").gameObject.GetComponent<Renderer>().material.color = parent_script.m_JointGlowColor;



}




	
// Update is called once per frame
void Update () {

	transform.localPosition = child_go.transform.localPosition;
	transform.localRotation = child_go.transform.localRotation;

	lr_bg.SetPosition(0, transform.position);
	lr_bg.SetPosition(1, transform.parent.position);

	lr_fg.SetPosition(0, transform.position);
	lr_fg.SetPosition(1, transform.parent.position);

}



}
}
