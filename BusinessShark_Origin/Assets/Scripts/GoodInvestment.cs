using UnityEngine;
using System.Collections;

public class GoodInvestment : PickupsBase {

	public bool vertical;

	float minH = -2.9f;
	float maxH = 4.3f;
	public float speed = 1.0f;

	//float magnitude = 5.0f;
	//float frequency = 20.0f;
	//Vector3 axis;
	//Vector3 pos;

	public enum MoveState
	{
		UP,
		DOWN
	};
	public MoveState VerticalState;

	float _t;

	bool toggle;
	protected override void Start()
	{
		base.Start();
		//pos = transform.position;
		//axis = Vector3.up;
	//	movable = true;
	//	amount = 5;
	}
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (vertical)
		{
			VerticalMove();
		}
		_t += Time.deltaTime;
	}
	
	protected override void PickupEffect()
	{
		//throw new System.NotImplementedException();
		//give player cash
		player.SendMessage("CalculateCash", amount, SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject, 0.2f);
		//Instantiate(shimmer, transform.position, Quaternion.identity);
	}
	void VerticalMove()
	{
		//pos += -transform.right * Time.deltaTime * speed;
		//transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
		switch (VerticalState)
		{ 
			case MoveState.DOWN:
				transform.Translate(-Vector3.up * Time.deltaTime * speed);
				if (transform.localPosition.y < minH)
				{
					VerticalState = MoveState.UP;
				}

				break;
			case MoveState.UP:
				transform.Translate(Vector3.up * Time.deltaTime * speed);
				if (transform.localPosition.y > maxH)
				{
					VerticalState = MoveState.DOWN;
				}

				break;
		}
	}
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		base.OnTriggerEnter2D(col);

		if(amount > 0)
		{
			audioControl.GoodInvestment();
		}
		else if(amount < 0)
		{
			audioControl.BadInvestment();
		}
	}
}
