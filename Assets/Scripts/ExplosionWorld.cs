using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionWorld : MonoBehaviour
{
		public GameObject[] players;
		public GameObject messageObject;
		private PriorityQueue<int, XPLOAction> actionQ;
	public int dropItemLikelihood;

		void Awake ()
		{
				actionQ = new PriorityQueue<int, XPLOAction> (new PriorityComparer ());
		}

		void Update ()
		{
				this.performAction ();
		}

		void performAction ()
		{
				bool actionFound = true;
				while (!actionQ.IsEmpty && actionFound) {
						KeyValuePair<int, XPLOAction> keyValue = actionQ.Peek ();
						if (Time.time * 1000 > keyValue.Key) {
								actionQ.Dequeue ();
								keyValue.Value.callAction ();
						} else {
								actionFound = false;
						}
				}
		}

		public void enqAction (XPLOAction action)
		{
				actionQ.Enqueue (action.getWhen (), action);
		}

		public void deleteFromActionQ (XPLOAction action)
		{
				//Todo: Temporary solution (ugly)
				KeyValuePair<int, XPLOAction> pair = new KeyValuePair<int, XPLOAction> (action.getWhen (), action);
				actionQ.Remove (pair);

//				pair = new KeyValuePair<int, XPLOAction> ((int)(Time.time + 0.3f), action);
//				actionQ.Add (pair);
		}

		public Vector4 performExplosion (Vector2 castPoint, int strength)
		{
				Vector4 explosionLength = new Vector4 ();

				if (strength > 0) {
						explosionLength.w = performExplosion (castPoint, Vector2.up, strength);
						explosionLength.x = performExplosion (castPoint + Vector2.right, Vector2.right, strength - 1) + 1;
						explosionLength.y = performExplosion (castPoint - Vector2.right, -Vector2.right, strength - 1) + 1;
						explosionLength.z = performExplosion (castPoint - Vector2.up, new Vector2 (0, - 1), strength - 1) + 1;
				}

				return explosionLength;
		}

		private ArrayList[] getHits (Vector2 castPoint, Vector2 direction, int strength)
		{
				RaycastHit2D[] hits = Physics2D.RaycastAll (castPoint, direction, strength);
				ArrayList[] hitsOrdered = new ArrayList[strength * 3 + 1];

				foreach (RaycastHit2D hit in hits) {
						int dist = (int)(Vector2.Distance (castPoint, hit.point) * 3);
						if (hitsOrdered [dist] == null) {
								hitsOrdered [dist] = new ArrayList ();
						}
						hitsOrdered [dist].Add (hit);
				}

				return hitsOrdered;
		}
	
		private int performExplosion (Vector2 castPoint, Vector2 direction, int strength)
		{
				int explosionLength = strength;
				
				ArrayList[] hitsOrdered = this.getHits (castPoint, direction, strength);
				
				bool explosionStopped = false;
				int bombExplosions = 1;
				for (int i = 0; i < hitsOrdered.Length; i++) {
						ArrayList hitList = hitsOrdered [i];
						if (!explosionStopped) {
								if (hitList != null) {
										foreach (RaycastHit2D hit in hitList) {
												XPLOExplodeAction exploder = hit.transform.gameObject.GetComponent<XPLOExplodeAction> ();

												if (exploder != null && !exploder.isExploded ()) {
														exploder.setExploded (true);
														deleteFromActionQ (exploder);
														exploder.setWhenFromNow (bombExplosions++ * 50);
														this.enqAction (exploder);
												}

												XPLODestroyAction destroyAction = hit.transform.gameObject.GetComponent<XPLODestroyAction> ();
												if (destroyAction != null) {
														destroyAction.setWhenFromNow (100);
														destroyAction.setImpactDir (direction * strength);
														this.enqAction (destroyAction);
												}

												XPLODropItemAction dropAction = hit.transform.gameObject.GetComponent<XPLODropItemAction> ();
												if (dropAction != null && !dropAction.hasDropped ()) {
														dropAction.setWhenFromNow (100);
														this.enqAction (dropAction);
														dropAction.setDropped (true);
												}

												XPLOFireCarpetAction fCarpetAction = hit.transform.gameObject.GetComponent<XPLOFireCarpetAction> ();
						
												if (fCarpetAction != null && !fCarpetAction.isCoveredByBarrier () 
														&& !fCarpetAction.isOnFireWhenPerformed ()) {
														fCarpetAction.isCoveredByBarrier ();
														fCarpetAction.setWhenFromNow (50);
														fCarpetAction.setOnFireWhenPerformed (true);
														this.enqAction (fCarpetAction);
												}

												XPLOBarrier barrier = hit.transform.gameObject.GetComponent<XPLOBarrier> ();
												if (barrier != null) {
														explosionLength = (int)(Vector2.Distance (castPoint, hit.point) + 0.5);
														explosionStopped = true;
												} 		
										}
								}
						}
				}

				return explosionLength;
		}

		public GameObject[] getPlayers ()
		{
				return this.players;
		}
}
