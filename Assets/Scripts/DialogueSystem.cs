using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
namespace Kamaii
{   
    [Serializable]
    public class DialogueCollection {
        // This class Exists because unity can't parse top level arrays. Annoying, I know.
        public Dialogue[] dialogues;
    }
    [Serializable]
    public class Dialogue {
        public string text;
        public string author;
        public static Dialogue[] LoadFromJSON(string name)
        {
            var JSONFile = Resources.Load<TextAsset>("Dialogues/" + name);
            return JsonUtility.FromJson<DialogueCollection>(JSONFile.text).dialogues;
        }
    }
    public class DialogueSystem : MonoBehaviour
    {
        private Dictionary<string, AudioClip> _voiceCache;
        private AudioSource _audioSource;
        [SerializeField]
        private float _delay;
        private TextMeshProUGUI _textMeshProUGUI;
        private int _index;
        private string _text;
        private Dialogue _currentDialogue;
        private bool _done = true;
        public Queue<Dialogue> Queue { get; private set; }
        private static DialogueSystem instance;
        private void Awake()
        {
            _voiceCache = new Dictionary<string, AudioClip>();
            _audioSource = GetComponent<AudioSource>();
            PlayerInput.OnInteract += Next;
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            if (instance == null)
                instance = this;
            else
                Debug.LogError("Error, found more than one dialogue system in the scene.");
            Queue = new Queue<Dialogue>();
        }
        private void ClearVoiceCache()
        {
            if (_voiceCache != null)
                _voiceCache.Clear();
        }
        public void Start()
        {
            Test();
        }
        private void Test()
        {
            Enqueue(Dialogue.LoadFromJSON("test"));
        }
        private void StartDialogue()
        {
            _text = "";
            _textMeshProUGUI.text = "";
            StopAllCoroutines();
            instance._done = false;
            StartCoroutine(CO_UpdateDialogue());
        }
        IEnumerator CO_UpdateDialogue()
        {
            AudioClip clip;
            _voiceCache.TryGetValue(_currentDialogue.author, out clip);
            while (!_done)
            {
                if (_index >= _currentDialogue.text.Length)
                {
                    _done = true;
                    _index = 0;
                    yield break;
                } 
                if(_currentDialogue.text[_index] == '{')
                {
                    int _searchIndex = _index + 1;
                    string value = "";
                    while(_currentDialogue.text[_searchIndex] != '}')
                    {
                        value += _currentDialogue.text[_searchIndex];
                        _searchIndex++;
                        if(_searchIndex > _currentDialogue.text.Length)
                        {
                            Debug.LogError("Error parsing pause in dialogue, could not find closing bracket in dialogue: " + this._currentDialogue.text);
                            yield break;
                        }
                    }
                    _index = _searchIndex + 1;
                    float delay = -1f;
                    float.TryParse(value, out delay);
                    if(delay <= 0f)
                    {
                        Debug.LogWarning("Found negative value in dialogue: " + this._currentDialogue.text);
                        yield return new WaitForSeconds(delay);
                    }
                    yield return new WaitForSeconds(delay);
                }
                _text += _currentDialogue.text[_index];
                _textMeshProUGUI?.SetText(_text);
                _index++;
                if(clip != null)
                _audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(_delay);
            }
        }
        public static void Enqueue(Dialogue[] dialogues)
        {
            instance.ClearVoiceCache();
            for(int i = 0; i < dialogues.Length; i++)
            {
                instance.Queue.Enqueue(dialogues[i]);
            }
            instance.Next();
        }
        public static void Enqueue(Dialogue dialogue)
        {
            instance.ClearVoiceCache();
            instance.Queue.Enqueue(dialogue);
        }
        private void Next()
        {
            if(!_done || Queue.Count <= 0)
            {
                return;
            }
            _currentDialogue = Queue.Dequeue();
            LoadVoiceIntoCache();
            StartDialogue();
        }
        public void LoadVoiceIntoCache()
        {
            if (!_voiceCache.ContainsKey(_currentDialogue.author))
            {
                try
                {
                    AudioClip clip = Resources.Load<AudioClip>("Voices/" + _currentDialogue.author);
                    _voiceCache.Add(_currentDialogue.author, clip);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
        public static void Clear()
        {
            instance.Queue.Clear();
        }
    }

}

