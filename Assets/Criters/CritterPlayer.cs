using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterPlayer : MonoBehaviour {
    [Header("Configurations")]
    public float Force=350f;
    public float GravityCompensation=100f;
    public float MaximumMagnitude;
    public GameObject puxador;
    public GameObject seta;
    public bool pressed;
    public LayerMask groundLayer;
    public Transform GroundTarget;
    [Space(10)]
    [Header("Debug-only")]
    public bool grounded;
    public Vector2 InitialPosition;
    public Vector2 CurrentPosition;
    public Vector2 distance;
    public float setaFullScaleY= 0.8449444f;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var ground = Physics2D.OverlapCircle(GroundTarget.position, 0.2f, groundLayer);
        if (ground == null)
        {
            grounded = false;
        } else
        {
            grounded = true;

        }
		if(Input.GetMouseButtonDown(0) && grounded)
        {
           // print("Epa;");
            var camera = Camera.main;
            if (camera == null)
                print("xx");

            var hit = Physics2D.Raycast(camera.ScreenToWorldPoint((Vector3)Input.mousePosition+new Vector3(0f,0f,transform.position.z-Camera.main.transform.position.z)), Vector2.up * 0.05f);
            if (hit.collider!=null && hit.collider.gameObject==gameObject)
            {
                //print("EPA2");
                InitialPosition = transform.position;
                pressed = true;
            }
        }
        if(pressed)
        {
            puxador.SetActive(true);
            seta.SetActive(true);

            var posPuxador = (Vector2)Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition + new Vector3(0f, 0f, transform.position.z - Camera.main.transform.position.z));
            distance = posPuxador - (Vector2)transform.position;
            if (distance.magnitude > MaximumMagnitude)
            {
                distance = distance.normalized * MaximumMagnitude;
            }
            puxador.transform.position = transform.position+(Vector3)distance;
            seta.transform.position= transform.position - (Vector3)distance;
            seta.transform.rotation = Quaternion.FromToRotation(Vector2.down, distance);
            Vector3 setaScale = seta.transform.localScale;
            setaScale.y = setaFullScaleY * (distance.magnitude / MaximumMagnitude);
            seta.transform.localScale = setaScale;
        } else
        {
            puxador.SetActive(false);
            seta.SetActive(false);

        }
        if (Input.GetMouseButtonUp(0))
        {
            if(pressed)
            {
                var addForce = -distance * Force * (distance.magnitude/MaximumMagnitude)+ (GravityCompensation*rb.gravityScale * Vector2.up);
                rb.AddForce(addForce);
            }
            pressed = false;
        }
    }
}
