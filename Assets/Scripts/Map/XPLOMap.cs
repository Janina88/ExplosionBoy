using UnityEngine;
using System.Collections;

public interface XPLOMap
{
	GameObject getObjectAt(int x, int y);

	GameObject setObjectAt(GameObject go, int x, int y);
}