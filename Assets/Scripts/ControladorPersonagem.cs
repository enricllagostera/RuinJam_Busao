using UnityEngine;
using System.Collections;

public class ControladorPersonagem : MonoBehaviour {
	private PessoaInfo _pessoa;

	void Start () {
		_pessoa = GetComponent<Pessoa>().info;
		_pessoa.jogador = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Fase.Mover (_pessoa, EDirecao.Cima);
			_pessoa.direcao = EDirecao.Cima;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Fase.Mover (_pessoa, EDirecao.Baixo);
			_pessoa.direcao = EDirecao.Baixo;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Fase.Mover (_pessoa, EDirecao.Esquerda);
			_pessoa.direcao = EDirecao.Esquerda;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Fase.Mover (_pessoa, EDirecao.Direita);
			_pessoa.direcao = EDirecao.Direita;
		}

		if (Fase.mapa[_pessoa.x, _pessoa.z].tipo == ETipo.Cadeira) {
			_pessoa.direcao = Fase.mapa[_pessoa.x, _pessoa.z].direcao;
		}
	}
}
