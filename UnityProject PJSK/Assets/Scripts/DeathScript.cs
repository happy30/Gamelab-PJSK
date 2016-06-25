using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	public void Die()
    {
        GameObject.Find("GameManager").GetComponent<StatsManager>().health = GameObject.Find("GameManager").GetComponent<StatsManager>().maxHealth;
        GameObject.Find("GameManager").GetComponent<LoadController>().LoadScene("Lyndor"); 
    }
}
