using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class OffsetAnimado : MonoBehaviour {

	public float velocidade;
	public bool parado;

	void Update () {
		if (!parado) renderer.material.mainTextureOffset = new Vector2 (renderer.material.mainTextureOffset.x + Time.deltaTime * velocidade, 0);
	}

	void Start () {
		// controle de animacao
		MessageKit.addObserver (Eventos.Andar, QuandoAndar);
		MessageKit.addObserver (Eventos.Parar, QuandoParar);
		parado = false;
	}
	
	void QuandoAndar () {
		parado = false;
	}
	
	void QuandoParar () {
		parado = true;
	}
	
	void OnDestroy () {
		MessageKit.clearMessageTable();
	}
}
