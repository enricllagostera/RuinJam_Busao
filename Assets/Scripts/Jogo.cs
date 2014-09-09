using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class Jogo : MonoBehaviour {

	public static EEtapa etapa;
	public bool fimDeJogo;
	private bool mudo;
	public int parada;
	public int paradaAlvo;
	public float tempo;
	public float intervaloAtual;
	public float intervaloMinimo;
	public float intervaloMaximo;
	public float paradaAtual;
	public float paradaMin;
	public float paradaMax;
	public Transform titulo;
	public Sprite perdeu;
	public bool ganhou = false;

	// Use this for initialization
	void Start () {
		etapa = EEtapa.Inicio;
		fimDeJogo = false;
		mudo = false;
		MessageKit.addObserver (Eventos.Saiu, QuandoSair);
		parada = 0;
		Time.timeScale = 0;
		paradaAlvo = Random.Range (3, 15);
		titulo.GetComponentInChildren<GUIText>().text = "Desça na " + paradaAlvo + "a parada.";
	}

	void Update () {

		// começa o jogo
		if (Input.anyKeyDown && etapa == EEtapa.Inicio) {
			print ("comecar o jogo");
			StartCoroutine ("Circular");
			MessageKit.post (Eventos.Comecou);
			Time.timeScale = 1;
			titulo.gameObject.SetActive(false);
		} 

		// terminou o jogo
		if (Input.anyKeyDown && fimDeJogo) {
			Application.LoadLevel(Application.loadedLevel);
		}

		// opcoes gerais durante o jogo
		// sair do jogo
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		// desligar / ligar o som
		if (Input.GetKeyDown(KeyCode.M)) {
			mudo = !mudo;
			AudioListener.volume = (mudo)? 0 : 1;
		}

		// logica sempre rolando
		if (etapa != EEtapa.Inicio)
			tempo += Time.deltaTime;
	}

	// fluxo principal do jogo
	IEnumerator Circular () {
		while (true) {
			// caminho ateh a parada
			print ("andar : " + Time.time);
			MessageKit.post (Eventos.Andar);
			etapa = EEtapa.Andando;
			tempo = 0;
			intervaloAtual = intervaloMinimo + Random.Range (intervaloMaximo - intervaloMinimo, intervaloMaximo);
			yield return new WaitForSeconds (intervaloAtual);
			// chegou na parada
			parada++;
			tempo = 0;
			print ("parar : " + Time.time);
			MessageKit.post (Eventos.Parar);
			etapa = EEtapa.Parando;
			paradaAtual = paradaMin + Random.Range (paradaMax - paradaMin, paradaMax);
			yield return new WaitForSeconds (paradaAtual);

		}
	}

	void QuandoSair () {
		print ("fim do jogo");
		fimDeJogo = true;
		etapa = EEtapa.Final;
		// se ganhou, reinicia, queria o que?
		titulo.gameObject.SetActive(true);
		MessageKit.post (Eventos.Acabou);
		if (parada == paradaAlvo) {
			titulo.GetComponent<SpriteRenderer>().color = new Color (1, 1, 0, 0.5f);
			titulo.GetComponentInChildren<GUIText>().text = "por Enric Llagostera.";
		}
		else if (parada < paradaAlvo) {
			titulo.GetComponent<SpriteRenderer>().sprite = perdeu;
			titulo.GetComponentInChildren<GUIText>().text = "A pressa é inimiga da perfeição.";
		}
		else {
			titulo.GetComponent<SpriteRenderer>().sprite = perdeu;
			titulo.GetComponentInChildren<GUIText>().text = "Quem espera nunca alcança.";
		}
	}
}
