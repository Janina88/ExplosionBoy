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

	
	private int faceDir;
	public static int up = 0;
	public static int down = 1;
	public static int left = 2;
	public static int right = 3;

	public void Start() {
		if (items == null) {
			items = new List<XPLOItem> ();
		} else {
			foreach(XPLOItem item in items) {
				item.attachToPlayer (this);
			}
		}
		this.bomb = (GameObject)GameObject.Instantiate (bomb);
	}

	public void addItem(XPLOItem item) {
		items.Add (item);
		item.attachToPlayer (this);
	}

	public int getFaceDir() {
		return this.faceDir;
	}

	public void setFaceDir(int faceDir) {
		this.faceDir = faceDir;
	}
}
