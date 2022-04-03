using UnityEngine;
using System.Collections;
public class WaveSpawn : MonoBehaviour {

	public int WaveSize;
	public int totalWave;
	public GameObject[] EnemyPrefab;
	public GameObject StartButton;
	public Transform spawnPoint;
	public Transform[] WayPoints;
	int enemyCount=0;
	int wavecount = 0;
	int nowEnemy = 0;

	void Update()
	{
		if(enemyCount == WaveSize)
		{
			CancelInvoke("SpawnEnemy");
			enemyCount = 0;
			if (totalWave > wavecount)
			{
				CreateButton();
			}
		}
		
	}

	void SpawnEnemy()
	{
		enemyCount++;
		GameObject enemy = GameObject.Instantiate(EnemyPrefab[nowEnemy],spawnPoint.position,Quaternion.identity) as GameObject;
        
		// Mushroom 예외
		if (enemy.GetComponent<Enemy>()) {
			enemy.GetComponent<Enemy>().waypoints = WayPoints;
		}
		else {
			enemy.GetComponent<Mushroom>().waypoints = WayPoints;
		}
		
    }

	// call by GameManager, for starting wave as now wave number
	void WaveStart(float[] data) {
		InvokeRepeating("SpawnEnemy", data[0], data[1]);
		WaveSize = (int) data[2];
		enemyCount = 0;
		wavecount++;
		if (wavecount >= 3) {
			nowEnemy = 1;
		}
	}

	void CreateButton() {
		Instantiate(StartButton, new Vector3((float)15.5, (float)4.5, (float)-16), this.transform.rotation);
	}
}
