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
            if(dragObject != null) 
                dragObject.GetComponent<Word>().isDrag = false;

            isDrag = false;
            dragObject = null;
        }

        // 드래그중인 오브젝트가 없을 시 리턴
        if (dragObject == null) return;

        // 드래그
        if (isDrag)
        {
            ref bool isRight = ref dragObject.GetComponent<Word>().isRight;
            float tableEndPosY = dragObject.GetComponent<Word>().tableEndPosY;

            Vector2 nowCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveOffset = nowCamPos - saveCamPos;

            Move();

            // 드래그에 따른 이미지 크기 변환, 작아짐 / 커짐
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
        // 이동
        dragObject.position = startingPos + moveOffset;

        // 화면 밖 예외처리
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
            RaycastHit2D hit = Physics2D.Raycast(Ray.origin, Ray.direction, 1000);

            if (hit)
            {
                if (hit.collider.CompareTag("WORD") && !isDrag)
                {
                    isDrag = true;
                    dragObject = hit.collider.gameObject.transform.parent;

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
        }
    }       // 레이캐스트
}
