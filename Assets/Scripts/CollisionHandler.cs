using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("OK");
                break;
            case "Finish":
                Debug.Log("Level Complete");
                break;
            case "Fuel":
                Debug.Log("Picked up fuel");
                break;
            default:
                Debug.Log("Dead");
                break;
        }
    }
}