using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    public int attackPower;

	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyBehaviour>().health -= attackPower;
        }
    }
}
