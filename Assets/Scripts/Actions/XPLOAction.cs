using UnityEngine;

public abstract class XPLOAction : MonoBehaviour
{
		private int when;
		private int refractoryPeriod;
		private int lastCall;
	
		public void setWhenFromNow (int when)
		{
				this.setWhen ((int)(Time.time * 1000) + when);
		}
	
		public void setWhen (int when)
		{
				this.when = when;
		}
	
		public int getWhen ()
		{
				return this.when;
		}

		public bool callAction ()
		{
				if ((int)(Time.time * 1000) > this.lastCall + this.getRefractoryPeriod ()) {
						this.lastCall = ((int)Time.time * 1000);
						this.performAction ();
						return true;
				}

				return false;
		}

		public abstract void performAction ();

		public void setRefractoryPeriod (int refractoryPeriod)
		{
				this.refractoryPeriod = refractoryPeriod;
		}

		public abstract int getRefractoryPeriod ();

		// Use this for initialization
		void Start ()
		{
		
		}
}
