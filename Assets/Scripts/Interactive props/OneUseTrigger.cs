using UnityEngine;

public class OneUseTrigger : MonoBehaviour
{
    public delegate void TriggerEnterHandler();
    public event TriggerEnterHandler OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            OnTriggerEnterEvent?.Invoke();
        }
    }
}
