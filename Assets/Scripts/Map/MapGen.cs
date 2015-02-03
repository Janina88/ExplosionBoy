using UnityEngine;
using System.Collections;

[AddComponentMenu("Map Tools/MapGen")]
public class MapGen : MonoBehaviour {
	public GameObject object2Put;
	public Texture2D where2Put;
	public float tileSize;

	// Use this for initialization
	void Start () {
		Vector3 origin = transform.position;
		origin.x += where2Put.width / 2;
		origin.y += where2Put.height / 2;
		Vector3 spawnPos = new Vector3 (0, 0, origin.z);
		int mapheight = where2Put.height;
		for (int i = 0; i < where2Put.width; i++) {
			for(int j = 0; j < mapheight; j++) {
				float alpha = where2Put.GetPixel(i, j).a;
				if(alpha != 0) {
					spawnPos.x = i * tileSize; // - origin.x;
					spawnPos.y = j * tileSize; // - origin.y;
					GameObject tile = (GameObject)Instantiate(object2Put, spawnPos, Quaternion.identity);
					tile.name = object2Put.name + "_" + i + "_" + j;
					tile.transform.parent = gameObject.transform;
					tile.SetActive(true);
					tile.GetComponent<SpriteRenderer>().sortingOrder = 100 + mapheight - j;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
