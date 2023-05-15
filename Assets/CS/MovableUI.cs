using UnityEngine;
using UnityEngine.EventSystems;

// 헤더 드래그 앤 드롭에 의한 UI 이동
public class MovableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    Transform _targetTr; // 이동될 UI

    Vector2 _startingPoint;
    Vector2 _moveBegin;
    Vector2 _moveOffset;

    private void Awake()
    {
        // 이동 대상 UI를 지정하지 않은 경우, 자동으로 부모로 초기화
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // 드래그 :  시작 위치 지정
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // 최한단으로 내림 ( 화면에서 해당 오브젝트를 최상단으로... )
        transform.SetAsLastSibling();

        _startingPoint = _targetTr.position;
        _moveBegin = eventData.position;
    }

    // 드래그 : 마우스 커서 위치로 이동
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _moveBegin;

        int nx = 1050, ny = 700;
        if (_startingPoint.x + _moveOffset.x > (Screen.width / 2) + nx) return;
        if (_startingPoint.x + _moveOffset.x < (Screen.width / 2) - nx) return;

        if (_startingPoint.y + _moveOffset.y > (Screen.height / 2) + ny) return;
        if (_startingPoint.y + _moveOffset.y < (Screen.height / 2) - ny) return;

        // 이동
        _targetTr.position = _startingPoint + _moveOffset;
    }
}
