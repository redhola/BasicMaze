using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player'ın transformunu buraya sürükleyin.
    public Vector3 offset; // Bu değeri ayarlayarak kameranın pozisyonunu ince ayar yapabilirsiniz.

    void Update()
    {
        // Kamera her frame'de player'ın pozisyonunu takip eder, verilen offset değeri ile ayarlanır.
        transform.position = player.position + offset;
    }
}
