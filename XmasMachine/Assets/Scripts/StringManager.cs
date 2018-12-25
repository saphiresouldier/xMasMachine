using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class StringManager : MonoBehaviour
{
    public List<string> GeneratedStrings = new List<string>();
    public List<TextMesh> DisplayTexts = new List<TextMesh>();
    public AudioSource doneSound;
    public AudioSource letterSound;
    public int toysCollected = 0;
    public TextMesh ToyCounter;

    //use alphabet2 for extra difficulty
    const string alphabet = "abcdefghijklmnopqrstuvwxyz";
    //const string alphabet_ger = "abcdefghijklmnopqrstuvwxyzöäüß";

    private void Start()
    {
        ToyCounter.text = "Toys: " + toysCollected;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {

            if (GeneratedStrings.Count != 0)
            {
                if(Input.inputString != null)
                CheckInputLetter(Input.inputString[0]);

            }

        }
    }

    public void AddToyString(TextMesh text)
    {
        int stringLength = (int)(Random.value * 4.0d + 3.0d);

        string gen = GenerateRandomString(stringLength);
        GeneratedStrings.Add(gen);
        DisplayTexts.Add(text);
        text.text = gen;
    }

    public bool CheckInputLetter(char letter)
    {
        // Debug.Log("Entered letter: " + letter);

        for (int i = 0; i < GeneratedStrings.Count; i++)
        {
            string current = GeneratedStrings[i];

             if (current[current.Length - 1] == letter)
            {
                //Correct input
                Debug.Log("Correct Letter!");

                //remove letter from string
                GeneratedStrings[i] = current.Substring(0, current.Length - 1);

                //Check if last letter of string, delete toy object
                if (current.Length <= 1)
                {
                    Debug.Log("THIS WAS THE LAST LETTER!");


                    toysCollected = toysCollected + 1;

                    ToyCounter.text = "Toys: " + toysCollected;

                    //juicy stuff
                    doneSound.Play();
                    GameObject go = DisplayTexts[i].gameObject.transform.parent.gameObject;
                    go.transform.GetChild(1).gameObject.SetActive(false); //deactivate todo sprite
                    go.transform.GetChild(2).gameObject.SetActive(true); //activate done sprite
                    go.GetComponent<Collider2D>().enabled = false;
                    go.GetComponentInChildren<ParticleSystem>().Play();

                    //remove object, remove string
                    DisplayTexts[i].text = "";
                    //Destroy(DisplayTexts[i].gameObject.transform.parent.gameObject);

                    DisplayTexts.Remove(DisplayTexts[i]);
                    GeneratedStrings.Remove(GeneratedStrings[i]);
                }
                else
                {
                    if (DisplayTexts[i] != null && DisplayTexts[i].text != null)
                        DisplayTexts[i].text = GeneratedStrings[i];
                }

                letterSound.Play();

                return true;
            }
        }
        return true;
    }

    public string GenerateRandomString(int length)
    {
        string rs = new string('-', length);

        var building = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            double rand = Random.value * alphabet.Length;
            char letter = alphabet[(int)rand];

            //Debug.Log("Chosen letter: " + letter + " from index " + (int)rand);

            building.Append(letter);
        }

        rs = building.ToString();
        //Debug.Log(rs);
        return rs;
    }
}
