using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
   [SerializeField] private ParticleSystem particles;
   public float lineOfSight, inRange, frequency;
   private Transform player;
   
   void Start(){
     player = GameObject.FindGameObjectWithTag("Player").transform;
   }

    void Update(){
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight){
            particles.Play();
        }
        else if(distanceFromPlayer <= inRange){
            if(particles.name == "mosquitos")
            {
                var noise = particles.noise;
                noise.strength = frequency * Time.deltaTime;
            }
            }   
        else if (distanceFromPlayer > lineOfSight){
            particles.Stop();
        }
    }
     private  void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight) ;
        Gizmos.DrawWireSphere(transform.position, inRange) ;
    }
}
