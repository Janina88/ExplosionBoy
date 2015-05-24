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

	override public void performAction (XPLOPlayer player, string inputKey)
	{
		if (inputKey == "Fire1") {
			Vector2 pos = new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y);
			RaycastHit2D[] hits = Physics2D.RaycastAll (pos, 
			                                            Vector2.up, 0.2f);

			foreach(RaycastHit2D hit in hits) {
				XPLOExplodeAction explodeAction = hit.transform.gameObject.GetComponent<XPLOExplodeAction>();
				if(explodeAction != null) {
					foreach(SpriteRenderer renderer in explodeAction.gameObject.GetComponentsInChildren<SpriteRenderer>()) {
						renderer.sortingOrder = 1000;
					}
					explodeAction.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
					explodeAction.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 300);
				}
			}
		}
	}
}
