using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelComplete();
        }
    }
}