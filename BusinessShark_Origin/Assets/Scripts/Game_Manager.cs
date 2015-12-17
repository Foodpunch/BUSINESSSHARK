using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	//Game Speed.
	public int baseGameSpeed;

	//Level Time.
	public float LevelTime;
	int cash;
	public int richLimit;		//limit for what is rich
	public int poorLimit;		//limit for what is poor
	Slider timeLine;
	Animator pauseMenu;
	Animator eventAnnouncer;
	Animator endGameAnim;
	bool gameEnd;
	public	bool gameStart;
	float _t;

	//Player Component
	GameObject player;

	//Pause Stuff
	public bool isPaused;

	//Event Time.
	public float minEventTime;
	public float maxEventTime;

	private float eventTime;

	//Event Season.
	public Pickup_Manager pickUp_Manager;
	public Sprite BearSprite;
	public Sprite BullSprite;
	Image eventIMG;

	//Mountain
	public GameObject[] mount_BG;
	public Material mount_Bull;
	public Material mount_Bear;

	//Sky
	public GameObject[] sky_BG;
	public Material sky_Bull;
	public Material sky_Bear;

	//Tree
	public GameObject[] tree_BG;
	public Material tree_Bull;
	public Material tree_Bear;

	//Ground 
	public GameObject[] ground_BG;

	//Event Season Transition.
	public Renderer[] lightningFlash;

	//Trail.
	public Material trail;

	//Audio Control
	private Audio_Control audioControl;

	public Text _text;
	public Image _loseShark;
	public Image _WinShark;
	public Text _Cash;

	// Use this for initialization
	void Start () 
	{
		//Start Game.
		gameStart = false;

		//Starting Cash.
		endGameAnim = GameObject.Find("EndGame").GetComponent<Animator>();

		//Player
		player = GameObject.FindGameObjectWithTag("Player");

		//Progress Bar In Level.
		timeLine = GameObject.FindGameObjectWithTag("Timeline").GetComponent<Slider>();
		timeLine.value = LevelTime;

		//Pause
		pauseMenu = GameObject.FindGameObjectWithTag("Pause").GetComponent<Animator>();

		//Season Annoucer.
		eventAnnouncer = GameObject.Find("EventAnnouncer").GetComponent<Animator>();
		eventIMG = GameObject.Find("EventAnnouncer").GetComponent<Image>();

		//Audio.
		audioControl = GameObject.Find("AudioControl").GetComponent<Audio_Control>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check Level time 
		
		CheckTime();
		CheckCash();

		//Pause stuff
		PauseLogic();

		//Lightning Color Transition Lerp.
		for (int i = 0; i < lightningFlash.Length; i++)
			lightningFlash[i].material.color = Color.Lerp(lightningFlash[i].material.color, Color.clear, Time.deltaTime);
	}
	void PauseLogic()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && gameStart)
		{
			isPaused = !isPaused;
		}
		if (isPaused)
		{
			Time.timeScale = 0;
			pauseMenu.SetBool("_pause", true);
			//pause menu transitions
		}
		if (!isPaused)
		{
			pauseMenu.SetBool("_pause", false);
			Time.timeScale = 1;
			//pause meny transitions
		}
	}
	void CheckTime()
	{
		if (gameStart)
		{
			timeLine.value -= Time.deltaTime;
			if (timeLine.value < 0)
			{
				timeLine.value = 0;
				gameEnd = true;
			}
		}
		
	}
	void CheckCash()
	{
		cash = player.GetComponent<PlayerController>().Cash;
		if (cash <= 0)
		{
			gameEnd = true;
			//Lose Screen plays
		}
		if (gameEnd)
		{
 		//GAE ENDED
			endGameAnim.SetBool("gameEnd", true);
			if (cash <= 0)
			{
				_WinShark.GetComponent<Image>().color = new Color(1, 1, 1, 0);
				_text.GetComponent<Text>().text = "Lose";
				_Cash.GetComponent<Text>().text = cash.ToString();
			}
			if(cash > 0)
			{
				_loseShark.GetComponent<Image>().color = new Color(1, 1, 1, 0);
				_text.GetComponent<Text>().text = "Win";
				_Cash.GetComponent<Text>().text = cash.ToString();
		
			}

		}
	}
	public IEnumerator BullBearSeason()
	{
		eventTime = Random.Range(minEventTime, maxEventTime);
		//Lightning Flash.
		for (int i = 0; i < lightningFlash.Length; i++)
			lightningFlash[i].material.SetColor("_Color", Color.white);

		if (gameStart)
		{
			audioControl.SeasonChange();
			Debug.Log("Season Change");
			//Check Season, BULL.
			if (pickUp_Manager.EventSeason == Pickup_Manager.EventState.Bear)
			{
				//Change Season.
				pickUp_Manager.EventSeason = Pickup_Manager.EventState.Bull;

				//Change Sprite
				eventIMG.sprite = BullSprite;
				//Play animation
				eventAnnouncer.SetTrigger("Event");

				//Change Trail Colour.
				trail.SetColor("_TintColor", Color.green);

				//Change Material.
				for (int i = 0; i < sky_BG.Length; i++)
				{
					mount_BG[i].GetComponent<Renderer>().material = mount_Bull;
					sky_BG[i].GetComponent<Renderer>().material = sky_Bull;
					tree_BG[i].GetComponent<Renderer>().material = tree_Bull;
				}

			}

			//Check Season, BEAR.
			else if (pickUp_Manager.EventSeason == Pickup_Manager.EventState.Bull)
			{
				//Change Season.
				pickUp_Manager.EventSeason = Pickup_Manager.EventState.Bear;

				//Change Sprite
				eventIMG.sprite = BearSprite;

				//Play Animation
				eventAnnouncer.SetTrigger("Event");

				//Change Trail Colour.
				trail.SetColor("_TintColor", Color.red);

				//Change Material
				for (int i = 0; i < sky_BG.Length; i++)
				{
					mount_BG[i].GetComponent<Renderer>().material = mount_Bear;
					sky_BG[i].GetComponent<Renderer>().material = sky_Bear;
					tree_BG[i].GetComponent<Renderer>().material = tree_Bear;
				}
			}
		} //End of gameStart.

		yield return new WaitForSeconds(eventTime);

		StartCoroutine("BullBearSeason");
	}
	public void PauseGame()
	{
		isPaused = !isPaused;
		
	}
	public void GameStart()
	{
		gameStart = true;
	}
}
