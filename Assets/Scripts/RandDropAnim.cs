using UnityEngine;
using System.Collections;

public class RandDropAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponentsInChildren<Animator> ();
		Animator[] anims = this.gameObject.GetComponentsInChildren<Animator> ();

		foreach (Animator anim in anims) {
			anim.SetInteger ("dropType", Random.Range(0, 2));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
