using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }
    void Awake()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        }
        
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    public string spawnSoundName;
    public int score = 0;

    public Transform enemyDeathParticles;

    [SerializeField]
    private GameObject gameOverUI;

    //cache
    private AudioManager audioManager;
    void Start()
    {
        _remainingLives = maxLives;
        if (enemyDeathParticles == null)
            Debug.LogError("Khong co enemyDeathParticles");
        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("Khong tim thay AudioManager trong scene!");
        }
        audioManager.StopSound("Music");
        audioManager.PlaySound("GameMusic");
        audioManager.PlaySound("StartQuote");
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!");
        gameOverUI.SetActive(true);
        audioManager.PlaySound("GameOver");
        audioManager.StopSound("GameOver");
    }

    public IEnumerator _HoiSinhPlayer()
    {
        audioManager.PlaySound(spawnSoundName);
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(GameObject.FindGameObjectWithTag("reSpDes"), 2f);
        //Add hiệu ứng
    }

    public static void KillPlayer(Player player)
    {
        gm.audioManager.PlaySound("DeadVoice");
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if(_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._HoiSinhPlayer());
        }
        
    }
    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
        
    }

    public void _KillEnemy(Enemy _enemy)
    {
        score += 10;
        audioManager.PlaySound("Explosion");
        Instantiate(enemyDeathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_enemy.gameObject);
    }
}
