﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Elements.Pools;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using PedSyncer.Control;
using PedSyncer.Model;
using PedSyncer.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace PedSyncer.Model
{
    public class Ped : AltV.Net.EntitySync.Entity, IWritable
    {

        #region Properties
        public const ulong PED_TYPE = 1654;
        public const int STREAMING_RANGE = 200;
        #endregion

        //Directory to handle all peds
        public static ConcurrentDictionary<ulong, Ped> peds = new ConcurrentDictionary<ulong, Ped>();

        //Updated Data
        public List<string> UpdatedData = new List<string>();

        /**
		 * Always true currently
		 *
		 * Will give information about the validity
		 * ToDo: When is a ped invalid?
		 */

        public bool Valid
        {
            get
            {
                if (this.TryGetData<bool>("valid", out bool value)) return value;
                return true;
            }
            set
            {
                UpdatedData.Add("valid");
                this.SetData("valid", value);
            }
        }

        /**
		 * Current heading of the ped
		 */

        public double Heading
        {
            get
            {
                if (this.TryGetData<double>("heading", out double value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("heading");
                this.SetData("heading", value);
            }
        }

        /**
		 * Ped-Style Properties
		 */

        public string Model
        {
            get
            {
                if (this.TryGetData<string>("model", out string value)) return value;
                return "";
            }
            set
            {
                UpdatedData.Add("model");
                this.SetData("model", value);
            }
        }

        public int Drawable1
        {
            get
            {
                if (this.TryGetData<int>("drawable1", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable1");
                this.SetData("drawable1", value);
            }
        }

        public int Drawable2
        {
            get
            {
                if (this.TryGetData<int>("drawable2", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable2");
                this.SetData("drawable2", value);
            }
        }

        public int Drawable3
        {
            get
            {
                if (this.TryGetData<int>("drawable3", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable3");
                this.SetData("drawable3", value);
            }
        }

        public int Drawable4
        {
            get
            {
                if (this.TryGetData<int>("drawable4", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable4");
                this.SetData("drawable4", value);
            }
        }

        public int Drawable5
        {
            get
            {
                if (this.TryGetData<int>("drawable5", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable5");
                this.SetData("drawable5", value);
            }
        }

        public int Drawable6
        {
            get
            {
                if (this.TryGetData<int>("drawable6", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable6");
                this.SetData("drawable6", value);
            }
        }

        public int Drawable7
        {
            get
            {
                if (this.TryGetData<int>("drawable7", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable7");
                this.SetData("drawable7", value);
            }
        }

        public int Drawable8
        {
            get
            {
                if (this.TryGetData<int>("drawable8", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable8");
                this.SetData("drawable8", value);
            }
        }

        public int Drawable9
        {
            get
            {
                if (this.TryGetData<int>("drawable9", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable9");
                this.SetData("drawable9", value);
            }
        }

        public int Drawable10
        {
            get
            {
                if (this.TryGetData<int>("drawable10", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable10");
                this.SetData("drawable10", value);
            }
        }

        public int Drawable11
        {
            get
            {
                if (this.TryGetData<int>("drawable11", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("drawable11");
                this.SetData("drawable11", value);
            }
        }

        public int Texture1
        {
            get
            {
                if (this.TryGetData<int>("texture1", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture1");
                this.SetData("texture1", value);
            }
        }

        public int Texture2
        {
            get
            {
                if (this.TryGetData<int>("texture2", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture2");
                this.SetData("texture2", value);
            }
        }

        public int Texture3
        {
            get
            {
                if (this.TryGetData<int>("texture3", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture3");
                this.SetData("texture3", value);
            }
        }

        public int Texture4
        {
            get
            {
                if (this.TryGetData<int>("texture4", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture4");
                this.SetData("texture4", value);
            }
        }

        public int Texture5
        {
            get
            {
                if (this.TryGetData<int>("texture5", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture5");
                this.SetData("texture5", value);
            }
        }

        public int Texture6
        {
            get
            {
                if (this.TryGetData<int>("texture6", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture6");
                this.SetData("texture6", value);
            }
        }

        public int Texture7
        {
            get
            {
                if (this.TryGetData<int>("texture7", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture7");
                this.SetData("texture7", value);
            }
        }

        public int Texture8
        {
            get
            {
                if (this.TryGetData<int>("texture8", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture8");
                this.SetData("texture8", value);
            }
        }

        public int Texture9
        {
            get
            {
                if (this.TryGetData<int>("texture9", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture9");
                this.SetData("texture9", value);
            }
        }

        public int Texture10
        {
            get
            {
                if (this.TryGetData<int>("texture10", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture10");
                this.SetData("texture10", value);
            }
        }

        public int Texture11
        {
            get
            {
                if (this.TryGetData<int>("texture11", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("texture11");
                this.SetData("texture11", value);
            }
        }

        public int Prop0
        {
            get
            {
                if (this.TryGetData<int>("prop0", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("prop0");
                this.SetData("prop0", value);
            }
        }

        public int Prop1
        {
            get
            {
                if (this.TryGetData<int>("prop1", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("prop1");
                this.SetData("prop1", value);
            }
        }

        public int Prop2
        {
            get
            {
                if (this.TryGetData<int>("prop2", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("prop2");
                this.SetData("prop2", value);
            }
        }

        public int Prop6
        {
            get
            {
                if (this.TryGetData<int>("prop6", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("prop6");
                this.SetData("prop6", value);
            }
        }

        public int Prop7
        {
            get
            {
                if (this.TryGetData<int>("prop7", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("prop7");
                this.SetData("prop7", value);
            }
        }

        public int PropTexture0
        {
            get
            {
                if (this.TryGetData<int>("proptexture0", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("proptexture0");
                this.SetData("proptexture0", value);
            }
        }

        public int PropTexture1
        {
            get
            {
                if (this.TryGetData<int>("proptexture1", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("proptexture1");
                this.SetData("proptexture1", value);
            }
        }

        public int PropTexture2
        {
            get
            {
                if (this.TryGetData<int>("proptexture2", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("proptexture2");
                this.SetData("proptexture2", value);
            }
        }

        public int PropTexture6
        {
            get
            {
                if (this.TryGetData<int>("proptexture6", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("proptexture6");
                this.SetData("proptexture6", value);
            }
        }

        public int PropTexture7
        {
            get
            {
                if (this.TryGetData<int>("proptexture7", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("proptexture7");
                this.SetData("proptexture7", value);
            }
        }

        public string Gender
        { get; set; }

        //Ped Flags
        public bool[] Flags
        {
            get
            {
                if (this.TryGetData<bool[]>("flags", out bool[] value)) return value;
                return new bool[0];
            }
            set
            {
                UpdatedData.Add("flags");
                this.SetData("flags", value);
            }
        }

        //Will contain information if the ped is invincible
        public bool Invincible
        {
            get
            {
                if (this.TryGetData<bool>("invincible", out bool value)) return value;
                return false;
            }
            set
            {
                UpdatedData.Add("invincible");
                this.SetData("invincible", value);
            }
        }

        //The vehicle the ped sits in
        private IVehicle? vehicle = null;
        public IVehicle? Vehicle
        {
            get
            {
                return vehicle;
            }
            set
            {
                vehicle = value;
                UpdatedData.Add("vehicle");
                if (value == null) this.SetData("vehicle", null);
                else this.SetData("vehicle", value.Id);
            }
        }

        //If the ped is in a vehicle, this tells the current seat of the ped
        public int Seat
        { get; set; }

        //Currently inactive - HP-Stats of the ped
        public List<int> Injuries
        { get; set; }

        public bool HasBlood
        {
            get
            {
                if (this.TryGetData<bool>("hasBlood", out bool value)) return value;
                return false;
            }
            set
            {
                UpdatedData.Add("hasBlood");
                this.SetData("hasBlood", value);
            }
        }

        //Property to set the armour of the ped
        public int Armour
        {
            get
            {
                if (this.TryGetData<int>("armour", out int value)) return value;
                return 0;
            }
            set
            {
                UpdatedData.Add("armour");
                this.SetData("armour", value);
            }
        }

        //Property to set the health of the ped
        public int Health
        {
            get
            {
                if (this.TryGetData<int>("health", out int value)) return value;
                return 200;
            }
            set
            {
                UpdatedData.Add("health");
                this.SetData("health", value);
            }
        }

        //Property to set the death of the ped
        public bool Dead
        {
            get
            {
                if (this.TryGetData<bool>("dead", out bool value)) return value;
                return false;
            }
            set
            {
                UpdatedData.Add("dead");
                this.SetData("dead", value);
            }
        }

        //Currently inactive - Weapons of the ped
        public List<string> Weapons
        { get; set; }

        public Dictionary<int, int> Ammo
        { get; set; }

        //Currently inactive - Aim-Position of the Ped
        public Vector3 WeaponAimPos
        { get; set; }

        //Current Task of the Ped with its params
        public string Task
        {
            get
            {
                if (this.TryGetData<string>("task", out string value)) return value;
                return "";
            }
            set
            {
                UpdatedData.Add("task");
                this.SetData("task", value);
            }
        }

        public List<string> TaskParams
        {
            get
            {
                if (this.TryGetData<List<string>>("taskParams", out List<string> value)) return value;
                return new List<string>();
            }
            set
            {
                UpdatedData.Add("taskParams");
                this.SetData("taskParams", value);
            }
        }

        //Current Scenario the ped is playing
        public string Scenario
        {
            get
            {
                if (this.TryGetData<string>("Scenario", out string value)) return value;
                return "";
            }
            set
            {
                UpdatedData.Add("Scenario");
                this.SetData("Scenario", value);
            }
        }

        //Set the ped freezing and holding the position
        public bool Freeze
        {
            get
            {
                if (this.TryGetData<bool>("freeze", out bool value)) return value;
                return false;
            }
            set
            {
                UpdatedData.Add("freeze");
                this.SetData("freeze", value);
            }
        }

        //Tells if the ped is randomly wandering
        //Caution: if the ped is not freezed, it will not wandering
        public bool Wandering
        {
            get
            {
                if (this.TryGetData<bool>("wandering", out bool value)) return value;
                return false;
            }
            set
            {
                if (value)
                {
                    this.StartWandering();
                } else
                {
                    pathPositions = new List<IPathElement>();
                    this.NearFinalPosition = false;
                    this.CurrentNavmashPositionsIndex = 0;
                }
                UpdatedData.Add("wandering");
                this.SetData("wandering", value);
            }
        }

        /**
		 * If the Ped is Wandering, this tells the intermediate position
		 * of his wandering and also the final destination of his wandering.
		 *
		 * After the ped reached his final position a new route will be calculated.
		 */
        public List<IPathElement> pathPositions = new List<IPathElement>();

        public List<IPathElement> PathPositions
        {
            get
            {
                return pathPositions;
            }
            set
            {
                if (value.Count == 0) return;
                this.pathPositions = value;
                this.NearFinalPosition = false;
                Alt.EmitAllClients("pedSyncer:server:path", this.Id, this.PathPositions);
            }
        }

        public bool NearFinalPosition
        { get; set; }

        public int CurrentNavmashPositionsIndex
        { get; set; }

        //Return Value
        public object GetValue(string key)
        {
            if (this.TryGetData<object>(key, out object value)) return value;
            return false;
        }

        /**
		 * Object Methods
		 */
        public Ped(float x, float y, float z, string? model = null) : base(PED_TYPE, new Vector3(x, y, z), 0, STREAMING_RANGE)
        {
            peds[this.Id] = this;
            this.Valid = true;
            this.Model = model;

            this.Vehicle = null;
            this.Seat = 0;

            this.Weapons = new List<string>();
            this.Ammo = new Dictionary<int, int>();

            this.Flags = new bool[426];

            this.NearFinalPosition = false;
            this.CurrentNavmashPositionsIndex = 0;
            //AltAsync.Do(() =>
            //{
            AltEntitySync.AddEntity(this);
            Alt.EmitAllClients("pedSyncer:server:create", this);
            //});
        }

        public void Destroy()
        {      
            Alt.EmitAllClients("pedSyncer:server:delete", this.Id);
        }

        //Method to start the wandering of the ped
        private void StartWandering(IPathElement? StartNavMesh = null)
        {
            NavigationMesh NavigationMeshControl = NavigationMesh.getInstance();

            if (StartNavMesh == null) StartNavMesh = NavigationMeshControl.getMeshByPosition(this.Position);

            //TODO: Invinite Loop
            if (StartNavMesh == null)
            {
                this.PathPositions = new List<IPathElement>();
                this.PathPositions.Add(NavigationMeshControl.getNearestMeshByPosition(this.Position));
                return;
            }

            this.PathPositions = StartNavMesh.GetWanderingPath();
        }

        //Method to let the ped further wander at the moment the ped reaches the final destination
        public void ContinueWandering()
        {
            if (!this.Wandering) return;
            if (PathPositions.Count < 2) return;

            this.PathPositions = this.PathPositions[this.PathPositions.Count - 1].GetWanderingPathByDirection(
                Vector3Utils.directionalAngle(this.PathPositions[this.PathPositions.Count - 1].Position, this.PathPositions[this.PathPositions.Count - 2].Position)
            );

            this.NearFinalPosition = false;
        }

        //Method to set the flags
        public void SetFlags(bool[] flags) {
            this.Flags = flags;
        }

        //Property and Method to generate a random ped model
        private static Dictionary<int, List<int>> ModelsToNavMeshAreas = new Dictionary<int, List<int>>();

        public void SetRandomModel()
        {
            if (this.Model == "")
            {
                if (ModelsToNavMeshAreas.Count == 0) 
                    Ped.ModelsToNavMeshAreas = FileControl.LoadDataFromJsonFile<Dictionary<int, List<int>>>("resources/pedSyncer/server/ModelsToAreas.json");

                Random RandomKey = new Random();

                NavigationMeshPolyFootpath nearestNavMesh = NavigationMesh.getInstance().getNearestMeshByPosition(this.Position);

                if (nearestNavMesh != null) this.Model = ParseModelHash(Ped.ModelsToNavMeshAreas[nearestNavMesh.AreaId][RandomKey.Next(0, Ped.ModelsToNavMeshAreas[nearestNavMesh.AreaId].Count - 1)]);
                else this.Model = ParseModelHash(Ped.ModelsToNavMeshAreas[10000][RandomKey.Next(0, Ped.ModelsToNavMeshAreas[10000].Count - 1)]);
            }
        }

        //Property and Method to generate a random ped style
        private static Dictionary<string, ModelData> ModelData = new Dictionary<string, ModelData>();

        public void SetRandomStyle()
        {
            if (!ModelData.ContainsKey(this.Model.ToLower())) return;

            Random RandomKey = new Random();

            ModelData modelData = ModelData[this.Model];

            this.Drawable1 = RandomKey.Next(0, modelData.Drawable1);
            this.Texture1 = modelData.Texture1.ContainsKey(this.Drawable1) ? RandomKey.Next(0, modelData.Texture1[this.Drawable1]) : 0;

            this.Drawable2 = RandomKey.Next(0, modelData.Drawable2);
            this.Texture2 = modelData.Texture2.ContainsKey(this.Drawable2) ? RandomKey.Next(0, modelData.Texture2[this.Drawable2]) : 0;

            this.Drawable3 = RandomKey.Next(0, modelData.Drawable3);
            this.Texture3 = modelData.Texture3.ContainsKey(this.Drawable3) ? RandomKey.Next(0, modelData.Texture3[this.Drawable3]) : 0;

            this.Drawable4 = RandomKey.Next(0, modelData.Drawable4);
            this.Texture4 = modelData.Texture4.ContainsKey(this.Drawable4) ? RandomKey.Next(0, modelData.Texture4[this.Drawable4]) : 0;

            this.Drawable5 = RandomKey.Next(0, modelData.Drawable5);
            this.Texture5 = modelData.Texture5.ContainsKey(this.Drawable5) ? RandomKey.Next(0, modelData.Texture5[this.Drawable5]) : 0;

            this.Drawable6 = RandomKey.Next(0, modelData.Drawable6);
            this.Texture6 = modelData.Texture6.ContainsKey(this.Drawable6) ? RandomKey.Next(0, modelData.Texture6[this.Drawable6]) : 0;

            this.Drawable7 = RandomKey.Next(0, modelData.Drawable7);
            this.Texture7 = modelData.Texture7.ContainsKey(this.Drawable7) ? RandomKey.Next(0, modelData.Texture7[this.Drawable7]) : 0;

            this.Drawable8 = RandomKey.Next(0, modelData.Drawable8);
            this.Texture8 = modelData.Texture8.ContainsKey(this.Drawable8) ? RandomKey.Next(0, modelData.Texture8[this.Drawable8]) : 0;

            this.Drawable9 = RandomKey.Next(0, modelData.Drawable9);
            this.Texture9 = modelData.Texture9.ContainsKey(this.Drawable9) ? RandomKey.Next(0, modelData.Texture9[this.Drawable9]) : 0;

            this.Drawable10 = RandomKey.Next(0, modelData.Drawable10);
            this.Texture10 = modelData.Texture10.ContainsKey(this.Drawable10) ? RandomKey.Next(0, modelData.Texture10[this.Drawable10]) : 0;

            this.Drawable11 = RandomKey.Next(0, modelData.Drawable11);
            this.Texture11 = modelData.Texture11.ContainsKey(this.Drawable11) ? RandomKey.Next(0, modelData.Texture11[this.Drawable11]) : 0;

            this.Prop0 = RandomKey.Next(0, modelData.Prop0);
            this.PropTexture0 = modelData.PropTexture0.ContainsKey(this.Prop0) ? RandomKey.Next(0, modelData.PropTexture0[this.Prop0]) : 0;

            this.Prop1 = RandomKey.Next(0, modelData.Prop1);
            this.PropTexture1 = modelData.PropTexture1.ContainsKey(this.Prop1) ? RandomKey.Next(0, modelData.PropTexture1[this.Prop1]) : 0;

            this.Prop2 = RandomKey.Next(0, modelData.Prop2);
            this.PropTexture2 = modelData.PropTexture2.ContainsKey(this.Prop2) ? RandomKey.Next(0, modelData.PropTexture2[this.Prop2]) : 0;

            this.Prop6 = RandomKey.Next(0, modelData.Prop6);
            this.PropTexture6 = modelData.PropTexture6.ContainsKey(this.Prop6) ? RandomKey.Next(0, modelData.PropTexture6[this.Prop6]) : 0;

            this.Prop7 = RandomKey.Next(0, modelData.Prop7);
            this.PropTexture7 = modelData.PropTexture7.ContainsKey(this.Prop7) ? RandomKey.Next(0, modelData.PropTexture7[this.Prop7]) : 0;
        }

        //Method to serialize this object
        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(this.Id);

            writer.Name("dimension");
            writer.Value(this.Dimension);

            writer.Name("pos");
            writer.BeginObject();
            writer.Name("x");
            writer.Value(this.Position.X);
            writer.Name("y");
            writer.Value(this.Position.Y);
            writer.Name("z");
            writer.Value(this.Position.Z);
            writer.EndObject();

            writer.Name("netOwner");
            if (this.NetOwner != null) writer.Value(((PlayerClient)this.NetOwner).GetPlayer().Id);
            else writer.Value("");

            writer.Name("valid");
            writer.Value(this.Valid);

            writer.Name("heading");
            writer.Value(this.Heading);

            writer.Name("model");
            writer.Value(this.Model);

            writer.Name("drawable1");
            writer.Value(this.Drawable1);

            writer.Name("drawable2");
            writer.Value(this.Drawable2);

            writer.Name("drawable3");
            writer.Value(this.Drawable3);

            writer.Name("drawable4");
            writer.Value(this.Drawable4);

            writer.Name("drawable5");
            writer.Value(this.Drawable5);

            writer.Name("drawable6");
            writer.Value(this.Drawable6);

            writer.Name("drawable7");
            writer.Value(this.Drawable7);

            writer.Name("drawable8");
            writer.Value(this.Drawable8);

            writer.Name("drawable9");
            writer.Value(this.Drawable9);

            writer.Name("drawable10");
            writer.Value(this.Drawable10);

            writer.Name("drawable11");
            writer.Value(this.Drawable11);

            writer.Name("texture1");
            writer.Value(this.Texture1);

            writer.Name("texture2");
            writer.Value(this.Texture2);

            writer.Name("texture3");
            writer.Value(this.Texture3);

            writer.Name("texture4");
            writer.Value(this.Texture4);

            writer.Name("texture5");
            writer.Value(this.Texture5);

            writer.Name("texture6");
            writer.Value(this.Texture6);

            writer.Name("texture7");
            writer.Value(this.Texture7);

            writer.Name("texture8");
            writer.Value(this.Texture8);

            writer.Name("texture9");
            writer.Value(this.Texture9);

            writer.Name("texture10");
            writer.Value(this.Texture10);

            writer.Name("texture11");
            writer.Value(this.Texture11);

            writer.Name("prop0");
            writer.Value(this.Prop0);

            writer.Name("prop1");
            writer.Value(this.Prop1);

            writer.Name("prop2");
            writer.Value(this.Prop2);

            writer.Name("prop6");
            writer.Value(this.Prop6);

            writer.Name("prop7");
            writer.Value(this.Prop7);

            writer.Name("proptexture0");
            writer.Value(this.PropTexture0);

            writer.Name("proptexture1");
            writer.Value(this.PropTexture1);

            writer.Name("proptexture2");
            writer.Value(this.PropTexture2);

            writer.Name("proptexture6");
            writer.Value(this.PropTexture6);

            writer.Name("proptexture7");
            writer.Value(this.PropTexture7);

            writer.Name("gender");
            writer.Value(this.Gender);

            writer.Name("invincible");
            writer.Value(this.Invincible);

            writer.Name("vehicle");
            if (this.Vehicle != null) writer.Value(this.Vehicle.Id.ToString());
            else writer.Value("");

            writer.Name("seat");
            writer.Value(this.Seat);

            writer.Name("hasBlood");
            writer.Value(this.HasBlood);

            writer.Name("armour");
            writer.Value(this.Armour);

            writer.Name("health");
            writer.Value(this.Health);

            writer.Name("task");
            writer.Value(this.Task);

            writer.Name("taskParams");
            writer.BeginArray();
            foreach (string value in this.TaskParams) writer.Value(value);
            writer.EndArray();

            writer.Name("scenario");
            writer.Value(this.Scenario);

            writer.Name("freeze");
            writer.Value(this.Freeze);

            writer.Name("wandering");
            writer.Value(this.Wandering);

            writer.Name("navmashPositions");
            writer.BeginArray();
            foreach (IPathElement navMeshPos in this.PathPositions)
            {
                writer.BeginObject();
                writer.Name("x");
                writer.Value(navMeshPos.Position.X);
                writer.Name("y");
                writer.Value(navMeshPos.Position.Y);
                writer.Name("z");
                writer.Value(navMeshPos.Position.Z);
                writer.Name("streetCrossing");
                writer.Value(typeof(StreetCrossing).IsInstanceOfType(navMeshPos));
                writer.EndObject();
            }
            writer.EndArray();

            writer.Name("nearFinalPosition");
            writer.Value(this.NearFinalPosition);

            writer.Name("currentNavmashPositionsIndex");
            writer.Value(this.CurrentNavmashPositionsIndex);

            writer.EndObject();
        }

        /**
		 * Class Methods
		 */
        public static List<Ped> All
        {
            get
            {
                return peds.Values.ToList<Ped>();
            }
        }

        public static Ped? GetByID(ulong Id)
        {
            if (!peds.ContainsKey(Id)) return null;
            return peds[Id];
        }

        public static List<Ped> GetNear(Vector3 Position, float Distance)
        {
            List<Ped> NearPeds = new List<Ped>();
            foreach (Ped ped in peds.Values)
            {
                if (Vector3Utils.InDistanceBetweenPos(ped.Position, Position, Distance)) NearPeds.Add(ped);
            }

            return NearPeds;
        }

        static Dictionary<string, string> PedsToHashes = new Dictionary<string, string>();
        public static string ParseModelHash(int ModelHash)
        {
            if (PedsToHashes.Count == 0) PedsToHashes = FileControl.LoadDataFromJsonFile<Dictionary<string, string>>("resources/pedSyncer/server/HashesToStrings.json");

            if (!PedsToHashes.ContainsKey(ModelHash.ToString("X"))) return "";
            return PedsToHashes[ModelHash.ToString("X")];
        }

        //Method to generate wandering peds as citizens
        public static void CreateCitizenPeds(int PedCount)
        {
            Console.WriteLine("CreateCitizenPeds called, with pedCount " + PedCount);
            NavigationMesh NavigationMeshControl = NavigationMesh.getInstance();

            int ScenarioCount, WanderingCount;
            WanderingCount = (int)Math.Round(PedCount * 0.8);
            ScenarioCount = PedCount - WanderingCount;

            //Load Drawable Data
            if (ModelData.Count == 0)
            {
                Dictionary<string, ModelData> ModelDataTemp = FileControl.LoadDataFromJsonFile<Dictionary<string, ModelData>>("resources/pedSyncer/server/data/PedDrawables.json");

                foreach (string Model in ModelDataTemp.Keys)
                {
                    if (!ModelData.ContainsKey(Model.ToLower())) ModelData.Add(Model.ToLower(), ModelDataTemp[Model]);
                }
            }

            //Load random navMeshes to spawn peds on it
            List<NavigationMeshPolyFootpath> RandomSpawnsList = NavigationMeshControl.getRandomSpawnMeshes(WanderingCount);

            //Spawn the peds on these navMeshes and let the ped wander
            Parallel.ForEach(RandomSpawnsList, RandomSpawn =>
            {
                Ped ped = new Ped(RandomSpawn.Position.X, RandomSpawn.Position.Y, RandomSpawn.Position.Z);
                ped.SetRandomModel();
                ped.SetRandomStyle();
                ped.Wandering = true;
            });

            Scenarios Scenarios = Scenarios.getInstance();

            //Load random ScenarioPoints to spawn peds on it
            List<ScenarioPoint> ScenarioPoints = Scenarios.GetRandomScenarioSpots(ScenarioCount);
            foreach(ScenarioPoint ScenarioPoint in ScenarioPoints)
            {
                Ped ped = new Ped(ScenarioPoint.Position.X, ScenarioPoint.Position.Y, ScenarioPoint.Position.Z, Scenarios.GetRandomModelByScenarioPoint(ScenarioPoint));
                ped.Heading = ScenarioPoint.Position.W/Math.PI*180;
                ped.Scenario = ScenarioPoint.IType;
            }

            Console.WriteLine("[PedSyncer] CreateCitizenPeds " + Ped.All.Count + " peds created");
        }
    }
}