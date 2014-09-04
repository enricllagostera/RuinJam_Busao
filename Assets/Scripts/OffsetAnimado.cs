using UnityEngine;
using System.Collections;

public class OffsetAnimado : MonoBehaviour {

	public float velocidade;

	void Update () {
		renderer.material.mainTextureOffset = new Vector2 (renderer.material.mainTextureOffset.x + Time.deltaTime * velocidade, 0);
	}
}
