using UnityEngine;
using System.Collections;

public class XPLOExplosionForceBuff : XPLOItem {
	public int dropLikelihood;

	override public void attachToPlayer (XPLOPlayer player)
	{
		this.gameObject.transform.parent = player.gameObject.transform;
		Detonator detonator = player.bomb.GetComponent<Detonator> ();
		if (detonator != null) {
			detonator.strength++;
		}
	}

	override public void detachFromPlayer (XPLOPlayer player)
	{
		Detonator detonator = player.bomb.GetComponent<Detonator> ();
		if (detonator != null) {
			detonator.strength--;
		}
	}

	override public int getDropLikelihood() {
		return this.dropLikelihood;
	}
}
