using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DoorOpen
 * 게임 내 문 오브젝트를 열리는 애니메이션 관리
 */
public class DoorOpen : MonoBehaviour
{
    public bool isOpened = false;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private Quaternion closedRot;
    private Quaternion openedRot;
    private Coroutine doorCoroutine;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        closedRot = door.transform.rotation;
        openedRot = Quaternion.Euler(door.transform.eulerAngles + new Vector3(0, openAngle, 0));
    }
    public void DoorOpenStart()
    {
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);
        if (!isOpened)
            doorCoroutine = StartCoroutine(OpenDoor());
    }
    private IEnumerator OpenDoor()
    {
        isOpened = true;
        while(Quaternion.Angle(door.transform.rotation,openedRot) > 0.1f)
        {
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, openedRot, Time.deltaTime * openSpeed);
            yield return null;
        }
        door.transform.rotation = openedRot;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
