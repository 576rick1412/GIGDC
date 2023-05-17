using UnityEngine;

public class Word : MonoBehaviour
{
    [HideInInspector]
    public bool isRight = false;

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
    }

    public void StateChange()
    {
        smallObject.SetActive(!isRight);
        bigObject.SetActive(isRight);
    }
}
