// DA Logo (c)Dynamic Arts http://dynamicarts.com


using UnityEngine;
using System.Collections;

using DArts;

namespace DArts {

public class DALogo : MonoBehaviour {


[SerializeField]
private Texture2D cursor_img;

private bool isOver;

void OnMouseEnter() {
	Cursor.SetCursor (cursor_img, Vector2.zero, CursorMode.Auto);
	isOver = true;
}
       
void OnMouseExit() {
	Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	transform.localScale = new Vector3(1.0f,1.0f,1.0f);
	isOver = false;
}

void OnMouseDown() {
	transform.localScale = new Vector3(1.2f,1.2f,1.2f);
}


public void OnMouseUp() {
	if (isOver) {
		transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		Application.OpenURL ("https://dynamicarts.com");
	}
}	



}
}
