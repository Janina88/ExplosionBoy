using UnityEngine;
using System.Collections;

public class XPLODestroyAction : XPLOAction
{
		public GameObject[] parts;
		private bool destroyed = false;
		private int destructionPhase = 0;
		private Vector2 impactDir;

		override public void performAction ()
		{
				if (destructionPhase == 0) {
						Vector3 spawnPos = gameObject.transform.position;
		
						foreach (GameObject part in parts) {
								GameObject tile = (GameObject)Instantiate (part, spawnPos, Quaternion.identity);
								tile.layer = LayerMask.NameToLayer ("EffectLayer");
								tile.SetActive (true);
								tile.rigidbody2D.AddForce (this.impactDir * 500);
								tile.rigidbody2D.AddTorque (Random.value * 600 - 300);
						}

						XPLOPlayer player = gameObject.GetComponent<XPLOPlayer> ();
						if (player != null) {
								player.health = 0;
						}

						
						this.destructionPhase = 1;
						this.setWhenFromNow (600);
						GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ().enqAction (this);
						
						this.gameObject.renderer.enabled = false;
						foreach (SpriteRenderer r in gameObject.transform.GetComponentsInChildren<SpriteRenderer>()) {
								r.enabled = false;
						}
						ParticleSystem pSystem = this.gameObject.GetComponent<ParticleSystem> ();
						if (pSystem != null) {
//								pSystem.enableEmission = true;
						}
						Collider2D collider = this.gameObject.GetComponent<Collider2D> ();
//						if (collider != null) {
//								collider.enabled = false;
//						}

				} else {
						gameObject.SetActive (false);
				}
		}

		public void setImpactDir (Vector2 impactDir)
		{
				this.impactDir = impactDir;
		}
	
		public Vector2 getImpactDir ()
		{
				return this.impactDir;
		}

		public void setDestroyed (bool destroyed)
		{
				this.destroyed = destroyed;
		}
	
		public bool isDestroyed ()
		{
				return this.destroyed;
		}

		override public int getRefractoryPeriod ()
		{
				return 500;
		}
}

