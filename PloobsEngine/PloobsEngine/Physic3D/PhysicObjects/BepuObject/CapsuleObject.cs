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
using Microsoft.Xna.Framework;
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class CapsuleObject : BepuEntityObject
    {
        public CapsuleObject(Vector3 pos, float height, float radius, float mass,Matrix? orientation,MaterialDescription md = null)
            : base(md,mass)
        {
            if (!orientation.HasValue)
                orientation = Matrix.Identity;
            
            entity = new Capsule(pos, height, radius, mass);
            entity.Orientation = Quaternion.CreateFromRotationMatrix(orientation.Value);            
        }
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.CAPSULEOBJECT; }
        }

        public override Vector3 Scale
        {
            get
            {                
                return base.Scale;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Capsule Scale, adjust the values in the construtor", LogLevel.RecoverableError);
                base.Scale = Vector3.One;
            }
        }

#if WINDOWS

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
    }

}
