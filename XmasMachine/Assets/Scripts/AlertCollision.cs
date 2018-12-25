using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlertCollision : MonoBehaviour
{
    public StringManager stringManager;
    public GameCoreLoop coreLoop;
    public AudioSource hitSound;
    public GameObject Fire;
    public List<GameObject> Hearts;
    public TextMesh ToyCounter;

    /*
     * 
     * TODO: -add points, remove points 
     * 
     * 
     * */


    private IEnumerator FireRoutine()
    {
        Fire.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        Fire.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " collided");
        GameObject tmpGO = other.gameObject;
        TextMesh currentTextMesh = tmpGO.GetComponentInChildren<TextMesh>();
        string tmpString = currentTextMesh.text; 

        if (tmpString.Length > 0)
        {

            Debug.Log("!!!!!! " + tmpString);
            
            //TODO removeString 

            //stringManager.DisplayTexts.Remove(currentTextMesh);

            Debug.Log("..." + stringManager.DisplayTexts.Count);

            int currentLives = coreLoop.getPlayerLives() -1;
            coreLoop.PlayerLives = currentLives;

            if (currentLives == 2)
            {
                Hearts[2].SetActive(false);
            }
            else if (currentLives == 1)
            {
                Hearts[1].SetActive(false);
            }
            else if (currentLives == 0)
            {
                foreach(GameObject o in Hearts)
                {
                    o.SetActive(true);
                }

                // TODO Game Over 
                
                restartLevel();

            } 
            //Debug.Log("current lives... " + currentLives);

            for (int i = 0; i < stringManager.DisplayTexts.Count; i++){

                Debug.Log("..." + i);

                //Debug.Log(stringManager.DisplayTexts[i].ToString());
                //Debug.Log("..." + i + ": "+ stringManager.DisplayTexts[i].text);

                if (stringManager.DisplayTexts[i] != null)
                {
                    Debug.Log("display :" + i + "::"+ stringManager.DisplayTexts[i].text);
                    if(stringManager.DisplayTexts[i].Equals(currentTextMesh)) {
                        stringManager.DisplayTexts.Remove(currentTextMesh);
                        stringManager.GeneratedStrings.RemoveAt(i);

                        Destroy(tmpGO);
                        hitSound.Play();
                        StartCoroutine(FireRoutine());

                        return;
                    }
                        
                }
               /* if (stringManager.DisplayTexts[i].text == tmpString) {

                }*/
            }

            
        }
    }

    private void restartLevel()
    {
        coreLoop.PlayerLives = 3;
        stringManager.toysCollected = 0;
        stringManager.DisplayTexts.Clear();
        stringManager.GeneratedStrings.Clear();

        ToyCounter.text = "Toys: 0" ;
        SceneManager.LoadScene("0-startScreen");



    }


}
