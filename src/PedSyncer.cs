﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using pedSyncer;
using PedSyncer.Control;
using PedSyncer.Model;
//using pedSyncer.Task;

namespace PedSyncer
{
    internal class PedSyncer : Resource
    {            
        //Define here if u want to activate the DebugMode Clientside
        public static bool DebugModeClientSide = false;
        //Define here if u want to activate the DebugMode ServerSide
        public static bool DebugModeServerSide = false;

        private void InitEntitySync()
        {
            //Prepare the EntitySync - Ped Limit is 256
            AltEntitySync.Init(
                4,
                (threadId) => 100,
                (threadId) => false,
                (threadCount, repository) => new PedSyncerNetworkLayer(threadCount, repository),
                (entity, threadCount) => (entity.Id % threadCount),
                (entityId, entityType, threadCount) => (entityId % threadCount),
                (threadId) => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 128),
                new IdProvider()
            );

            Console.WriteLine("[INFO] GameEntityResource InitEntitySync startet");
        }

        public override void OnStart()
        {
            this.InitEntitySync();

            /**
             * Prepare the Events and route them to the controllers
             */
            Alt.OnClient<IPlayer, Dictionary<string, string>>("pedSyncer:client:firstSpawn", Events.OnFirstSpawn);
            Alt.OnClient<IPlayer, string>("pedSyncer:client:positions", Events.OnPositionUpdate);
            Alt.OnClient<IPlayer, ulong, string, string[]>("pedSyncer:client:task", Events.OnTaskUpdate);
            Alt.OnClient<IPlayer>("pedSyncer:client:ready", Events.OnPlayerConnect);

            AltEntitySync.OnEntityRemove += Events.OnEntityRemove;

            Console.WriteLine("Started");

            /**
             * Load all files (navMeshes & StreetCrossing)
             */
            NavigationMesh.getInstance();
            StreetCrossingControl.getInstance();

            //Start serverside ped movement calculation
            PedMovement.GetInstance();

            //Create citizen vehicles
            PedVehicles.GetInstance().SpawnRandomCitizenVehicles(1000);

            //Create citizen peds who wanders - delete this line if you don't wanna have citizens
            Ped.CreateCitizenPeds(2000);

            //NodeJS Wrapper
            PedSyncerWrapper.RegisterWrapperFunctions();
        }
        public override void OnTick()
        {
            //TaskRunning.OnTick();
        }
        public override void OnStop()
        {
            Console.WriteLine("Stopped");
        }
    }
}
