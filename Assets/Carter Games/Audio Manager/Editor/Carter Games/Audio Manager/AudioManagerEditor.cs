using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

/************************************************************************************
 * 
 *							          Audio Manager
 *							  
 *				        Custom Audio Manager Inspector Editor Script
 *			
 *			                        Script written by: 
 *			        Jonathan Carter (https://jonathan.carter.games)
 *			        
 *									Version: 2.3.4
 *						   Last Updated: 11/10/2020 (d/m/y)					
 * 
*************************************************************************************/

namespace CarterGames.Assets.AudioManager
{
    /// <summary>
    /// Editor Class (*Not Static*) | The Audio Manager custom inspector editor script, should be placed in an /editor folder. 
    /// </summary>
    [CustomEditor(typeof(AudioManager)), CanEditMultipleObjects]
    public class AudioManagerEditor : Editor
    {
        // Colours for the Editor Buttons
        private Color32 scanCol = new Color32(41, 176, 97, 255);
        private Color32 redCol = new Color32(190, 42, 42, 255);

        private bool shouldShowMessage;

        private List<AudioClip> audioList;              // List of Audioclips used to add the audio to the library in the Audio Manager Script
        private List<string> audioStrings;              // List of Strings used to add the names of the audio clips to the library in the Audio Manager Script

        private AudioManager audioManagerScript;        // Reference to the Audio Manager Script that this script overrides the inspector for

        private string _newPath = "";

        private bool showDirectories = false;
        private bool showClips = true;
        private bool isSetup = false;



