using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using System.Collections.Generic;

public class PigScript : PickupsBase {

	public float timer = 9f;		//INFO - Times how long the effect lasts
	public float speed = 15;		//INFO - Varies fast the effect lerps

	public float goodTime = 5f;		//INFO - How long the good side of pig lasts
	public float badTime = 3f;		//INFO - How long bad side lasts

	public List<Sprite> pigSprite = new List<Sprite>(); //INFO - Pig sprite
	
	float minV = -70;				//INFO - Min Value for Vortex
	float maxV = 70;				//INFO - Max Value for Vortex

	float minA = -20;				//INFO - Min Value for Abberation
	float maxA = 20;				//INFO - Max Value for Abberation

	float _t = 0;				//INFO - System Timer for pig effect;
	float _t2 = 0;				//INFO - Timer for pig state

	bool pigEffect;			//INFO - Toggles effect

	Camera mainCam;			//INFO - Main Camera
	GameObject _player;		//INFO - Player
	SpriteRenderer spriteRend; //INFO - current obj's sprite renderer

	//Pig state
	public enum Pig
	{
		GOOD,
		BAD
	};
	public Pig pigState = Pig.GOOD;
	
	protected override void Start()
	{
		//base.Start();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_player = GameObject.Find("Player");
		spriteRend = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
		audioControl = GameObject.Find("AudioControl").GetComponent<Audio_Control>();
		moveSpeed = 0.05f;	
		Destroy(gameObject, timer + 15f);	
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		_t += Time.deltaTime;		//INFO - Adds time 
		_t2 += Time.deltaTime;
		
		switch (pigState)
		{
 			case Pig.GOOD:
				spriteRend.sprite = pigSprite[0];
				if(pigEffect)
				{
					Hallucination();
					spriteRend.color = new Color(1, 1, 1, 0);
				}
				else if (_t2 > goodTime + Random.Range(0,2))
				{
					pigState = Pig.BAD;
					_t2 = 0;
				}
				break;
			case Pig.BAD:
				spriteRend.sprite = pigSprite[1];
				if (pigEffect)
				{
					LoseAQuarter();
				}
				else if(_t2 > badTime+Random.Range(3,5))
				{
					pigState = Pig.GOOD;
					_t2 = 0;
				}
				break;
		}
		
	}
	void Hallucination()
	{
		//player.GetComponent<PlayerController>().pig = true;
		mainCam.GetComponent<Vortex>().angle = Mathf.PingPong(_t * speed, maxV - minV) + minV;
		mainCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.PingPong(_t * speed, maxA - minA) + minA;
		if (_t > timer)
		{
			//player.GetComponent<PlayerController>().pig = false;
			mainCam.GetComponent<Vortex>().angle = 0;
			mainCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0.2f;
			_t = 0;
			pigEffect = false;
		}
	}
	void LoseAQuarter()
	{
		float temp = _player.GetComponent<PlayerController>().Cash;
		_player.GetComponent<PlayerController>().Cash -= Mathf.RoundToInt(temp / 4);
		//_player.GetComponent<PlayerController>().Cash -= temp;
		Debug.Log(_player.GetComponent<PlayerController>().Cash);
		_t2 = 0;
		pigEffect = false;

	}
	protected override void PickupEffect()
	{
		//throw new System.NotImplementedException();
		audioControl.PigSound();
		pigEffect = true;
		_t=0;
		Instantiate(collectEffect, transform.position, Quaternion.identity);
	}
}
