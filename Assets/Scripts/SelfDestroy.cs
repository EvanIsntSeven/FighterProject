using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
public float destructionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, destructionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
