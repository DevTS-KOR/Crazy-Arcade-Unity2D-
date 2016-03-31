using UnityEngine;
using System.Collections;

public class OpenLogo : MonoBehaviour {

    private float StartTime;
    private float EndTime;
    // Use this for initialization
    void Start ()
    {
        StartTime = Time.deltaTime;
    }

    void Update()
    {
        EndTime += Time.deltaTime;

        print(EndTime - StartTime);
        if(EndTime - StartTime > 3f)
        {
            Application.LoadLevel("Opening");
        }
    }

}
