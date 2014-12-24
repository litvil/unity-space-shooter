using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text gameOverText;
	public Text restartText;
	
	private bool gameOver;
	private bool restart;
	private int score;


	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i=0; i < hazardCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver){
				restart = true;
				restartText.text = "Press 'R' ";
				break;
			}
		}
	}

	void Start () {
		score = 0;
		gameOver = false;
		restart = false;
		gameOverText.text = restartText.text = "";
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	
	void Update () {
		if (restart && Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel(Application.loadedLevel);	
		}

	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();

	}

	void UpdateScore () {
		scoreText.text = score.ToString ();
	}

	public void GameOver(){
		gameOver = true;
		gameOverText.text = "Game Over!";
	}
}
