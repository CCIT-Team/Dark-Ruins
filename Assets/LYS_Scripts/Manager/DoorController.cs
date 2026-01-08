


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
        [SerializeField]
        string _soundname;

        Vector3 _initialPos;

        void Awake()
        {
            _toMove*=_movRate;
            _initialPos = transform.position;
        }
        [ContextMenu("fuc")]
        public override void DetectPuzzleComplete(bool IsUp)
        {
            if(IsUp && _toMove < 0)
            {
                _toMove *=-1;
            }
            else if(IsUp==false && _toMove > 0)
            {
                _toMove *=-1;
            }
            StartCoroutine(MoveRoutine(IsUp));
        }
        private IEnumerator MoveRoutine(bool isUp)
        {
            if(_soundname is not null)
            {
                Managers_YGU.Sound.Play(_soundname, eSound.UI);

            }
            float acc = 0;

            while(acc <= _movTime)
            {
                yield return null;
                acc += _movRate;
                var pos = transform.position;
                pos.y += _toMove;
                transform.position = pos;

                if((isUp == false) && pos.y < _initialPos.y)
                {
                    transform.position = _initialPos;
                    break;
                }

            }

            if(isUp==false)
            {
                transform.position = _initialPos;
            }    
        }
    }
}