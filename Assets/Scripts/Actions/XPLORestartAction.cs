using UnityEngine;
using System.Collections;

public class XPLORestartAction : XPLOAction {

	override public void performAction() {
		ExplosionWorld xploWorld = GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ();
		XPLOMessage message = xploWorld.messageObject.GetComponent<XPLOMessage> ();
		message.hideMessage ();

		Application.LoadLevel(Application.loadedLevel);
	}

	override public int getRefractoryPeriod ()
	{
		return 50;
	}
}
