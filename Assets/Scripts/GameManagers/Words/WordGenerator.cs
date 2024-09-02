using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour {
    private const int WordListLength = 300;
    private List<string> WordList = new List<string>();

    public string GenerateWord(){
        System.Random Rnd = new System.Random();

        int Index  = Rnd.Next(0, 300);  
        return WordList[Index];
    }

    public void Initiate(int SequenceLength){
        string FileName = "Length_" + SequenceLength;
        TextAsset TextFile = Resources.Load<TextAsset>("WordLists/" + FileName);

        if (TextFile != null) {
            string[] Lines = TextFile.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            WordList.AddRange(Lines);
        } 
        
        else {
            Debug.LogError("File not found: " + FileName);
        }
    }
}   
