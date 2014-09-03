using UnityEngine;
using System.Collections;

public class Bloco : MonoBehaviour {

	public BlocoInfo info;
	public bool snap = true;

	void Start () {
		info.x = Mathf.FloorToInt(transform.position.x);
		info.z = Mathf.FloorToInt(transform.position.z);
		Fase.mapa[info.x, info.z] = info;
		Fase.mapa[info.x, info.z].pessoa = PessoaInfo.nula;
	}

	void Update () {
		info.x = Mathf.FloorToInt(transform.position.x);
		info.z = Mathf.FloorToInt(transform.position.z);
		info = Fase.mapa[info.x, info.z];
		if (snap) {
			Vector3 alvo = new Vector3 (info.x, transform.position.y, info.z);
			transform.position = Vector3.Lerp (transform.position, alvo, Time.deltaTime);
		}
	}
}
