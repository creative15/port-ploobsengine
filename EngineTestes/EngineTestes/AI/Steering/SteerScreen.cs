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
using Bnoerj.AI.Steering;
using Bnoerj.AI.Steering.Pedestrian;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class SteerScreen : IScene
    {
        IPlugIn PlugIn;
        Path path = new Path(new List<Vector3>() { new Vector3(50, 50, 50), new Vector3(50, 50, 0), new Vector3(0, 50, 0) }, 5);
        
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-0.97f, true, 1), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            renderTech = new ForwardRenderTecnich(desc);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);            
            PlugIn = new PedestrianPlugIn(this.World, path,
                (pd) =>
                {
                    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green), TextureType.DIFFUSE);
                    ///Physic info (position, rotation and scale are set here)
                    BoxObject tmesh = new BoxObject(Vector3.Zero, 1, 1, 1, 10, new Vector3(5), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                    ///Forward Shader (look at this shader construction for more info)
                    ForwardXNABasicShader shader = new ForwardXNABasicShader();
                    ///Deferred material
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    ///The object itself
                    return new IObject(fmaterial, simpleModel, tmesh);
                });

            PlugIn.Init();

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                ///Physic info (position, rotation and scale are set here)
                BoxObject tmesh = new BoxObject(Vector3.Zero, 1, 1, 1, 10, new Vector3(1000, 1, 1000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                ///Forward Shader (look at this shader construction for more info)
                ForwardXNABasicShader shader = new ForwardXNABasicShader();
                ///Deferred material
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                ///The object itself
                this.World.AddObject(new IObject(fmaterial, simpleModel, tmesh));
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));
        }        
        protected override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
            PlugIn.Update();                
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: Basic Screen Forward", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
        }

    }
}