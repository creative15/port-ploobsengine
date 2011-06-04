﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;

namespace PloobsEngine.Features.DebugDraw
{
    public class DebugLines : IDebugDrawShape
    {
        List<VertexPositionColor> verts = new List<VertexPositionColor>();

        /// <summary>
        /// The basic effect used to draw boxes.
        /// </summary>
        private static BasicEffect effect = null;

        /// <summary>
        /// Add a Line
        /// You cant remove it        
        /// </summary>
        /// <param name="StartPoint">The start point.</param>
        /// <param name="EndPoint">The end point.</param>
        /// <param name="color">The box's color.</param>
        public void AddLine(Vector3 StartPoint, Vector3 EndPoint, Color color)
        {
            VertexPositionColor vp1 = new VertexPositionColor(StartPoint, color);
            VertexPositionColor vp2 = new VertexPositionColor(EndPoint, color);
            verts.Add(vp1);
            verts.Add(vp2);            
        }

        /// <summary>
        /// Clears the lines.
        /// </summary>
        public void ClearLines()
        {
            verts.Clear();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLines"/> class.        
        /// Use addline to add lines =P
        /// </summary>
        public DebugLines()
        {
            Visible = true;
        }
         
        public void  Initialize(Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {
            if (effect == null)
            {
                effect = factory.GetBasicEffect();
                effect.VertexColorEnabled = true;
                effect.LightingEnabled = false;
                effect.TextureEnabled = false;                
            }        
        }
    

        /// <summary>
        /// Draws the box.
        /// </summary>
        /// <param name="view">The viewing matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        public void Draw(RenderHelper render, Matrix view, Matrix projection)
        {
            if (Visible)
            {
                if (verts.Count == 0)
                    return;
                //// Setup the effect.                
                effect.View = view;
                effect.Projection = projection;                                   
                render.RenderUserPrimitive<VertexPositionColor>(effect,PrimitiveType.LineList, verts.ToArray(), 0, verts.Count() / 2);
                
            }
        }

        #region IDebugDrawShape Members

        public bool Visible
        {
            get;
            set;
        }

        #endregion
    }    
}