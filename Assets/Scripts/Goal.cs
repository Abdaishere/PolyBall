using UnityEngine;

public class Goal : MonoBehaviour
{
    
    void Start()
    {
        // set collider radius
        var radius = Main.Radius - Main.LineWidth;
        transform.localScale = new Vector3(radius, radius);
    }
    
    void Update()
    {
        if (Main.GameStarted == false)
        {
            
        }
    }
}
