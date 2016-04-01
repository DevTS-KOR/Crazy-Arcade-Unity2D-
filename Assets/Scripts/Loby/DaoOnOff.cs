using UnityEngine;
using System.Collections;

public class DaoOnOff : MonoBehaviour {

    public GameObject DaoActive;
    public AudioClip ClickSound;
    private AudioSource ClickSource = null;

    //외부 스크립트를 가져온다.
    private MaridOnOff GetMarid;

    void Start()
    {
        DaoActive.SetActive(false);
        ClickSource = GetComponent<AudioSource>();

    }

    public void DaoSetActive()
    {
        //MaridButton(1p)에 연결된 MaridOnOff 스크립트의 정보를 가져온다.
        GetMarid = GameObject.Find("MaridButton(1p)").GetComponent<MaridOnOff>();
        if (Global.MaridSelect_1p == true)
        {
            GetMarid.MaridSetActive();
            Global.MaridSelect_1p = false;
        }

        DaoActive.SetActive(!DaoActive.active);
        ClickSource.PlayOneShot(ClickSound, 1f);

        //캐릭터 중복선택 방지
        if (Global.DaoSelect_1p == true)
            Global.DaoSelect_1p = false;
        else
            Global.DaoSelect_1p = true;



    }
}
