using UnityEngine;
using System.Collections;

public class Pickup_Manager : MonoBehaviour 
{
	//Player
	public GameObject player;

	//Spawn Timing.
	public float pickUp_Timer;
	public float chunks_Timer;

	//Spawn Position.
	private float ini_XOffset; //Initial X Offset Position.

	public float min_XPos; //Min X Position.
	public float max_XPos; //Max X Position.

	public float min_YPos; //Min Y Position.
	public float max_YPos; //Max Y Position.

	private float xPos;	//Final X.
	private float yPos; //Final Y.

	private Vector2 spawnPosition; //Final Spawn Position.

	//Data controllin Spawning.
	public int chances;
	private int finalChances;

	//Spawn Object Manager Data.
	public GameObject[] spawnPickUps;
	public GameObject[] spawnBearChunks;
	public GameObject[] spawnBullChunks;

	//Types of Event.
	public enum EventState
	{
		Bull,
		Bear
	};

	public EventState EventSeason = EventState.Bull;

	public void StartSpawn()
	{
		//Start the different Spawner.
		StartCoroutine("SpawnPickUp");
		StartCoroutine("SpawnChunks");

		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		//Move the Spawner.
		transform.Translate(Vector3.right * Time.deltaTime);
	}

	IEnumerator SpawnPickUp()
	{
		yield return new WaitForSeconds(pickUp_Timer);

		//Data setting the Spawn Position & Spawn Object.
		finalChances = Random.Range(0, chances); //Max Range - Exclusive.

		//Randomising the spawning Position. (Controlled through Inspector)
		xPos = Random.Range(min_XPos, max_XPos);
		yPos = Random.Range(min_YPos, max_YPos);

		//Update the offset of the spawnManager between the player.
		ini_XOffset = transform.position.x;

		spawnPosition = new Vector2(xPos + ini_XOffset, yPos); //Final Spawn Positon.
		//	Debug.Log(ini_XOffset);		

		//Spawn the PickUps.
		Instantiate(spawnPickUps[finalChances], spawnPosition, Quaternion.identity);

		StartCoroutine("SpawnPickUp");
	}

	IEnumerator SpawnChunks()
	{

		yield return new WaitForSeconds(chunks_Timer);

		switch (EventSeason)
		{
			case EventState.Bear:
				finalChances = Random.Range(0, spawnBearChunks.Length); //Max Range - Exclusive.

				//Randomising the spawn Position. (Controlled through Inspector)
				xPos = Random.Range(min_XPos, max_XPos);

				//Update the offset of the spawnManager between the player.
				ini_XOffset = transform.position.x;

				spawnPosition = new Vector2(ini_XOffset, transform.position.y);//Final Spawn Position.

				//Spawn the Chunks.
				Instantiate(spawnBearChunks[finalChances], spawnPosition, Quaternion.identity);
				break;

			case EventState.Bull:
				finalChances = Random.Range(0, spawnBullChunks.Length); //Max Range - Exclusive.

				//Randomising the spawn Position. (Controlled through Inspector)
				xPos = Random.Range(min_XPos, max_XPos);

				//Update the offset of the spawnManager between the player.
				ini_XOffset = transform.position.x;

				spawnPosition = new Vector2(ini_XOffset, transform.position.y);//Final Spawn Position.

				//Spawn the Chunks.
				Instantiate(spawnBullChunks[finalChances], spawnPosition, Quaternion.identity);
				break;
		}
		StartCoroutine("SpawnChunks");
	}
}
