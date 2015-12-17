using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToastLogic : MonoBehaviour {
	float alphaLerp;
	float timer;

	int fadeTime = 5;
	 float moveTime = 0.5f;
	// Use this for initialization
	void Start () {
		alphaLerp = 1f;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		alphaLerp = Mathf.Lerp(1, 0, timer);
		timer += Time.deltaTime/fadeTime;
		gameObject.transform.Translate(Vector3.up * Time.deltaTime / moveTime,Space.Self);
		gameObject.GetComponent<Text>().color = new Color(gameObject.GetComponent<Text>().color.r, gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b, alphaLerp);
		Destroy(gameObject, 5f);
		
	}
}
