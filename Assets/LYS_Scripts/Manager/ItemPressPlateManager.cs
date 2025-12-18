using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LYS_Work
{
    public class ItemPressPlateManager : MonoBehaviour
    {
        [SerializeField]
        private string keyname;
        [SerializeField]
        ItemPuzzleCompletedDetector[] targets;
        public event Action OnPuzzleCompletedAction;
        private bool _completed = false;
        void Start()
        {
            foreach(var t in targets)
            {
                OnPuzzleCompletedAction += t.DetectPuzzleComplete;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if(!_completed && collision.gameObject.name == keyname)
            {
                OnPuzzleCompletedAction?.Invoke();
                _completed = true;
            }
        }
    }

}