using System;
using TMPro;
using UnityEngine;

namespace GD
{
    public class RotateObjectValueDrawer : MonoBehaviour
    {
        [SerializeField] private RotateObjectController _rotateObjectController;
        [SerializeField] private TMP_Text _rawDataText;
        [SerializeField] private TMP_Text _normalizedDataText;

        private void Update()
        {
            _rawDataText.text = $"raw data: {_rotateObjectController.Value}";
            _normalizedDataText.text = $"normalized data: {_rotateObjectController.NormalizedValue}";
        }
    }
}