﻿//modified from http://answers.unity3d.com/questions/64419/making-credits-scroll-upwards-on-screen.html
//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public Text creditTextItem;
    public Text creditsText;
    public Canvas CreditsCanvas;
    public GameObject top;
    public MenuNavigation nav;
	public Font myFont;
	public int creditsTextSize;
	public Color myColor;
    private List<Text> Credits = new List<Text>();
    private TextReader tr;
    public string path;
    private List<string> credits = new List<string>();
    public float speed = 20.2f;
    private bool restart = false;
    
    void OnEnable()
    {
        if (restart)
        {
            Start();
            restart = false;
        }
        
    }

    void OnDisable()
    {
        //destroy text so there arent multiple credit scenes rolling
        Text[] destroy = GetComponentsInChildren<Text>();
        foreach (Text t in destroy)
        {
            if (t.text != "Credits")
            {
                Destroy(t.gameObject);
            }   
        }
        restart = true;
    }
    
    void Start()
    {
        creditsText.rectTransform.anchoredPosition3D = new Vector3(0,0,0);

        credits.Clear();
        // Set the path for the credits.txt file
        // Create reader & open file
        TextAsset credfile = (TextAsset) Resources.Load("Credits");
        Debug.Log(credfile);
        tr = new StringReader(credfile.text);
        string temp;
        while ((temp = tr.ReadLine()) != null)
        {
            // Read a line of text
            credits.Add(temp);
        }
        // Close the stream
        tr.Close();
        CreateCredits();

    }
    
    void Update()
    {
        if (Credits.Count != 0)
        {
            //move word "credits" up once credits cross middle of screen
            if (Credits[0].transform.localPosition.y > 0)
            {
                if (creditsText.rectTransform.anchoredPosition3D.y < creditsText.rectTransform.rect.height)
                {
                    creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
                }

            }
            //move credits up
            for (int i = 0;i < Credits.Count; i++)
            {
                Credits[i].transform.Translate(Vector3.up * Time.deltaTime * speed);
                //once credits reach the top, destroy them
                if (Credits[i].transform.localPosition.y > top.transform.localPosition.y)
                {
                    Destroy(Credits[i].gameObject);
                    Credits.RemoveAt(i);
                }

            }
            
        }
        else if (isActiveAndEnabled)
        {
            //restart game once credits finish rolling
            if (Manager.Instance.gameOver)
            {
                Manager.Instance.gameOver = false;
                SceneManager.LoadScene(0);
            }
            nav.goToMainMenu();
        }
    }

    void CreateCredits()
    {
        Credits.Clear();
        for (int i = 0; i < credits.Count; i++)
        {
            string c = credits[i];
            Text newTextItem = Instantiate(creditTextItem);
            newTextItem.text = c;
            newTextItem.transform.Translate(Vector3.down * i * newTextItem.preferredHeight*2);

			//design
			newTextItem.font = myFont;
			newTextItem.fontSize = creditsTextSize;
			newTextItem.color = myColor;
			newTextItem.rectTransform.sizeDelta = new Vector2 (300,50);

            Credits.Add(newTextItem);
            newTextItem.transform.SetParent(CreditsCanvas.transform, false);
        }
    }

}
