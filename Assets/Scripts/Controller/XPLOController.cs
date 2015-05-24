using UnityEngine;
using System.Collections;

public class XPLOController : MonoBehaviour
{
		
	public string inputSuffix;
	public string[] eventKeys;
	private Vector3 bombSpawnVector;
	private GameObject lastDroppedBomb;
	private LayerMask defaultLayers = 1;
	private Animator[] anims;
	private XPLOActionItem itemToControl;

	void Start ()
	{
		this.gameObject.GetComponentsInChildren<Animator> ();
		anims = this.gameObject.GetComponentsInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float threshold = 0.3f;
		Vector2 dir = new Vector2 ();
		Vector2 dirAnalog = new Vector2 ();

		if (Mathf.Abs (Input.GetAxisRaw ("Horizontal_" + inputSuffix)) > 
			Mathf.Abs (Input.GetAxisRaw ("Vertical_" + inputSuffix))) {
			dirAnalog.x = Input.GetAxisRaw ("Horizontal_" + inputSuffix);
			dirAnalog.y = 0;
		} else {
			dirAnalog.x = 0;
			dirAnalog.y = Input.GetAxisRaw ("Vertical_" + inputSuffix);
		}
		
		if (dirAnalog.x > threshold)
			dir.x = 1;
		if (dirAnalog.x < -threshold)
			dir.x = -1;
		if (dirAnalog.y > threshold)
			dir.y = 1;
		if (dirAnalog.y < -threshold)
			dir.y = -1;

		this.handleItemEvents ();

		dir = this.handleObstacle (dir);

		dir *= gameObject.GetComponent<XPLOPlayer> ().speed * Time.deltaTime;
		foreach (Animator anim in this.anims) {
			anim.SetFloat ("SpeedX", dir.x);
			anim.SetFloat ("SpeedY", dir.y);
		}

		gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);

		bool dropBomb = Input.GetButtonDown ("Fire1_" + inputSuffix);

		GameObject bomb = gameObject.GetComponent<XPLOPlayer> ().bomb;
		XPLOPlayer player = gameObject.GetComponent<XPLOPlayer> ();
		if (dropBomb && !this.isFieldBlocked () && player.curNumBombs < player.numBombsMax) {
			bombSpawnVector.x = (int)(gameObject.transform.position.x + 0.5);
			bombSpawnVector.y = (int)(gameObject.transform.position.y + 0.5);
						
			lastDroppedBomb = ((GameObject)Instantiate (bomb, bombSpawnVector, Quaternion.identity));
			lastDroppedBomb.transform.parent = gameObject.transform.parent;
			lastDroppedBomb.layer = LayerMask.NameToLayer ("PlayerLayer_" + inputSuffix);
			lastDroppedBomb.SetActive (true);
			player.curNumBombs++;

			XPLOExplodeAction action = lastDroppedBomb.GetComponent<XPLOExplodeAction> ();

			action.setWhenFromNow (3000);
			GameObject.Find ("ExplosionWorld").GetComponent<ExplosionWorld> ().enqAction (action);
		}

