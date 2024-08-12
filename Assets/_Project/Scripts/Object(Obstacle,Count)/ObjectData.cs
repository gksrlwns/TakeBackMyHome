using UnityEngine;
public enum ObjectSetActiveType {Left, Rigth, Both };

public class ObjectDataController : MonoBehaviour
{
    [SerializeField] ObjectSetActiveType position;

    public GameObject[] leftRightObjects;

    public void SetPosition(int index) => transform.position = new Vector3(0, 0, index * 20f);

    public void SetActiveSelf(ObjectSetActiveType setActiveType)
    {
        switch (setActiveType)
        {
            case ObjectSetActiveType.Left:
            case ObjectSetActiveType.Rigth:
                if(leftRightObjects.Length.Equals(1))
                {
                    leftRightObjects[0].SetActive(true);
                    break;
                }
                for (int i = (int)setActiveType; i < leftRightObjects.Length; i += 2)
                {
                    leftRightObjects[i].SetActive(true);
                }
                break;
            case ObjectSetActiveType.Both:
                for (int i = 0; i < leftRightObjects.Length; i++)
                {
                    leftRightObjects[i].SetActive(true);
                }
                break;
        }
    }

}
