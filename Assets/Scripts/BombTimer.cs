using UnityEngine;
using System.Collections;

public class BombTimer : MonoBehaviour
{
		public Detonator detonator;
		public float delay;
		private float startTime;
		private bool exploded = false;

		// Use this for initialization
		void Start ()
		{
				startTime = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (startTime == 0) {
						startTime = Time.time;
				}
				if (!exploded && Time.time - startTime > delay) {
						exploded = true;
						AudioSource sound = gameObject.GetComponent<AudioSource> ();
						if (sound != null) {
								gameObject.GetComponent<AudioSource> ().Play ();
						}

						detonator.Explode ();

						gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				}

				if (exploded) {
						gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}

		}
}