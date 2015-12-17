using UnityEngine;
using System.Collections;

public class EventAnnoucer : MonoBehaviour 
{
	private Animator anim;

	public void Animate()
	{
		anim = GetComponent<Animator>();
		anim.SetTrigger("Event");
	}
}
