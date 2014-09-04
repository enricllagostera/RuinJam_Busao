using UnityEngine;
using System.Collections;

public class Seta : MonoBehaviour {

	private Vector3 offset;
	public Pessoa pessoa;

	void Start () {
		offset = transform.position - pessoa.transform.position;
	}

	void Update () {
		transform.position = pessoa.transform.position + offset;
		switch (pessoa.info.direcao) {
		case EDirecao.Direita : 
			transform.localEulerAngles = new Vector3(90, 90, 0); 
			break;
		case EDirecao.Esquerda : 
			transform.localEulerAngles = new Vector3(90, -90, 0); 
			break;
		case EDirecao.Cima : 
			transform.localEulerAngles = new Vector3(90, 0, 0); 
			break;
		case EDirecao.Baixo : 
			transform.localEulerAngles = new Vector3(90, 180, 0); 
			break;
		}
	}
}
