﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Input;
using PloobsEngine.SceneControl._2DScene;
using PloobsEngine.Physic2D.Farseer;
using PloobsEngine.Modelo2D;
using PloobsEngine.Material2D;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace PloobsEnginePhone7Template
{
    /// <summary>
    /// Gestures works normally in 2D and 3D
    /// Gestures uses the same idea of Ploobs Inputs for PC Windows
    /// </summary>
    public class InputGestureScreen : I2DScene
    {
        int doubleTapCount = 0;        
        Texture2D tile;        
        Border border;
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();            
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;

            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)));            
        }

        void rt_RenderBackGround(GraphicInfo ginfo,RenderHelper render)
        {
            Rectangle source = new Rectangle(0, 0, ginfo.Viewport.Width, ginfo.Viewport.Height);
            render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearWrap, BlendState.Opaque, RasterizerState.CullNone,DepthStencilState.Default);
            render.RenderTexture(tile, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            render.RenderEnd();            
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IContentManager contentManager)
        {
            tile = factory.GetTexture2D("Textures/tile");


            FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;

            ///border
            border = new Border(fworld, factory, GraphicInfo, factory.CreateTexture2DColor(1, 1, Color.Red));

            ///from texture
            {
                Texture2D tex = factory.GetTexture2D("Textures//goo");
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, tex);
                
                I2DObject o = new I2DObject(fs, mat, model);
                o.Name = "Goo";
                this.World.AddObject(o);
            }

            ///rectangle
            Vertices verts = PolygonTools.CreateRectangle(5, 5);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, verts);

                I2DObject o = new I2DObject(fs, mat, model);
                o.Name = "Rectangle";
                this.World.AddObject(o);
            }

            ///circle
            CircleShape circle = new CircleShape(5, 1);
            {
                IModelo2D model = new SpriteFarseer(factory, circle, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();                
                FarseerObject fs = new FarseerObject(fworld, circle);
                
                I2DObject o = new I2DObject(fs, mat, model);
                o.Name = "Circle";
                this.World.AddObject(o);
            }

            ///camera            
            this.World.Camera2D = new Camera2D(GraphicInfo);

            base.LoadContent(GraphicInfo, factory, contentManager);
            SimpleConcreteGestureInputPlayable SimpleConcreteGestureInputPlayable = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.DoubleTap, (sample) => doubleTapCount++);
            this.BindInput(SimpleConcreteGestureInputPlayable);

            this.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.FreeDrag,
                (sample) =>
                {   
                    this.World.Camera2D.MoveCamera(sample.Delta * 0.1f);
                }
            ));
            
        }
       
        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);

            border.Draw(render, this.World.Camera2D);
                        
            render.RenderTextComplete("PloobsEngine Gestures with Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("DoubleTap Count " + doubleTapCount, new Vector2(20, 30), Color.Red, Matrix.Identity);
        }

    }
}