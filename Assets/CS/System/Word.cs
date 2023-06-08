using UnityEngine;

public class Word : MonoBehaviour
{
    [HideInInspector]
    public bool isRight = false;

    [HideInInspector]
    public bool isDrag = false;

    [HideInInspector]
    public float tableEndPosY = -3.5f;

    [SerializeField] GameObject smallObject;
    [SerializeField] GameObject bigObject;

    void Start()
    {
        smallObject = transform.GetChild(0).gameObject;
        bigObject   = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        StateChange();

        // 문서 아래로 떨어지도록.....
        if(!isDrag && !isRight)
        {
            var POS = transform.position;

            if (POS.y != tableEndPosY)
            {
                int N = 100;
                if (POS.y > tableEndPosY) N *= -1;
                
                Vector3 gravity = new Vector2(0f, N);
                transform.position += gravity * Time.deltaTime;

                if (POS.y < tableEndPosY)
                    transform.position = new Vector3(POS.x, tableEndPosY, 0);
            }
        }
    }

    public void StateChange()
    {
        smallObject.SetActive(!isRight);
        bigObject.SetActive(isRight);
    }
}
