using System;
using System.Collections.Generic;
using CategoryQuestions.Assets.Scripts.Core.Animations;
using CategoryQuestions.Assets.Scripts.Core.ExtendedMethods;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CategoryQuestions.Core.Audio
{
    public class MediaPlayer : MediaPlayerBase
    {
        public int CountPopBallSounds = 2;
        public List<string> BallConnected = new List<string> { "hit_ball", "hit_ball_2" };

        public MediaPlayer(GameObject root, string soundsPath) : base(root, soundsPath)
        {
            OffsetTime = TimeSpan.FromMilliseconds(100);
        }

        #region Background Music

        private AudioSource _backgroundMusic;
        private Storyboard _backgroundVolumeTuner;
        private AudioSource _popBallSound;
        private string _currentBackgroundMusicName;

        public void PlayHomeBackgroundMusic()
        {
            var houseBackgroundMusicName = "amb";
            if (_currentBackgroundMusicName != houseBackgroundMusicName)
            {
                SetBackgroundMusic(houseBackgroundMusicName, 0);
                PlayBackgroundMusic();
                MakeBackgroundMusicLoud();
            }
        }

        public void StopHomeBackgroundMusic()
        {
            StopBackgroundMusic();
        }

        private void SetBackgroundMusic(string backgroundMusic, float volume = 0.6f)
        {
            if (_backgroundMusic == null)
            {
                _backgroundMusic = _root.AddComponent<AudioSource>();
            }
            _currentBackgroundMusicName = backgroundMusic;
            var soundPath = GetSoundPath(backgroundMusic);
            _backgroundMusic.clip = Resources.Load<AudioClip>(soundPath);
            _backgroundMusic.loop = true;
            _backgroundMusic.volume = volume;
        }

        private void PlayBackgroundMusic()
        {
            if (_backgroundMusic == null || _backgroundMusic.isPlaying) return;
            _backgroundMusic.Play();
        }

        private void StopBackgroundMusic()
        {
            if (_backgroundMusic != null)
            {
                _backgroundMusic.Stop();
                _currentBackgroundMusicName = null;
            }
        }

        private void MakeBackgroundMusicLoud()
        {
            TuneBackroundVolumeTo(1f);
        }

        private void MakeBackgroundMusicQuiet()
        {
            TuneBackroundVolumeTo(0.1f);
        }

        public void PlayPopBall()
        {
            if (_popBallSound == null || !_popBallSound.isPlaying)
            {
                var soundName = string.Format("pop_ball_{0}", new System.Random().Next(CountPopBallSounds));
                _popBallSound = LoadSound(soundName);
            }
            PlaySound(_popBallSound);
        }

        private void TuneBackroundVolumeTo(float targetValue)
        {
            TuneBackroundVolumeTo(targetValue, TimeSpan.FromSeconds(0.75d));
        }

        private void TuneBackroundVolumeTo(float targetValue, TimeSpan duration)
        {
            if (_backgroundMusic == null || Mathf.Abs(_backgroundMusic.volume - targetValue) < 0.01f)
            {
                return;
            }

            var doubleAnimation = new DoubleAnimation
            {
                Duration = duration,
                From = _backgroundMusic.volume,
                To = targetValue
            };
            doubleAnimation.Tick += value => _backgroundMusic.volume = (float)value;

            if (_backgroundVolumeTuner != null)
            {
                Object.Destroy(_backgroundVolumeTuner);
            }

            _backgroundVolumeTuner = _root.AddComponent<Storyboard>();
            _backgroundVolumeTuner.Children.Add(doubleAnimation);
            _backgroundVolumeTuner.Completed += (sender, args) => Object.Destroy(_backgroundVolumeTuner);

            _backgroundVolumeTuner.Begin();
        }

        #endregion

        public void SetVolume(string soundName, float volume)
        {
            var audioSource = LoadSound(soundName);
            audioSource.volume = volume;
        }

        public TimeSpan GetSoundLineDuration(List<string> soundLine)
        {
            var soundLineDuration = TimeSpan.Zero;
            for (int i = 0; i < soundLine.Count; i++)
            {
                var sound = soundLine[i];
                soundLineDuration += GetSoundDuration(sound);
                if (i < soundLine.Count - 1)
                {
                    soundLineDuration += OffsetTime;
                }
            }
            return soundLineDuration;
        }

        public void PlaySoundBallConnected()
        {
            PlaySound(BallConnected.GetRandomItem());
        }
    }
}