		if (lastDroppedBomb != null && lastDroppedBomb.GetComponent<Collider2D> () && !lastDroppedBomb.GetComponent<Collider2D> ().bounds.Intersects (gameObject.GetComponent<Collider2D> ().bounds)) {
			lastDroppedBomb.layer = LayerMask.NameToLayer ("Default");
			lastDroppedBomb = null;
		}
	}

	public void handleItemEvents ()
	{
		foreach (string eventKey in this.eventKeys) {
			bool keyDown = Input.GetButtonDown (eventKey + "_" + inputSuffix);
			if (keyDown && this.itemToControl != null) {
				this.itemToControl.performAction (this.gameObject.GetComponent<XPLOPlayer> (), eventKey);
			}
		}
	}

	public void setItemToControl (XPLOActionItem item)
	{
		this.itemToControl = item;
	}

	private Vector2 handleObstacle (Vector2 dir)
	{
		Vector2 rayPos = new Vector2 ();
		Vector2 rayDir = new Vector2 ();

		float rayOffset = 0.5f;
		if (dir.y < 0 && dir.x == 0) {
			rayPos.x = gameObject.transform.position.x - rayOffset;
			rayPos.y = gameObject.transform.position.y;
			rayDir = -Vector2.up;

			bool wouldHit1 = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			rayPos.x = gameObject.transform.position.x + rayOffset;
			rayPos.y = gameObject.transform.position.y;
			rayDir = -Vector2.up;
			
			bool wouldHit2 = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y;
			rayDir = -Vector2.up;
						
			bool wouldHitMid = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);
			
			if (wouldHit1 && !wouldHitMid) {
				dir.x = 1;
				dir.y = 0;
			} else  if (!wouldHitMid && wouldHit2) {
				dir.x = -1;
				dir.y = 0;
			}
		} else if (dir.y > 0 && dir.x == 0) {
			rayPos.x = gameObject.transform.position.x - rayOffset;
			rayPos.y = gameObject.transform.position.y;
			rayDir = Vector2.up;
			
			bool wouldHit1 = this.getObstacle (dir, rayPos, rayDir);
			
			Debug.DrawRay (rayPos, rayDir, Color.red);
			
			rayPos.x = gameObject.transform.position.x + rayOffset;
			rayPos.y = gameObject.transform.position.y;
			rayDir = Vector2.up;
			
			bool wouldHit2 = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y;
			rayDir = Vector2.up;
			
			bool wouldHitMid = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			if (wouldHit1 && !wouldHitMid) {
				dir.x = 1;
				dir.y = 0;
			} else  if (!wouldHitMid && wouldHit2) {
				dir.x = -1;
				dir.y = 0;
			} 
		} else if (dir.x < 0 && dir.y == 0) {
			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y - rayOffset;
			rayDir = -Vector2.right;

			bool wouldHit1 = this.getObstacle (dir, rayPos, rayDir);
			
			Debug.DrawRay (rayPos, rayDir, Color.red);
			
			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y + rayOffset;
			rayDir = -Vector2.right;
			
			bool wouldHit2 = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y;
			rayDir = -Vector2.right;
			
			bool wouldHitMid = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			if (wouldHit1 && !wouldHitMid) {
				dir.x = 0;
				dir.y = 1;
			} else  if (!wouldHitMid && wouldHit2) {
				dir.x = 0;
				dir.y = -1;
			}
		} else if (dir.x > 0 && dir.y == 0) {
			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y - rayOffset;
			rayDir = Vector2.right;
			
			bool wouldHit1 = this.getObstacle (dir, rayPos, rayDir);
			
			Debug.DrawRay (rayPos, rayDir, Color.red);
			
			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y + rayOffset;
			rayDir = Vector2.right;
			
			bool wouldHit2 = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			rayPos.x = gameObject.transform.position.x;
			rayPos.y = gameObject.transform.position.y;
			rayDir = Vector2.right;
			
			bool wouldHitMid = this.getObstacle (dir, rayPos, rayDir);
			Debug.DrawRay (rayPos, rayDir, Color.red);

			if (wouldHit1 && !wouldHitMid) {
				dir.x = 0;
				dir.y = 1;
			} else  if (!wouldHitMid && wouldHit2) {
				dir.x = 0;
				dir.y = -1;
			}
		}
		
		return dir;
	}
	
	private bool getObstacle (Vector2 originalDir, Vector2 rayPos, Vector2 rayDir)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (rayPos, rayDir, 1f, defaultLayers);
			
		bool wouldHit = false;
		foreach (RaycastHit2D hit in hits) {
			if (hit.transform.gameObject != this.gameObject) {
				Collider2D cObject = hit.transform.gameObject.GetComponent<Collider2D> ();
				if (cObject != null && !cObject.isTrigger) {
					wouldHit = true;
				}
			}
		}
			
		return wouldHit;
	}

	public bool isFieldBlocked ()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (this.gameObject.transform.position, Vector2.up, 0.5f);
		
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
	
}
