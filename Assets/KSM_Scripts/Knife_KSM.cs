using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Knife_KSM : MonoBehaviour
{
    public Type type;
    public enum Type { Knife, Gun };
    public int damage = 3;
    public float rate = 4f;

    public BoxCollider knifeArea;
    public ParticleSystem[] hitEffect;

    private bool hasPlayedHitEffectThisSwing;
    private List<Monster_KSM> hitMonstersList;

    private void Awake()
    {
        hitMonstersList = new List<Monster_KSM>();
    }

    public void Use()
    {
        if (type == Type.Knife)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        hitMonstersList.Clear();
        hasPlayedHitEffectThisSwing = false;
        knifeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        knifeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster_KSM monster = other.GetComponentInParent<Monster_KSM>();

        if (monster == null || hitMonstersList.Contains(monster))
        {
            return;
        }

        if (monster != null)
        {
            bool hitRegistered = false;
            bool isWeakPoint = false;

            if (other == monster.weakPointCollider)
            {
                isWeakPoint = true;
                hitRegistered = true;
            }
            else if (other == monster.hitCollider)
            {
                isWeakPoint = false;
                hitRegistered = true;
            }

            if (hitRegistered)
            {
                monster.TakeDamage(damage, transform.root, isWeakPoint);
                hitMonstersList.Add(monster);

                if (!hasPlayedHitEffectThisSwing)
                {
                    Vector3 hitPoint = other.ClosestPoint(transform.position);
                    HitParticles(hitPoint);
                    hasPlayedHitEffectThisSwing = true;
                }
            }
        }
    }

    public void HitParticles(Vector3 position)
    {
        foreach (ParticleSystem ps in hitEffect)
        {
            if (ps != null)
            {
                ps.transform.position = position;

                ps.Play();
            }
        }
    }
}