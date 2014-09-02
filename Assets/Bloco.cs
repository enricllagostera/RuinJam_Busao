using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Bloco : MonoBehaviour {

	public BlocoInfo info;
	public bool snap = true;

	void Start () {
		Fase.mapa[info.x, info.z] = info;
		Fase.mapa[info.x, info.z].pessoa = Pessoa.nula;
	}

	void Update () {
		info.x = Mathf.FloorToInt(transform.position.x);
		info.z = Mathf.FloorToInt(transform.position.z);
		if (snap) {
			transform.position = new Vector3 (info.x, transform.position.y, info.z);
		}
	}
}
