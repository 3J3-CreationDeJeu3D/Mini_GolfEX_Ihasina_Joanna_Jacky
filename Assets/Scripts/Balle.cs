using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Balle : MonoBehaviour
{

    // [Header("État de jeu")]
 [SerializeField]  TMP_Text NbrCoups;
     [SerializeField] float nbCoups = 0f;


    [Header("Paramètres de tir")]
    Rigidbody rigidbodyBalle;
    LineRenderer lineRendererBalle;
    AudioSource audioSourceBalle;
     [SerializeField] AudioClip SonErreur;
      [SerializeField] AudioClip sonFin;

    [Header("Gauge de force")]
    [SerializeField] float forceTir;
    
    [SerializeField] float angle;
    [SerializeField] float accumulateurForce = 0.1f;

    [SerializeField] Slider jaugeForce;

    [Header("Input Actions")]
    [SerializeField] InputAction tirAction;

    [SerializeField] InputAction angleAction;

    // [Header("Composant")]



    void Start()
    {
        rigidbodyBalle = GetComponent<Rigidbody>();
        lineRendererBalle = GetComponent<LineRenderer>();
        audioSourceBalle = GetComponent<AudioSource>();
        nbCoups = 0;
        MettreAJourUI();
    }

      public void MettreAJourUI()
    {
        NbrCoups.text =  $"{nbCoups} coups(s)";
    }

    void Update()
   
    {
         angle += angleAction.ReadValue<float>();
         Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        if (tirAction.WasPressedThisFrame())
        {
            forceTir = 0;
            jaugeForce.value = forceTir;
        }

        if (tirAction.IsPressed())
        {
            forceTir += accumulateurForce;
            forceTir = Mathf.Clamp(forceTir, jaugeForce.minValue, jaugeForce.maxValue);
            jaugeForce.value = forceTir;

        }

        if (tirAction.WasReleasedThisFrame())
        {
            //Envoyer balle
            rigidbodyBalle.AddForce(Vector3.forward * forceTir * Time.deltaTime, ForceMode.Impulse);
            forceTir = 0;
            jaugeForce.value = forceTir;
            nbCoups++;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "horsParcours")
        {
            

        }
    }

    void OnTriggerEnter(Collider collision)
    {
  if(collision.gameObject.tag == "trou")
        {
            
            
        }
    }

    // ===================
    void FrapperBalle()
    {

    }


    // IEnumerator FinJeu()
    // {

    // }

    void SauvegarderScore()
    {

    }

    //=================================
    // Gestion des inputs actions
    void OnEnable()
    {
        tirAction.Enable();
        angleAction.Enable();
    }

    void OnDisable()
    {
        tirAction.Disable();
        angleAction.Disable();
    }
}
