using UnityEngine;
using System.Collections;

public class XPLOStats : MonoBehaviour {
	private int[] numWins;

	// Use this for initialization
	void Start () {
		numWins = new int[gameObject.GetComponent<ExplosionWorld>().getPlayers().Length];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
