using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife3_KSM : ItemBase, IWeapon_KSM
{
    [Header("공격 설정")]
    public BoxCollider knifeArea;
    public ParticleSystem[] hitEffect;

    [SerializeField] private float _rate = 1.5f;
    public float rate { get { return _rate; } }

    public enum Type { Knife };
    public Type type = Type.Knife;
    public int damage = 30;
    public bool isAttacking = false;

    private List<IDamageable_KSM> hitTargetsList = new List<IDamageable_KSM>();

    protected override void Start()
    {
        base.Start();
        hitTargetsList = new List<IDamageable_KSM>();

        if (knifeArea != null) knifeArea.enabled = false;
    }

    public void Use()
    {
        if (!gameObject.activeInHierarchy || isAttacking) return;

        StartCoroutine(Swing());
    }

    public override void ItemUse(List<KeyCode> keys)
    {
        if (keys.Contains(KeyCode.Mouse0))
        {
            Use();
        }
    }

    IEnumerator Swing()
    {
        isAttacking = true;
        hitTargetsList.Clear();

        yield return new WaitForSeconds(0.2f);
        if (knifeArea != null) knifeArea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        if (knifeArea != null) knifeArea.enabled = false;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable_KSM damageable = other.GetComponentInParent<IDamageable_KSM>();
        if (other.isTrigger) return;
        if (other.CompareTag("Player")) return;
        if (damageable == null || hitTargetsList.Contains(damageable)) return;

        bool isWeakPoint = other.GetComponent<WeakPoint_KSM>() != null;

        damageable.OnDamaged(damage, transform.root, isWeakPoint);
        hitTargetsList.Add(damageable);

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        HitParticles(hitPoint);
    }

    public void HitParticles(Vector3 position)
    {
        if (hitEffect == null) return;
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