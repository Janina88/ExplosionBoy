using UnityEngine;
using System.Collections;

public class XPLOMessage : MonoBehaviour {
	public string message;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showMessage(string message) {
		this.message = message;

		TextMesh textM = gameObject.GetComponent<TextMesh> ();
		textM.text = message;

		gameObject.SetActive(true);
	}

	public void hideMessage() {
		gameObject.SetActive(false);
	}
}
