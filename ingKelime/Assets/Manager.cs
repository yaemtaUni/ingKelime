using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Manager : MonoBehaviour
{
    List<kelimeClass> kelimeListesi = new List<kelimeClass>();
    string path = "Assets/Resources/test.txt";
    void Start()
    {

        // listeyi array e çevirme
        kelimeClass[] kelimeArray = kelimeListesi.ToArray();

        // arraye çevirilmiş listeyi jsona çevirme
        string json = JsonHelper.ToJson(kelimeArray);

        // Dosyaya yazmak için
        
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write (json);
        writer.Close();

        // dosyadan okumak için
        StreamReader reader = new StreamReader(path);
        string str1 = reader.ReadToEnd().ToString();
        reader.Close();

        // json dan class array e çevirme
        kelimeClass [] dejsonArroy = JsonHelper.FromJson<kelimeClass> (json);

    }


    
}
