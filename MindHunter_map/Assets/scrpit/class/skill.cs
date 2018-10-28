using UnityEngine;
using UnityEditor;

public class Skill 
{
    public KeyCode key { get; set; }
    public int maxHoldFrames { get; set; }
    public int holdFrames { get; set; }
    public int cdFrames { get; set; }
    public int cdRestFrames { get; set; }
    public Skill(KeyCode key,int maxFrames)
    {
        this.key = key;
        this.maxHoldFrames = maxFrames;
    }
}