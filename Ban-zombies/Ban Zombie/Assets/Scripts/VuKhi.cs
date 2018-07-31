using UnityEngine;
using System.Collections;

public class VuKhi : MonoBehaviour {
    public Transform nguoi;
    public float fireRate = 0; //Tỷ lệ đạn / s
    public int satThuong = 60;  //Sát thương của súng
    public LayerMask biBan; //Những đối tượng nào có Layer được chọn sẽ bị gây sát thương
    float timeToSpawnEffect = 0; //Thời gian để tạo hiệu ứng
    public float effectSpawnRate = 10; //Tỷ lệ để tạo hiệu ứng liên tiếp nhau, tương tự như fireRate


    public Transform DuongDanPref; //Đối tượng đường đạn
    public Transform Flash_b;
    public Transform HitPrefab;
    public Transform zom;
    public string weaponShootSound = "DefaultShot";

    float timeToFire = 0; 
    Transform firePoint; //Điểm bắt đầu bắn (Là một đối tượng)

    //Caching
    AudioManager audioManager;

	// Use this for initialization
	void Awake () {
        firePoint = transform.FindChild("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("Khong tim thay doi tuong FirePoint");
        }
	}

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("Khong tim thay audioManager trong Game!");
        }
    }

	// Update is called once per frame
	void Update () {
	    if(fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
                Ban();
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Ban();
            }
        }
	}
    void Ban()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (mousePosition - firePointPosition), 100, biBan);
        //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 50, Color.green);
       

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.DamageEnemy(satThuong);
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;
            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
                
            HieuUngDuongDan(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate; //Công thức tính thời gian sinh hiệu ứng
        }
    }

    void HieuUngDuongDan(Vector3 hitPos, Vector3 hitNormal)
    {
        if (nguoi.localScale.x < 0) //Bên trái
        {
            //Thay đổi rotation trục Z khi quay qua trái (scale X nhân vật bị đổi từ 1 thành -1)
            Vector3 test = firePoint.transform.rotation.eulerAngles;
            test = new Vector3(test.x, test.y, -(test.z + 180));
            //Khởi tạo đối tượng đạn
            Transform trail = Instantiate(DuongDanPref, firePoint.position, Quaternion.Euler(test)) as Transform;
            LineRenderer lr = trail.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, hitPos);
            }
            Destroy(trail.gameObject, 0.05f);

            if(hitNormal != new Vector3(9999,9999,9999))
                {
                    Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));

                }

            Vector3 flashPos = firePoint.position;
            flashPos = new Vector3(flashPos.x, flashPos.y - 0.05f, flashPos.y);

            Transform clone = Instantiate(Flash_b, flashPos, Quaternion.Euler(test)) as Transform;
            //clone.parent = firePoint;
            Destroy(clone.gameObject, 0.05f);
            audioManager.PlaySound(weaponShootSound);

        }
        else
        {
            //Khởi tạo đối tượng đạn (đối tượng, vị trí xuất phát, )
            Transform trail = Instantiate(DuongDanPref, firePoint.position, firePoint.rotation) as Transform; //Bên phải
            LineRenderer lr = trail.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, hitPos);
            }
            Destroy(trail.gameObject, 0.05f);
            if (hitNormal != new Vector3(9999, 9999, 9999))
            {
                Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));

            }
            Vector3 flashPos = firePoint.position;
            flashPos = new Vector3(flashPos.x, flashPos.y + 0.1f, flashPos.y);
            Transform clone = Instantiate(Flash_b, flashPos, firePoint.rotation) as Transform;
            Destroy(clone.gameObject, 0.05f);
            audioManager.PlaySound(weaponShootSound);
        }
    }
}
