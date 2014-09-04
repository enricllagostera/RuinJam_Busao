using UnityEngine;
using System.Collections;

public class Pessoa : MonoBehaviour
{
	public PessoaInfo info;
	public float velocidade = 20f;
	private Vector3 posicaoPai;

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
			case EDirecao.Direita : transform.localEulerAngles = new Vector3(0, 90, 0); break;
			case EDirecao.Esquerda : transform.localEulerAngles = new Vector3(0, -90, 0); break;
			case EDirecao.Cima : transform.localEulerAngles = new Vector3(0, 0, 0); break;
			case EDirecao.Baixo : transform.localEulerAngles = new Vector3(0, 180, 0); break;
		}

		if (Fase.mapa[info.x, info.z].tipo == ETipo.Cadeira) {
			transform.localEulerAngles = new Vector3(0, 90, 0);
		}
	}
}

