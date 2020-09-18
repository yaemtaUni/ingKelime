using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<kelimeClass> kelimeListesi;
    public List<kelimeClass> kullanılacakKelimelistesi,kullanıldıKelimeListesi,kullanılmayacakKelimeListesi;
    string path = "Assets/Resources/test.txt";

    int mode=0,currentWord=0;

    public GameObject[] ekranlar;
    public Text ustKelime, altKelime;

    public InputField ingIF, trIF;

    
    private void OnApplicationQuit()
    {
        kelimeleriKaydet();
    }

    void Start()
    {
        #region yararlı kodlar
        //// listeyi array e çevirme
        //kelimeClass[] kelimeArray = kelimeListesi.ToArray();

        //// arraye çevirilmiş listeyi jsona çevirme
        //string json = JsonHelper.ToJson(kelimeArray);

        //// Dosyaya yazmak için

        //StreamWriter writer = new StreamWriter(path, false);
        //writer.Write (json);
        //writer.Close();

        //// dosyadan okumak için
        //StreamReader reader = new StreamReader(path);
        //string str1 = reader.ReadToEnd().ToString();
        //reader.Close();

        //// json dan class array e çevirme
        //kelimeClass [] dejsonArroy = JsonHelper.FromJson<kelimeClass> (json);

        #endregion


        //kelimeleri dosyadan okuma
        StreamReader reader = new StreamReader(path);
        string kelimelerJSON = reader.ReadToEnd().ToString();
        reader.Close();

        //// json dan class array e çevirme
        kelimeClass[] kelimelerArray = JsonHelper.FromJson<kelimeClass>(kelimelerJSON);

        //kelimeler arrayini listeye çevirme
        kelimeListesi = new List<kelimeClass>(kelimelerArray);



    }

    public void anaEkranModSecmeButton(int modeF)
    {
        mode = modeF;

        //ana ekranı kapatıp çalışma ekranını açıyor
        ekranlar[0].SetActive(false);
        ekranlar[1].SetActive(true);

        calismaBaslat();

    }

    public void calismaBaslat()
    {
        kullanılacakKelimelistesi = new List<kelimeClass>();
        kullanılmayacakKelimeListesi = new List<kelimeClass>();
        kullanıldıKelimeListesi = new List<kelimeClass>();
        // hepsi
        if (mode == 1 || mode ==3) 
        {
            kullanılacakKelimelistesi = kelimeListesi;            
        }
        //türkçeden ingilizceye bilinemeyenler
        else if (mode == 2)
        {
            foreach (kelimeClass kc in kelimeListesi)
            {
                if (kc.trIngSonDogruSayisi < 9)
                    kullanılacakKelimelistesi.Add(kc);
                else
                    kullanılmayacakKelimeListesi.Add(kc);
            }            

        }
        //ingilizceden türkçeye bilinmeyenler
        else if (mode == 4)
        {
            foreach (kelimeClass kc in kelimeListesi)
            {
                if (kc.ingTrSonDogruSayisi < 9)
                    kullanılacakKelimelistesi.Add(kc);
                else
                    kullanılmayacakKelimeListesi.Add(kc);
            }            
        }

        yeniKelimeGetir();
    }

    #region Button Function
    
    public void anaEkranaDonButton()
    {
        kelimeleriKaydet();

        ekranlar[1].SetActive(false);
        ekranlar[2].SetActive(false);
        ekranlar[0].SetActive(true);

        altKelime.gameObject.SetActive(false);
    }
    public void kelimeButton()
    {
        altKelime.gameObject.SetActive(true);
    }
    public void dogruButton()
    {
        if (mode == 1 || mode == 2)
        {
            kullanılacakKelimelistesi[currentWord].trIngSonDogruSayisi++;
        }
            
        else if (mode == 3 || mode == 4)
        {
            kullanılacakKelimelistesi[currentWord].ingTrSonDogruSayisi++;            
        }

        kullanıldıKelimeListesi.Add(kullanılacakKelimelistesi[currentWord]);
        kullanılacakKelimelistesi.RemoveAt(currentWord);

        altKelime.gameObject.SetActive(false);
        yeniKelimeGetir();
    }
    public void yanlisButton()
    {
        if (mode == 1 || mode == 2)
            kullanılacakKelimelistesi[currentWord].trIngSonDogruSayisi=0;
        else if (mode == 3 || mode == 4)
            kullanılacakKelimelistesi[currentWord].ingTrSonDogruSayisi=0;

        kullanıldıKelimeListesi.Add(kullanılacakKelimelistesi[currentWord]);
        kullanılacakKelimelistesi.RemoveAt(currentWord);

        altKelime.gameObject.SetActive(false);
        yeniKelimeGetir();

    }

    public void anaEkranKelimeKaydetButton()
    {
        ekranlar[0].SetActive(false);
        ekranlar[2].SetActive(true);
    }
    public void kelimeEklemeEkraniAnasayfaButton()
    {
        ekranlar[2].SetActive(false);
        ekranlar[0].SetActive(true);
    }
    public void kelimeKaydetButton()
    {
        kelimeClass kc = new kelimeClass();
        kc.ing = ingIF.text;
        kc.tr = trIF.text;
        kc.trIngSonDogruSayisi = 0;
        kc.ingTrSonDogruSayisi = 0;

        kelimeListesi.Add(kc);

        ingIF.text = "";
        trIF.text = "";

        //// listeyi array e çevirme
        kelimeClass[] kelimeArray = kelimeListesi.ToArray();

        //// arraye çevirilmiş listeyi jsona çevirme
        string json = JsonHelper.ToJson(kelimeArray);

        //// Dosyaya yazmak için
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(json);
        writer.Close();

    }

    #endregion

    #region Helper Functions
    public void kelimeleriKaydet()
    {
        //kelimeListesi.Clear();

        List<kelimeClass> kcList = new List<kelimeClass>();

        if (kullanılacakKelimelistesi.Count > 0)
        {
            foreach (kelimeClass kc in kullanılacakKelimelistesi)
                kcList.Add(kc);
        }
        if (kullanıldıKelimeListesi.Count > 0)
        {
            foreach (kelimeClass kc in kullanıldıKelimeListesi)
                kcList.Add(kc);
        }
        if (kullanılmayacakKelimeListesi.Count > 0)
        {
            foreach (kelimeClass kc in kullanılmayacakKelimeListesi)
                kcList.Add(kc);
        }   
        

        //// listeyi array e çevirme
        kelimeClass[] kelimeArray = kcList.ToArray();

        //// arraye çevirilmiş listeyi jsona çevirme
        string json = JsonHelper.ToJson(kelimeArray);

        //// Dosyaya yazmak için
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(json);
        writer.Close();

        kelimeListesi = kcList;
    }

    public void yeniKelimeGetir()
    {
        if (kullanılacakKelimelistesi.Count > 0)
        {
            currentWord = Random.Range(0, kullanılacakKelimelistesi.Count);

            if (mode == 1 || mode == 2)
            {
                ustKelime.text = kullanılacakKelimelistesi[currentWord].tr;
                altKelime.text = kullanılacakKelimelistesi[currentWord].ing;
            }
            else if (mode == 3 || mode == 4)
            {
                ustKelime.text = kullanılacakKelimelistesi[currentWord].ing;
                altKelime.text = kullanılacakKelimelistesi[currentWord].tr;
            }
        }
        else
            anaEkranaDonButton();
        
    }
    #endregion





}
