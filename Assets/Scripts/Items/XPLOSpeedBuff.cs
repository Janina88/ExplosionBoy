using UnityEngine;
using System.Collections;

public class XPLOSpeedBuff : XPLOItem {
	public int dropLikelihood;
	public float speedIncreaseFactor;
	public float speedIncreaseLinear;
	
	override public void attachToPlayer (XPLOPlayer player)
	{
		player.speed = player.speed * this.speedIncreaseFactor + this.speedIncreaseLinear;
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
