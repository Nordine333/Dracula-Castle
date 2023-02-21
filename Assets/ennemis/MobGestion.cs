using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobGestion : MonoBehaviour
{
	public Image barreDeVie;
	public Transform target;
	public AudioClip attaqueSound;
	public float DistanceMaxToPlayer;
	public float DistanceToAttack;
	public float delaiAtk;
	public int vieMobMax = 100;
	public int valeurAttaqueMob = 10;
	
	private int vieMob;
	private float NextFenetreAtk;
	private Vector3 PositionDepartMob;
	private float DistanceToPlayer;
	private UnityEngine.AI.NavMeshAgent mobMeshAgent;
	private Animator mobAnim;
	private bool mort = false;
	
    // Start is called before the first frame update
    void Start()
    {
		vieMob = 100;
		
        mobMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        mobAnim = GetComponent<Animator>();   
        PositionDepartMob = transform.position; 
        NextFenetreAtk = Time.time;    
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(transform.position-target.position),3*Time.deltaTime);
		DistanceToPlayer = Vector3.Distance(transform.position,target.position);
		
		// deplacement
		if(DistanceToPlayer < DistanceMaxToPlayer && !mort)
		{
			mobMeshAgent.SetDestination(target.position);
		}
		else
		{
			mobMeshAgent.SetDestination(PositionDepartMob);
		}
		
		// attaque
		if(DistanceToPlayer < DistanceToAttack && !mort)
		{
		   RaycastHit hit;
		           Debug.DrawRay(transform.position,-this.transform.forward * DistanceToAttack, Color.blue);

		   if(Physics.Raycast(transform.position,-this.transform.forward,out hit))
		   {
			   if(target.transform.name == hit.transform.name)
			   {
				   if(Time.time > NextFenetreAtk) // delai entre 2 attaques écoulé
				{
					GetComponent<AudioSource>().PlayOneShot(attaqueSound);
					mobAnim.SetBool("atk",true);
					Debug.Log("Le mob atk");
					hit.transform.gameObject.GetComponent<joueurGestion>().setVie(
					hit.transform.gameObject.GetComponent<joueurGestion>().getVie()
					-valeurAttaqueMob);
					NextFenetreAtk = Time.time + delaiAtk; // calcule prochain fenetre d'atk
				}
				
				
			   }
				
			}
		}
		
        if(mobMeshAgent.velocity.magnitude > 0 && !mort)
        {
			mobAnim.SetBool("walk",true);
		}
		else
		{
			mobAnim.SetBool("walk",false);
		}

    }
    
    public void MobMeurt()
    {
		Destroy(barreDeVie);
		mort = true;
		mobMeshAgent.Stop();
		mobAnim.SetTrigger("mort");
		MeshCollider meshCollider = GetComponent<MeshCollider>(); // afin que les raycast traverse le cadavre
		if (meshCollider != null) {meshCollider.enabled = false;}
		else 
		{
			BoxCollider boxCollider = GetComponent<BoxCollider>(); // j'utilise boxCollider ou meshCollider selon les mobs
			if (boxCollider != null) {boxCollider.enabled = false;}
		}
		Destroy(gameObject,15f); // après 5 secondes detruit l'objet mob
		
	}
	
	public void AttackAnimEnd()
	{
		Debug.Log("fin");
		mobAnim.SetBool("atk",false);
	}
	
	public void setVie(int vie)
	{
		vieMob = Mathf.Clamp(vie,0,vieMobMax);
		Debug.Log(vieMob);
		barreDeVie.fillAmount = (float) vieMob / vieMobMax;
		if(vieMob <= 0) MobMeurt();
	}
	
	public int getVie()
	{
		return vieMob;
	}
}
