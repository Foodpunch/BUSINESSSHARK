using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChickenScript : PickupsBase {


	protected override void Start()
	{
		//base.Start();
		Destroy(gameObject, 45f);
		player = GameObject.Find("Player");
		audioControl = GameObject.Find("AudioControl").GetComponent<Audio_Control>();
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
		moveSpeed = 0.05f;	
	}

	protected override void PickupEffect()
	{
		//throw new System.NotImplementedException();
		player.GetComponent<PlayerController>().chicken = true;
		audioControl.ChickenSound();
		Instantiate(collectEffect, transform.position, Quaternion.identity);

	//	player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
	}

	//protected override void OnTriggerEnter2D(Collider2D col)
	//{
	//	base.OnTriggerEnter2D(col);
	
	//}
	
}
