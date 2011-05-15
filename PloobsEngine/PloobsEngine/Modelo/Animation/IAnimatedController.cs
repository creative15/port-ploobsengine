﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace PloobsEngine.Modelo.Animation
{
    public delegate void HandleAnimationObserverCall(Object obj );

    /// <summary>
    /// Animation Controler Interface
    /// User interact with animated model by this controller
    /// </summary>
    public interface IAnimatedController : ISerializable
    {
        /// <summary>
        /// Changes the animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        /// <param name="mode">The interpolation mode.</param>
        void ChangeAnimation(String animationName, AnimationChangeMode mode);
        /// <summary>
        /// Updates the controller.
        /// CAlled by the API
        /// </summary>
        /// <param name="gt">The gt.</param>
        void Update(GameTime gt);
        /// <summary>
        /// Changes the interpolation mode.
        /// </summary>
        /// <param name="im">The im.</param>
        void ChangeInterpolationMode(AnimationInterpolationMode im);
        /// <summary>
        /// Gets the bone transformations.
        /// </summary>
        /// <returns></returns>
        Matrix[] GetBoneTransformations();        
        /// <summary>
        /// Transforms the bone.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <param name="rot">The rot.</param>
        void TransformBone(String boneName, Quaternion rot);
        /// <summary>
        /// Gets the bone absolute transform.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <returns></returns>
        Matrix GetBoneAbsoluteTransform(String boneName);

    }

    /// <summary>
    /// AnimationChangeMode
    /// </summary>
   public  enum AnimationChangeMode
    {

        /// <summary>
        /// Use Blending
        /// </summary>
       Blend ,
       /// <summary>
       /// Dont use blending
       /// </summary>
       Normal

    }
   /// <summary>
   /// interpolations Modes
   /// </summary>
   public enum AnimationInterpolationMode
    {
        /// <summary>
        /// No interpolation, Dont use this !!!
        /// </summary>
       No_Interpolation ,
       /// <summary>
       /// Linear
       /// </summary>
       Linear_Interpolation ,
       /// <summary>
       /// Cubic
       /// </summary>
       Cubic_Interpolation ,
       /// <summary>
       /// Spherical
       /// </summary>
       Spherical_Interpolation
    }
}
