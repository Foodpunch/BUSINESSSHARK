using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float force;
	[HideInInspector]
	public int Cash;
	public Text CashText;
	Image Mash;

	private Rigidbody2D rigidBody2D;
	private Game_Manager gameController;

	public bool chicken;
	public bool pig;

	public SpriteRenderer spriteRenderer;
	public float alpha_Color;

	private Game_Manager gameManager;

	void Start () 
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Manager>();
		alpha_Color = 0.4f;
		Mash = GameObject.Find("Mash").GetComponent<Image>();
		Cash = 500;
		//Buttons
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
	}
	
	void Update () 
	{
		//Update Cash Text.
		CashText.text = Cash + "";

		//Has game started?

		if (gameManager.gameStart)
		{
			//Player Input Controller.
			if (Input.GetKey(KeyCode.Space) && !gameController.isPaused)
			{
				//transform.Translate(Vector3.up * force * Time.deltaTime);
				rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Force);
				spriteRenderer = GetComponent<SpriteRenderer>();
			}
			//Chicken Mechanic.
			if (chicken)
			{
				alpha_Color += Time.deltaTime / 9f;
				spriteRenderer.color = new Color(1, 1, 1, alpha_Color);
				transform.gameObject.tag = "Untagged";
				Mash.color = new Color(1, 1, 1, 1);
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					alpha_Color += 0.04f;
					//spriteRenderer.color = new Color(1,1,1, alpha_Color);
				}
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					alpha_Color += 0.04f;
					//	spriteRenderer.color = new Color(1, 1, 1, alpha_Color);
				}

				if (spriteRenderer.color.a >= 0.98f)
				{
					chicken = false;
				}
			}
			else if (chicken == false)
			{
				transform.gameObject.tag = "Player";
				alpha_Color = 0.4f;
				Mash.color = new Color(1, 1, 1, 0);
			}
		} //End of Player Input Controller.
	}

	void FixedUpdate()
	{
		//Player X Movement.
		transform.Translate(Vector2.right * gameManager.baseGameSpeed  * Time.deltaTime);
	}

	public void CalculateCash(int amt)
	{
		if(pig)
		{
			if (amt > 0)
			{
				Cash += amt * 2;
			}
		}

		else
		//Cash Increase.
		Cash += amt;
	}
}
