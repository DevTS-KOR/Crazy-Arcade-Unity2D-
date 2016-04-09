using UnityEngine;
using System.Collections;

public enum ANI_STATE
{
    IDLE,
    WALK,
    BEFORE_DIE,
    DIE
}
//클래스 직속의 속성을 사용 하기 위해서 필요.
[System.Serializable]
public class AniState
{
    public int StartIndex;
    public int EndIndex;
    public float FrameTime;
    public ANI_STATE State;
}

public class DaoAni : MonoBehaviour {

    public Texture2D[] textureList;      //애니메이션 이미지를 담당할 클래스
    public AniState[] States;

    private AniState CurAnistate;
    public int CurIndex;

    private bool IsSetAni;

    private string CurKey;
    void Start()
    {
        SetAni(ANI_STATE.IDLE);
        StartCoroutine(AniUpdate());
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if (IsSetAni == false)
                SetAni(ANI_STATE.WALK);

            IsSetAni = true;

           
            // StartCoroutine(AniUpdate());
            transform.Translate(Vector3.down * 5.0f * Time.deltaTime);

        }

        if( Input.GetKeyUp(KeyCode.DownArrow)
            || Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.RightArrow))
        {
            IsSetAni = false;
            SetAni(ANI_STATE.IDLE);
        }
    }

    public void SetAni(ANI_STATE aniState)
    {
        for(int i = 0; i<States.Length; ++i )
        {
            if( States[i].State == aniState)
            {
                CurAnistate = States[i];
                break;
            }
        }
        CurIndex = CurAnistate.StartIndex;
    }
    public IEnumerator AniUpdate()
    {
        while (true)
        {
            Renderer render = this.gameObject.GetComponent<Renderer>();
            if (render == null)
            {
                Debug.Log("Not Set Renderer");
                yield break;
            }
            if(CurAnistate == null)
            {
                Debug.Log("Not Set AniState");
                yield break;
            }

            CurIndex++;
            if (CurAnistate.EndIndex < CurIndex)
            {
                CurIndex = CurAnistate.StartIndex;
            }
            if(textureList.Length <= CurIndex)
            {
                Debug.Log("overFlow Index");
                yield break;
            }
            render.material.mainTexture = textureList[CurIndex];
            yield return new WaitForSeconds(CurAnistate.FrameTime);
        }
    }

}