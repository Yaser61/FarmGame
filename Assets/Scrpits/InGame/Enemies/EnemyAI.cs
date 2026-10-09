using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAI : MonoBehaviour, IA_Hitable
{
    [SerializeField] private EnemyState currentState = EnemyState.ControlState;
    [SerializeField] private FarmTile currentTarget;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private int totalHealth = 5;
    [SerializeField] private Material hitMaterial;
    private Material defaultMaterial;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int currentHealth;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = totalHealth;
        defaultMaterial = spriteRenderer.material;
    }
    private void Update() {
        EnemyProcess();
    }
    public void EnemyProcess()
    {
        switch (currentState)
        {
            case EnemyState.ControlState:
                currentTarget = FindClosestTile();
                animator.SetBool("isWalking", false);
                if (currentTarget) currentState = EnemyState.ChaseState;
                break;

            case EnemyState.ChaseState:
                if (currentTarget.currentPlant == null) currentState = EnemyState.ControlState;

                animator.SetBool("isWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, currentTarget.transform.position) <= 0.1f)
                {
                    currentState = EnemyState.AttackState;
                }
                break;

            case EnemyState.AttackState:
            animator.SetBool("isWalking", false);
                if (currentTarget.currentPlant == null)
                {
                    currentState = EnemyState.ControlState;
                    if(attackControlCoroutine != null)
                    {
                        StopCoroutine(attackControlCoroutine);
                        attackControlCoroutine = null;
                    }
                }
                else attackControlCoroutine ??= StartCoroutine(AttackControl());
                break;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private Coroutine attackControlCoroutine;

    private IEnumerator AttackControl()
    {
        yield return new WaitForSeconds(attackTime);
        currentTarget?.RemovePlant();
        attackControlCoroutine = null;
    }

    private FarmTile FindClosestTile()
    {
        float closestDistance = 9999;
        FarmTile newTarget = null;
        foreach (FarmTile item in FarmManager.Instance.plantedTiles)
        {
            float newDistance = Vector2.Distance(transform.position, item.transform.position);
            if (newDistance <= closestDistance)
            {
                closestDistance = newDistance;
                newTarget = item;
            }
        }
        return newTarget;
    }

    public GameObject Hit(GameObject from, int damage)
    {
        if (currentState == EnemyState.DeadState) return gameObject;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        if (currentHealth == 0)
        {
            Debug.Log("Slime bayıldı");
            animator.SetTrigger("dead");
            currentState = EnemyState.DeadState;
        }
        else
        {
            hitEffectCoroutine ??= StartCoroutine(HitEffect());
        }
        return gameObject;
    }

    private Coroutine hitEffectCoroutine;

    private IEnumerator HitEffect()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
        hitEffectCoroutine = null;
    }
}
[System.Serializable]
public enum EnemyState
{
    ControlState,
    ChaseState,
    AttackState,
    DeadState
}
