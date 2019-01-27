using System;
using System.Collections;
using UnityEngine;


namespace Game.Audio
{
    /// <summary>
    /// Источник звука. Например шаги. Может содержать несколько звуковых файлов.
    /// Требует наличия компонента AudioSource.
    /// Управление полем Clip в компоненте AudioSource происходит отсюда.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    class SoundComponent : MonoBehaviour
    {
        /// <summary>
        /// Имя источника звука
        /// </summary>
        [Tooltip("Имя источника звука")]
        [SerializeField]
        private string _name;
        /// <summary>
        /// Имя источника звука
        /// </summary>
        public string Name
        {
            get => _name;
        }
        /// <summary>
        /// Список звуков
        /// </summary>
        [Tooltip("Список звуков")]
        [SerializeField]
        private Sound[] _sounds;
        /// <summary>
        /// Ссылка на компонет AudioSource
        /// </summary>
        AudioSource _source;
        /// <summary>
        /// Ссылка на компонет AudioSource
        /// </summary>
        public AudioSource Source => _source;
        /// <summary>
        /// Громкость звука
        /// </summary>
        public float Volume
        {
            get => Source.volume;
            set => Source.volume = value < 0 ? 0 : value;
        }
        /// <summary>
        /// Скорость воспроизведения
        /// </summary>
        public float Pitch
        {
            get => Source.pitch;
            set => Source.pitch = value < -3 ? -3: value>3?3:value;
        }
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            if (_source.playOnAwake && _sounds.Length > 0)
            {
                _source.clip = _sounds[0].Clip;
                _source.Play();
            }            
        }
        /// <summary>
        /// Воспроизводит клип, если в источнике Audisource поле Clip не равно нулю
        /// </summary>
        public void Play()
        {
            if (_source.clip != null)
            {
                _source.Play();
            }
        }
       /// <summary>
       /// Воспроизодит кли с указанным именем
       /// </summary>
       /// <param name="name">Название клипа</param>
        public void Play(string name)
        {
            _source.clip = Array.Find(_sounds, s => s.Name == name).Clip;
            _source.Play();

        }
        /// <summary>
        /// Останавливает воспроизведение
        /// </summary>
        public void Stop()
        {
            _source.Stop();
        }
        /// <summary>
        /// Источник еще воспроизводит звук?
        /// </summary>
        /// <returns></returns>
        public bool SourcePlaying() => _source.isPlaying;
        /// <summary>
        /// Играет ли звук с заданным именем
        /// </summary>
        /// <param name="name">Имя</param>
        /// <returns></returns>
        public bool SoundIsPlaying(string name) => _source.isPlaying && Array.Find(_sounds, s => s.Clip == _source.clip).Name == name;
        /// <summary>
        /// Воспроизводит случайный звук из списка Sounds
        /// </summary>
        public void PlayRandomSound()
        {
            _source.clip = _sounds[UnityEngine.Random.Range(0, _sounds.Length)].Clip;
            _source.Play();
        }
        /// <summary>
        /// Поднимает громкость звука источника с 0 до установленного в значении Audioclip за указанный промежуток времени
        /// </summary>
        /// <param name="fadeTime">длительность фейда в секундах</param>
        private IEnumerator FadeInCor(float fadeTime)
        {
            var currentVol = _source.volume;
            float t = 0f;
            _source.volume = 0f;
            
            while (t <= fadeTime)
            {
                _source.volume = Mathf.Lerp(0f, currentVol, t / fadeTime);
                t += Time.deltaTime;
                yield return null;
            }
            _source.volume = currentVol;

        }
        /// <summary>
        /// Поднимает громкость звука источника с 0 до установленного в значении Audioclip за указанный промежуток времени
        /// </summary>
        /// <param name="fadeTime">длительность фейда в секундах</param>
        public void FadeIn(float fadeTime)
        {
            StartCoroutine(FadeInCor( fadeTime));
        }
        /// <summary>
        /// Лпускает громкость звука источника до 0 за указанный промежуток времени
        /// </summary>
        /// <param name="fadeTime">длительность фейда в секундах</param>
        private IEnumerator FadeOutCor(float fadeTime)
        {            
            float t = 0f;
            float x = _source.volume;
            while (t <= fadeTime)
            {
                _source.volume = Mathf.Lerp(x, 0f, t / fadeTime);
                t += Time.deltaTime;
                yield return null;
            }
            _source.volume = 0f;   
        }
        /// <summary>
        /// Поднимает громкость звука источника с 0 до установленного в значении Audioclip за указанный промежуток времени
        /// </summary>
        /// <param name="fadeTime">длительность фейда в секундах</param>
        public void FadeOut( float fadeTime)
        {
            StartCoroutine(FadeOutCor(fadeTime));
        }
        
    }
}
