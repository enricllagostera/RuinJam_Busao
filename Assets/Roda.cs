using UnityEngine;
using System.Collections;

public class Roda : MonoBehaviour {

	public float velocidade;
	public bool parado = false;
	
	void Update () {
		if (!parado) transform.Rotate (0, velocidade * Time.deltaTime, 0);
	}
}
