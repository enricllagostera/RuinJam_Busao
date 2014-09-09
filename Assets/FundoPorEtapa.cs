using UnityEngine;
using System.Collections;

public class FundoPorEtapa : MonoBehaviour {

	public Color corAndando;
	public Color corParado;
	public float suavizacao = 2f;

	void Update () {
		if (Jogo.etapa == EEtapa.Andando) {
			renderer.material.color = Color.Lerp (renderer.material.color, corAndando, Time.deltaTime * suavizacao);
		}
		else {
			renderer.material.color = Color.Lerp (renderer.material.color, corParado, Time.deltaTime * suavizacao);
		}
	}
}
