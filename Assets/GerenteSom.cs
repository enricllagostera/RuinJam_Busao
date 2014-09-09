using UnityEngine;
using System.Collections;

public class GerenteSom : MonoBehaviour {
	public static GerenteSom i;
	public AudioClip descer;
	public AudioClip subir;
	public AudioClip mover;
	public AudioClip empurrao;

	// Use this for initialization
	void Awake () {
		i = this;
	}
}
