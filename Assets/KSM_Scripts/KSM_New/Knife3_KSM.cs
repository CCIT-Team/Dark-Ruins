using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife3_KSM : ItemBase, IWeapon_KSM
{
    [SerializeField] private float _rate = 1.5f;
    public float rate { get { return _rate; } }

    [Header("공격 설정")]
    public BoxCollider knifeArea;

    private HashSet<IDamageable_KSM> hitTargets = new HashSet<IDamageable_KSM>();

    public enum Type { Knife };
    public Type type = Type.Knife;
    public int damage = 30;
    public bool isAttacking = false;

    protected override void Start()
    {
        base.Start();
        hitTargets = new HashSet<IDamageable_KSM>();

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
        hitTargets.Clear();

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
        if (damageable == null) return;
        if (hitTargets.Contains(damageable)) return;

        bool isWeakPoint = other.GetComponent<WeakPoint_KSM>() != null;

        if (isWeakPoint)
        {
            damageable.OnDamaged(damage, transform.root, true);
        }
        else
        {
            damageable.OnDamaged(damage, transform.root, false);
        }

        hitTargets.Add(damageable);
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        PlayBloodEffect(hitPoint);
    }

    private void PlayBloodEffect(Vector3 position)
    {
        if (BloodPoolManager_KSM.Instance == null) return;

        Vector3 direction = (transform.position - position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        BloodPoolManager_KSM.Instance.PlayBloodEffect(position, rotation);
    }
}