using UnityEngine;
using System.Collections;

public class ControladorPersonagem : MonoBehaviour {
	private PessoaInfo _pessoa;

	void Start () {
		_pessoa = GetComponent<Pessoa>().info;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Fase.Mover (_pessoa, EDirecao.Cima);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Fase.Mover (_pessoa, EDirecao.Baixo);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Fase.Mover (_pessoa, EDirecao.Esquerda);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Fase.Mover (_pessoa, EDirecao.Direita);
		}
	}
}
