


using System;
using System.Collections;
using UnityEngine;

namespace LYS_Work
{
    public class DoorController : ItemPuzzleCompletedDetector
    {
        [SerializeField]
        private float _toMove=6;
        [SerializeField]
        private float _movRate=0.01f;
        private float _movTime=1f;
        void Awake()
        {
            _toMove*=_movRate;
        }
        [ContextMenu("fuc")]
        public override void DetectPuzzleComplete()
        {
            StartCoroutine(MoveRoutine());
        }
        private IEnumerator MoveRoutine()
        {
            while(_movTime >= 0)
            {
                yield return null;
                _movTime -= _movRate;
                var pos = transform.position;
                pos.y += _toMove;
                transform.position = pos;
            }
        }
    }
}