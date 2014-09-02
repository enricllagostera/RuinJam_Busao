using UnityEngine;
using System.Collections;

[System.Serializable]
public class Pessoa {
	public int parada;
	public int x;
	public int z;
	public Pessoa () {
		parada = -1;
		x = -1;
		z = -1;
	}

	public static Pessoa nula = new Pessoa();
}

