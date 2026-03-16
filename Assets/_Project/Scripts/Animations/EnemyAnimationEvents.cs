using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyController _ec;

    private void Awake()
    {
        if (_ec == null) _ec = GetComponentInParent<EnemyController>();
    }

    public void AE_DestroygameObject()
    {
        if (_ec == null) return;
        _ec.DestroyGOEnemy();
    }

}
