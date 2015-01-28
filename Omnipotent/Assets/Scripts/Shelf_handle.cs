﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shelf_handle : MonoBehaviour {

	public cursor_handle cursor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnMouseOver () {
		cursor.EnterShelf();
		GetComponent<Image> ().CrossFadeAlpha(1.8f, 0.4f, true);
	}

	public void OnMouseExit () {
		cursor.ExitShelf ();
		GetComponent<Image> ().CrossFadeAlpha(1.0f, 0.4f, true);
	}

	public void OnMouseClick () {
		cursor.Build (0);
		Debug.Log ("Clicked");
	}
}