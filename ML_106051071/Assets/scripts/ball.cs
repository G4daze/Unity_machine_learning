using UnityEngine;

public class ball : MonoBehaviour
{
    public static bool complete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "進球感應")
        {
            complete = true;
        }


    }



}
