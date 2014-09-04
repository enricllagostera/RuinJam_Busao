using UnityEngine;
using System.Collections;

public class ControladorNPC : MonoBehaviour
{
	private PessoaInfo _pessoa;
	public float intervaloMovimento;
	
	void Start () {
		_pessoa = GetComponent<Pessoa>().info;
		StartCoroutine("Mover");
	}
	
	IEnumerator Mover () {
		while (true) {
			yield return new WaitForSeconds (intervaloMovimento);
			Fase.Mover (_pessoa, _pessoa.direcao);
			_pessoa.direcao = Utils.DirecaoAleatoria();
		}
	}
}

