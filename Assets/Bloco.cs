using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Bloco : MonoBehaviour {

	public ETipo tipo;
	public int x;
	public int z;
	public bool snap = true;

	void Start () {
		Fase.mapa[x, z].tipo = tipo;
		Fase.mapa[x, z].pessoa = null;
	}

	void Update () {
		x = Mathf.FloorToInt(transform.position.x);
		z = Mathf.FloorToInt(transform.position.z);
		if (snap) {
			transform.position = new Vector3 (x, transform.position.y, z);
		}
	}
}
