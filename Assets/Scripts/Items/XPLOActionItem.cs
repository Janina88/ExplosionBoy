using UnityEngine;
using System.Collections;

public abstract class XPLOActionItem : XPLOItem {

	public abstract void performAction (XPLOPlayer player, string inputKey);
}
