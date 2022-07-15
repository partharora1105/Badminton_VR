using UnityEngine;

public class stickToController : MonoBehaviour
{
    [SerializeField] private GameObject controller;

    void Update()
    {
        gameObject.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
        gameObject.transform.rotation = new Quaternion(controller.transform.rotation.x, controller.transform.rotation.y, controller.transform.rotation.z, controller.transform.rotation.w);
       
    }
}
