using UnityEngine;

public class FinishLVLTrigger : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private UIManager _UIManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            _UIManager.Winner();
        }
    }

}
