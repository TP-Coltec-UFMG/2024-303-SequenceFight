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
        string FilePath = "./Assets/Scripts/GameManagers/Words/WordLists/Length_";
        FilePath = String.Concat(FilePath, SequenceLength,".txt");

        StreamReader Sr = new StreamReader(FilePath);

        for (int i = 0; i < WordListLength; i++) {
            WordList.Add(Sr.ReadLine());
        }

        Sr.Close();
    }
}   
