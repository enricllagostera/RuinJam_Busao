using UnityEngine;
using System.Collections;

public class GerenteSom : MonoBehaviour {
	public static GerenteSom i;
	public AudioClip descer;
	public AudioClip subir;
	public AudioClip mover;
	public AudioClip empurrao;

	public Transform sonsGerais;

	// Use this for initialization
	void Awake () {
		i = this;
	}

	void Update () {
		if (Jogo.etapa == EEtapa.Inicio || Jogo.fimDeJogo) {
			foreach (AudioSource som in sonsGerais.GetComponents<AudioSource>()) {
				som.volume = Mathf.Lerp (som.volume, 0.3f, Time.deltaTime *2f);
			}
		}
		else {
			foreach (AudioSource som in sonsGerais.GetComponents<AudioSource>()) {
				som.volume = Mathf.Lerp (som.volume, 0.6f, Time.deltaTime * 2f);
			}
		}
	}
}
