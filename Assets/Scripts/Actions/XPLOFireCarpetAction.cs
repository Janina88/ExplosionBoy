using UnityEngine;
using System.Collections;

public class XPLOFireCarpetAction : XPLOAction
{
		public int burnHowLongInMillis;
		private bool onFireWhenPerformed;
		private bool onFire;
	
		void OnTriggerEnter2D (Collider2D other)
		{
				if (this.onFire) {
						XPLODestroyAction destroyAction = other.gameObject.GetComponent<XPLODestroyAction> ();
						if (destroyAction != null) {
								destroyAction.setWhenFromNow (50);
								GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ().enqAction (destroyAction);
						}
				}
		}

		override public void performAction ()
		{
				if (this.onFireWhenPerformed) {
						gameObject.transform.GetChild (0).gameObject.SetActive (true);
						this.onFire = true;

						this.setOnFireWhenPerformed (false);
						this.setWhenFromNow (this.burnHowLongInMillis);
						GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ().enqAction (this);
				} else {
						gameObject.transform.GetChild (0).gameObject.SetActive (false);
						this.onFire = false;
				}
		}

		public void setOnFireWhenPerformed (bool onFireWhenPerformed)
		{
				this.onFireWhenPerformed = onFireWhenPerformed;
		}

		public bool isOnFireWhenPerformed ()
		{
				return this.onFireWhenPerformed;
		}

		public bool isCoveredByBarrier ()
		{
				RaycastHit2D[] hits = Physics2D.RaycastAll (this.transform.position, Vector2.up, 0.5f);
		
				foreach (RaycastHit2D hit in hits) {
						XPLOBarrier barrier = hit.transform.gameObject.GetComponent<XPLOBarrier> ();
						if (barrier != null) {
								return true;
						}
				}

				return false;
		}

	override public int getRefractoryPeriod ()
	{
		return 50;
	}
}
