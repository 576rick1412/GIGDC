using UnityEngine;
using UnityEngine.EventSystems;

// ��� �巡�� �� ��ӿ� ���� UI �̵�
public class MovableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    Transform _targetTr; // �̵��� UI

    Vector2 _startingPoint;
    Vector2 _moveBegin;
    Vector2 _moveOffset;

    private void Awake()
    {
        // �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // �巡�� :  ���� ��ġ ����
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // ���Ѵ����� ���� ( ȭ�鿡�� �ش� ������Ʈ�� �ֻ������... )
        transform.SetAsLastSibling();

        _startingPoint = _targetTr.position;
        _moveBegin = eventData.position;
    }

    // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _moveBegin;

        int nx = 1050, ny = 700;
        if (_startingPoint.x + _moveOffset.x > (Screen.width / 2) + nx) return;
        if (_startingPoint.x + _moveOffset.x < (Screen.width / 2) - nx) return;

        if (_startingPoint.y + _moveOffset.y > (Screen.height / 2) + ny) return;
        if (_startingPoint.y + _moveOffset.y < (Screen.height / 2) - ny) return;

        // �̵�
        _targetTr.position = _startingPoint + _moveOffset;
    }
}
