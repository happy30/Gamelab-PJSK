using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public int healthBonus;
    public int attackPowerBonus;

    public StatsManager stats;

    //Once this weapon is activated, add stats permanently (WIP)
    void Start()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        stats.maxHealth += healthBonus;
        stats.attackPower += attackPowerBonus;
    }
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButtonDown("Fire1"))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
	}
}
