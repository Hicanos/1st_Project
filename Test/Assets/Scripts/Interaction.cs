using UnityEngine;

public class Interaction : MonoBehaviour
{
    private float checkRate = 0.05f;
    private float lastCheckTime;
    private float maxCheckDistance = 2;
    private LayerMask layerMask;

    private GameObject curInteractGameObject;

    private Camera camera;
    private bool nowFirstPerson = true;

    private Transform interactionRayPointTransform;

    private void Start()
    {
        layerMask = 1 << 6;
        camera = Camera.main;
        lastCheckTime = Time.time;

        //구성을 단순화하기 위해 이렇게 초기화했습니다. GetChild를 활용해서 초기화하는 방법은 권장되지 않습니다.
        interactionRayPointTransform = transform.GetChild(0).GetChild(1);
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = returnInteractionRay();
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    Debug.Log($"{curInteractGameObject.name}과 상호작용할 수 있습니다.");
                }
            }
            else
            {
                curInteractGameObject = null;
            }
        }

        // 스페이스 바를 눌렀을 때 시점을 전환합니다.
        if (Input.GetKeyDown(KeyCode.Space)) SwitchView();
    }

    public void SwitchView()
    {
        if (nowFirstPerson)
        {
            nowFirstPerson = false;
            camera.transform.localPosition = new Vector3(0, 0.5f, -5);
        }
        else
        {
            nowFirstPerson = true;
            camera.transform.localPosition = Vector3.zero;
        }
    }

    private Ray returnInteractionRay()
    {
        Ray ray;
        if (nowFirstPerson)
        {
           ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        }
        else
        {
            //TODO
            //interactionRayPointTransform를 활용할 것
            //여기는 3인칭 InteractionRay
            ray = new Ray(interactionRayPointTransform.position, interactionRayPointTransform.forward);
            //전방으로 레이발사
        }
        return ray;
    }
}