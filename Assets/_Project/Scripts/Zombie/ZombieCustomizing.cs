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
    /// �ʱ�ȭ �۾� : ��� Ŀ���͸���¡ ������Ʈ���� ����.
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
    /// Random index�� ���� Ŀ���͸���¡ ����
    /// </summary>
    public void SetZombieCustomizing()
    {
        int bodyIndex = Random.Range(0, bodys.Count);
        int headIndex = Random.Range(0, heads.Count);
        bodys[bodyIndex].gameObject.SetActive(true);
        heads[headIndex].gameObject.SetActive(true);
    }
}
