using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymation : MonoBehaviour {
	public Material m;
	public int updateOnFrame = 3;
	public Vector2 clayOffset = new Vector2(0.5f, 0.5f);
	int frameCount = 0;
	// public float updateProb = 0.1f;

	void Start() {
		if (m == null)
			m = GetComponent<MeshRenderer>().sharedMaterial;
	}

	void FixedUpdate() {
		frameCount = (frameCount + 1) % updateOnFrame;
		if (frameCount == 0) {
			Vector2 v = new Vector2(Random.Range(0, clayOffset.x), Random.Range(0, clayOffset.y));
			m.SetTextureOffset("_Normal", v);
			m.SetTextureOffset("_Smoothness", v);
		}
	}

	private void OnDisable() {
		Vector2 v = new Vector2(0, 0);
		m.SetTextureOffset("_Normal", v);
		m.SetTextureOffset("_Smoothness", v);
	}
}
