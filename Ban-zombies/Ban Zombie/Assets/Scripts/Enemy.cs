using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [System.Serializable] //Hiển thị class và các đặc tính của nó
    public class EnemyStats
    {
        public int maxHealth = 5000;

        private int _curHealth;
        public int damage = 40;
        public int curHealth
        {
            get { return _curHealth;}
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    private int pointCoinTemp;
    private int pointCoin;
    public EnemyStats stats = new EnemyStats();
    CameraShake camShake;

    public Transform coinPre;
    public Transform coinPointPos;

    private AudioManager audioManager;

    [Header("Tuy chon: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;
    void Start()
    {
        
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("camShake == null");
        }

        stats.Init();
        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("Enemy: Ko tim thay audioManager!");
        }

    }
    public void DamageEnemy(int damage)
    {
        
        audioManager.PlaySound("Hit");
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this); //Kill zombie
            camShake.Shake(0.1f, 0.4f); //Rung man hinh khi zombie chet
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        pointCoin += 60;

        if(pointCoin >= 200)
        {
            Instantiate(coinPre, coinPointPos.position, coinPointPos.rotation);
            pointCoinTemp = pointCoin - 200;
            pointCoin = 0;
            pointCoin += pointCoinTemp;
        }
        
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if(_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(999999);
        }
    }
}
