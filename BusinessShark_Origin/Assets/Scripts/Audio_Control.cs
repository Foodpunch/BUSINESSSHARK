using UnityEngine;
using System.Collections;

public class Audio_Control : MonoBehaviour
{

	//public AudioClip[] audioSound;
	public AudioClip crash;
	public AudioClip yell;
	public AudioClip bearRoar;
	public AudioClip bullRoar;
	public AudioClip Stock;
	public AudioClip Chicken;
	public AudioClip Pig;
	public AudioClip SharkBusinessMan;

	private AudioSource[] audioSourceMain;
	private Pickup_Manager pickUpManager;

	public AudioSource audioSource0;
	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public AudioSource audioSource3;

	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(transform.gameObject);
		//Audio Source.
		audioSourceMain = GetComponents<AudioSource>(); 
		audioSource0 = audioSourceMain[0];
		audioSource1 = audioSourceMain[1];
		audioSource2 = audioSourceMain[2];
		audioSource3 = audioSourceMain[3];

		audioSource3.clip = SharkBusinessMan;

		//Season State.
		pickUpManager = GameObject.Find("Pickup Manager").GetComponent<Pickup_Manager>();
	}

	public IEnumerator StartGame()
	{
		audioSource0.clip = crash;
		audioSource0.PlayDelayed(1f);

		yield return new WaitForSeconds(2f);

		Debug.Log("Yell");
		audioSource0.clip = yell;
		audioSource0.Play();
	}

	public void SeasonChange()
	{
		if(pickUpManager.EventSeason == Pickup_Manager.EventState.Bear)
		{
			audioSource1.clip = bearRoar;
			audioSource1.Play();
		}
		else if(pickUpManager.EventSeason == Pickup_Manager.EventState.Bull)
		{
			audioSource1.clip = bullRoar;
			audioSource1.Play();
		}
	}

	public void GoodInvestment()
	{
		audioSource2.clip = Stock;
		audioSource2.Play();
	}
	public void BadInvestment()
	{
		audioSource2.clip = Stock;
		audioSource2.Play();
	}

	public void PigSound()
	{
		audioSource2.clip = Pig;
		audioSource2.Play();
	}

	public void ChickenSound()
	{
		audioSource2.clip = Chicken;
		audioSource2.Play();
	}
}
