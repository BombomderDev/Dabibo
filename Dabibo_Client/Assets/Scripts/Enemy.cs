using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : AttackingObj
{
	private int nowHp = 10;
	private Animator animator;
	public GameObject loseHpEffectObj;
	public GameObject gainMoneyEffectObj;

	// Use this for initialization
	protected override void Start ()
	{
		nowHp = GameMgr.instance.EnemyMaxHp;
		animator = GetComponent<Animator>();
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	}

	protected override void AttemptAttack<T>(Transform target)
	{		
		base.AttemptAttack<T>(target);
	}
	
	protected override void OnCantAttack<T>(T component)
	{

	}

	public void DamageEnemy(int loss)
	{
		nowHp -= loss;
		GameMgr.instance.ChangeEnemyHpText(nowHp);
		animator.SetTrigger("enemyChop");
		GameObject effectObj = Instantiate(loseHpEffectObj, Vector3.zero, Quaternion.identity) as GameObject;
		effectObj.transform.parent = GameMgr.instance.UICanvasObj.transform;
		effectObj.transform.localPosition = Vector3.zero;
		effectObj.transform.localScale = Vector3.one;
		effectObj.transform.Find("LoseHp").GetComponent<Text>().text = "-"+GameMgr.instance.PlayerHitDamage;
		effectObj.AddComponent<DestroyAtTime>().time = 0.8f;
		CheckIfDied();
	}

	private void CheckIfDied()
	{
		if(nowHp <= 0)
		{
			int gainMoney = GetMonsterMoney();
			GameMgr.instance.AddMoney(gainMoney);
			GameObject effectObj = Instantiate(gainMoneyEffectObj, Vector3.zero, Quaternion.identity) as GameObject;
			effectObj.transform.parent = GameMgr.instance.UICanvasObj.transform;
			effectObj.transform.localScale = Vector3.one;
			effectObj.transform.localPosition = Vector3.zero;
			effectObj.transform.Find("GainMoney").GetComponent<Text>().text = "+" + gainMoney + " Money";
			effectObj.AddComponent<DestroyAtTime>().time = 0.8f;
			//Application.LoadLevel(Application.loadedLevel);
			Destroy(this.gameObject);
			GameMgr.instance.EnemyKillCount ++;

			if(GameMgr.instance.EnemyKillCount >= 2)
			{
				GameMgr.instance.ShowGameOver();
			}
		}
	}

	public int GetMonsterMoney()
	{
		return GameMgr.instance.NowStage;
	}
}
