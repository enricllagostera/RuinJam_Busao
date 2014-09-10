using UnityEngine;
using System.Collections;

public class Bloco : MonoBehaviour {

	public BlocoInfo info;
	public bool snap = true;
	private Vector3 posicaoPai;

	void Start () {
		posicaoPai = transform.parent.position;
		info.x = Mathf.FloorToInt(transform.localPosition.x);
		info.z = Mathf.FloorToInt(transform.localPosition.z);
		Fase.mapa[info.x, info.z] = info;
		Fase.mapa[info.x, info.z].pessoa = new PessoaInfo ();
		if (snap) {
			Vector3 alvo = new Vector3 (posicaoPai.x + info.x, transform.position.y, posicaoPai.z + info.z);
			transform.position = alvo;
		}
	}
}
