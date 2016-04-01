using UnityEngine;
using System.Collections;

public class MaridOnOff : MonoBehaviour {

    public GameObject MaridActive;
    public AudioClip ClickSound;
    private AudioSource ClickSource = null;

    //외부 스크립트를 가져온다.
    private DaoOnOff GetDao;

    void Start()
    {
        MaridActive.SetActive(false);
        ClickSource = GetComponent<AudioSource>();
    }

    public void MaridSetActive()
    {
        //DaoButton(1p)에 연결된 DaoOnOff 스크립트의 정보를 가져온다.
        GetDao = GameObject.Find("DaoButton(1p)").GetComponent<DaoOnOff>();
        if (Global.DaoSelect_1p == true)
        {
            GetDao.DaoSetActive();
            Global.DaoSelect_1p = false;
        }

        MaridActive.SetActive(!MaridActive.active);
        ClickSource.PlayOneShot(ClickSound, 1f);
        
        //캐릭터 중복선택 방지
        if (Global.MaridSelect_1p == true)
            Global.MaridSelect_1p = false;
        else
            Global.MaridSelect_1p = true;



    }
}
