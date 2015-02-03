using UnityEngine;
using System.Collections;

public class XPLOBombItem : XPLOItem
{
		public int dropLikelihood;
	
		override public void attachToPlayer (XPLOPlayer player)
		{
				player.numBombsMax++;
		}
	
		override public void detachFromPlayer (XPLOPlayer player)
		{
				player.numBombsMax--;
		}
	
		override public int getDropLikelihood ()
		{
				return this.dropLikelihood;
		}
}
