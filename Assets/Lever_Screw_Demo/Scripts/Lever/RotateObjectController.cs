using UnityEngine;

namespace GD
{
    public class RotateObjectController : MonoBehaviour, IWorldDraggable
    {
        [SerializeField] private Transform _leverRoot;
        [SerializeField] private Axis _localAxis;
        [SerializeField] private float _startValue;
        [SerializeField] private Vector2 _constraints;

        private Quaternion _lastRotation;
        private float _value;
        public float Value => _value;
        public float NormalizedValue => (_value - _constraints.x) / (_constraints.y - _constraints.x);

        private void Start()
        {
            _value =  Mathf.Clamp(_startValue, _constraints.x, _constraints.y);
            _leverRoot.localRotation = Quaternion.Euler(GetAxis(_localAxis) * _value);
            _lastRotation = _leverRoot.localRotation;
        }
        
        public void OnDrag(Vector3 worldPosition)
        {
            var localPosition = _leverRoot.parent.InverseTransformPoint(worldPosition);
            var leverRootLocalPosition = _leverRoot.localPosition;
            var localAxis = GetAxis(_localAxis);
            var plane = new Plane(localAxis, leverRootLocalPosition);
            var pos = plane.ClosestPointOnPlane(localPosition);
            var rotation = Quaternion.LookRotation(
                localAxis,
                (pos - leverRootLocalPosition).normalized);
            _value += CalculateValueDelta(rotation, localAxis);
            _leverRoot.localRotation = ClampAngle(rotation, localAxis);
            _lastRotation = _leverRoot.localRotation;
        }
        
        private float CalculateValueDelta(Quaternion rotation, Vector3 axis)
        {
            return Vector3.SignedAngle(_lastRotation * Vector3.up, rotation * Vector3.up, axis);
        }
        
        private Quaternion ClampAngle(Quaternion rot, Vector3 axis)
        {
            if (_value <= _constraints.x)
            {
                _value = _constraints.x;
                return Quaternion.Euler(axis * _constraints.x);
            }
            if (_value >= _constraints.y)
            {
                _value = _constraints.y;
                return Quaternion.Euler(axis * _constraints.y);
            }

            return rot;
        }
        
        private Vector3 GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.x:
                    return new Vector3(1, 0, 0);
                case Axis.z:
                    return new Vector3(0, 0, 1);
            }
            return new Vector3();
        }
    }
    
    public enum Axis
    {
        x,
        z
    }
}

