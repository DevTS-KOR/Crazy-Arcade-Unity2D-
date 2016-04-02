using UnityEngine;
using System.Collections;

public class ChildCharSelect : MonoBehaviour {

	public GameObject DaoObject;
	public GameObject MaridObject;

    DaoControl PlayerDao_1;
    MaridControl PlayerMarid_1;
    // Use this for initialization
    void Start () {
		
		if (FirstPlayerInfo.strCharName == "Dao") 
		{
            //Player1에 Hierarchy의 Dao오브젝트가 가지고 있는 DaoControl 스크립트를 가져온다.
            PlayerDao_1 = GameObject.Find("Dao").GetComponent<DaoControl>();

			//선택된 캐릭터 활성화 나머지 캐릭터 비활성화.
			DaoObject.SetActive (true);
			MaridObject.SetActive (false);
		}
        else if(FirstPlayerInfo.strCharName == "Marid")
        {
            //Player1에 Hierarchy의 Dao오브젝트가 가지고 있는 DaoControl 스크립트를 가져온다.
            PlayerMarid_1 = GameObject.Find("Marid").GetComponent<MaridControl>();

            //선택된 캐릭터 활성화 나머지 캐릭터 비활성화.
            DaoObject.SetActive(false);
            MaridObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
            if (FirstPlayerInfo.strCharName == "Dao")
                PlayerDao_1.MoveLeft ();
            else if (FirstPlayerInfo.strCharName == "Marid")
                PlayerMarid_1.MoveLeft();
        }

		else if (Input.GetKey (KeyCode.RightArrow)) 
		{
            if (FirstPlayerInfo.strCharName == "Dao")
                PlayerDao_1.MoveRight ();
            else if (FirstPlayerInfo.strCharName == "Marid")
                PlayerMarid_1.MoveRight();
        }

		else if (Input.GetKey (KeyCode.UpArrow)) 
		{
            if (FirstPlayerInfo.strCharName == "Dao")
                PlayerDao_1.MoveUp ();
            else if (FirstPlayerInfo.strCharName == "Marid")
                PlayerMarid_1.MoveUp();
        }

		else if (Input.GetKey (KeyCode.DownArrow))
		{
            if (FirstPlayerInfo.strCharName == "Dao")
                PlayerDao_1.MoveDown ();
            else if (FirstPlayerInfo.strCharName == "Marid")
                PlayerMarid_1.MoveDown();
        }
	}
}
