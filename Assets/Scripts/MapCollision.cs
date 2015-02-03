using UnityEngine;
using System.Collections;

public class MapCollision : MonoBehaviour {
	public Texture2D collisionMap;

	// Use this for initialization
	void Start () {
		int x = 0;
		int y = 0;

		for (int i=0; i < collisionMap.width; i++) {
			for(int j=0; j < collisionMap.height; j++) {
				Debug.Log(collisionMap.GetPixel(x, y));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
