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
using EngineTestes.Bilboard;

namespace EngineTestes
{
    public class CPUBillboardScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        BillboardComponent BillboardComponent;
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            BillboardComponent = new BillboardComponent();
            engine.AddComponent(BillboardComponent);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            SphericalBillboard3D Billboard3D = new SphericalBillboard3D(factory.GetTexture2D("Textures\\grama1"), new Vector3(100, 20, 100), Vector2.One * 0.2f);
            BillboardComponent.Billboards.Add(Billboard3D);

            {
                List<Vector3> poss = new List<Vector3>();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        float x, y;
                        x = i * 10;
                        y = j * 10;
                        poss.Add(new Vector3(x, 50 , y));
                    }
                }

                CPUBilboardModel bm = new CPUBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss, Vector2.One * 0.01f);
                ForwardAlphaTestShader cb = new ForwardAlphaTestShader(128,CompareFunction.GreaterEqual);                                
                ForwardMaterial matfor = new ForwardMaterial(cb);
                matfor.RasterizerState = RasterizerState.CullNone;
                GhostObject go = new GhostObject();                
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);
            }


            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
                        
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
