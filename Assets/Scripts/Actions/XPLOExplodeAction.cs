using UnityEngine;
using System.Collections;

public class XPLOExplodeAction : XPLOAction
{
		private bool exploded = false;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		override public void performAction ()
		{
				Detonator detonator = gameObject.GetComponent<Detonator> ();
				int strength = detonator.strength;

				// Model
				Vector2 castPoint = gameObject.transform.position;
				this.setExploded (true);
				Vector4 explosionLength = GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ().performExplosion (castPoint, strength);	
				gameObject.transform.parent.GetChild (0).GetComponent<XPLOPlayer> ().curNumBombs--;		

				// View
				detonator.Explode (explosionLength);

				gameObject.GetComponent<CircleCollider2D> ().enabled = false;

				foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren<SpriteRenderer> ()) {
						renderer.enabled = false;
				}

				AudioSource sound = gameObject.GetComponent<AudioSource> ();
				if (sound != null) {
						gameObject.GetComponent<AudioSource> ().Play ();
				}
		}

		public void setExploded (bool exploded)
		{
				this.exploded = exploded;
		}

		public bool isExploded ()
		{
				return this.exploded;
		}

		override public int getRefractoryPeriod ()
		{
				return 1000;
		}
}
