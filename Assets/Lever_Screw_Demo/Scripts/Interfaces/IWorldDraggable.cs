using UnityEngine;

namespace GD
{
    public interface IWorldDraggable
    {
        void OnDrag(Vector3 worldPosition);
    }
}