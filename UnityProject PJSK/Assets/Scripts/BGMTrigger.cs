using UnityEngine;
using System.Collections;

public class BGMTrigger : MonoBehaviour {

    public enum SoundToChangeTo
    {
        HubTown,
        Conversation,
        Field,
        Forest,
        Temple,
        Lyndor,
        Castle,
        FinalBattle
    };

    public SoundToChangeTo sound;


    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (sound == SoundToChangeTo.Castle)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Castle);
                Camera.main.GetComponent<ToggleCameraEffects>().EnterForest(false);
                GameObject[] trees = GameObject.FindGameObjectsWithTag("TempleTree");
                {
                    for (int i = 0; i < trees.Length; i++)
                    {
                        trees[i].GetComponent<ParticleSystem>().enableEmission = false;
                    }

                }
            }
                
            if (sound == SoundToChangeTo.Conversation)
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Conversation);
            if (sound == SoundToChangeTo.Field)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Field);
                Camera.main.GetComponent<ToggleCameraEffects>().EnterForest(false);
                GameObject[] trees = GameObject.FindGameObjectsWithTag("TempleTree");
                {
                    for (int i = 0; i < trees.Length; i++)
                    {
                        trees[i].GetComponent<ParticleSystem>().enableEmission = false;
                    }

                }
            }

            if (sound == SoundToChangeTo.Forest)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Forest);

            }

            if (sound == SoundToChangeTo.Temple)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Temple);
                Camera.main.GetComponent<ToggleCameraEffects>().EnterForest(true);
                GameObject[] trees = GameObject.FindGameObjectsWithTag("TempleTree");
                {
                    for(int i = 0; i < trees.Length; i++)
                    {
                        trees[i].GetComponent<ParticleSystem>().enableEmission = true;
                    }
                    
                }
            }
                
            if (sound == SoundToChangeTo.Lyndor)
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Lyndor);
            if (sound == SoundToChangeTo.HubTown)
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.HubTown);
            if (sound == SoundToChangeTo.FinalBattle)
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.FinalBattle);
        }
        
    }
}
