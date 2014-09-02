using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BlocoInfo {
	public ETipo tipo;
	public EDirecao direcao;
	public int x;
	public int z;
	public PessoaInfo pessoa;
	public BlocoInfo (int pX, int pZ) {
		x = pX;
		z = pZ;
		tipo = ETipo.Vazio;
		direcao = EDirecao.Direita;
		pessoa = null;
	}
}

public enum ETipo {
	Vazio,
	Parede,
	Cadeira,
	Porta
}

public enum EDirecao {
	Cima = 1,
	Direita = 2,
	Baixo = -1,
	Esquerda = -2
}

public class Fase : MonoBehaviour {

	public static BlocoInfo[,] mapa;
	public List<BlocoInfo> blocos;

	public int largura;
	public int altura;
	public float tamanhoBloco;

	// Use this for initialization
	void Awake () {
		mapa = new BlocoInfo[largura, altura];
		for (int i = 0; i < mapa.GetLength(0); i++) {
			for (int j = 0; j < mapa.GetLength(1); j++) {
				mapa[i, j] = new BlocoInfo (i, j);
			}
		}
	}
	
	// Debug
	/*
	void OnDrawGizmos () {
		for (int i = 0; i < mapa.GetLength(0); i++) {
			for (int j = 0; j < mapa.GetLength(1); j++) {
				Vector3 pos = transform.position + new Vector3 (i * tamanhoBloco, 0, j * tamanhoBloco);
				if (mapa[i, j].tipo == ETipo.Vazio) {
					Gizmos.color = Color.blue;
				}
				else if (mapa[i, j].tipo == ETipo.Parede) {
					Gizmos.color = Color.red;
				}
				else if (mapa[i, j].tipo == ETipo.Cadeira) {
					Gizmos.color = Color.yellow;
				}
				Gizmos.DrawCube (pos, Vector3.one * 0.5f);
			}
		}
	}
	*/

	#region Statics
	public static bool Permitido (BlocoInfo origem, EDirecao dirAlvo) {

		EDirecao dirOrigem = (EDirecao) ((int) dirAlvo * -1);

		// checa se alvo existe
		if (!Valido (origem, dirAlvo)) {
			print ("permitido: invalido");
			return false;
		}
		BlocoInfo alvo = Vizinho (origem, dirAlvo);

		if (origem.tipo == ETipo.Cadeira && (Mathf.Abs ((int) dirAlvo) == Mathf.Abs ((int) origem.direcao))){
			return false;
		}

		// dependendo do tipo do alvo
		switch (alvo.tipo) {
		case ETipo.Parede : print ("permitido: parede"); return false;
		case ETipo.Vazio : print ("permitido: vazio"); return true; // a melhorar
		case ETipo.Porta : 
			if (alvo.pessoa == PessoaInfo.nula) {
				print ("permitido: porta vazia");
				return true;
			}
			print ("permitido: porta cheia");
			return false;
		case ETipo.Cadeira : 
			// nao tem ninguem na cadeira
			if (alvo.pessoa == PessoaInfo.nula) {
				if ( Mathf.Abs ((int) dirOrigem) == Mathf.Abs ((int) alvo.direcao) ) {
					print ("cadeira vazia, direcao ruim");
					return false;
				} 
				else {
					print ("cadeira vazia, direcao ok");
					return true;
				}
			}
			// tem alguem na cadeira
			else {
				print ("tem alguem na cadeira");
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
		BlocoInfo blocoOrigem = mapa[pessoa.x, pessoa.z];
		BlocoInfo blocoAlvo = Vizinho (blocoOrigem, direcao);
		if (blocoAlvo != null) {
			if (Permitido (blocoOrigem, direcao)) {
				blocoAlvo.pessoa = pessoa;
				blocoOrigem.pessoa = PessoaInfo.nula;
				pessoa.x = blocoAlvo.x;
				pessoa.z = blocoAlvo.z;
				return true;
			}
			print ("nao permitido");
		}
		print ("alvo nulo");
		return false;
	}
	#endregion
}
