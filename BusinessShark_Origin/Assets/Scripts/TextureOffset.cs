using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour {

	public Renderer renderer;
	public Vector2 textureOffset;

	public string textureName;

	private Game_Manager gameManager;
	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent<Renderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
	}
	// Update is called once per frame
	void Update () 
	{
		//Move the Background.
		transform.Translate(Vector2.left * Time.deltaTime);

		//Move the texture.
		renderer.material.SetTextureOffset(textureName, textureOffset * gameManager.baseGameSpeed * Time.time);
	}
}
