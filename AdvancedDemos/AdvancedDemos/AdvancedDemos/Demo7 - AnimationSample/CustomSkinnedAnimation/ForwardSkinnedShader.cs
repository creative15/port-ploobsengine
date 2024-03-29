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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Material
{
    public class ForwardSkinnedShader : IShader
    {
        public ForwardSkinnedShader()
        {
            
        }
        
        private SkinnedEffect effect;

        public SkinnedEffect SkinnedEffect
        {
            get { return effect; }            
        }

        Matrix[] bones;

        public void SetBones(Matrix[] bones)
        {
            this.bones = bones;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            effect = factory.GetSkinnedEffect();
            base.Initialize(ginfo,factory,obj);            
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt">gametime</param>
        /// <param name="obj">the obj</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights"></param>
        protected override void  Draw(GameTime gt, IObject obj, RenderHelper render, Cameras.ICamera cam, IList<Light.ILight> lights)
        {
            if (bones == null)
                return;

            base.Draw(gt, obj, render, cam, lights);
            
            effect.View = cam.View;
            effect.Projection = cam.Projection;
            effect.SetBoneTransforms(bones);

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {                    
                    effect.Texture = obj.Modelo.getTexture(Modelo.TextureType.DIFFUSE,i,j);                    
                    effect.World = bi[j].ModelLocalTransformation * obj.WorldMatrix;
                    render.RenderBatch(bi[j],effect);
                }
            }
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
        }
    }
}
