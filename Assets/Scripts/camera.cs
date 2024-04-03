using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // Takip edilecek karakterin Transform bileşeni

    void Update()
    {
        if (target != null)
        {
            transform.position =new Vector3(target.position.x,target.position.y,transform.position.z); // Kameranın pozisyonunu karakterin pozisyonuna eşitle
        }
    }
}