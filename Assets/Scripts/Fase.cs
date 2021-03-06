﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31.MessageKit;

public class Fase : MonoBehaviour {

	public static BlocoInfo[,] mapa;
	public List<BlocoInfo> blocos;

	public int largura;
	public int altura;
	public float tamanhoBloco;

	private Animator _anim;

	// Use this for initialization
	void Awake () {
		mapa = new BlocoInfo[largura, altura];
		for (int i = 0; i < mapa.GetLength(0); i++) {
			for (int j = 0; j < mapa.GetLength(1); j++) {
				mapa[i, j] = new BlocoInfo (i, j);
			}
		}
		_anim = GetComponent<Animator>();
	}

	void Start () {
		// controle de animacao
		MessageKit.addObserver (Eventos.Andar, QuandoAndar);
		MessageKit.addObserver (Eventos.Parar, QuandoParar);
	}

	void QuandoAndar () {
		_anim.SetTrigger("Andando");
	}

	void QuandoParar () {
		_anim.SetTrigger("Parando");
	}

	void OnDestroy () {
		MessageKit.clearMessageTable();
	}


	#region Statics
	public static bool Permitido (BlocoInfo origem, EDirecao dirAlvo) {

		EDirecao dirOrigem = (EDirecao) ((int) dirAlvo * -1);

		// checa se alvo existe
		if (!Valido (origem, dirAlvo)) {
			//print ("permitido: invalido");
			return false;
		}
		BlocoInfo alvo = Vizinho (origem, dirAlvo);

		if (!PessoaInfo.Vazia (alvo.pessoa)) {
			if (Mover (alvo.pessoa, dirAlvo)) {
				GerenteSom.i.audio.PlayOneShot (GerenteSom.i.empurrao);
				DebugGui.texto = "Licença.";
			}
			else {
				DebugGui.texto = "Parou, parou!";
				return false;
			}
		}

		// caso especial: sair de cadeira pelos lados errados
		if (origem.tipo == ETipo.Cadeira && (Mathf.Abs ((int) dirAlvo) == Mathf.Abs ((int) origem.direcao))){
			return false;
		}

		// caso especial: voltar na catraca
		if (alvo.tipo == ETipo.Catraca && dirOrigem == EDirecao.Esquerda) {
			DebugGui.texto = "Pagou, não volta mais.";
			return false;
		}

		// caso especial: tentando passar na catraca
		if (alvo.tipo == ETipo.Catraca && dirOrigem == EDirecao.Direita) {
			// se tem bilhete
			if (origem.pessoa.bilhete) {
				DebugGui.texto = "Pagou a passagem";
				origem.pessoa.bilhete = false;
				return true;
			}
			else {
				DebugGui.texto = "Voce tah sem bilhete";
				return false;
			}
		}
		// saindo da catraca
		if (origem.tipo == ETipo.Catraca) {
			if (dirAlvo != EDirecao.Esquerda) {
				DebugGui.texto = "pagou tem q passar";
				return false;
			}
			else {
				DebugGui.texto = "passou";
				return true;
			}
		}

		// dependendo do tipo do alvo
		switch (alvo.tipo) {
		case ETipo.Parede : //print ("permitido: parede"); 
			return false;
		case ETipo.Vazio : //print ("permitido: vazio"); 
			return true; // a melhorar
		case ETipo.Porta : 
			if (PessoaInfo.Vazia (alvo.pessoa)) {
				//print ("permitido: porta vazia");
				return true;
			}
			//print ("permitido: porta cheia");
			return false;
		case ETipo.Cadeira : 
			// nao tem ninguem na cadeira
			if (PessoaInfo.Vazia (alvo.pessoa)) {
				if ( Mathf.Abs ((int) dirOrigem) == Mathf.Abs ((int) alvo.direcao) ) {
					//print ("cadeira vazia, direcao ruim");
					return false;
				} 
				else {
					//print ("cadeira vazia, direcao ok");
					return true;
				}
			}
			// tem alguem na cadeira
			else {
				//print ("tem alguem na cadeira");
				// tem espaco pra empurrar
				/*
				if (Permitido (Vizinho (alvo, dirAproximacao), dirAproximacao)) {
					// tem que empurrar essa pessoa
					return true;
				}
				else {
					return false;
				}
				*/
			}
			break;
		}
		return false;
	}

	public static bool Valido (int x, int z) {
		if (x >= mapa.GetLength (0) || x < 0 || z >= mapa.GetLength (1) || z < 0) {
			return false;
		}
		return true;
	}

	public static bool Valido (BlocoInfo bloco, EDirecao direcao) {
		switch (direcao) {
		case EDirecao.Cima : return Valido (bloco.x, bloco.z+1);
		case EDirecao.Baixo : return Valido (bloco.x, bloco.z-1);
		case EDirecao.Direita : return Valido (bloco.x+1, bloco.z);
		case EDirecao.Esquerda : return Valido (bloco.x-1, bloco.z);
		}
		return false;
	}

	public static BlocoInfo Vizinho (BlocoInfo bloco, EDirecao direcao) {
		switch (direcao) {
		case EDirecao.Cima : if (Valido (bloco.x, bloco.z+1)) 
			return mapa [bloco.x, bloco.z+1]; 
			break;
		case EDirecao.Baixo : if (Valido (bloco.x, bloco.z-1)) return mapa [bloco.x, bloco.z-1]; break;
		case EDirecao.Direita : if (Valido (bloco.x+1, bloco.z)) return mapa [bloco.x+1, bloco.z]; break;
		case EDirecao.Esquerda : if (Valido (bloco.x-1, bloco.z)) return mapa [bloco.x-1, bloco.z]; break;
		}
		return null;
	}

	public static bool Mover (PessoaInfo pessoa, EDirecao direcao) {
		if (PessoaInfo.Vazia (pessoa)) {
			return false;
		}
		BlocoInfo blocoOrigem = mapa[pessoa.x, pessoa.z];
		BlocoInfo blocoAlvo = Vizinho (blocoOrigem, direcao);
		if (blocoAlvo != null) {
			if (Permitido (blocoOrigem, direcao)) {
				blocoAlvo.pessoa = pessoa;
				blocoOrigem.pessoa = new PessoaInfo ();
				pessoa.x = blocoAlvo.x;
				pessoa.z = blocoAlvo.z;

				// debug pessoa moveu
				if (pessoa.jogador) GerenteSom.i.audio.PlayOneShot(GerenteSom.i.mover);

				return true;
			}
			//print ("nao permitido");
		}
		//print ("alvo nulo");
		return false;
	}
	#endregion
}
