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
    public ParticleSystem hitEffect;

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
        knifeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        knifeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster_KSM monster = other.GetComponent<Monster_KSM>();

        if (monster != null && other == monster.hitCollider)
        {
            monster.TakeDamage(damage, transform.root);

            //if (hitEffect != null)
            //{
            //    hitEffect.transform.position = other.ClosestPoint(transform.position);
            //    hitEffect.Play();
            //}
        }
    }
}