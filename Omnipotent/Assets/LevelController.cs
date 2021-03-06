using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	bool gamePaused = false;
	bool levelInProgress = false;
	public int currentLevel = 1;
	bool levelInit = true;
	float tutTimer = 15.0f;
	public GameObject TutImage;
	public Toggle[] toggle;
	public int[] objForLevels;

	public int nosOfObjectives;
	public bool[]objectiveComplete=new bool[]{false,false, false, false, false};
	public int[] ObjectiveNos;
	public int freeLevel = 4;

	public GameObject zombieManager;
	public GameObject buildingManager;
	public GameObject peopleManager;
	public GameObject BossManager;
	public Text numPeopleText;
	public bool zombieStatus ;
	public int humanStatus ;
	public int houseStatus ;


	bool lmode = false;
	bool fmode = false;
	float ftimer = 3.0f;
	Vector3 fLoc = new Vector3();
	int prevHouseNos;

	public XP_handle xp_handler;

	public List<string>currentPower = new List<string>();
	//Power Chart
	public enum MODE {
		DEFAULT,
		BUILD,
		THUNDER_CLAP,
		WINDY,
		GMBC,
		MJOLNIR,
		FIREBALL,
		TORNADO,
		BOO,
		HEY
	}
	public MODE Powermode = MODE.DEFAULT;

	public Vector3 PowerLoc = new Vector3();
	public Vector3 TornadoLoc = new Vector3 ();

	List<GameObject> DamageT = new List<GameObject>();

	//Levels: min requirements
	int minHouses2Build=5 ;

	int spawnZombs = 10;

	bool autoSpawn = false;
	float time2Spawn = 10.0f;

	float survivalTime = 100.0f;
	float BossRate = 20.0f;
	float ZombRate = 10.0f;

	void initMinReqs(int level){
		//int nosZombies = level * Random.Range (5,zombies2Kill);
		//int nosHouses = level * Random.Range (1,minHouses2Build);

		//zombieManager.GetComponent<ZombieManager> ().initZombies (nosZombies);
		//housemanager.init(nosHouses)
		//minHouses2Build = level * minHouses2Build;

	}

	void updateObjectives(){
		if (currentLevel == 1) {
			if(buildingManager.GetComponent<houseManager>().nosHouses >= minHouses2Build){
				toggle[1].isOn = true;
				toggle[0].isOn = true;
				toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				objectiveComplete[0] = true;
				objectiveComplete[1] = true;
				Debug.Log("changed color");
			}
		}
		if (currentLevel == 2) {
			if(Powermode == MODE.GMBC){
				objectiveComplete[0] = true;
				toggle[0].isOn = true;
				toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed weather");
			}
			if(Powermode == MODE.HEY){
				objectiveComplete[1] = true;
				toggle[1].isOn = true;
				toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed Hey");
			}
			if(Powermode == MODE.BOO){
				objectiveComplete[2] = true;
				toggle[2].isOn = true;
				toggle[2].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed Boo");
			}
			if(xp_handler.LevelUpReached){
				objectiveComplete[3] = true;
				toggle[3].isOn = true;
				toggle[3].transform.GetChild(1).GetComponent<Text>().color=Color.green;
			}
		}

		if (currentLevel == 3) {

			if(zombieManager.GetComponent<ZombieManager>().checkZombsDead() == true){
				objectiveComplete[0] = true;
				toggle[0].isOn = true;
				toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed zombies");
			}

			if(Powermode == MODE.MJOLNIR){
				objectiveComplete[1] = true;
				toggle[1].isOn = true;
				toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed lightening");
			}
			if(Powermode == MODE.TORNADO){
				objectiveComplete[2] = true;
				toggle[2].isOn = true;
				toggle[2].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed Tornado");
			}
			if(Powermode == MODE.FIREBALL){
				objectiveComplete[3] = true;
				toggle[3].isOn = true;
				toggle[3].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed Fireball");
			}
			if(xp_handler.LevelUpReached){
				objectiveComplete[4] = true;
				toggle[4].isOn = true;
				toggle[4].transform.GetChild(1).GetComponent<Text>().color=Color.green;
				//Debug.Log("changed XP");
			}
		}
		if (currentLevel == 4) {
			// no logic for next level here.. check the switch function itself
		}
	}

	void displayObjective(){
		if (currentLevel == 1) {
			toggle[0].transform.GetChild(1).GetComponent<Text>().text = "Explore The World!";
			toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			toggle[0].gameObject.SetActive(true);
			toggle[0].isOn = false;
				
			toggle[1].transform.GetChild(1).GetComponent<Text>().text = "Create 5 Houses!";
			toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			toggle[1].gameObject.SetActive(true);
			toggle[1].isOn = false;
			}
		if (currentLevel == 2) {
			toggle[0].transform.GetChild(1).GetComponent<Text>().text = "Change The Weather!!";
			toggle[0].gameObject.SetActive(true);
			toggle[0].isOn = false;
			toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.red;

			toggle[1].transform.GetChild(1).GetComponent<Text>().text = "Call Somebody";
			toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			toggle[1].gameObject.SetActive(true);
			toggle[1].isOn = false;

			toggle[2].transform.GetChild(1).GetComponent<Text>().text = "Scare a Human";
			toggle[2].gameObject.SetActive(true);
			toggle[2].isOn = false;
			toggle[2].transform.GetChild(1).GetComponent<Text>().color=Color.red;

			toggle[3].transform.GetChild(1).GetComponent<Text>().text = "Fill The XP Bar!";
			toggle[3].gameObject.SetActive(true);
			toggle[3].isOn = false;
			toggle[3].transform.GetChild(1).GetComponent<Text>().color=Color.red;
		}
		if (currentLevel == 3) {
			toggle[0].transform.GetChild(1).GetComponent<Text>().text = "Kill All Zombies";
			toggle[0].gameObject.SetActive(true);
			toggle[0].isOn = false;
			toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			
			toggle[1].transform.GetChild(1).GetComponent<Text>().text ="Trigger Lightning";
			toggle[1].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			toggle[1].gameObject.SetActive(true);
			toggle[1].isOn = false;
			
			toggle[2].transform.GetChild(1).GetComponent<Text>().text = "Use Tornado";
			toggle[2].gameObject.SetActive(true);
			toggle[2].isOn = false;
			toggle[2].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			
			toggle[3].transform.GetChild(1).GetComponent<Text>().text = "Throw a Fireball";
			toggle[3].gameObject.SetActive(true);
			toggle[3].isOn = false;
			toggle[3].transform.GetChild(1).GetComponent<Text>().color=Color.red;

			toggle[4].transform.GetChild(1).GetComponent<Text>().text = "Fill The XP Bar!";
			toggle[4].gameObject.SetActive(true);
			toggle[4].isOn = false;
			toggle[4].transform.GetChild(1).GetComponent<Text>().color=Color.red;

		}
		if (currentLevel == 4) {
			toggle[0].transform.GetChild(1).GetComponent<Text>().text = "Protect Them!";
			toggle[0].gameObject.SetActive(true);
			toggle[0].isOn = false;
			toggle[0].transform.GetChild(1).GetComponent<Text>().color=Color.red;
		}
	}


	bool ObjectiveCompleted(){
		for (int i=0; i<nosOfObjectives; i++) {
			if(objectiveComplete[i]==false)
				return false;
		}
		return true;
	}

	void resetObjectives(){
		Debug.Log (nosOfObjectives+" resetting ");
		for (int i=0; i<nosOfObjectives; i++) {
			toggle[i].gameObject.SetActive(false);
			objectiveComplete [i] = false;
		}
		zombieManager.GetComponent<ZombieManager> ().checkZombsDead ();
		nosOfObjectives = ObjectiveNos [currentLevel];
	}

	public void updateSources(){
		peopleManager.GetComponent<LoadVoxelPeople> ().updateSources ();
		zombieManager.GetComponent<ZombieManager> ().updateDest ();
	}

	// Use this for initialization
	void Start () {
		
		currentLevel = 5;
		//init Person manager
		peopleManager = GameObject.FindGameObjectWithTag ("PeopleManager");
		//int nosPeople = currentLevel * Random.Range (10,humans2create);
		//peopleManager.GetComponent<LoadVoxelPeople> ().initPerson (nosPeople);

		buildingManager = GameObject.FindGameObjectWithTag ("Building");

		// Init Zombie manager
		zombieManager = (GameObject)Instantiate (Resources.Load ("ZombieManager"), gameObject.transform.position, Quaternion.identity) as GameObject;
		zombieManager.transform.parent = transform;
		GameObject.FindGameObjectWithTag("Hand").GetComponent<cursor_handle>().currentLevel = zombieManager;
		buildingManager = GameObject.FindGameObjectWithTag ("Building");
		prevHouseNos = 0;
	}
	/*
	public void setLevel(int level){
		currentLevel = level;
	}


	private void resetLevel(){
		zombieManager.GetComponent<ZombieManager> ().deleteZombie ();
		if (currentLevel > 1) {
						//int nosPeople = currentLevel * Random.Range (10, humans2create);
						//peopleManager.GetComponent<LoadVoxelPeople> ().initPerson (nosPeople);
						initMinReqs (currentLevel);
				}
	}*/


	void setCurrentPower(){
		if (MODE.FIREBALL == Powermode) {
			BossManager.GetComponent<WildManagement>().Powermode = WildManagement.MODE.FIREBALL;
			zombieManager.GetComponent<ZombieManager>().Powermode = ZombieManager.MODE.FIREBALL;
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.FIREBALL;
		}
		if (MODE.TORNADO == Powermode) {
			zombieManager.GetComponent<ZombieManager>().Powermode = ZombieManager.MODE.TORNADO;
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.TORNADO;
		}
		if (MODE.MJOLNIR == Powermode) {
			BossManager.GetComponent<WildManagement>().Powermode = WildManagement.MODE.MJOLNIR;
			zombieManager.GetComponent<ZombieManager>().Powermode = ZombieManager.MODE.MJOLNIR;
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.MJOLNIR;
		}
		if (MODE.THUNDER_CLAP == Powermode) {
			zombieManager.GetComponent<ZombieManager>().Powermode = ZombieManager.MODE.THUNDER_CLAP;
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.THUNDER_CLAP;
		}
		if (MODE.HEY == Powermode) {
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.HEY;
		}
		if (MODE.BOO == Powermode) {
			peopleManager.GetComponent<LoadVoxelPeople>().Powermode = LoadVoxelPeople.MODE.BOO;
		}
		Powermode = MODE.DEFAULT;	
	}

	// Level Logic

	void LevelLogic(){
		if (gamePaused) {
				} else {
			if(currentLevel == 1){
				//Debug.Log("Every Frame_0");
				if(levelInit == true){
					xp_handler.LevelUp(currentLevel);
					nosOfObjectives = ObjectiveNos[currentLevel];
					levelInit = false;
					tutTimer = 15.0f;
					Sprite newTutImage = Resources.Load<Sprite>("level1");
					TutImage.GetComponent<Image>().sprite = newTutImage;
					TutImage.SetActive(true);
					displayObjective();
				}else{
					if(tutTimer <= 0.0f){
						TutImage.SetActive(false);
						updateObjectives();
						bool nextLevelStatus = ObjectiveCompleted();
						if(nextLevelStatus == true){
							levelInit = true;
							currentLevel++;
							resetObjectives();
						}
					}else{
						tutTimer-=Time.deltaTime;
					}
				}
			}else{
				if(currentLevel == 2){
					if(levelInit == true){
						xp_handler.LevelUp(currentLevel);
						nosOfObjectives = ObjectiveNos[currentLevel];
						levelInit = false;
						tutTimer = 15.0f;
						Sprite newTutImage = Resources.Load<Sprite>("level2");
						TutImage.GetComponent<Image>().sprite = newTutImage;
						TutImage.SetActive(true);
						displayObjective();
					}else{
						if(tutTimer <= 0.0f){
							TutImage.SetActive(false);
							updateObjectives();
							bool nextLevelStatus = ObjectiveCompleted();
							if(nextLevelStatus == true){
								levelInit = true;
								currentLevel++;
								resetObjectives();
							}
						}else{
							tutTimer-=Time.deltaTime;
						}
					}
				}else{
					if(currentLevel == 3){
						if(levelInit == true){
							xp_handler.LevelUp(currentLevel);
							nosOfObjectives = ObjectiveNos[currentLevel];
							levelInit = false;
							tutTimer = 15.0f;
							Sprite newTutImage = Resources.Load<Sprite>("level3");
							TutImage.GetComponent<Image>().sprite = newTutImage;
							TutImage.SetActive(true);
							displayObjective();
						}else{
							if(tutTimer <= 0.0f){
								TutImage.SetActive(false);
								updateObjectives();
								bool nextLevelStatus = ObjectiveCompleted();
								if(peopleManager.GetComponent<LoadVoxelPeople>().people.Count == 0){
									resetObjectives();
									zombieManager.GetComponent<ZombieManager>().deleteZombie();
									for(int i=0;i<buildingManager.GetComponent<houseManager>().nosHouses;i++){
										peopleManager.GetComponent<LoadVoxelPeople> ().initPersonRandom (5);
									}
									levelInit = true;
								}else{
								if(nextLevelStatus == true){
									levelInit = true;
									currentLevel++;
									resetObjectives();
									}
								}
							}else{
								tutTimer-=Time.deltaTime;
								if(tutTimer<=0.0f){
//									Debug.Log(tutTimer+" "+spawnZombs);
									zombieManager.GetComponent<ZombieManager>().initZombies(spawnZombs);
								}
							}
						}
					}else{
						if(currentLevel == 4){

							if(levelInit == true){
								Debug.Log("levelInit: "+levelInit);
								xp_handler.LevelUp(currentLevel);
								nosOfObjectives = ObjectiveNos[currentLevel];
								levelInit = false;
								tutTimer = 15.0f;
								Sprite newTutImage = Resources.Load<Sprite>("level4");
								TutImage.GetComponent<Image>().sprite = newTutImage;
								TutImage.SetActive(true);
								displayObjective();
							}else{
								if(tutTimer <= 0.0f){
									TutImage.SetActive(false);
									updateObjectives();
									bool nextLevelStatus = ObjectiveCompleted();
									survivalTime -= Time.deltaTime;
									ZombRate -= Time.deltaTime;
									BossRate -= Time.deltaTime;
									//Debug.Log("People Dead Reset");
									if(peopleManager.GetComponent<LoadVoxelPeople>().people.Count == 0){
										Debug.Log("People Dead Reset");
										levelInit = true;
										resetObjectives();
										survivalTime = 100.0f;
										zombieManager.GetComponent<ZombieManager>().deleteZombie();
										for(int i=0;i<buildingManager.GetComponent<houseManager>().nosHouses;i++){
											peopleManager.GetComponent<LoadVoxelPeople> ().initPersonRandom (5);
										}
										return;
									}
									Debug.Log(survivalTime+"Survival Time");
									if(survivalTime <= 0.0){
											currentLevel++;
											ZombRate = 20.0f;
											BossRate = 40.0f;
										levelInit = true;
										}else{
										if(ZombRate<=0.0f){
											ZombRate = 10.0f;
											zombieManager.GetComponent<ZombieManager>().initZombies(1);
										}
										if(BossRate<=0.0f){
											if(BossManager.GetComponent<WildManagement>().enemList.Count<=5){
											BossRate = 30.0f;
											BossManager.GetComponent<WildManagement>().SpawnEnemy(1);
											}else
												BossRate = 30.0f;
										}
									}
								}else{
									tutTimer-=Time.deltaTime;
									if(tutTimer<=0.0f){
										Debug.Log(tutTimer+" "+spawnZombs);
										zombieManager.GetComponent<ZombieManager>().initZombies(spawnZombs);
										BossManager.GetComponent<WildManagement>().SpawnEnemy(1);
									}
								}
							}
						}else{
							if(currentLevel == 5){
								if(levelInit){
								xp_handler.LevelUp(currentLevel);
									levelInit = false;
								}
								BossRate-=Time.deltaTime;
								ZombRate-=Time.deltaTime;
								if(ZombRate<=0.0f){
									ZombRate = 20.0f;
									zombieManager.GetComponent<ZombieManager>().initZombies(1);
								}
								if(BossRate<=0.0f){
									if(BossManager.GetComponent<WildManagement>().enemList.Count<=5){
										BossRate = 30.0f;
										BossManager.GetComponent<WildManagement>().SpawnEnemy(1);
									}else
										BossRate = 30.0f;
								}
							}
						}
					}
				}
				//Debug.Log("Every Frame_3");
			}
		}
		setCurrentPower ();
	}


	// Update is called once per frame
	void Update () {
		numPeopleText.text = peopleManager.GetComponent<LoadVoxelPeople>().people.Count.ToString();
		//Debug.Log(fLoc+" Fire "+Powermode.ToString()+" ");
		if (Powermode == MODE.FIREBALL || fmode == true) {
			//Debug.Log(fLoc+" Fire ");
			if(fmode == false){
				fLoc = PowerLoc;
				fmode = true;
			}
			if(ftimer<=0.0f){
				Debug.Log(fLoc+" ");
				GameObject obs = (GameObject)Instantiate (Resources.Load("Damage"), fLoc, Quaternion.identity) as GameObject;
				obs.transform.Rotate(0,Random.Range(0,360),0);
				obs.transform.parent = transform;
				DamageT.Add (obs);
				fmode = false;
				ftimer = 3.0f;
			}else{
				ftimer -= Time.deltaTime;
			}
		}

				LevelLogic ();

				// bool houseStatus = getstatus
				if (buildingManager.GetComponent<houseManager> ().houseCreated) {
						buildingManager.GetComponent<houseManager> ().houseCreated = false;
						updateSources ();
						peopleManager.GetComponent<LoadVoxelPeople> ().initPerson (5);
				}
				
				
				return;
	}
}