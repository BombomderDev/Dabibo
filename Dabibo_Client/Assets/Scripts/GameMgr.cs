using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
	public static GameMgr instance = null;

	public SceneMgr sceneMgr;
	public int EnemyMaxHp
	{
		get{return enemyMaxHp;}
	}
	public int PlayerHitDamage
	{
		get{return damage;}
	}
	public int NowStage
	{
		get{return stage;}
	}
	public bool IsGameOver
	{
		get{return isGameOver;}
	}
	public Camera MainCamera
	{
		get{return mainCamera;}
	}
	public GameObject UICanvasObj
	{
		get{return uiCanvasObj;}
	}
	public int EnemyKillCount
	{
		set{enemyKillCount = value;}
		get{return enemyKillCount;}
	}
	public float lostTimeRate = 1.0F;


	private int stage = 1;
	private int enemyMaxHp = 1;
	private int enemyKillCount = 0;
	private int timing = 1;
	private float nextLostTime = 0.0F;
	private int damage = 1;
	private int money = 0;
	private int level = 1;
	private bool isGameOver = false;

	private GameObject uiCanvasObj;
	private Text attackText;
	private Text levelText;
	private Text stageText;
	private Text enemyHpText;
	private Text timingText;
	private Text moneyText;
	private Text infoText;
	private Button restartBtn;
	private Button gainMoneyBtn;
	private Button costMoneyBtn;
	private Button levelUpBtn;
	private Button startBtn;
	private Camera mainCamera;
	private GameObject skyObj;
	private GameObject grassObj;
	private GameObject player;

	// Use this for initialization
	void Awake ()
	{
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		sceneMgr = GetComponent<SceneMgr>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

		InitGame();
	}

	private void OnLevelWasLoaded(int index)
	{
//		stage++;
//		stageText = GameObject.Find("StageLevel").GetComponent<Text>();
//		stageText.text = "Stage:" + stage;
		InitGame();
	}

	private void InitGame()
	{
		skyObj = GameObject.Find("Sky");
		grassObj = GameObject.Find("Grass");
		player = GameObject.Find("Player");

		sceneMgr.SetUpScene();

		uiCanvasObj = GameObject.Find("UI_Information").gameObject;

		startBtn = uiCanvasObj.transform.Find("StartBtn").GetComponent<Button>();
		startBtn.onClick.AddListener(StartGame);
		startBtn.gameObject.SetActive(true);

		infoText = uiCanvasObj.transform.Find("Info").GetComponent<Text>();
		infoText.text = "Push Button to Start";

		enemyHpText = uiCanvasObj.transform.Find("EnemyHp").GetComponent<Text>();
		enemyMaxHp = GetEnemyHp(stage);
		enemyHpText.text = "EnemyHp:" + enemyMaxHp + "/" + enemyMaxHp;
//
//		timing = GetTiming();
//		timingText = uiCanvasObj.transform.Find("Timing").GetComponent<Text>();
//		timingText.text = "Timing:" + timing;
//
		moneyText = uiCanvasObj.transform.Find("Money").GetComponent<Text>();
		moneyText.text = "Money:" + money;

		damage = GetDamage(level);
		attackText = uiCanvasObj.transform.Find("PlayerAttack").GetComponent<Text>();
		attackText.text = "Hit:" + damage +"/Per";
//
//		levelText = uiCanvasObj.transform.Find("PlayerLevel").GetComponent<Text>();
//		levelText.text = "Level:" + level;
//
//		infoText = uiCanvasObj.transform.Find("Info").GetComponent<Text>();
//		infoText.text = string.Empty;
//
		restartBtn = uiCanvasObj.transform.Find("RestartBtn").GetComponent<Button>();
		restartBtn.onClick.AddListener(RestartGame);
		restartBtn.gameObject.SetActive(false);
//
//		gainMoneyBtn = uiCanvasObj.transform.Find("GainMoneyBtn").GetComponent<Button>();
//		gainMoneyBtn.onClick.AddListener(GainMoney);
//		gainMoneyBtn.gameObject.SetActive(true);
//
//		costMoneyBtn = uiCanvasObj.transform.Find("CostMoneytBtn").GetComponent<Button>();
//		costMoneyBtn.onClick.AddListener(CostMoney);
//		costMoneyBtn.gameObject.SetActive(true);
//
//		levelUpBtn = uiCanvasObj.transform.Find("LevelUpBtn").GetComponent<Button>();
//		levelUpBtn.onClick.AddListener(LevelUp);
//		levelUpBtn.gameObject.SetActive(true);
	}

	private int GetEnemyHp(int stage)
	{
		int enemyHp = (int)Mathf.Pow(stage,2);
		return 10;
	}

	public void ChangeEnemyHpText(int nowHp)
	{
		int showHp = nowHp < 0 ? 0 : nowHp;
		enemyHpText.text = "EnemyHp:" + showHp + "/" + enemyMaxHp;
	}

	private int GetTiming()
	{
		return 30;
	}

	public void ChangeTimingText(int nowTime)
	{
		int showTiming = nowTime < 0 ? 0 : nowTime;
		timingText.text = "Timing:" + showTiming;
	}

	private int GetDamage(int playerLevel)
	{
		int damage = playerLevel;
		return damage;
	}

	private void ChangeHitDamageText(int damage)
	{
		attackText.text = "Hit:" + damage +"/Per";
	}

	private void ChangeMoneyText(int money)
	{
		moneyText.text = "Money:" + money;
	}

	private void ChangeLevelText(int playerLevel)
	{
		levelText.text = "Level:" + playerLevel;
	}

	void Update ()
	{
//		if(Time.time > nextLostTime)
//		{
//			nextLostTime = Time.time + lostTimeRate;
//			timing--;
//			ChangeTimingText(timing);
//			CheckIfGameOver();
//		}
	}

	private void CheckIfGameOver()
	{
		if(timing <= 0)
		{
			isGameOver = true;

			stageText.text = string.Empty;
			enemyHpText.text = string.Empty;
			timingText.text = string.Empty;
			attackText.text = string.Empty;
			levelText.text = string.Empty;
			moneyText.text = string.Empty;
			infoText.text = "GameOver\nGot Stage " + stage;
			restartBtn.gameObject.SetActive(true);
			gainMoneyBtn.gameObject.SetActive(false);
			costMoneyBtn.gameObject.SetActive(false);
			levelUpBtn.gameObject.SetActive(false);
		}
	}

	private void RestartGame()
	{
//		InitData();
		money = 0;
		enemyKillCount = 0;
		isGameOver = false; 
		Application.LoadLevel(Application.loadedLevel);
	}

	public void InitData()
	{
		stage = 0;
		enemyMaxHp = 0;
		timing = GetTiming();
		nextLostTime = 0.0F;
		level = 1;
		money = 0;
		isGameOver = false;
	}

	public void AddMoney(int gainMoney)
	{
		if(gainMoney < 0 && money < Mathf.Abs(gainMoney))return;
		money += gainMoney;
		if(money <= 0) money = 0;
		ChangeMoneyText(money);
	}

	private void GainMoney()
	{
		AddMoney(10);
	}

	private void CostMoney()
	{
		AddMoney(-10);
	}

	private void LevelUp()
	{
		if(money < 5)return;

		money -= 5;
		level ++;
		damage = GetDamage(level);
		ChangeHitDamageText(damage);
		ChangeMoneyText(money);
		ChangeLevelText(level);
	}

	public void StartGame()
	{
		startBtn.gameObject.SetActive(false);
		infoText.gameObject.SetActive(false);

		skyObj.GetComponent<Background>().enabled = true;
		grassObj.GetComponent<Background>().enabled = true;
		sceneMgr.SetUpEmemy();
	}

	public void ReStartGame()
	{
		StartGame();
		enemyMaxHp = GetEnemyHp(stage);
		enemyHpText.text = "EnemyHp:" + enemyMaxHp + "/" + enemyMaxHp;
		player.GetComponent<Player>().ReSetEnemyTrans();
	}

	public void StartFight()
	{		
		skyObj.GetComponent<Background>().enabled = false;
		grassObj.GetComponent<Background>().enabled = false;

		player.GetComponent<Player>().Attack();

	}

	public void ShowGameOver ()
	{
		isGameOver = true;
		infoText.text = "GameOver\n";
		enemyHpText.text = string.Empty;
		attackText.text = string.Empty;
		moneyText.text = string.Empty;
		
		skyObj.GetComponent<Background>().enabled = false;
		grassObj.GetComponent<Background>().enabled = false;
		infoText.gameObject.SetActive(true);
		restartBtn.gameObject.SetActive(true);
	}
}
