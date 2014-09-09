using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class ControlaTeto : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MessageKit.addObserver (Eventos.Comecou, QuandoComecar);
		MessageKit.addObserver (Eventos.Acabou, QuandoAcabar);
	}

	void QuandoComecar () {
		foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
			rend.enabled = false;
		}
		renderer.enabled = false;
	}

	void QuandoAcabar () {
		foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
			rend.enabled = true;
		}
		renderer.enabled = true;
	}
}
