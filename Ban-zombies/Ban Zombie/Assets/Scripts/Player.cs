using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [System.Serializable] //Hiển thị class và các đặc tính của nó
    public class PlayerStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int curHealth
        {
            get{return _curHealth;}
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }//Clamp để chắc chắn value (máu hiện tại sẽ không < 0)

        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    public PlayerStats stats = new PlayerStats();
    public int gioiHanRoiMatMau = -20;

    [SerializeField]
    private StatusIndicator statusIndicator;


    void Start()
    {
        stats.Init();
        if(statusIndicator == null)
        {
            Debug.LogError("Khong co status indicator tham chieu Nguoi");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
    void Update()
    {
        if (transform.position.y <= gioiHanRoiMatMau)
            DamagePlayer(9999);
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if(stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
