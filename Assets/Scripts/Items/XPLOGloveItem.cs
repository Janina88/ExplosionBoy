using UnityEngine;
using System.Collections;

public class XPLOGloveItem : XPLOActionItem
{
	public int dropLikelihood;
	
	override public void attachToPlayer (XPLOPlayer player)
	{
		XPLOController control = player.gameObject.GetComponent<XPLOController> ();
		control.setItemToControl (this);
	}
	
	override public void detachFromPlayer (XPLOPlayer player)
	{
		XPLOController control = player.gameObject.GetComponent<XPLOController> ();
		control.setItemToControl (null);
	}
	
	override public int getDropLikelihood ()
	{
		return this.dropLikelihood;
	}

	override public void performAction (XPLOPlayer player, string inputKey, IList consumedEvents)
	{
		if (inputKey == "Fire1") {
			Vector2 throwDir = this.getThrowDir(player);
			Vector2 pos = new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y);
			RaycastHit2D[] hits = Physics2D.RaycastAll (pos, 
			                                            Vector2.up, 0.2f);

			foreach(RaycastHit2D hit in hits) {
				XPLOExplodeAction explodeAction = hit.transform.gameObject.GetComponent<XPLOExplodeAction>();
				if(explodeAction != null) {
					XPLOHoppingAction hoppingAction = hit.transform.gameObject.GetComponent<XPLOHoppingAction>();
					hoppingAction.setHoppingDir(throwDir);
					hoppingAction.performAction ();

					consumedEvents.Add (inputKey);
//					explodeAction.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
//					explodeAction.gameObject.GetComponent<Rigidbody2D>().AddForce(throwDir * 300);
				}
			}
		}
	}

	private Vector2 getThrowDir(XPLOPlayer player) {
		Vector2 throwDir = new Vector2();
		int faceDir = player.getFaceDir();
		if (faceDir == XPLOPlayer.up) {
			throwDir.x = 0;
			throwDir.y = 1;
		} else if (faceDir == XPLOPlayer.down) {
			throwDir.x = 0;
			throwDir.y = -1;
		} else if (faceDir == XPLOPlayer.left) {
			throwDir.x = -1;
			throwDir.y = 0;
		} else {
			throwDir.x = 1;
			throwDir.y = 0;
		}

		return throwDir;
	}
}
