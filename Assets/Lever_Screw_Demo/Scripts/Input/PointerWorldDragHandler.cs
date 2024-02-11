using UnityEngine;
using UnityEngine.EventSystems;

namespace GD
{
    [RequireComponent(typeof(IWorldDraggable))]
    public class PointerWorldDragHandler : MonoBehaviour, IDragHandler
    {
        private IWorldDraggable _worldDraggable;
        private void Awake()
        {
            _worldDraggable = GetComponent<IWorldDraggable>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == null) return;
            _worldDraggable.OnDrag(eventData.pointerCurrentRaycast.worldPosition);
        }
    }
}

