using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    float midLinePosX;

    int sorting = 0;
    bool isDrag = false;
    Transform dragObject = null;

    Vector2 startingPos;
    Vector2 saveCamPos; 
    Vector2 moveOffset;

    [SerializeField] Button yes_Button;
    [SerializeField] Button no_Button;

    void Awake()
    {
        yes_Button.onClick.AddListener(() => Yes_Button());
        no_Button .onClick.AddListener(() =>  No_Button());
    }

    void Start()
    {
        midLinePosX = GameObject.Find("Line").transform.position.x;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Cast();
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(dragObject != null) 
                dragObject.GetComponent<Word>().isDrag = false;

            isDrag = false;
            dragObject = null;
        }

        // �巡������ ������Ʈ�� ���� �� ����
        if (dragObject == null) return;

        // �巡��
        if (isDrag)
        {
            ref bool isRight = ref dragObject.GetComponent<Word>().isRight;

            Vector2 nowCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveOffset = nowCamPos - saveCamPos;

            Move();

            // �巡�׿� ���� �̹��� ũ�� ��ȯ, �۾��� / Ŀ��
            if (dragObject.position.x < midLinePosX)
            {
                if (nowCamPos.x < midLinePosX)
                {
                    isRight = false;

                    if (startingPos.x > midLinePosX)
                    {
                        dragObject.GetComponent<Word>().StateChange();
                        dragObject.position = nowCamPos;
                    }
                }
            }

            if (nowCamPos.x > midLinePosX)
                isRight = true;
        }
    }

    void Move()
    {
        // �̵�
        dragObject.position = startingPos + moveOffset;

        // ȭ�� �� ����ó��
        float endlineX = 8.8f, endlineY = 5f;
        var POS = dragObject.position;

        Vector3 AutoSet = new Vector3(POS.x, POS.y, POS.z);

        if (dragObject.position.x > endlineX     ) AutoSet.x = endlineX;
        if (dragObject.position.x < endlineX * -1) AutoSet.x = endlineX * -1;
        if (dragObject.position.y > endlineY     ) AutoSet.y = endlineY;
        if (dragObject.position.y < endlineY * -1) AutoSet.y = endlineY * -1;

        dragObject.position = AutoSet;
    }

    void Cast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Ray.origin, Ray.direction * 10, Color.red, 0.2f);
            RaycastHit2D hit = Physics2D.Raycast(Ray.origin, Ray.direction, 1000,LayerMask.GetMask("Cast_Skip"));

            if (hit)
            {
                if (hit.collider.CompareTag("WORD") && !isDrag)
                {
                    isDrag = true;
                    dragObject = hit.collider.gameObject.transform.parent;

                    Cast_Word(hit.collider.gameObject.transform.parent);
            }
        }
    }       // ����ĳ��Ʈ

    void Cast_Word(Transform hitObject)
    {
            isDrag = true;
            dragObject = hitObject;

            sorting++;

            dragObject.transform.GetChild(0).gameObject.
                GetComponent<SpriteRenderer>().sortingOrder = sorting;

            dragObject.transform.GetChild(1).gameObject.
                transform.GetChild(0).GetComponent<Canvas>().sortingOrder = sorting;

            dragObject.GetComponent<Word>().isDrag = true;

            saveCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startingPos = dragObject.position;
        }
    }

    public void Yes_Button()
    {
        Debug.Log("����");
    }

    public void No_Button()
    {
        // �������ø���� ���ε����� �ƹ��� �� �ź� ���� �ϳ��� �ִٸ� �źη� �Ǵܵ�
        Debug.Log("�ź�");
    }
}
