using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XPLOMessage : MonoBehaviour
{
	public string message;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void showMessage (string message)
	{
		this.message = message;

		GameObject textObject = gameObject.transform.FindChild ("Text").gameObject;

		Text t = textObject.GetComponent<Text> ();
		t.text = message;


		gameObject.SetActive (true);
	}

	public void hideMessage ()
	{
		gameObject.SetActive (false);
	}
}
