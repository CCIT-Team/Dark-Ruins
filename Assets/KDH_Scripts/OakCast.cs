using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakCast : CreatureBase
{
    //public enum State { IDLE, DIE }
    //public State currentState;
    private const int _damage = 30;
    private bool _bombed = false;
    public override void OnDead()
    {
        if(_bombed==true)
        {
            return;
        }
        _bombed = true;
        Managers_YGU.Sound.Play3D("Barrel_Explosion", this.transform.position);
        transform.GetChild(0).gameObject.SetActive(false);
        ParticleSystem _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particleSystem.Simulate(0f, true, true);
        _particleSystem.Play();
        Collider[] hits = Physics.OverlapSphere(transform.position, 4.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent<CreatureController_KSM>(out CreatureController_KSM c) == true)
            {
                c.OnDamaged(_damage, transform, false);
#if UNITY_EDITOR
                Debug.Log("대미지줌");
#endif
            }
        }

        Destroy(gameObject,2f); //뒤에 float는 적용 이후 얼마 후에 터질것인가를 나타냄
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
