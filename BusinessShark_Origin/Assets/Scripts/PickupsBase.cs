using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class PickupsBase : MonoBehaviour {

	
	//Public Variables
	public bool movable;						//INFO - Determines whether object moves by code or animation
	 protected float moveSpeed;				//INFO - Move speed of pickup
	public Text itemToast;						//EDIT - Text for item that player has picked
	public string[] toastText;					//INFO - Toast Desc List
	public int amount;						//INFO - Amount of Cash item gives/takes

	public GameObject collectEffect;

	//Protected Variables
	protected GameObject player;				//INFO - Get component
	protected Game_Manager gameManager;			//INFO - Get component
	protected Audio_Control audioControl;

	//Private Variables
	

	//Abstract functions
	protected abstract void PickupEffect();		//INFO - Pickup function effects


	// Use this for initialization
	protected virtual void Start () 
	{
		Destroy(gameObject, 30f);
		player = GameObject.Find("Player");
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
		audioControl = GameObject.Find("AudioControl").GetComponent<Audio_Control>();
		moveSpeed = 0.05f;	
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		if (movable)
		{
			PickupMovement();
		}
	}
	void PickupMovement()
	{
		gameObject.transform.Translate(-Vector3.right * gameManager.baseGameSpeed * moveSpeed);
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			PickupEffect();
			SpawnToast();
		}
		//if (col.gameObject.transform.tag == "Bounds")
		//{
		//	Destroy(gameObject);
		//}
	}
	public void SpawnToast()
	{
		Text textClone = Instantiate(itemToast, GameObject.FindGameObjectWithTag("Canvas").transform.localPosition, Quaternion.identity) as Text;
		textClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
		textClone.text = toastText[Random.Range(0, toastText.Length-1)	];
	}
}
