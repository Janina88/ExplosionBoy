using UnityEngine;
using System.Collections;

public class RandSprite : MonoBehaviour {
	public Sprite[] sprites;
	public int[] spriteProbs;

	// Use this for initialization
	void Start () {
		int x = getRandomObjectNum ();
		Debug.Log (x);
		Sprite sprite2Use = sprites [x];

		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (renderer != null) {
			renderer.sprite = sprite2Use;
		}

		if(false && Random.Range(0f,1f) > 0.5) {
			gameObject.transform.localScale = new Vector3(-1,1,1);
			for(int i = 0; i < gameObject.transform.childCount; i++) {
//				GameObject go = new GameObject();
//				go.transform.parent = gameObject.transform;
//				go.transform.localScale = new Vector3(1,1,1);
				Transform shadowTransform = gameObject.transform.GetChild(i).transform;
				shadowTransform.localPosition = new Vector3(
					-shadowTransform.localPosition.x,
					shadowTransform.localPosition.y,
					shadowTransform.localPosition.z);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int getRandomObjectNum() {
		int likelihoodSum = 0; //itemComps.Length * 100;
		foreach (int prob in spriteProbs) {
			likelihoodSum += prob;
		}
		
		int r = Random.Range (0, likelihoodSum);
		int k = 0;
		int objectNum = 0;
		foreach(int prob in spriteProbs) {
			k += prob;
			if (r <= k) {
				break;
			}
			objectNum++;
		}
		return objectNum;
	}
}
