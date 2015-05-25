using UnityEngine;
using System.Collections;

public class XPLOHoppingAction : XPLOAction {
	public Vector2 hoppingDir;
	public int hoppingSpeed = 300;
	public int groundLayer = 100;
	public int hoppingLayer = 1000;

	private bool isHopping;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isHopping) {
			Vector2 pos1 = new Vector2 (this.gameObject.transform.position.x - 0.4f, this.gameObject.transform.position.y - 0.4f);
			Vector2 pos2 = new Vector2 (this.gameObject.transform.position.x + 0.4f, this.gameObject.transform.position.y + 0.4f);
			if (!this.isFieldBlocked (pos1) && !this.isFieldBlocked (pos2)) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

				gameObject.GetComponent<Collider2D> ().enabled = true;
				SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer r in renderers) {
					r.sortingOrder = this.groundLayer;
				}

				isHopping = false;
			}
		}
	}

	public bool isFieldBlocked (Vector2 pos)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (pos, Vector2.zero);
		Debug.DrawRay (pos, Vector2.up, Color.green);
		foreach (RaycastHit2D hit in hits) {
			Collider2D collider = hit.transform.gameObject.GetComponent<Collider2D> ();
			
			if (collider != null && collider.gameObject != this.gameObject && !collider.isTrigger) {
				if (collider.gameObject.layer != LayerMask.NameToLayer("EffectLayer")) {
					return true;
				}
			}
		}
		
		return false;
	}

	public void setHoppingDir(Vector2 hoppingDir) {
		this.hoppingDir = hoppingDir;
	}

	override public void performAction() {
		gameObject.GetComponent<Collider2D> ().enabled = false;
		SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer r in renderers) {
			r.sortingOrder = this.hoppingLayer;
		}
		gameObject.GetComponent<Rigidbody2D>().AddForce(hoppingDir * this.hoppingSpeed);

		isHopping = true;
	}

	override public int getRefractoryPeriod ()
	{
		return 50;
	}
}