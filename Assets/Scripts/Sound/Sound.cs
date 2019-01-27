using UnityEngine;
using System;

namespace Game.Audio
{
    /// <summary>
    /// Звуковое сообщение.
    /// </summary>
    [Serializable]
    public class Sound
    {
        /// <summary>
        /// Звуковая дорожка
        /// </summary>
        [Tooltip("Звуковая дорожка")]
        public AudioClip Clip;
        /// <summary>
        /// Название дорожки
        /// </summary>
        [Tooltip("Название дорожки")]
        public string Name;
        

    }
}