        // Overrides the Inspector of the Audio Manager Script with this stuff...
        public override void OnInspectorGUI()
        {
            // References the Audio Manager Script
            audioManagerScript = (AudioManager)target;

            // If the audio source is not attached
            if (!isSetup)
            {
                // Init Setup if needed (makes an audio folder and audio manager file if not alreadt there and adds an audio source to the game object this is on so it can preview sounds)
                FirstSetup();

                // Sets the boolean values up to what they were set to last.
                showDirectories = serializedObject.FindProperty("shouldShowDir").boolValue;
                showClips = serializedObject.FindProperty("shouldShowClips").boolValue;

                isSetup = true;
            }


            // Logo, Title & docs/discord links
            HeaderDisplay();

            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(5f);

            // Audio Manager File (AMF) field
            EditorGUILayout.BeginHorizontal();
            SerializedProperty fileProp = serializedObject.FindProperty("audioManagerFile");
            EditorGUILayout.PropertyField(fileProp, new GUIContent("File In Use: "));
            EditorGUILayout.EndHorizontal();


            // if file exsists
            if (audioManagerScript.audioManagerFile)
            {
                // if file does not equal the last file inputted
                if (!audioManagerScript.lastAudioManagerFile)
                {
                    // make the last file this file
                    audioManagerScript.lastAudioManagerFile = audioManagerScript.audioManagerFile;
                }
                else if (audioManagerScript.lastAudioManagerFile != audioManagerScript.audioManagerFile)
                {
                    audioManagerScript.lastAudioManagerFile = audioManagerScript.audioManagerFile;
                }

                EditorGUILayout.BeginHorizontal();
                SerializedProperty prefabProp = serializedObject.FindProperty("soundPrefab");
                EditorGUILayout.PropertyField(prefabProp, new GUIContent("Prefab: "));

                // Saves the selection into the SO for future use...
                if (audioManagerScript.soundPrefab)
                {
                    audioManagerScript.audioManagerFile.soundPrefab = audioManagerScript.soundPrefab;
                }

                EditorGUILayout.EndHorizontal();
            }


            GUILayout.Space(5f);
            EditorGUILayout.EndVertical();

            GUILayout.Space(10f);

            if (audioManagerScript.audioManagerFile)
            {
                // Directories & Clips Buttons
                if (audioManagerScript.audioManagerFile.soundPrefab != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (!showDirectories)
                    {
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Show Directories", GUILayout.Width(120)))
                        {
                            serializedObject.FindProperty("shouldShowDir").boolValue = !serializedObject.FindProperty("shouldShowDir").boolValue;
                            showDirectories = serializedObject.FindProperty("shouldShowDir").boolValue;
                        }
                    }
                    else
                    {
                        GUI.color = Color.white;
                        if (GUILayout.Button("Hide Directories", GUILayout.Width(120)))
                        {
                            serializedObject.FindProperty("shouldShowDir").boolValue = !serializedObject.FindProperty("shouldShowDir").boolValue;
                            showDirectories = serializedObject.FindProperty("shouldShowDir").boolValue;
                        }
                    }

                    if (!showClips)
                    {
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Show Clips", GUILayout.Width(95)))
                        {
                            serializedObject.FindProperty("shouldShowClips").boolValue = !serializedObject.FindProperty("shouldShowClips").boolValue;
                            showClips = serializedObject.FindProperty("shouldShowClips").boolValue;
                        }
                    }
                    else
                    {
                        GUI.color = Color.white;
                        if (GUILayout.Button("Hide Clips", GUILayout.Width(95)))
                        {
                            serializedObject.FindProperty("shouldShowClips").boolValue = !serializedObject.FindProperty("shouldShowClips").boolValue;
                            showClips = serializedObject.FindProperty("shouldShowClips").boolValue;
                        }
                    }
                    GUI.color = Color.white;
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();


                    // Directories Display
                    if (showDirectories)
                    {
                        EditorGUILayout.Space();

                        EditorGUILayout.BeginVertical("Box");

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.LabelField("Directories", EditorStyles.boldLabel, GUILayout.Width(75f));
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                        // controls the directories 
                        if (audioManagerScript.audioManagerFile.hasDirectories)
                        {
                            DirectoriesDisplay();
                        }
                        else
                        {
                            EditorGUILayout.HelpBox("No directories on file, use the buttons below to add a new directory to scan.", MessageType.Info);
                            EditorGUILayout.Space();

                            EditorGUILayout.Space();
                            _newPath = EditorGUILayout.TextField(new GUIContent("Path To Add:"), _newPath);

                            EditorGUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            GUI.color = scanCol;

                            if (GUILayout.Button("Continue", GUILayout.Width(80)))
                            {
                                audioManagerScript.audioManagerFile.directory = new List<string>();
                                AddToDirectories(_newPath);
                                audioManagerScript.audioManagerFile.hasDirectories = true;
                                serializedObject.Update();
                            }

                            GUI.color = Color.white;
                            GUILayout.FlexibleSpace();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.EndVertical();
                    }
                }


                // Assigns the sound prefab if there is one in the SO, this can still be edited by the user
                if ((audioManagerScript.audioManagerFile) && (audioManagerScript.audioManagerFile.soundPrefab))
                {
                    audioManagerScript.soundPrefab = audioManagerScript.audioManagerFile.soundPrefab;
                }

                GUILayout.Space(10f);


                // Clips Display
                if (showClips)
                {
                    EditorGUILayout.BeginVertical("Box");

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.LabelField("Clips", EditorStyles.boldLabel, GUILayout.Width(45f));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    if (CheckAmount() > 0)
                    {
                        if (audioManagerScript.audioManagerFile.hasDirectories && CheckAmount() > audioManagerScript.GetNumberOfClips())
                        {
                            serializedObject.FindProperty("hasScannedOnce").boolValue = true;  // Sets the has scanned once to true so the scan button turns into the re-scan button
                            
                            // Init Lists
                            audioList = new List<AudioClip>();
                            audioStrings = new List<string>();

                            // Auto filling the lists 
                            AddAudioClips();
                            AddStrings();

                            // Updates the lists
                            audioManagerScript.audioManagerFile.clipName = audioStrings;
                            audioManagerScript.audioManagerFile.audioClip = audioList;

                            audioManagerScript.UpdateLibrary();
                            serializedObject.Update();
                        }
                        else if (audioManagerScript.audioManagerFile.hasDirectories && CheckAmount() == audioManagerScript.GetNumberOfClips())
                        {
                            DisplayNames();
                        }
                        else
                        {
                            // *** Labels ***
                            HelpLabels();
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("No audio clips found in your project.", MessageType.Warning);
                    }

                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Please assign a Audio Manager File to use this asset.", MessageType.Info, true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            
            // applies and changes to SO's
            serializedObject.ApplyModifiedProperties();
        }


        /// <summary>
        /// Runs the Init setup for the manager if needed....
        /// </summary>
        private void FirstSetup()
        {
            // Adds an Audio Source to the gameobject this script is on if its not already there (used for previewing audio only) 
            // * Hide flags hides it from the inspector so you don't notice it there *
            if (audioManagerScript.gameObject.GetComponent<AudioSource>())
            {
                audioManagerScript.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
                audioManagerScript.GetComponent<AudioSource>().playOnAwake = false;
            }
            else
            {
                audioManagerScript.gameObject.AddComponent<AudioSource>();
                audioManagerScript.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
                audioManagerScript.GetComponent<AudioSource>().playOnAwake = false;
            }

            // Makes the audio directoy if it doesn't exist in your project
            // * This will not create a new folder if you already have an audio folder *
            // * As of V2 it will also create a new Audio Manager audioManagerFile if there isn't one in the audio folder *
            if (!Directory.Exists(Application.dataPath + "/Audio"))
            {
                AssetDatabase.CreateFolder("Assets", "Audio");

                if (!Directory.Exists(Application.dataPath + "/Audio"))
                {
                    AssetDatabase.CreateFolder("Assets/Audio", "Files");
                }

                AssetDatabase.CreateFolder("Assets/Audio", "Files");

                AudioManagerFile _newAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
                AssetDatabase.CreateAsset(_newAMF, "Assets/Audio/Files/Audio Manager File.asset");
                audioManagerScript.audioManagerFile = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager File.asset", typeof(AudioManagerFile));
                audioManagerScript.audioManagerFile.directory = new List<string>();
            }
            else if (((Directory.Exists(Application.dataPath + "/Audio")) && (!Directory.Exists(Application.dataPath + "/Audio/Files"))))
            {
                AssetDatabase.CreateFolder("Assets/Audio", "Files");
                AudioManagerFile _newAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
                AssetDatabase.CreateAsset(_newAMF, "Assets/Audio/Files/Audio Manager File.asset");
                audioManagerScript.audioManagerFile = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager File.asset", typeof(AudioManagerFile));
                audioManagerScript.audioManagerFile.directory = new List<string>();
            }
        }


        /// <summary>
        /// Shows the header info including logo, asset name and documentation/discord buttons.
        /// </summary>
        private void HeaderDisplay()
        {
            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            // Note: if you are using an older version of the asset, the directory/name of the logo may not match this and therefore will display the text title only
            if (Resources.Load<Texture2D>("Carter Games/Audio Manager/LogoAM"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager/LogoAM"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            // Label that shows the name of the script / tool & the Version number for user reference sake.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Audio Manager", EditorStyles.boldLabel, GUILayout.Width(102f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 2.3.4", GUILayout.Width(85f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            // Links to the docs and discord server for the user to access quickly if needed.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Documentation", GUILayout.Width(110f)))
            {
                Application.OpenURL("https://carter.games/audiomanager.html");
            }
            GUI.color = Color.cyan;
            if (GUILayout.Button("Discord", GUILayout.Width(65f)))
            {
                Application.OpenURL("https://carter.games/discord");
            }
            GUI.color = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }


        /// <summary>
        /// Checks to see how many files are found from the sacn so it can be displayed.
        /// </summary>
        /// <returns></returns>
        private int CheckAmount()
        {
            int _amount = 0;
            List<string> _allFiles = new List<string>();

            if (audioManagerScript.audioManagerFile.directory != null)
            {
                for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
                {
                    if (AreAllDirctoryStringsBlank())
                    {
                        _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));
                    }
                    else
                    {
                        if (Directory.Exists(Application.dataPath + "/audio/" + audioManagerScript.audioManagerFile.directory[i]))
                        {
                            // 2.3.1 - adds a range so it adds each directory to the asset 1 by 1
                            _allFiles.AddRange(new List<string>(Directory.GetFiles(Application.dataPath + "/audio/" + audioManagerScript.audioManagerFile.directory[i])));
                        }
                        else
                        {
                            // !Warning Message - shown in the console should there not be a directory named what the user entered
                            _allFiles = new List<string>();
                            shouldShowMessage = true;
                        }
                    }
                }
            }

            // Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
            if (_allFiles.Count > 0)
            {
                foreach (string Thingy in _allFiles)
                {
                    string _path = "Assets" + Thingy.Replace(Application.dataPath, "").Replace('\\', '/');

                    if (AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip)))
                    {
                        ++_amount;
                    }
                }
            }

            return _amount;
        }



        /// <summary>
        /// Adds all strings for the found clips to the AMF.
        /// </summary>
        private void AddStrings()
        {
            for (int i = 0; i < audioList.Count; i++)
            {
                if (audioList[i] == null)
                {
                    audioList.Remove(audioList[i]);
                }
            }

            int _ignored = 0;

            for (int i = 0; i < audioList.Count; i++)
            {
                if (audioList[i].ToString().Contains("(UnityEngine.AudioClip)"))
                {
                    audioStrings.Add(audioList[i].ToString().Replace(" (UnityEngine.AudioClip)", ""));
                }
                else
                {
                    _ignored++;
                }
            }

            if (_ignored > 0)
            {
                // This message should never show up, but its here just incase
                Debug.LogAssertion("(*Audio Manager*): " + _ignored + " entries ignored, this is due to the files either been in sub directories or other files that are not Unity AudioClips.");
            }
        }


        /// <summary>
        /// Adds all the audioclips to the AMF.
        /// </summary>
        private void AddAudioClips()
        {
            // Makes a new lsit the size of the amount of objects in the path
            // In V:2.3 - Updated to allow for custom folders the hold audio clips (so users can organise their clips and still use the manager, default will just get all sounds in "/audio")
            List<string> _allFiles = new List<string>();


            // as of 2.3.1 - goes through all directories and scans the files before moving to sort them....
            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
            {
                if (AreAllDirctoryStringsBlank())
                {
                    _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));
                }
                else
                {
                    if (Directory.Exists(Application.dataPath + "/audio/" + audioManagerScript.audioManagerFile.directory[i]))
                    {
                        // 2.3.1 - adds a range so it adds each directory to the asset 1 by 1
                        _allFiles.AddRange(new List<string>(Directory.GetFiles(Application.dataPath + "/audio/" + audioManagerScript.audioManagerFile.directory[i])));
                    }
                    else
                    {
                        // !Warning Message - shown in the console should there not be a directory named what the user entered
                        Debug.LogWarning("(*Audio Manager*): Path does not exist! please make sure you spelt your path correctly: " + Application.dataPath + "/audio/" + audioManagerScript.audioManagerFile.directory[i]);
                        shouldShowMessage = true;
                    }
                }
            }

            // Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
            if (_allFiles.Any())
            {
                AudioClip _source;

                foreach (string _thingy in _allFiles)
                {
                    string _path = "Assets" + _thingy.Replace(Application.dataPath, "").Replace('\\', '/');

                    if (AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip)))
                    {
                        _source = (AudioClip)AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip));
                        audioList.Add(_source);
                    }
                }

                audioManagerScript.audioManagerFile.isPopulated = true;
            }
            else
            {
                // !Warning Message - shown in the console should there be no audio in the directory been scanned
                Debug.LogWarning("(*Audio Manager*): Please ensure there are Audio files in the directory: " + Application.dataPath + "/Audio");
            }
        }


        /// <summary>
        /// Creates the display that is used to show all the clips with play/stop buttons next to them.
        /// </summary>
        private void DisplayNames()
        {
            // Used as a placeholder for the clip name each loop
            string _elements;


            // Going through all the audio clips and making an element in the Inspector for them
            if (audioManagerScript.audioManagerFile && audioManagerScript.audioManagerFile.clipName.Any())
            {
                for (int i = 0; i < audioManagerScript.audioManagerFile.clipName.Count; i++)
                {
                    _elements = audioManagerScript.audioManagerFile.clipName[i];

                    // Starts the ordering
                    EditorGUILayout.BeginHorizontal();

                    // Changes the GUI colour to green for the buttons
                    GUI.color = scanCol;

                    // If there are no clips playing it will show "preview clip" buttons for all elements
                    if (!audioManagerScript.GetComponent<AudioSource>().isPlaying)
                    {
                        if (Resources.Load<Texture2D>("Carter Games/Audio Manager/Play"))
                        {
                            if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                                audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                                audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                            }
                        }
                    }
                    // if a clip is playing, the clip that is playing will have a "stop clip" button instead of "preview clip" 
                    else if (audioManagerScript.GetComponent<AudioSource>().clip == audioManagerScript.audioManagerFile.audioClip[i])
                    {
                        GUI.color = redCol;

                        if (Resources.Load<Texture2D>("Carter Games/Audio Manager/Stop"))
                        {
                            if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager/Stop"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().Stop();
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("S", GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().Stop();
                            }
                        }
                    }
                    // This just ensures the rest of the elements keep a button next to them
                    else
                    {
                        if (Resources.Load<Texture2D>("Carter Games/Audio Manager/Play"))
                        {
                            if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                                audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                            {
                                audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                                audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                            }
                        }
                    }

                    // Resets the GUI colour
                    GUI.color = Color.white;

                    // Adds the text for the clip
                    EditorGUILayout.TextArea(_elements, GUILayout.ExpandWidth(true));

                    // Ends the GUI ordering
                    EditorGUILayout.EndHorizontal();
                }
            }
        }


        /// <summary>
        /// Checks to see if there are no directoires...
        /// </summary>
        /// <returns></returns>
        private bool AreAllDirctoryStringsBlank()
        {
            int _check = 0;

            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
            {
                if (audioManagerScript.audioManagerFile.directory[i] == "")
                {
                    ++_check;
                }
            }


            if (_check == audioManagerScript.audioManagerFile.directory.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Adds the value inputted to both directories.
        /// </summary>
        /// <param name="value"></param>
        private void AddToDirectories(string value)
        {
            audioManagerScript.audioManagerFile.directory.Add(value);
        }


        /// <summary>
        /// Displays the directories if there are more than one in the AMF.
        /// </summary>
        private void DirectoriesDisplay()
        {
            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                audioManagerScript.audioManagerFile.directory[i] = EditorGUILayout.TextField(new GUIContent("Path #" + (i + 1).ToString() + ": "), audioManagerScript.audioManagerFile.directory[i]);

                if (i != audioManagerScript.audioManagerFile.directory.Count)
                {
                    GUI.color = scanCol;
                    if (GUILayout.Button("+", GUILayout.Width(25)))
                    {
                        audioManagerScript.audioManagerFile.directory.Add("");
                    }
                }

                GUI.color = Color.white;

                if (i != 0)
                {
                    GUI.color = Color.red;
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        audioManagerScript.audioManagerFile.directory.RemoveAt(i);
                    }
                }
                else
                {
                    GUI.color = Color.black;
                    if (GUILayout.Button("", GUILayout.Width(25)))
                    {
                    }
                }

                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }


        /// <summary>
        /// Shows a variety of help labels when stuff goes wrong, these just explain to the user what has happened and how they should go about fixing it.
        /// </summary>
        private void HelpLabels()
        {
            if (audioManagerScript.audioManagerFile && audioManagerScript.audioManagerFile.directory.Count > 0)
            {
                if ((audioManagerScript.audioManagerFile.isPopulated))
                {
                    if (audioManagerScript.soundLibrary.Count > 0 && audioManagerScript.soundLibrary.Count != CheckAmount())
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();

                        string _errorString = null;

                        if (audioManagerScript.audioManagerFile.directory.Count != 0)
                        {
                            _errorString = "No clips found in one of these directories, please check you have all directories spelt correctly:\n";

                            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
                            {
                                _errorString = _errorString + "assets/audio/" + audioManagerScript.audioManagerFile.directory[i] + "\n";
                            }
                        }
                        else
                        {
                            _errorString = "No clips found in: " + "assets/audio/";
                        }

                        EditorGUILayout.HelpBox(_errorString, MessageType.Info, true);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (audioManagerScript.audioManagerFile.audioClip.Count != CheckAmount())
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();

                        string _errorString = null;

                        if (audioManagerScript.audioManagerFile.directory.Count != 0)
                        {
                            _errorString = "No clips found in one of these directories, please check you have all directories spelt correctly:\n";

                            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
                            {
                                _errorString = _errorString + "assets/audio/" + audioManagerScript.audioManagerFile.directory[i] + "\n";
                            }
                        }
                        else
                        {
                            _errorString = "No clips found in: " + "assets/audio/";
                        }

                        EditorGUILayout.HelpBox(_errorString, MessageType.Info, true);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (CheckAmount() == 0)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();

                        string _errorString = null;

                        if (audioManagerScript.audioManagerFile.directory.Count != 0)
                        {
                            _errorString = "No clips found in one of these directories, please check you have all directories spelt correctly:\n";

                            for (int i = 0; i < audioManagerScript.audioManagerFile.directory.Count; i++)
                            {
                                _errorString = _errorString + "assets/audio/" + audioManagerScript.audioManagerFile.directory[i] + "\n";
                            }
                        }
                        else
                        {
                            _errorString = "No clips found in: " + "assets/audio/";
                        }

                        EditorGUILayout.HelpBox(_errorString, MessageType.Info, true);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }
                    else
                    {
                    }
                }
                else if (shouldShowMessage)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("There was a problem scanning, please check the console for more information", MessageType.Warning, true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }
        }
    }
}