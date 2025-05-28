using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LightControlledVisibility : MonoBehaviour
{
    public Light lanternLight; // 특수한 오브젝트를 보이게하는 랜턴
    public LayerMask layerMask; // 검사대상 오브젝트만 검사하도록 하는 레이어마스크
    private List<VisibilityFilter> trackedFilters = new List<VisibilityFilter>();

    void Start()
    {
        trackedFilters = FindObjectsOfType<VisibilityFilter>().ToList();
    }

    void Update()
    {
        foreach (var filter in trackedFilters)
        {
            filter.objectRenderer.enabled= false;
        }

        //직선으로 검출.
        RaycastHit[] hits = Physics.RaycastAll(lanternLight.transform.position, lanternLight.transform.forward, 10f, layerMask);
//직선 레이 코드
//- **[조건 1]** 직선으로 Ray를 발사하여 검출된 모든 대상을 hits에 저장해요. = RaycastAll
//-** [조건 2] * *lanternLight로부터 lanternLight기준 앞(+z) 방향으로 10만큼의 거리를 검사해요. (10f)
//- ** [조건 3] * *layerMask에 포함되어있는 모든 레이어 중 하나에 해당하는 것만 검출해요. (layerMask체크)
        foreach (var hit in hits)
        {
            var hitFilter = hit.transform.GetComponent<VisibilityFilter>();
            if (hitFilter != null && hitFilter.IsLightColorMatching(lanternLight.color))
            {
                hitFilter.objectRenderer.enabled = true;
            }
        }
    }
}