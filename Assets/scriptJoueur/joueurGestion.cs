using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joueurGestion : MonoBehaviour
{
	public GameObject SceneController_;
	
	public GameObject key;

	public GameObject MenuEchapCanvas;
	
	public GameObject InteractionCleCanvas;
	public GameObject InteractionPorteCanvas;
	public GameObject InteractionBateauCanvas;

	public AudioClip attaqueSound;
	public Image barreDeVie;
	public Camera camera;
	public Animator mainDroiteAnimator;
	public float distanceAtk;
	public float delaiAtk;
	public int vieJoueurMax = 100;
	public int valeurAttaqueJoueur = 30;
	
	private int vieJoueur;
		
	//attaque
	private float NextFenetreAtk;
	private Ray ray;
	private RaycastHit hit;
	
	// inventaire
	private bool keyRecup = false;
	
	private Transform parentPlayer;

    // Start is called before the first frame update
    void Start()
    {
		parentPlayer = transform.parent;
		Cursor.visible = false;
		vieJoueur = 100;
		
        NextFenetreAtk = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position,this.transform.forward * distanceAtk, Color.red);
    }
    
    public void OnEchap()
    {
		MenuEchapCanvas.SetActive(!MenuEchapCanvas.activeSelf);
		Cursor.visible = MenuEchapCanvas.activeSelf;
		
	}
    
    void OnEpee()
    {
		
		if(Time.time > NextFenetreAtk) // delai entre 2 attaques écoulé
		{
			GetComponent<AudioSource>().PlayOneShot(attaqueSound);
			mainDroiteAnimator.SetTrigger("attackBasique");
			ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f));
			if(Physics.Raycast(ray, out hit))
			{	
				if(hit.distance <= distanceAtk && hit.transform.tag == "Mob")
				{
					Debug.Log("J'atk");
					hit.transform.gameObject.GetComponent<MobGestion>().setVie(
					hit.transform.gameObject.GetComponent<MobGestion>().getVie()
					-valeurAttaqueJoueur);
				}
			}
			NextFenetreAtk = Time.time + delaiAtk; // calcule prochain fenetre d'atk		
		}
		
	}
	
	public void JoueurMeurt()
    {
		Debug.Log("Le joueur est mort");
		SceneController_.GetComponent<SceneController>().ReloadScene();
	}
	
	public void setVie(int vie)
	{
		vieJoueur = Mathf.Clamp(vie,0,vieJoueurMax);
		barreDeVie.fillAmount = (float) vieJoueur / vieJoueurMax;
		Debug.Log(vieJoueur);
		if(vieJoueur <= 0) JoueurMeurt();
	}
	
	public int getVie()
	{
		return vieJoueur;
	}
	
	void OnInteragir()
	{
		if(InteractionCleCanvas!=null) 
		{
			if(InteractionCleCanvas.activeSelf)
			{
				keyRecup = true;
				InteractionPorteCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Appuyez sur E pour ouvrir la porte";
				Destroy(key);
				Destroy(InteractionCleCanvas);
			}
		}
		if(InteractionPorteCanvas!=null) 
		{
			if(InteractionPorteCanvas.activeSelf && keyRecup)
			{
				Destroy(InteractionPorteCanvas);
				SceneController_.GetComponent<SceneController>().NextScene();
			}
		}
		if(InteractionBateauCanvas!=null) 
		{
			if(InteractionBateauCanvas.activeSelf)
			{
				Destroy(InteractionBateauCanvas);
				SceneController_.GetComponent<SceneController>().LoadCreditsScene();
			}
		}
	}
	
	public bool getKeyRecup()
	{
		return keyRecup;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "DeadZone") {JoueurMeurt();}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if(collision.transform.tag == "Pieges") {setVie(getVie()-20);}
		if(collision.transform.tag == "Soin" && getVie() != vieJoueurMax) 
		{
			setVie(getVie() + 50); 
			Destroy(collision.gameObject);
			Debug.Log("Joueur soigné");
		}
		
	}
	
	void OnCollisionStay(Collision collision)
	{
		if(collision.transform.tag == "Lac") {setVie(getVie()-1);}
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Plateforme")
		{
             transform.parent = other.transform;
        }
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Plateforme")
		{
             transform.parent = parentPlayer;
        }
	}
	
	public void Invincibilite()
	{
		if(getVie()>100){ vieJoueurMax = 100; setVie(100); } //retire l'invincibilité
		else {vieJoueurMax = 1000000; setVie(1000000);} // active l'invincibilité
	}

}
