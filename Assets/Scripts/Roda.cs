using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class Roda : MonoBehaviour {

	public float velocidade;
	public bool parado = false;
	
	void Update () {
		if (!parado) transform.Rotate (0, velocidade * Time.deltaTime, 0);
	}

	void Start () {
		// controle de animacao
		MessageKit.addObserver (Eventos.Andar, QuandoAndar);
		MessageKit.addObserver (Eventos.Parar, QuandoParar);
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
