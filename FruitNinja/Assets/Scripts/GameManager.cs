using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] slashedPrefabs;
    public GameObject[] fruitsPrefabs;
    public GameObject bombPrefab;
    
    public float spawnOffset;
    public Transform spawnPosition;
    public float minVelocityUp;
    public float maxVelocityUp;

    private float score;
    private float highscore;

    public Text currScore;
    public Text currHighscore;
    public GameObject gameOverUI;
    public Text gameOverScore;
    public Text gameOverHighscore;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFruits", 2f, 2f);
        currScore.text = "0";
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        currHighscore.text = "Best: " + highscore.ToString();
    }

    public void SpawnFruits() {
        int rand = Random.Range(1, 5);
        
        for (int i = 0; i < rand; i++) {
            // instantiate random fruits/bombs 
            int fruitPos = GetRandomFruit();
            
            Vector3 spawnPos = new Vector3(spawnPosition.position.x + Random.Range(-spawnOffset, spawnOffset), 
                spawnPosition.position.y, spawnPosition.position.z);
            
            if (fruitPos < 6) {
                Instantiate(fruitsPrefabs[0], spawnPos, Random.rotation);
            } else if (fruitPos < 12) {
                Instantiate(fruitsPrefabs[1], spawnPos, Random.rotation);
            } else if (fruitPos < 18) {
                Instantiate(fruitsPrefabs[2], spawnPos, Random.rotation);
            } else {
                Instantiate(bombPrefab, spawnPos, Random.rotation);
            }
        }
    }

    private Quaternion GetRandomRotation() {
        float randAngle = Random.Range(45, 135);
        int randAxis = Random.Range(0, 3);

        if (randAxis == 0) {
            return Quaternion.AngleAxis(randAngle, Vector3.up);
        } else if (randAxis == 1) {
            return Quaternion.AngleAxis(randAngle, Vector3.right);
        } else {
            return Quaternion.AngleAxis(randAngle, Vector3.forward);
        }
    }

    private int GetRandomFruit() {
        return Random.Range(0, 20);
    }

    public void AddScore(float point) {
        score += point;
        currScore.text = score.ToString();

        if (highscore < score) {
            highscore = score;
            currHighscore.text = "Best: " + highscore.ToString();
            PlayerPrefs.SetFloat("Highscore", highscore);
        }
    }

    public void GameOver() {
        Time.timeScale = 0;
        gameOverScore.text = score.ToString();
        gameOverHighscore.text = "Best: " + highscore.ToString();
        gameOverUI.SetActive(true);
    }

    public void RetryButton() {
        Time.timeScale = 1;
        foreach (var items in GameObject.FindGameObjectsWithTag("Interactable")) {
            Destroy(items);
        }
        currScore.text = "0";
        gameOverUI.SetActive(false);
    }
}
