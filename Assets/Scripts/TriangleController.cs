using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleController : MonoBehaviour
{

    public float stepSize = 0.001f;
    public bool Scale;
    public bool Position;
    public bool Rotation;
    private float rotatedegree = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Position)
                this.transform.position += this.transform.up  * stepSize;
            if (Rotation)
                transform.rotation =  Quaternion.Euler( transform.rotation.eulerAngles + new Vector3(stepSize * 100, 0, 0));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(stepSize* 1000, 0,0);

        }

        if ( Input.GetKey(KeyCode.E))
        {
            if (Position)
                this.transform.position -= this.transform.up *  stepSize;
            if (Rotation)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-stepSize * 100, 0, 0));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(-stepSize * 1000, 0, 0);
        }

        if ( Input.GetKey(KeyCode.A))
        {
            if (Position) this.transform.position += this.transform.right  *stepSize;
            if (Rotation)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, stepSize * 100, 0));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(0, stepSize * 1000, 0);
        }
        if ( Input.GetKey(KeyCode.D))
        {
            if (Position)
                this.transform.position -= this.transform.right  *  stepSize;
            if (Rotation)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -stepSize * 100, 0));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(0, -stepSize * 1000, 0);
        }
        if ( Input.GetKey(KeyCode.W))
        {
            if (Position)
                this.transform.position += this.transform.forward * stepSize;
            if (Rotation)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, stepSize * 1000));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(0, 0, stepSize * 100);
        }
        if ( Input.GetKey(KeyCode.S))
        {
            if (Position)
                this.transform.position-= this.transform.forward* stepSize;
            if (Rotation)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -stepSize * 1000));
            if (Scale)
                transform.localScale = transform.localScale + new Vector3(0, 0,-stepSize * 100);
        }




   

    }
}
