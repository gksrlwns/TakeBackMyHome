using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieCustomizing : MonoBehaviour
{
    [SerializeField] bool isTrig;
    [SerializeField] List<ZombieBodyType> bodys;
    [SerializeField] List<ZombieHeadType> heads;
    private void OnValidate()
    {
        if(isTrig)
        {
            bodys = new List<ZombieBodyType>();
            heads = new List<ZombieHeadType>();

            bodys = GetComponentsInChildren<ZombieBodyType>(true).ToList();
            heads = GetComponentsInChildren<ZombieHeadType>(true).ToList();
            isTrig = false;
        }
    }
    /// <summary>
    /// 초기화 작업 : 모든 커스터마이징 오브젝트들을 끈다.
    /// </summary>
    public void InitializeSetUp()
    {
        foreach (ZombieBodyType type in bodys)
        {
            if (type.gameObject.activeSelf) type.gameObject.SetActive(false);
        }
        foreach (ZombieHeadType type in heads)
        {
            if (type.gameObject.activeSelf) type.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Random index를 통해 커스터마이징 세팅
    /// </summary>
    public void SetZombieCustomizing()
    {
        int bodyIndex = Random.Range(0, bodys.Count);
        int headIndex = Random.Range(0, heads.Count);
        bodys[bodyIndex].gameObject.SetActive(true);
        heads[headIndex].gameObject.SetActive(true);
    }
}
