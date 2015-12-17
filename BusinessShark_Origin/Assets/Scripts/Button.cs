using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject flying_Player;

	private GameObject driving_Player;
	private Rigidbody2D rigidBody_2D;
	public float waitTime;

	//Force for crash to work.
	public float crashForce;
	public float jumpForce;

	private Animator anim;
	private Pickup_Manager pickManager;
	private Game_Manager gameManager;
	private GameObject Menu_UI;

	private Audio_Control audioControl;

	public GameObject cliff;

	private bool groundDown = false;

	//Call at instantiation of this script.
	public void Start()
	{
		driving_Player = GameObject.Find("Player");
		anim = driving_Player.GetComponent<Animator>();
		rigidBody_2D = driving_Player.GetComponent<Rigidbody2D>();

		pickManager = GameObject.Find("Pickup Manager").GetComponent<Pickup_Manager>();
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
		Menu_UI = GameObject.Find("Menu_Title");

		//Audio.
		audioControl = GameObject.Find("AudioControl").GetComponent<Audio_Control>();
	}

	public void Begin()
	{
		anim.SetTrigger("Crash");
		pickManager.StartSpawn();
		Menu_UI.SetActive(false);
		StartCoroutine("FlyEffect");
	}

	public void Option()
	{
		Application.LoadLevel("Option");
	}

	public void Credits()
	{
		Application.LoadLevel("Credits");
	}

	public void BGM()
	{

	}
	
	IEnumerator FlyEffect()
	{
		//Play
		audioControl.audioSource3.Pause();
		audioControl.StartCoroutine("StartGame");
		yield return new WaitForSeconds(waitTime + 0.666f);

		groundDown = true;

		//Rigidbody Physics.
		rigidBody_2D.AddForce(Vector2.up * jumpForce * gameManager.baseGameSpeed, ForceMode2D.Impulse);
		rigidBody_2D.AddForce(Vector2.right * crashForce * gameManager.baseGameSpeed, ForceMode2D.Impulse);

		yield return new WaitForSeconds(waitTime - 1f);

		for (int i = 0; i < gameManager.tree_BG.Length; i++)
		{
			gameManager.tree_BG[i].SetActive(true);
		}
		
		//Start Game.			
		gameManager.StartCoroutine("BullBearSeason");
		gameManager.gameStart = true;
		audioControl.audioSource3.Play();
	}

	void Update()
	{
		if(groundDown)
		{
			for(int i = 0; i < gameManager.ground_BG.Length; i++)
			{
				Debug.Log("Ground Down");
				gameManager.ground_BG[i].transform.Translate(Vector3.down * Time.deltaTime * 2f, Space.World);
			}
		}
	}
}
