using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class Balle : MonoBehaviour
{
    [Header("État de jeu")]
    Vector3 positionBalle;
    [SerializeField] int nbCoups;
    public bool peutJouer;
 
 
    [Header("Paramètres de tir")]
    [SerializeField] float forceTir = 0f;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float angle;
 
 
    [Header("Gauge de force")]
 
    [SerializeField] float accumulateurForce = 50f;
 
    [SerializeField] Slider jaugeForce;
    [SerializeField] float forceMin = 0f;
    [SerializeField] float forceMax = 100f;
 
 
    [Header("Input Actions")]
    [SerializeField] InputAction tirAction;
    [SerializeField] InputAction angleAction;
 
    [Header("Composant")]
    LineRenderer lineRendererBalle;
    AudioSource audioSourceBalle;
    [SerializeField] AudioClip sonFin;
    [SerializeField] AudioClip sonErreur;
    [SerializeField] TMP_Text texteCoups;
 
 
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lineRendererBalle = GetComponent<LineRenderer>();
        audioSourceBalle = GetComponent<AudioSource>();
        nbCoups = 0;
        MettreAJourUI();
        peutJouer = true;
 
    }
 
    void Update()
    {
        if (peutJouer == true && GestionnaireJeu.instance.etat == EtatJeu.jeu)
        {
            angle += angleAction.ReadValue<float>();
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
 
            lineRendererBalle.SetPosition(0, transform.position);
            lineRendererBalle.SetPosition(1, transform.position + direction);
 
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
                positionBalle = transform.position;
                rigidbody.AddForce(direction * forceTir * Time.deltaTime, ForceMode.Impulse);
                nbCoups++;
                MettreAJourUI();
 
                forceTir = 0;
                jaugeForce.value = forceTir;
                //TODO appeler la coroutine
                StartCoroutine(AttendreFinCoup());
            }
        }
    }
// ===================================================collision=====================================
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "horsParcours")
        {


            
            audioSourceBalle.PlayOneShot(sonFin);
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
 
            transform.position = positionBalle;
        }
    }
//=====================================================trigger=========================================
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "trou")
        {
            audioSourceBalle.PlayOneShot(sonFin);
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.useGravity = false;
 
 
            transform.position = collision.transform.position;
            GestionnaireDeJeu.instance.TerminerJeu();
            Debug.Log("fin");
        }
    }
 
    // ===================
 
    IEnumerator AttendreFinCoup()
    {
        Debug.Log("Debut");
        peutJouer = false;
        lineRendererBalle.enabled = false;
        yield return new WaitForFixedUpdate(); 
  
        float vitesse = rigidbody.linearVelocity.magnitude;
        while (vitesse > 0.1f)
        {
            vitesse = rigidbody.linearVelocity.magnitude;
            yield return null; //attends au prochain
        }
 
        peutJouer = true;
        lineRendererBalle.enabled = true;
        Debug.Log("Fin");
 
    }
 
 
 
    void FrapperBalle()
    {
 
    }
    //==================================Afficher le nombre de coups================================================
 
    void MettreAJourUI()
    {
        texteCoups.text = $"{nbCoups} coup(s)";
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