using UnityEngine;

public class PointerInput : MonoBehaviour
{
    [SerializeField]
    GameObject robotGhost;
    [SerializeField]
    GameObject target;
    void Start()
    {
        robotGhost.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
                );
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Floor")))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    robotGhost.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    robotGhost.SetActive(true);
                }
                else if (robotGhost.activeSelf) 
                {
                    robotGhost.transform.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            robotGhost.SetActive(false);
            target.transform.position = robotGhost.transform.position;
            target.transform.rotation = robotGhost.transform.rotation;
        }
    }
}
