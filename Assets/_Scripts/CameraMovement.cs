using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform theplayer;
    public float camheight;
    public float camwidth;
    public Vector3 vt3cam;
    public Vector3 playerfollowlocal;

    void Start()
    {
        camheight = Camera.main.orthographicSize * 2f;
        camwidth = camheight * Camera.main.aspect;
    }

    void Update()
    {
        playerfollowlocal = new Vector3(Mathf.Floor(theplayer.position.x / camwidth) * camwidth + camwidth / 2, Mathf.Floor(theplayer.position.y / camheight) * camheight + camheight / 2, transform.position.z);

        if (playerfollowlocal != vt3cam)
        {
            vt3cam = playerfollowlocal;

            StartCoroutine(Cameramove());
        }
    }

    System.Collections.IEnumerator Cameramove()
    {
        while (Vector3.Distance(transform.position, vt3cam) > 0.05f)
        {

            transform.position = Vector3.Lerp(transform.position, vt3cam, Time.deltaTime * 5f);
            yield return null;

        }

        transform.position = vt3cam;

    }

}
