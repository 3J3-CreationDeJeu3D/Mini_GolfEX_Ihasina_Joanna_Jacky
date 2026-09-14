using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class Balle : MonoBehaviour
{

    // [Header("État de jeu")]



    // [Header("Paramètres de tir")]



    // [Header("Gauge de force")]
[SerializeField] float forceTir;
[SerializeField] float accumulateurForce = 0.1f;

[SerializeField] Slider jaugeForce;
    // [Header("Input Actions")]
   [Header("Input Actions")]
   [SerializeField] InputAction tirActions;


    // [Header("Composant")]


    void Start()
    {
        tirActions.Enable();
    }

    void Update()
    {

        if (tirActions.WasPressedThisFrame())
        {
             forceTir = 0;
        } if (tirActions.IsPressed()){
            
            forceTir += accumulateurForce;
        } if (tirActions.WasReleasedThisFrame())
        {
            forceTir = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnTriggerEnter(Collider collision)
    {

    }

    // ===================
    void FrapperBalle()
    {

    }

    void MettreAJourUI()
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

    }

    void OnDisable()
    {

    }
}
