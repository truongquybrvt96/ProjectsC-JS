using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    private bool lonHon;
    private bool facingRight;
    public Transform target;
    //Bao nhiêu lần/giây để cập nhật path
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;

    //Lưu đường đã tính toán
    public Path path;

    //Tốc độ của AI trên giây
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;
	
    //Khoảng cách tối đa từ AI tới điểm chuyển tiếp cho nó continue to điểm chuyển tiếp tiếp theo
    public float nextWaypointDistance = 3;

    //Điểm chuyển tiếp hiện tại
    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;

    public Transform healthObj;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            if(!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        //Bắt đầu tìm đường tới nhân vật của chúng ta và trả về cho phương thức OnPathComplete
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
        facingRight = true;
    }

    IEnumerator SearchForPlayer() //Tìm nhân vật, phòng trường hợp nhân vật chết mất xác
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player"); //Tìm gameoject của nhân vật
        if (sResult == null) //Nếu không tìm thấy, mỗi 0.5s tìm lại một lần, tránh việc tìm liên tục
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer()); //Tìm
        }
        else
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            return false;
        }
    }

    IEnumerator UpdatePath()
    {
        if(target != null)
        {
            if (transform.position.y + 1 < target.position.y)
            rb.AddForce(new Vector2(0f, 400f));
        }
        
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return false;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f/updateRate);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        //Debug.Log("Co path roi. Co loi ko: " + p.error);
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                if (lonHon == false)
                    Xoay();
                lonHon = true;
            }
            if (transform.position.x < target.position.x)
            {
                if(lonHon)
                    Xoay();

                lonHon = false;
            }
            
        }
    }

    void FixedUpdate()
    {
        
        //Debug.Log("EnemyY: " + transform.position.y + " TargetY: " + target.position.y);
        if(target == null)
        {
            return;
        }
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count) //Neu da den waypoint
        {
            if (pathIsEnded)
                return;

            //Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        
        }
        pathIsEnded = false;

        //Chi den waypoint tiep theo
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized; //Vector định hướng cho AI
        dir *= speed * Time.fixedDeltaTime;

        //Di chuyển AI
        rb.AddForce(dir, fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWaypointDistance) //Neu khoang cach hien tai nho hon khoan cach den waypoint tiep theo
        {
            currentWaypoint++;
            return;
        }

    }

    void Xoay()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        Vector3 healthScale = healthObj.transform.localScale;
        healthScale.x *= -1;

        healthObj.transform.localScale = healthScale;

    }
}
