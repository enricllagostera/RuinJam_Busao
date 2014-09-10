using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class Pessoa : MonoBehaviour
{
	public PessoaInfo info;
	public float velocidade = 20f;
	public Transform seta;
	public bool desceu = false;
	public Vector3 dirFinal;

	void Start () {
		info.parada = 10;
		info.x = Mathf.FloorToInt(transform.localPosition.x);
		info.z = Mathf.FloorToInt(transform.localPosition.z);
		Fase.mapa[info.x, info.z].pessoa = info;
		desceu = false;
	}

	// Update is called once per frame
	void Update ()
	{

		if (desceu) {
			transform.position = transform.position + dirFinal.normalized * 0.1f;
			return;
		}


		Vector3 alvo = new Vector3 (info.x, transform.localPosition.y, info.z);
		transform.localPosition = Vector3.Lerp (transform.localPosition, alvo, Time.deltaTime * velocidade);

		switch (info.direcao) {
			case EDirecao.Direita : 
				transform.localEulerAngles = new Vector3(0, 90, 0); 
				break;
			case EDirecao.Esquerda : 
				transform.localEulerAngles = new Vector3(0, -90, 0); 
				break;
			case EDirecao.Cima : 
				transform.localEulerAngles = new Vector3(0, 0, 0); 
				break;
			case EDirecao.Baixo : 
				transform.localEulerAngles = new Vector3(0, 180, 0); 
				break;
		}
		//Quaternion rotacao = seta.rotation;
		if (Fase.mapa[info.x, info.z].tipo == ETipo.Cadeira) {
			transform.localEulerAngles = new Vector3(0, 90, 0);
			//transform.localScale = new Vector3 (1, 0.9f, 1);
		}
		else {
			//transform.localScale = Vector3.one;
		}

		if (Fase.mapa[info.x, info.z].tipo == ETipo.Porta && Jogo.etapa == EEtapa.Parando) {
			if (info.jogador == false && info.x > 7) {
				DebugGui.texto = "Mais gente!";
			}
			else if (info.jogador == true && info.x > 7) {
				// nada
			}
			else if (!desceu) {
				StartCoroutine ("Destruir");
			}


		}
	}

	IEnumerator Destruir () {
		desceu = true;
		info.parada = -1;
		DebugGui.texto = "Tem gente saindo.";
		GerenteSom.i.audio.PlayOneShot(GerenteSom.i.descer);
		MessageKit.post (Eventos.Desceu);
		if (info.jogador) {
			print ("jogador saiu");
			MessageKit.post (Eventos.Saiu);
		}
		print (Fase.mapa[info.x, info.z].direcao.ToString());
		switch (Fase.mapa[info.x, info.z].direcao) {
			case EDirecao.Direita : 
				dirFinal = Vector3.right; 
				break;
			case EDirecao.Esquerda : 
				dirFinal = -Vector3.right; 
				break;
			case EDirecao.Cima : 
				dirFinal = Vector3.forward; 
				break;
			case EDirecao.Baixo : 
				dirFinal = -Vector3.forward; 
				break;
		}
		print (dirFinal);
		Fase.mapa[info.x, info.z].pessoa = new PessoaInfo();
		info = new PessoaInfo ();
		yield return new WaitForSeconds (4f);
		Destroy (gameObject);
	}
}

