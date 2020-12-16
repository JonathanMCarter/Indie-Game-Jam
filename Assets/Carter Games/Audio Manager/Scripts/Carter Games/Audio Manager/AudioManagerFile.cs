using System.Collections.Generic;
using UnityEngine;

/************************************************************************************
 * 
 *							          Audio Manager
 *							  
 *				           Audio Manager File Scriptable Object
 *			
 *			                        Script written by: 
 *			        Jonathan Carter (https://jonathan.carter.games)
 *			        
 *									Version: 2.3.4
 *						   Last Updated: 03/10/2020 (d/m/y)					
 * 
*************************************************************************************/

namespace CarterGames.Assets.AudioManager
{
    /// <summary>
    /// Scriptable Object (*Not Static*) | Holds the audio data that is used in the Audio Manager asset.
    /// </summary>
    [System.Serializable, CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
    public class AudioManagerFile : ScriptableObject
    {
        [SerializeField] public List<string> clipName;      // Holds a list of the clip names stored in the AMF.
        [SerializeField] public List<AudioClip> audioClip;  // Holds a list of the audio clips stored in the AMF.
        [SerializeField] public GameObject soundPrefab;     // Holds the prefab spawned in to play sound from this AMF.
        [SerializeField] public bool isPopulated;           // Holds the boolean value for whether or not this AMF has been used to store audio.
        [SerializeField] public bool hasDirectories;        // Holds the boolean value for whether or not this AMF has directories stored on it.
        [SerializeField] public List<string> directory;     // Holds a list of directory strings for use in the AM.
    }
}