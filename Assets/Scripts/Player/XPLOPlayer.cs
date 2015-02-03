using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XPLOPlayer : MonoBehaviour {
	public string name;
	public GameObject bomb;
	public int health;
	public List<XPLOItem> items;
	public int numBombsMax;
	public int curNumBombs;
	public float speed;

	public void Start() {
		items = new List<XPLOItem>();
		this.bomb = (GameObject)GameObject.Instantiate (bomb);
	}

	public void addItem(XPLOItem item) {
		items.Add (item);
		item.attachToPlayer (this);
	}
}
