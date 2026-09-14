using UnityEngine;

public class TourneMoulin : MonoBehaviour
{
    [SerializeField] private Vector3 axeRotation = Vector3.up;
    public float vitesse = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate(axeRotation * vitesse * Time.deltaTime);
         //transform.Rotate(Vector3.up),space self   serialzied filed flaopt vitesse roatation
    }
}
