using NUnit.Framework;
using UnityEditor.EditorTools;
using UnityEngine;

public enum EtatJeu
{
    jeu,
    fin
}

public class GestionnaireDeJeu : MonoBehaviour
{
    public static GestionnaireDeJeu instance;
    public EtatJeu etat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null )
        {
            instance = this;

        }
        else
        {
Destroy (this.gameObject);
        }
         etat = EtatJeu.jeu;
    }

    public void TerminerJeu()
    {
    etat = EtatJeu.fin;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}