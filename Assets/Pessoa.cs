using UnityEngine;
using System.Collections;

public class Pessoa : MonoBehaviour
{
	public PessoaInfo info;
	public float velocidade = 20f;
	
	void Start () {
		info.x = Mathf.FloorToInt(transform.position.x);
		info.z = Mathf.FloorToInt(transform.position.z);
		Fase.mapa[info.x, info.z].pessoa = info;
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 alvo = new Vector3 (info.x, transform.position.y, info.z);
		transform.position = Vector3.Lerp (transform.position, alvo, Time.deltaTime * velocidade);
	}
}

