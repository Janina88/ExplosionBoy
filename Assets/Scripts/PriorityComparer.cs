using System.Collections.Generic;

public class PriorityComparer : IComparer<int> {

	public int Compare(int i1, int i2) {
		return i1 - i2;
	}
}
