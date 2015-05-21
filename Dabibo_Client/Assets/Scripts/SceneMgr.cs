using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour
{
	public Vector2 enemyPos = new Vector2(0,0);
	public GameObject[] enemyTiles;

	private Transform sceneHolder;

	void LayoutEnemyAtRandom(GameObject[] tileArray)
	{
		GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
		Instantiate(tileChoice, enemyPos, Quaternion.identity);
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
}
