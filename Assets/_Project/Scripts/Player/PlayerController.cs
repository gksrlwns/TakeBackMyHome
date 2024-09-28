using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 10f;
    [SerializeField] float sideSpeed = 5f;
    [SerializeField] LayerMask groundLayer;
    public bool isArrive;

    Vector2 inputVec;
    Vector3 targetPosition;

    Camera mainCamera;
    
    bool isMoving;         
    float targetXPosition;
    
    private void Awake() => mainCamera = Camera.main;

    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;
        if (isArrive) return;
#if UNITY_ANDROID
        // ��ġ �� Raycast�� �̿��Ͽ� ��ġ�� �ľ� �� �÷��̾ �̵�
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // ��ġ�� ������ x�ุ ���
                targetXPosition = hit.point.x;
                isMoving = true;
            }
        }

#endif
#if UNITY_STANDALONE || UNITY_EDITOR
        Vector3 editorSide = inputVec.normalized * Time.deltaTime * sideSpeed;
        transform.Translate(editorSide);
#endif

        if (isMoving)
        {
            Vector3 currentPosition = transform.position;
            float newXPosition = Mathf.MoveTowards(currentPosition.x, targetXPosition, sideSpeed * Time.deltaTime);
            transform.position = new Vector3(newXPosition, currentPosition.y, currentPosition.z);

            if (Mathf.Abs(transform.position.x - targetXPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
        Vector3 forVec = Vector3.forward * Time.deltaTime * forwardSpeed;
        transform.Translate (forVec);

        targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -7f, 7f); // ���� ����
        transform.position = targetPosition;

    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }




}
