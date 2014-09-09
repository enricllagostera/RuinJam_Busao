using UnityEngine;
using System.Collections;

public class PortaEntrada : MonoBehaviour {

	public float intervalo;
	public float timer;
	private BlocoInfo _info;
	public Transform pai;
	public Transform prefabPessoa;

	// Use this for initialization
	void Start () {
		_info = GetComponent<Bloco>().info;
	}
	
	// Update is called once per frame
	void Update () {
		if (Jogo.etapa == EEtapa.Parando) {
			timer += Time.deltaTime;
			if (timer > intervalo) {
				if (_info.pessoa == PessoaInfo.nula) {
					Transform novo = Instantiate (prefabPessoa, transform.position + new Vector3(0.1f, 0, 0.1f), Quaternion.identity) as Transform;
					novo.parent = pai;
					novo.GetComponent<Pessoa>().info.direcao = EDirecao.Cima;
					GerenteSom.i.audio.PlayOneShot(GerenteSom.i.subir);
				}
				timer = 0;
			}
		}
	}
}
