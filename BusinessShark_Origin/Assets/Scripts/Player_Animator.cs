using UnityEngine;
using System.Collections;

public class Player_Animator : MonoBehaviour {

	private Animator anim;
	public int waitBreakTime;
	public int breakCycle;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		StartCoroutine("CycleBreak");
	}
	
	IEnumerator CycleBreak()
	{
		int temp = Random.Range(1, breakCycle);
		anim.SetInteger("Break", temp);

		yield return new WaitForSeconds(1f);

		temp = 0;
		anim.SetInteger("Break", temp);

		yield return new WaitForSeconds(waitBreakTime);

		StartCoroutine("CycleBreak");
	}
}
