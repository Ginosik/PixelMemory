using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCardDance : MonoBehaviour
{
    private float movespeed = -4;
    private float rotationspeed = 320;
    private float y;
    private float x;
    private float ry;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer pag = GetComponent<MeshRenderer>();
        x = Random.Range(-8f, 8f);
        y = Random.Range(6.6f, 60f);
        transform.position = new Vector3(x, y, transform.position.z);
        ry = Random.Range(0f, 180f);
        transform.rotation = Quaternion.Euler(transform.rotation.x, ry, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, movespeed * Time.deltaTime));
        transform.Rotate(new Vector2(0, rotationspeed * Time.deltaTime));

        if (transform.position.y < -7)
        {
            float x = Random.Range(-8f, 8f);
            y = Random.Range(6.6f, 10f);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
