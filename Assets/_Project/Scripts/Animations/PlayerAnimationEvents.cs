using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;
    [SerializeField] private PlayerShootController _shooter;

    private void Awake()
    {
        if (_pc == null) _pc = GetComponentInParent<PlayerController>();
        if (_shooter == null) _shooter = GetComponentInParent<PlayerShootController>();
    }

    public void AE_DestroygameObject()
    {
        if (_pc == null) return;
        _pc.DestroyGOPlayer();
    }

    public void AE_FootStepSfx()
    {
        _pc.FootStepSound();
    }

    public void AE_Shoot()
    {
        _shooter.TryToShoot();
    }
}
