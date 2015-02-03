using UnityEngine;
using System.Collections;

public class XPLOWinAction : XPLOAction {

	override public void performAction ()
	{
		XPLOPlayer xploPlayer = gameObject.GetComponent<XPLOPlayer> ();
		string msg = xploPlayer.name + " killed 'em all!";
		ExplosionWorld xploWorld = GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ();
		XPLOMessage message = xploWorld.messageObject.GetComponent<XPLOMessage> ();
		message.showMessage (msg);

		XPLORestartAction restartAction = new XPLORestartAction ();
		restartAction.setWhenFromNow (5000);
		xploWorld.enqAction (restartAction);
	}

	override public int getRefractoryPeriod ()
	{
		return 50;
	}
}
