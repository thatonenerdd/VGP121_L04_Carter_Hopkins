using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    Coroutine Speedchange;
    private int plives = 2;
    public int lives { get { return plives; }
        set {if (plives > value)
                plives = value;
            if (plives > maxlife)
                plives = maxlife;

        } }
    public int score = 0;
    public int maxlife = 10;
    public float speed = 5.5f;
    public float jumpForce = 300.0f;
    public bool isgrounded;
    public Transform groundcheck;
    public LayerMask isGroundlayer;
    public float groundcheckradius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        if (rb == null)
            Debug.Log("No Rigid Body");
        if (sr == null) Debug.Log("No Sprite Renderer");

        if (groundcheckradius <= 0) groundcheckradius = 0.02f;
        if (speed <= 0) speed = 5.5f;
        if (jumpForce <= 0) jumpForce = 300.0f;

        if (groundcheck == null)
        { GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundcheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        isgrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, isGroundlayer);

        if (isgrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }

        Debug.Log(xInput);
        Vector2 moveDirection = new Vector2(xInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetBool("Isgrounded", isgrounded);
        anim.SetFloat("xInput", Mathf.Abs(xInput));
        if (xInput < 0 && sr.flipX || xInput > 0 && !sr.flipX)
            sr.flipX = !sr.flipX;

        
    }
    public void StartSpeedChange()
    {
        if (Speedchange == null)
        Speedchange = StartCoroutine(SpeedChange());
        else
        { StopCoroutine(SpeedChange());
            Speedchange = null;
            speed /= 2;
            Speedchange = StartCoroutine(SpeedChange());
        }
    }
  IEnumerator SpeedChange()
    {
        speed *= 2;
        yield return new WaitForSeconds(5);
        speed /= 2;
        Speedchange = null;
    }
}
