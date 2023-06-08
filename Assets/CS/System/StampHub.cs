using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampHub : MonoBehaviour
{
    bool isOpen = false;

    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PopupUI()
    {
        isOpen = !isOpen;

        anim.SetInteger("isOpen", isOpen == true ? 1 : 2);

        if (isOpen)
        {

        }
        else
        {

        }
    }
}
