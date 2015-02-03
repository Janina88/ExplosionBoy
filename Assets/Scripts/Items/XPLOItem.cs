using UnityEngine;
using System.Collections;

public abstract class XPLOItem : MonoBehaviour
{
		public int amount = 1;
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				XPLOPlayer player = other.GetComponent<XPLOPlayer> ();
				if (player != null) {
						player.addItem (this);
						this.gameObject.SetActive (false);
				}
		}

		public abstract void attachToPlayer (XPLOPlayer player);

		public abstract void detachFromPlayer (XPLOPlayer player);

		public abstract int getDropLikelihood ();
}
