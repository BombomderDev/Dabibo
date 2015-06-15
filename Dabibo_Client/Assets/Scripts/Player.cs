using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : AttackingObj
{
	private Transform targetEnemy;
	private Animator animator;

	// Use this for initialization
	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		targetEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GameMgr.instance.IsGameOver)
			return;

		#if UNITY_EDITOR || UNITY_WEBPLAYER
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			//AttemptAttack<Enemy>(targetEnemy);
		}
		#elif UNITY_ANDROID || UNITY_IPHONE
		foreach(Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				AttemptAttack<Enemy>(targetEnemy);
			}
		}
		#endif
	}

	protected override void AttemptAttack<T>(Transform target)
	{
		animator.SetTrigger("playerHit");
		base.AttemptAttack<T>(target);
	}
	
	protected override void OnCantAttack<T>(T component)
	{
		Enemy hitEnemy = component as Enemy;
		hitEnemy.DamageEnemy(GameMgr.instance.PlayerHitDamage);
	}

	public void Attack()
	{	
		AttemptAttack<Enemy>(targetEnemy);
	}

	public void ReSetEnemyTrans()
	{
		targetEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
	}
}
