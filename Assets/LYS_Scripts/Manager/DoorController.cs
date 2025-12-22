


using System;
using System.Collections;
using UnityEngine;

namespace LYS_Work
{
    public class DoorController : ItemPuzzleCompletedDetector
    {
        private Vector3 _targetPos;
        private Vector3 _startpos;
        private float _movTime=1f;
        void Awake()
        {
            _targetPos = transform.position;
            _targetPos.y -= transform.localScale.y;
            _startpos = transform.position;
        }
        public override void DetectPuzzleComplete()
        {
            StartCoroutine(MoveRoutine());
        }
        private IEnumerator MoveRoutine()
        {
            while(_movTime >= 0)
            {
                yield return null;
                _movTime -= 0.01f;
                if(_movTime <= 1)
                {
                    transform.position = Vector3.LerpUnclamped(_targetPos,_startpos,_movTime);
                }
            }
            gameObject.SetActive(false);
            
        }
    }
}