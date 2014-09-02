using UnityEngine;
using System.Collections;

public class ControladorPersonagem : MonoBehaviour {
	public Pessoa pessoa;
	private Fase _fase;
	public bool bilhete = true;

	void Start () {
		_fase = GameObject.Find("Fase").GetComponent<Fase>();
	}
	
	// Update is called once per frame
	void Update () {
		pessoa.x = Mathf.FloorToInt(transform.position.x);
		pessoa.z = Mathf.FloorToInt(transform.position.z);

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Fase.Mover (pessoa, EDirecao.Cima);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Fase.Mover (pessoa, EDirecao.Baixo);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Fase.Mover (pessoa, EDirecao.Esquerda);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Fase.Mover (pessoa, EDirecao.Direita);
		}

		transform.position = new Vector3 (pessoa.x, transform.position.y, pessoa.z);
	}
}
