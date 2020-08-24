using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int horizontalSpeed;
    public float verticalSpeed;
    Vector3 startPos;
    float time = 0;

    float bubbleMaxTime = 5.0f;

    public int movementType;

    private void Start()
    {
        startPos = transform.position;
        movementType = Random.Range(0, 2);
    }
    private void Update()
    {
        SelectMovement();
        if(time >= bubbleMaxTime)
        {
            GameManager.Instance.currentBubbles--;
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        GameManager.Instance.currentShower++;
        GameManager.Instance.currentBubbles--;
        Destroy(gameObject); 
    }

    void SelectMovement()
    {
        if(movementType == 0)
        {
            SinMovement();
        }
        else
        {
            CosMovement();
        }
    }

    void SinMovement()
    {
        time += Time.deltaTime;
        transform.position = startPos + new Vector3(Mathf.Sin(Time.time * horizontalSpeed) * 0.5f, time * verticalSpeed, 0);
    }

    void CosMovement()
    {
        time += Time.deltaTime;
        transform.position = startPos + new Vector3(Mathf.Cos(Time.time * horizontalSpeed) * 0.5f, time * verticalSpeed, 0);
    }
}
