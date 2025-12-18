using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakCast : CreatureBase
{
    //public enum State { IDLE, DIE }
    //public State currentState;
    private const int _damage = 999;

    public override void OnDead()
    {
        Destroy(gameObject,1.0f); //뒤에 float는 적용 이후 얼마 후에 터질것인가를 나타냄
    }

    private void OnDisable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 4.0f);
        for(int i=0; i<hits.Length; i++)
        {
            if (hits[i].TryGetComponent<CreatureController_KSM>(out CreatureController_KSM c)==true)
            {
                c.OnDamaged(_damage, transform, false);
#if UNITY_EDITOR
                Debug.Log("대미지줌");
#endif
            }
        }
    }
    //private void ChangeState(State newState)
    //{
    //    if (currentState == State.DIE) return;

    //    StopAllCoroutines();
    //    currentState = newState;

    //    StartCoroutine(currentState.ToString());
    //}
    //public IEnumerator DIE()
    //{

    //    //anim.SetTrigger("Die");

    //    yield return new WaitForSeconds(4f);

    //    Destroy(gameObject);
    //}
}
