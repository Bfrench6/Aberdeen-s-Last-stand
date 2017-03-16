using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public Text creditTextItem;
    public Canvas CreditsCanvas;
    private List<Text> Credits = new List<Text>();
    private TextReader tr;
    public string path;
    private List<string> credits = new List<string>();
    public float speed = 20.2f;



    // Use this for initialization
    void Start()
    {
        // Set the path for the credits.txt file
        path = "Assets/Resources/Credits.txt";
        // Create reader & open file
        tr = new StreamReader(path);
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
    // Update is called once per frame
    void Update()
    {
        if (Credits.Count != 0)
        {
            for (int i = 0;i < Credits.Count; i++)
            {
                Credits[i].transform.Translate(Vector3.up * Time.deltaTime * speed);
                
            }
        }
    }
    void CreateCredits()
    {
        for (int i = 0; i < credits.Count; i++)
        {
            string c = credits[i];
            Text newTextItem = Instantiate(creditTextItem);
            newTextItem.text = c;
            newTextItem.transform.Translate(Vector3.down * i * newTextItem.preferredHeight*2);
            Debug.Log(c);
            Credits.Add(newTextItem);
            newTextItem.transform.SetParent(CreditsCanvas.transform, false);

        }
    }

}
