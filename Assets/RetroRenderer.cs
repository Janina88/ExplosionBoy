﻿using UnityEngine;
using System.Collections;

public class RetroRenderer : MonoBehaviour {
	public Material mat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, mat);
	}
}
