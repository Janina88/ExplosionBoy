using UnityEngine;
using System.Collections;

public class XPLODeathMatchObjective : MonoBehaviour
{
		private bool objectiveFulfilled;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!this.objectiveFulfilled) {
						GameObject[] players = gameObject.GetComponent<ExplosionWorld> ().getPlayers ();
						int numSurvivors = 0;
						XPLOPlayer survivor = null;
						foreach (GameObject player in players) {
								if (player.activeSelf) {
										XPLOPlayer playerComp = player.GetComponent<XPLOPlayer> ();
										if (playerComp.health > 0) {
												numSurvivors++;
												survivor = playerComp;
										}
								}
						}

						if (numSurvivors == 1) {
								this.objectiveFulfilled = true;
								XPLOWinAction winAction = survivor.GetComponent<XPLOWinAction> ();
								winAction.setWhenFromNow (1500);
								gameObject.GetComponent<ExplosionWorld> ().enqAction (winAction);
						}

						if (numSurvivors == 0) {
								this.objectiveFulfilled = true;
								XPLOWinAction winAction = survivor.GetComponent<XPLOWinAction> ();
								winAction.setWhenFromNow (1500);
								gameObject.GetComponent<ExplosionWorld> ().enqAction (winAction);
						}
				}
		}
}
