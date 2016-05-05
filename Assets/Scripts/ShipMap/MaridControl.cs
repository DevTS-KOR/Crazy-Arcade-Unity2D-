using UnityEngine;
using System.Collections;

public class MaridControl : MonoBehaviour {

    public void MoveLeft()
    {
        this.transform.Translate(new Vector3(-FirstPlayerInfo.fSpeed, 0.0f, 0.0f * Time.deltaTime));
    }

    public void MoveRight()
    {
        this.transform.Translate(new Vector3(FirstPlayerInfo.fSpeed, 0.0f, 0.0f * Time.deltaTime));
    }

    public void MoveUp()
    {
        this.transform.Translate(new Vector3(0.0f, FirstPlayerInfo.fSpeed, 0.0f * Time.deltaTime));
    }

    public void MoveDown()
    {
        this.transform.Translate(new Vector3(0.0f, -FirstPlayerInfo.fSpeed, 0.0f * Time.deltaTime));
    }
}
