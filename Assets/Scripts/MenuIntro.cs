using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIntro : MonoBehaviour
{
    int dernierScore; 
    [SerializeField] TMP_Text texteScore;

    void Start()
    {
        //si la clé existe, on l'enregistre, sinon on met une valeur par défaut
        if (PlayerPrefs.HasKey("score"))
        {
        dernierScore = PlayerPrefs.GetInt("score");
        }
        else
        {
        dernierScore = 0;
        }
    texteScore.text = $"Dernier score : {dernierScore} coups";
    }
    public void Demarrer()
    {
        SceneManager.LoadScene("Jeu");
    }
}



