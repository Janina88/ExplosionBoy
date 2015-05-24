using UnityEngine;
using System.Collections;

public class XPLODestructor : MonoBehaviour
{
		public GameObject[] parts;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void destroyThis (Vector2 impactDir)
		{
				Vector3 spawnPos = gameObject.transform.position;

				foreach (GameObject part in parts) {
						GameObject tile = (GameObject)Instantiate (part, spawnPos, Quaternion.identity);
						tile.layer = LayerMask.NameToLayer ("EffectLayer");
						tile.SetActive (true);
						tile.GetComponent<Rigidbody2D>().AddForce (impactDir * 500);
						tile.GetComponent<Rigidbody2D>().AddTorque (Random.value * 600 - 300);
				}

				gameObject.SetActive (false);
		}
}
