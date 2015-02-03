using UnityEngine;
using System.Collections;

public class XPLODropItemAction : XPLOAction
{
		public GameObject items;
		private bool dropped = false;

		override public void performAction ()
		{
				XPLOItem[] itemComps = items.GetComponentsInChildren<XPLOItem> (true);

		int likelihoodSum = 0; //itemComps.Length * 100;
				foreach (XPLOItem itemComp in itemComps) {
						likelihoodSum += itemComp.getDropLikelihood();
				}

		likelihoodSum *= 100 / GameObject.Find("ExplosionWorld").GetComponent<ExplosionWorld>().dropItemLikelihood;

				int r = Random.Range (0, likelihoodSum);
				foreach (XPLOItem itemComp in itemComps) {
						if (r <= itemComp.getDropLikelihood ()) {
								GameObject.Instantiate (itemComp.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
								break;
						}
						r -= itemComp.getDropLikelihood ();
				}
				
		}

		public bool hasDropped ()
		{
				return this.dropped;
		}

		public void setDropped (bool dropped)
		{
				this.dropped = dropped;
		}

	override public int getRefractoryPeriod ()
	{
		return 50;
	}
}
