using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife_KSM : MonoBehaviour
{
    public Type type;
    public enum Type { Knife, Gun };
    public int damage = 3;
    public float rate = 4f;

    public BoxCollider knifeArea;
    public ParticleSystem[] hitEffect;

    private List<Collider> hitList;

    private void Awake()
    {
        hitList = new List<Collider>();
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
        hitList.Clear();
        knifeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        knifeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitList.Contains(other))
        {
            return;
        }

        Monster_KSM monster = other.GetComponent<Monster_KSM>();

        if (monster != null && other == monster.hitCollider)
        {
            monster.TakeDamage(damage, transform.root);

            hitList.Add(other);

            HitParticles();
        }
    }

    public void HitParticles()
    {
        foreach (ParticleSystem ps in hitEffect)
        {
            ps.Play();
        }
    }
}