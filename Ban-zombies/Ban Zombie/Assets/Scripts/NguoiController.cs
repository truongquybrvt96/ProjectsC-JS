
using System.Collections;
using UnityEngine.UI;

public class NguoiController : MonoBehaviour {


    public float moveSpeed;

    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;



    public Transform vuKhi;



    public Transform textHealthObj;

    public float doCao;
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
	}
	void Update()
    {
        //NHAY
        //Kiem tra neu dang cham dat ma nhan nut Jump thi nhay theo doCao
        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("ChamDat", grounded);
            myRB.AddForce(new Vector2(0, doCao));
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        //NHAY
        //Kiem tra co cham dat chua bang 2 tham so transform groundCheck va groundLayer, tra ve true if touched the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("ChamDat", grounded); 
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        //DI CHUYEN
        //Lay gia tri a va d
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("Dichuyen", Mathf.Abs(move));

        myRB.velocity = new Vector2(move * moveSpeed * Time.deltaTime, myRB.velocity.y);

        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= transform.position.x && facingRight)
        {
            Xoay();

        }
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && !facingRight)
        {
            Xoay();
        }
	}
    void Xoay()
    {

        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 

        //Xoay text health
        Vector3 txtScale = textHealthObj.localScale;
        txtScale.x *= -1;
        textHealthObj.localScale = txtScale;
    } 
}
