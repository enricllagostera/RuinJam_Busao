using UnityEngine;
using System.Collections;

public static class Utils {
	public static EDirecao DirecaoAleatoria () {
		int rand = Random.Range (0, 3);
		switch (rand) {
		case 0 : return EDirecao.Baixo; break;
		case 1 : return EDirecao.Cima; break;
		case 2 : return EDirecao.Esquerda; break;
		case 3 : return EDirecao.Direita; break;
		}
		return EDirecao.Direita;
	}
}

public static class Eventos {
	public const int Comecou = 10;
	public const int Andar = 0;
	public const int Parar = 1;
	public const int Saiu = 2;
	public const int Desceu = 3;
	public const int Acabou = 20;
}


#region Infos
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
		pessoa = PessoaInfo.nula;
	}
}

[System.Serializable]
public class PessoaInfo {
	public bool jogador;
	public int parada;
	public int x;
	public int z;
	public bool bilhete = true;
	public EDirecao direcao;
	
	public PessoaInfo () {
		parada = -1;
		x = -1;
		z = -1;
		bilhete = false;
		direcao = EDirecao.Direita;
		jogador = false;
	}
	
	public static PessoaInfo nula = new PessoaInfo();
}
#endregion

#region Enums
public enum EEtapa {
	Inicio,
	Andando,
	Parando,
	Final
}

public enum ETipo {
	Vazio,
	Parede,
	Cadeira,
	Porta,
	Catraca
}

public enum EDirecao {
	Cima = 1,
	Direita = 2,
	Baixo = -1,
	Esquerda = -2
}
#endregion

