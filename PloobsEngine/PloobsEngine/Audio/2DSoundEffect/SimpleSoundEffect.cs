﻿#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Audio
{
    public class SimpleSoundEffect
    {
        SoundEffect mySoundEffect;
        SoundEffectInstance mySoundEffectInstance;        

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSoundEffect"/> class.
        /// </summary>
        /// <param name="cmanager">The cmanager.</param>
        /// <param name="name">The name.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="pitch">The pitch(-1 to 1).</param>
        /// <param name="pan">The pan (-1 to 1).</param>
        /// <param name="isLooped">if set to <c>true</c> [is looped].</param>
        public SimpleSoundEffect(IContentManager cmanager , string name, float volume = 1, float pitch = 0, float pan = 0, bool isLooped = false)
        {
            System.Diagnostics.Debug.Assert(cmanager != null);
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(name));
            System.Diagnostics.Debug.Assert(volume >= -1 && volume <= 1);
            System.Diagnostics.Debug.Assert(pitch >= -1 && pitch <= 1);
            System.Diagnostics.Debug.Assert(pan >= -1 && pan <= 1);            
            this.mySoundEffect = cmanager.GetAsset<SoundEffect>(name);
            this.Duration = mySoundEffect.Duration;
            this.Name = mySoundEffect.Name;
            this.mySoundEffectInstance = mySoundEffect.CreateInstance();
            mySoundEffectInstance.Pan = pan;
            mySoundEffectInstance.Pitch = pitch;
            mySoundEffectInstance.Volume = volume;
            mySoundEffectInstance.IsLooped = isLooped;
        }
        
        public TimeSpan Duration
        {
            get;
            internal set;
        }

        public String Name
        {
            get;
            internal set;
        }

        public SoundEffectInstance MySoundEffectInstance
        {
            get { return mySoundEffectInstance; }
        }        

        public void ForcePlay()
        {
            this.mySoundEffect.Play();
        }
        public void Play()
        {

            this.mySoundEffectInstance.Play();
        }
        public void Stop()
        {
            this.mySoundEffectInstance.Stop();

        }

        public void Pause()
        {
            this.mySoundEffectInstance.Pause();
        }

        public void Resume()
        {
            this.mySoundEffectInstance.Resume();
        }

        public SoundState State
        {
            get
            {
                return this.mySoundEffectInstance.State;                
            }
        }

        public void setVolume(float volume)
        {
            System.Diagnostics.Debug.Assert(volume >= -1 && volume <= 1);
            this.mySoundEffectInstance.Volume = volume;
        }
    }
}
