using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class Pessoa : MonoBehaviour
{
	public PessoaInfo info;
	public float velocidade = 20f;
	public Transform seta;

	void Start () {
		info.x = Mathf.FloorToInt(transform.localPosition.x);
		info.z = Mathf.FloorToInt(transform.localPosition.z);
		Fase.mapa[info.x, info.z].pessoa = info;
	}

	// Update is called once per frame
	void Update ()
	{
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
			else {
				DebugGui.texto = "Tem gente saindo.";
				GerenteSom.i.audio.PlayOneShot(GerenteSom.i.descer);
				MessageKit.post (Eventos.Desceu);
				Destroy (gameObject);
				Fase.mapa[info.x, info.z].pessoa = PessoaInfo.nula;
				if (info.jogador) {
					print ("jogador saiu");
					MessageKit.post (Eventos.Saiu);
				}
			}


		}
	}
}

