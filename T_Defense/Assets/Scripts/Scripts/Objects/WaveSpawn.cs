using UnityEngine;
using System.Collections;
public class WaveSpawn : MonoBehaviour {

	public int WaveSize;
	public int totalWave;
	public GameObject EnemyPrefab;
	public GameObject StartButton;
	public Transform spawnPoint;
	public Transform[] WayPoints;
	int enemyCount=0;
	int wavecount = 0;

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
		GameObject enemy = GameObject.Instantiate(EnemyPrefab,spawnPoint.position,Quaternion.identity) as GameObject;
        
		enemy.GetComponent<Enemy>().waypoints = WayPoints;
    }

	// call by GameManager, for starting wave as now wave number
	void WaveStart(float[] data) {
		InvokeRepeating("SpawnEnemy", data[0], data[1]);
		WaveSize = (int) data[2];
		enemyCount = 0;
		wavecount++;
	}

	void CreateButton() {
		// Transform tp = new Vector3(spawnPoint.transform.position.x, 
		// 						   spawnPoint.transform.position.y, 
		// 						   spawnPoint.transform.position.z + 10);
		// tp.position.z += 10;

		// Transform tempP = spawnPoint;
		// tempP.localPosition = new Vector3(spawnPoint.transform.position.x, 
		// 						   spawnPoint.transform.position.y, 
		// 						   spawnPoint.transform.position.z + 10);
		// Instantiate(StartButton, tempP.position, this.transform.rotation);


		Instantiate(StartButton, new Vector3((float)2.7, (float)4.5, (float)-2.65), this.transform.rotation);

	}
}
