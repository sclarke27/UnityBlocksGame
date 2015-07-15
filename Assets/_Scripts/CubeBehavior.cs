using UnityEngine;
using System.Collections;



public class CubeBehavior : MonoBehaviour
{

    int totalHits = 3;
    int currentHits = 0;
    GameObject mainGame;
    static ScoreTracker score;

	AudioClip boxDrop;


    // Use this for initialization
    void Start()
    {
        score = ScoreTracker.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        switch (totalHits - currentHits)
        {
            case 0:
                {
                    gameObject.renderer.material.color = Color.red;
                    break;
                }
            case 1:
                {
                    gameObject.renderer.material.color = Color.yellow;
                    break;
                }
            case 2:
                {
                    gameObject.renderer.material.color = Color.green;
                    break;
                }
                    

        }
    }

    void OnCollisionEnter(Collision col)
    {

		
	}
	
	void OnCollisionExit(Collision col)
	{
		float radius = 7.2F;
		float power = 1700.0F;
		
		//		if (col.gameObject.name != "Cube2(Clone)")
		//        {
		
		StaticObjectBehaviour colStaticBehaviour = (StaticObjectBehaviour)col.gameObject.GetComponent("StaticObjectBehaviour");
		if (colStaticBehaviour != null && col.gameObject.name != "MainTerrain")
		{
			if (colStaticBehaviour.causeBlockDestruction)
			{
				audio.PlayOneShot(gameObject.audio.clip);
				currentHits++;
			}
		}
		if (col.gameObject.name == "Projectile(Clone)")
		{
			audio.PlayOneShot(gameObject.audio.clip);
			currentHits++;
			
			Destroy(col.gameObject);
		}
		if (col.gameObject.name == "MainTerrain")
		{
			audio.PlayOneShot(gameObject.audio.clip);
			currentHits++;
		}
		
		if (currentHits > totalHits)
		{
			Vector3 explosionPos = col.gameObject.transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders)
			{
				if (hit && hit.rigidbody) {
					//hit.GetComponentInParent("RigidBody").AddExplosionForce(power, explosionPos, radius, 3.0F);
					hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3.0F);
				}
				
			}
			score.TickBlockBreakByShotCount();
			Destroy(gameObject);
		}
		
		
		//		} 
	}
}
