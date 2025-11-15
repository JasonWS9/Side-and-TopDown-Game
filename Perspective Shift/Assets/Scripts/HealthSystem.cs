using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private int health = 10;



    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
