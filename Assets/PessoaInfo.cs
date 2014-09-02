using UnityEngine;
using System.Collections;

[System.Serializable]
public class PessoaInfo {
	public int parada;
	public int x;
	public int z;
	public bool bilhete = true;

	public PessoaInfo () {
		parada = -1;
		x = -1;
		z = -1;
	}

	public static PessoaInfo nula = new PessoaInfo();
}

