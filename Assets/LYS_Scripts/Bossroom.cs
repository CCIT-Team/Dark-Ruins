using System.Collections;
using System.Collections.Generic;
using LYS_Work;
//using UnityEditor.Animations;
using UnityEngine;

public class Bossroom : MonoBehaviour
{
    [SerializeField]
    List<DoorController> _doors;

    [SerializeField]
    List<GameObject> _activationTargets;

    bool _isActivated;

    [SerializeField]
    float _activationInterval;

    [SerializeField]
    Animator anim;

    IEnumerator Activate()
    {
        foreach(var d in _doors)
        {
            d.DetectPuzzleComplete(false);
        }

        foreach(var t in _activationTargets)
        {
            t.SetActive(true);
            yield return new WaitForSeconds(_activationInterval);
        }

        anim.gameObject.SetActive(true);
        anim.Play("Landing");
        
        
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(_isActivated)
        {
            return;
        }
        if(collision.gameObject.tag == "Player")
        {
            gameObject.transform.position = new Vector3(-100,-100,-100);
            _isActivated = true;
            StartCoroutine(Activate());
        }
    }
}
