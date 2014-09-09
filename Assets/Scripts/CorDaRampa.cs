using UnityEngine;
using System.Collections;

public class CorDaRampa : MonoBehaviour {

	public Texture2D tex;

	// Use this for initialization
	void Start () {
		renderer.material.color = tex.GetPixel (Random.Range(0, tex.width), 1);
	}
}
