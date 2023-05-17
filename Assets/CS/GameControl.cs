using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    float midLinePosX;

    int sorting = 0;
    bool isDrag = false;
    Transform dragObject = null;

    Vector2 startingPos;
    Vector2 saveCamPos; 
    Vector2 moveOffset;
    void Awake()
    {
        
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
            isDrag = false;
            dragObject = null;
        }

        // �巡������ ������Ʈ�� ���� �� ����
        if (dragObject == null) return;

        // �巡��
        if (isDrag)
        {
            Vector2 nowCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveOffset = nowCamPos - saveCamPos;

            // �̵�
            dragObject.position = startingPos + moveOffset;

            // �巡�׿� ���� �̹��� ũ�� ��ȯ, �۾��� / Ŀ��
            if (dragObject.position.x < midLinePosX)
            {
                if (nowCamPos.x < midLinePosX)
                {
                    dragObject.GetComponent<Word>().isRight = false;

                    if (startingPos.x > midLinePosX)
                    {
                        dragObject.GetComponent<Word>().StateChange();
                        dragObject.position = nowCamPos;
                    }
                }
            }

            if (nowCamPos.x > midLinePosX)
                dragObject.GetComponent<Word>().isRight = true;
        }
    }

    void Cast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Ray.origin, Ray.direction * 10, Color.red, 0.2f);
            RaycastHit2D hit = Physics2D.Raycast(Ray.origin, Ray.direction, 1000);

            if (hit)
            {
                if (hit.collider.CompareTag("WORD") && !isDrag)
                {
                    isDrag = true;
                    dragObject = hit.collider.gameObject.transform.parent;

                    sorting++;
                    dragObject.transform.GetChild(1).gameObject.
                        transform.GetChild(0).GetComponent<Canvas>().sortingOrder = sorting;

                    saveCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    startingPos = dragObject.position;
                }
            }
        }
    }       // ����ĳ��Ʈ
}
