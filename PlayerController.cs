using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public float playerSpeed;
    public bool isDoingAMinigame = false;

    private Rigidbody rb;
    public SphereCollider interactCollider;

    private Object collisionedObject;

    public GameObject playerRenderer;
    public Animator anim;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactCollider = GetComponent<SphereCollider>();

        anim = playerRenderer.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDoingAMinigame)
        {
            Movement();
        }
    }

    void Movement()
    {
        float hv = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(hv * playerSpeed, rb.velocity.y, rb.velocity.z) * Time.deltaTime;

        if (hv > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            anim.SetBool("Move", true);
        }
        else if (hv == 0)
        {
            anim.SetBool("Move", false);
        }
        else if(hv < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            anim.SetBool("Move", true);
        }



    }

    /*
    void Interact()
    {
        interactCollider.enabled = false;
        GameManager.Instance.numberActions--;
        UIManager.Instance.Transition();
    }
    */

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Object") && Input.GetKeyDown(KeyCode.E) && !isDoingAMinigame)
        {
            if (GameManager.Instance.numberActions <= 0)
            {
                if (col.GetComponent<Object>().objType == Object.ObjType.BED)
                {
                    col.GetComponent<Object>().DoAction();
                    StartCoroutine(Interact());
                }
                else if (col.GetComponent<Object>().objType == Object.ObjType.STAIRS)
                {
                    col.GetComponent<Object>().DoAction();
                }
            }
            else if (col.GetComponent<Object>().objType == Object.ObjType.BED)
            {
                col.GetComponent<Object>().DoAction();
                StartCoroutine(Interact());
            }
            else if (col.GetComponent<Object>().objType == Object.ObjType.STAIRS)
            {
                col.GetComponent<Object>().DoAction();
            }
            else
            {
                StartCoroutine(Interact());
                collisionedObject = col.GetComponent<Object>();
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Object") && Input.GetKeyDown(KeyCode.E) && !isDoingAMinigame)
        {
            if (GameManager.Instance.numberActions <= 0)
            {
                if (col.GetComponent<Object>().objType == Object.ObjType.BED)
                {
                    col.GetComponent<Object>().DoAction();
                    UIManager.Instance.Transition();
                }
                else if (col.GetComponent<Object>().objType == Object.ObjType.STAIRS)
                {
                    col.GetComponent<Object>().DoAction();
                }
            }
            else if (col.GetComponent<Object>().objType == Object.ObjType.BED)
            {
                col.GetComponent<Object>().DoAction();
                UIManager.Instance.Transition();
            }
            else if (col.GetComponent<Object>().objType == Object.ObjType.STAIRS)
            {
                col.GetComponent<Object>().DoAction();
            }
            else
            {
                collisionedObject = col.GetComponent<Object>();
                StartCoroutine(Interact());
            }
        }
    }

    public IEnumerator Interact()
    {
        interactCollider.enabled = false;
        GameManager.Instance.numberActions--;
        UIManager.Instance.Transition();

        while (UIManager.Instance.transition)
        {
            yield return null;
        }
        if (collisionedObject != null)
        {
            collisionedObject.DoAction();
        }
    }
}
