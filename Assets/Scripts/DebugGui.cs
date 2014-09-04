using UnityEngine;
using System.Collections;

public class DebugGui : MonoBehaviour {

	public static string texto;

	private GUIText _guiText;

	void Start () {
		_guiText = GetComponent<GUIText>();
	}

	void Update () {
		_guiText.text = texto;
	}
}
