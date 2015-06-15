using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour
{
	public Vector2 enemyPos = new Vector2(0,0);
	public GameObject[] enemyTiles;
	public float enemyMovespeed = 0.0f;
	private Transform enemyTrans;

	private Transform sceneHolder;

	void LayoutEnemyAtRandom(GameObject[] tileArray)
	{
		GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
		GameObject enemyObj = Instantiate(tileChoice, enemyPos, Quaternion.identity) as GameObject;
		enemyTrans = enemyObj.transform;
		enemyMovespeed = 0.0f;
	}

	private void SceneSetUp()
	{
		sceneHolder = new GameObject("Scene").transform;
		// TODO 建立場景
	}

	public void SetUpScene()
	{
		SceneSetUp();
		LayoutEnemyAtRandom(enemyTiles);
	}

	// Update is called once per frame
	void Update () {
		if(enemyTrans != null)
		{
			if(enemyTrans.position.x <= 0.5f)
			{
				enemyMovespeed = 0.0f;
				GameMgr.instance.StartFight();
			}
			else
			{
				enemyTrans.position = new Vector3 (enemyTrans.position.x - enemyMovespeed * Time.deltaTime,enemyTrans.position.y,enemyTrans.position.z);
			}
		}
		else
		{
			if(!GameMgr.instance.IsGameOver)
			{
				LayoutEnemyAtRandom(enemyTiles);
				GameMgr.instance.ReStartGame();
			}
		}
	}

	public void SetUpEmemy()
	{
		enemyMovespeed = 1.0f;
	}
}
