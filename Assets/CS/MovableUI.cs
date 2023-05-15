using UnityEngine;
using UnityEngine.EventSystems;

// ��� �巡�� �� ��ӿ� ���� UI �̵�
public class MovableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform _targetTr; // �̵��� UI

    private Vector2 _startingPoint;
    private Vector2 _moveBegin;
    private Vector2 _moveOffset;

    private void Awake()
    {
        // �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // �巡�� ���� ��ġ ����
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _startingPoint = _targetTr.position;
        _moveBegin = eventData.position;
    }

    // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _moveBegin;

        if (_startingPoint.x + _moveOffset.x > (Screen.width / 2) + 1050) return;
        if (_startingPoint.x + _moveOffset.x < (Screen.width / 2) - 1050) return;

        if (_startingPoint.y + _moveOffset.y > (Screen.height / 2) + 700) return;
        if (_startingPoint.y + _moveOffset.y < (Screen.height / 2) - 700) return;

        // �̵�
        _targetTr.position = _startingPoint + _moveOffset;
    }
}
