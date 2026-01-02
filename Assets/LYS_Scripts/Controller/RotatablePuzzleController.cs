using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LYS_Work.Controller
{
    public class RotatablePuzzleController : MonoBehaviour
    {
        private Vector3 _clickedPos;
        private Vector3 _rotationStartPos;
        private Vector3 _accumulatedRotation;

        void Awake()
        {
            _accumulatedRotation = transform.localEulerAngles;
        }
        public void DoUpdate()
        {
            if (Input.GetMouseButton(0) == false)
            {
                _clickedPos = Input.mousePosition;
                _rotationStartPos = _accumulatedRotation;
                return;
            }

            float dy = (Input.mousePosition.x - _clickedPos.x) / (_clickedPos.x + Screen.width)*-1;
            float dx = (Input.mousePosition.y - _clickedPos.y) / (_clickedPos.y + Screen.height);

            _accumulatedRotation.x = Mathf.LerpUnclamped(_rotationStartPos.x, _rotationStartPos.x + 360, dx);
            _accumulatedRotation.y = Mathf.LerpUnclamped(_rotationStartPos.y, _rotationStartPos.y + 360, dy);
            transform.localRotation = Quaternion.Euler(_accumulatedRotation);
        }
        

    }
}
