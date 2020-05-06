// Check Box Additional Parameters (c)Dynamic Arts http://dynamicarts.com


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;
using DArts;

namespace DArts {


public class DAChkBox : MonoBehaviour {

	[SerializeField]
	private Color m_CheckColor;
	public string param_01;
	public string param_02;

	

	void Start() {
		transform.Find("Background/Checkmark").GetComponent<Image>().color = m_CheckColor;
	}


	void Reset() {
		m_CheckColor = hexStr2Color("#009900");
		param_01 = "";
		param_02 = "";
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
