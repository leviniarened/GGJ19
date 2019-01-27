using System;
using UnityEngine;

namespace Game.Audio
{
    /// <summary>
    /// Верхний уровень контроля звука. С помощью него можно обратиться ко всем источникам звука через поле Name.
    /// Например на игроке могут присутствовать шаги, дыхание, реакции и тд. С помощью этого компонента можно гибко управлять ими.
    /// </summary>
    class SoundController:MonoBehaviour
    {
        /// <summary>
        /// Список источников звука
        /// </summary>
        [Tooltip("Список источников звук")]
        [SerializeField]
        private SoundComponent[] _sources;
        /// <summary>
        /// Ворвращает источник звука по заданному имени
        /// </summary>
        /// <param name="name">имя источника звука</param>
        /// <returns></returns>
        public SoundComponent GetSource(string name) => Array.Find(_sources, s => s.Name == name);

         
    }
}
