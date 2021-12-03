using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TextVoixData : ScriptableObject
{
    public bool VerifEnigme1;
    public bool VerifA;
    public string Enigme1;

    public bool VerifEnigme2;
    public bool VerifB;
    public string Enigme2;

    public bool VerifEnigme3;
    public bool VerifC;
    public string Enigme3;

    public bool VerifEnigme3Bis;
    public bool VerifD;
    public string Enigme3Bis;

    public List<string> dialEnigme;

    public List<string> dialIndice;

}
