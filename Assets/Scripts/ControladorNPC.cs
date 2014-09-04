using UnityEngine;
using System.Collections;

public class ControladorNPC : MonoBehaviour
{
	private PessoaInfo _pessoa;
	public float intervaloMovimento;
	
	void Start () {
		intervaloMovimento = intervaloMovimento + intervaloMovimento * Random.Range(-0.5f, 0.5f);
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

