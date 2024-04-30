using UnityEngine;

public class LevelKeyLogic : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            GameManager.Instance.CollectibleTaken();
            Destroy(gameObject);
        }
    }
}
