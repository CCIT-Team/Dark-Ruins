


using LYS_Work.Controller;
using UnityEngine;

namespace LYS_Work.Manager
{
    public class RotatablePuzzleManager : MonoBehaviour
    {
        private Transform _savedParentObj;
        private Quaternion _savedQuaternion;
        private Quaternion _savedLocalQuaternion;
        private Vector3 _savedPosition;
        private Vector3 _savedLocalPosition;
        private Vector3 _savedLocalScale;

        private LYS_Work.Token.Token _token;
        private LYS_Work.Token.Token _key;
        private RotatablePuzzleController _controller;

        void Awake()
        {
            _token = _key = new Token.Token();
            _controller = GetComponent<RotatablePuzzleController>();
        }

        void Update()
        {
            if (_token == _key)
            {
                return;
            }
            _controller.DoUpdate();
        }

        public bool DoPuzzle(Transform trackTarget, int scaleSize, ref LYS_Work.Token.Token outRefToken)
        {
            
            if (_token is null)
            {
                Debug.LogError("토큰 획득 실패로 인한 퍼즐 시작 실패");
                return false;
            }

            _savedParentObj = transform.parent;
            _savedQuaternion = transform.rotation;
            _savedLocalQuaternion = transform.localRotation;
            _savedPosition = transform.position;
            _savedLocalPosition = transform.localPosition;
            _savedLocalScale = transform.localScale;

            transform.localScale *= scaleSize;

            outRefToken = _token;
            _token = null;
            return true;
        }

        public bool EndPuzzle(LYS_Work.Token.Token token)
        {
            if (_token is not null && _key != token)
            {
                return false;
            }

            _token = token;

            transform.SetParent(_savedParentObj);
            if (_savedParentObj is null)
            {
                transform.SetPositionAndRotation(_savedPosition, _savedQuaternion);
            }
            else
            {
                transform.SetLocalPositionAndRotation(_savedLocalPosition, _savedLocalQuaternion);
            }

            transform.localScale = _savedLocalScale;
            _savedParentObj = null;

            return true;
        }
    }
}