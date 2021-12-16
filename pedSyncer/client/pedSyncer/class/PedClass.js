import { inDistanceBetweenPos } from "../utils/functions.js";
import * as alt from 'alt-client';
import native from 'natives';
import { loadModel } from "../utils/functions.js";
import { unloadModel } from "../utils/functions.js";
import { getVehicleById } from "../utils/functions.js";
function asyncGeneratorStep(gen, resolve, reject, _next, _throw, key, arg) {
    try {
        var info = gen[key](arg);
        var value = info.value;
    } catch (error) {
        reject(error);
        return;
    }
    if (info.done) {
        resolve(value);
    } else {
        Promise.resolve(value).then(_next, _throw);
    }
}
function _asyncToGenerator(fn) {
    return function() {
        var self = this, args = arguments;
        return new Promise(function(resolve, reject) {
            var gen = fn.apply(self, args);
            function _next(value) {
                asyncGeneratorStep(gen, resolve, reject, _next, _throw, "next", value);
            }
            function _throw(err) {
                asyncGeneratorStep(gen, resolve, reject, _next, _throw, "throw", err);
            }
            _next(undefined);
        });
    };
}
var peds = {
};
var pedsToScriptID = {
};
var pedsProxies = {
};
class PedClass {
    debug = false;
    dimension = 0;
    id = null;
    scriptID = 0;
    netOwner = null;
    valid = true;
    created = false;
    pos = {
        x: 0,
        y: 0,
        z: 0
    };
    rot = {
        x: 0,
        y: 0,
        z: 0
    };
    heading = 0;
    drawable1 = null;
    drawable2 = null;
    drawable3 = null;
    drawable4 = null;
    drawable5 = null;
    drawable6 = null;
    drawable7 = null;
    drawable8 = null;
    drawable9 = null;
    drawable10 = null;
    drawable11 = null;
    texture1 = null;
    texture2 = null;
    texture3 = null;
    texture4 = null;
    texture5 = null;
    texture6 = null;
    texture7 = null;
    texture8 = null;
    texture9 = null;
    texture10 = null;
    texture11 = null;
    prop0 = null;
    prop1 = null;
    prop2 = null;
    prop6 = null;
    prop7 = null;
    proptexture0 = null;
    proptexture1 = null;
    proptexture2 = null;
    proptexture6 = null;
    proptexture7 = null;
    gender = null;
    invincible = null;
    vehicle = null;
    seat = null;
    injuries = null;
    hasBlood = null;
    armour;
    health;
    dead;
    weapons;
    ammo;
    weaponAimPos = {
        x: 0,
        y: 0,
        z: 0
    };
    //Current Task of the Ped with its params
    task;
    taskParams;
    //Current Scenario the ped is playing
    scenario;
    freeze;
    navmashPositions;
    nearFinalPosition;
    nextNavMeshStation;
    spawnTrys = 0;
    flags = {
    };
    metaData = {
    };
    syncedMetaData = {
    };
    model;
    wandering;
    /**
     * Spawn the ped, decide if this is the first time this ped will ever be created
     */ spawn() {
        return _asyncToGenerator((function*() {
            //Load the model of this ped
            yield loadModel(this.model);
            //Create a random ped with a random style fitting to the current location
            if (this.vehicle == null) this.scriptID = native.createPed(4, native.getHashKey(this.model), this.pos.x, this.pos.y, this.pos.z, 0, false, false);
            else {
                let vehicle = getVehicleById(this.vehicle);
                this.scriptID = native.createPedInsideVehicle(vehicle.scriptID, 4, native.getHashKey(this.model), this.seat, false, false);
                this.taskParams = [
                    "scriptID",
                    vehicle.scriptID,
                    10,
                    786491
                ];
                this.task = "taskVehicleDriveWander";
                this.sendTask();
            }
            if (this.scriptID == 0 && this.spawnTrys < 10) {
                let that = this;
                this.spawnTrys++;
                alt.setTimeout(()=>{
                    that.spawn();
                }, 500);
                return;
            }
            //Store this ped by his scriptID as a key
            pedsToScriptID[this.scriptID] = this;
            native.setEntityInvincible(this.scriptID, this.invincible);
            native.freezeEntityPosition(this.scriptID, this.freeze);
            this.pos = JSON.parse(JSON.stringify(native.getEntityCoords(this.scriptID, true)));
            this.rot = native.getEntityRotation(this.scriptID, 0);
            //Set the heading of the ped
            if (this.vehicle == null) native.setEntityHeading(this.scriptID, this.heading);
            //Don't know why this is important, but it is...
            native.setEntityAsMissionEntity(this.scriptID, true, false);
            //Set the peds style
            this.setPedComponentVariation();
            //Set Ped-Attributes
            native.setPedArmour(this.scriptID, this.armour);
            native.setEntityHealth(this.scriptID, this.health, 0);
            if (this.dead) native.setPedToRagdoll(this.scriptID, -1, -1, 0, false, false, false);
            //Start the peds wandering
            if (typeof this.scriptID !== "undefined" && this.scriptID != 0) {
                if (this.wandering) this.startPath();
                if (this.scenario != null) this.startScenario();
            }
        }).bind(this))();
    }
    /**
     * This will be executed at the time the server assigns this client as a netowner.
     * The purpose of the netowner is to communicate the current position of the ped
     * to the server, so the server can sync that to all other clients which doesnt
     * have this ped streamed in.
     */ becomeNetOwner() {
        this.netOwner = alt.Player.local.id;
        if (this.vehicle != null || this.task == "taskVehicleDriveWander") {
            let vehicle = getVehicleById(this.vehicle);
            if (vehicle != null) native.taskVehicleDriveWander(this.scriptID, vehicle.scriptID, 10, 786491);
        }
    }
    releaseNetOwner() {
        this.netOwner = null;
    }
    /**
     * This will be executed at the time this client leaves the streaming-range of
     * the ped.
     * 
     * Peds have to be deleted because the "ped-cache" of GTA is limited.
     */ outOfRange() {
        //Delete ped
        native.deleteEntity(this.scriptID);
        native.deletePed(this.scriptID);
        //Delete ped scriptID reference
        delete pedsToScriptID[this.scriptID];
        //Set scriptID of the ped to 0
        this.scriptID = 0;
        alt.setTimeout(()=>{
            unloadModel(this.model);
        }, 5000);
    }
    /**
     * Method to set the path of the ped
     * 
     * Should only used by events from the server, because if you use it on client-side
     * it will not be synced to the other clients
     * 
     * @param navmashPositionsToAdd An array of positions {x: ..., y: ..., z: ...} 
     */ setPath(navmashPositionsToAdd) {
        //If there are no navmeshPositions: Stop
        if (!this.wandering) return;
        //Reset the route of the player
        this.navmashPositions = [];
        //Store and parse the navmeshPositions
        for (let pos of navmashPositionsToAdd)this.navmashPositions.push(pos);
        //Reset the nearFinalPosition-variable
        this.nearFinalPosition = false;
        //Start the peds wandering
        this.startPath();
    }
    /**
     * Method to start the peds wandering
     */ startPath() {
        //If there are no navmeshPositions: Stop
        if (!this.wandering) return;
        /**
         * Set nextNavMeshStation
         * 
         * nextNavMeshStation is the index of the next street crossing position in navmashPositions
         */ this.nextNavMeshStation = this.navmashPositions.length - 1;
        for(let navMeshKey in this.navmashPositions){
            if (this.navmashPositions[navMeshKey].streetCrossing) {
                this.nextNavMeshStation = navMeshKey;
                break;
            }
        }
        //If the ped is spawned: Let it wander
        if (this.scriptID != 0) {
            //If this client is not the netOwner move it to its current position (for sync)
            if (this.netOwner != alt.Player.local.id) native.setEntityCoordsNoOffset(this.scriptID, this.pos.x, this.pos.y, this.pos.z, false, false, false);
            /**
             * Let the ped walk to the end of its path
             * 
             * taskFollowNavMeshToCoord seems not to be the best task-native for this task
             * because it will not follow the gta5-internal navMeshes, it selects the shortest
             * path regardless of the navMeshes. By this, it walks threw streets, alleys, back- & 
             * courtyards which all don't have navMeshes.
             * 
             * So a solution could be to let the ped walk threw all navMeshes in .navmashPositions.
             * This will not be a good solution because the walking of the ped will looks very
             * weird and not smooth. It will walk like zigzag. So this solution is the smoothing-
             * solution.
             * 
             * If there is no other native or gta5-natural solution the navMeshes has to be smoothed,
             * maybe by linear regression.
             */ native.taskFollowNavMeshToCoord(this.scriptID, this.navmashPositions[this.nextNavMeshStation].x, this.navmashPositions[this.nextNavMeshStation].y, this.navmashPositions[this.nextNavMeshStation].z, 1, -1, 0, true, 0);
            this.taskParams = [
                "scriptID",
                this.navmashPositions[this.nextNavMeshStation].x,
                this.navmashPositions[this.nextNavMeshStation].y,
                this.navmashPositions[this.nextNavMeshStation].z,
                1,
                -1,
                0,
                true,
                0
            ];
            this.task = "taskFollowNavMeshToCoord";
            this.sendTask();
        }
    }
    pathPositionReached() {
        //If there are no navmeshPositions: Stop
        if (!this.wandering) return;
        let currenctNextNavMeshStation = this.nextNavMeshStation;
        /**
         * Set nextNavMeshStation
         * 
         * nextNavMeshStation is the index of the next street crossing position in navmashPositions
         */ this.nextNavMeshStation = this.navmashPositions.length - 1;
        for(let navMeshKey in this.navmashPositions){
            if (currenctNextNavMeshStation >= navMeshKey) continue;
            if (this.navmashPositions[navMeshKey].streetCrossing) {
                this.nextNavMeshStation = navMeshKey;
                break;
            }
        }
        /**
         * Let the ped walk to the end of its path
         * 
         * taskFollowNavMeshToCoord seems not to be the best task-native for this task
         * because it will not follow the gta5-internal navMeshes, it selects the shortest
         * path regardless of the navMeshes. By this, it walks threw streets, alleys, back- & 
         * courtyards which all don't have navMeshes.
         * 
         * So a solution could be to let the ped walk threw all navMeshes in .navmashPositions.
         * This will not be a good solution because the walking of the ped will looks very
         * weird and not smooth. It will walk like zigzag. So this solution is the smoothing-
         * solution.
         * 
         * If there is no other native or gta5-natural solution the navMeshes has to be smoothed,
         * maybe by linear regression.
         */ native.taskFollowNavMeshToCoord(this.scriptID, this.navmashPositions[this.nextNavMeshStation].x, this.navmashPositions[this.nextNavMeshStation].y, this.navmashPositions[this.nextNavMeshStation].z, 1, -1, 0, true, 0);
        this.taskParams = [
            "scriptID",
            this.navmashPositions[this.nextNavMeshStation].x,
            this.navmashPositions[this.nextNavMeshStation].y,
            this.navmashPositions[this.nextNavMeshStation].z,
            1,
            -1,
            0,
            true,
            0
        ];
        this.task = "taskFollowNavMeshToCoord";
        this.sendTask();
    }
    /**
     * Method to get the the last navmashPositions
     */ getPathFinalDestination() {
        if (this.navmashPositions.length > 0) return this.navmashPositions[this.navmashPositions.length - 1];
        return null;
    }
    /**
     * Method to start the peds scenario
     */ startScenario() {
         alt.log("scenario started")
        native.setEntityHeading(this.scriptID, this.heading);
        let that = this;
        alt.setTimeout(()=>{
            native.taskStartScenarioInPlace(that.scriptID, that.scenario, 0, false);
            that.task = "taskStartScenarioInPlace";
            that.taskParams = [
                that.scriptID,
                that.scenario,
                0,
                false
            ];
            this.sendTask();
        }, 1000);
    }
    /**
     * Method to set the peds style, using the setPedComponentVariation-native
     */ setPedComponentVariation() {
        // native.setPedComponentVariation(this.scriptID, 0, this.drawable0, this.texture0, 0); todo
        native.setPedComponentVariation(this.scriptID, 1, this.drawable1, this.texture1, 0);
        native.setPedComponentVariation(this.scriptID, 2, this.drawable2, this.texture2, 0);
        native.setPedComponentVariation(this.scriptID, 3, this.drawable3, this.texture3, 0);
        native.setPedComponentVariation(this.scriptID, 4, this.drawable4, this.texture4, 0);
        native.setPedComponentVariation(this.scriptID, 5, this.drawable5, this.texture5, 0);
        native.setPedComponentVariation(this.scriptID, 6, this.drawable6, this.texture6, 0);
        native.setPedComponentVariation(this.scriptID, 7, this.drawable7, this.texture7, 0);
        native.setPedComponentVariation(this.scriptID, 8, this.drawable8, this.texture8, 0);
        native.setPedComponentVariation(this.scriptID, 9, this.drawable9, this.texture9, 0);
        native.setPedComponentVariation(this.scriptID, 10, this.drawable10, this.texture10, 0);
        native.setPedComponentVariation(this.scriptID, 11, this.drawable11, this.texture11, 0);
        native.setPedPropIndex(this.scriptID, 0, this.prop0, this.proptexture0, false);
        native.setPedPropIndex(this.scriptID, 1, this.prop1, this.proptexture1, false);
        native.setPedPropIndex(this.scriptID, 2, this.prop2, this.proptexture2, false);
        native.setPedPropIndex(this.scriptID, 6, this.prop6, this.proptexture6, false);
        native.setPedPropIndex(this.scriptID, 7, this.prop7, this.proptexture7, false);
    }
    /**
     * Method to update the peds properties
     * 
     * @param ped Ped which has to be updated
     */ updateProperties(ped4) {
        let componentSet = false;
        for (let key of Object.keys(ped4)){
            this[key] = ped4[key];
            if (key.includes("drawable") || key.includes("texture") || key.includes("prop") || key.includes("proptexture")) componentSet = true;
        }
        if (componentSet) this.setPedComponentVariation();
    }
    /**
     * Update the peds positions
     * 
     * @param ped 
     */ updatePos(ped1) {
        this.pos = ped1.pos;
        this.rot = ped1.rot;
        this.heading = ped1.heading;
    }
    sendTask() {
        if (this.netOwner == alt.Player.local.id) {
            let params = [];
            for(let key in this.taskParams){
                if (this.taskParams[key] == this.scriptID) params.push("scriptID");
                else params.push(this.taskParams[key]);
            }
            alt.emitServer("pedSyncer:client:task", this.id, this.task, params);
        }
    }
    deleteMeta(key) {
        this.metaData[key] = undefined;
        delete this.metaData[key];
    }
    getMeta(key1) {
        return this.metaData[key1];
    }
    hasMeta(key2) {
        return typeof this.metaData[key2] !== "undefined";
    }
    setMeta(key3, value1) {
        this.metaData[key3] = value1;
    }
    getSyncedMeta(key4) {
        return this.syncedMetaData[key4];
    }
    hasSyncedMeta(key5) {
        return typeof this.syncedMetaData[key5] !== "undefined";
    }
    /**
     * Class Methods
     */ //Method to get the ped by the ID
    static getByID(id) {
        return pedsProxies[id];
    }
    //Method to get the ped by the scriptID
    static getByScriptID(id1) {
        return pedsProxies[pedsToScriptID[id1].id];
    }
    //Method to get all peds near to the given position and an given radius to this position
    static getNear(pos, distance = 5) {
        let pedsReturn = [];
        for (let ped of Object.values(peds).filter((p)=>inDistanceBetweenPos(pos, p['pos'], distance)
        )){
            pedsReturn.push(ped);
        }
        return pedsReturn;
    }
    //Method to get all peds which are streamed to the player
    static getAllStreamedPeds() {
        return Object.values(pedsToScriptID);
    }
    static emitParse(ped2) {
        let newPed = {
        };
        for(let key in ped2){
            newPed[key] = ped2[key] + "";
        }
        return newPed;
    }
    /**
     * Support-Methods
     */ /**
     * Method to set all positions of the peds to which this client is the netOwner and
     * send these new positions to the server.
     */ static setMyPedPoses() {
        //Iterate over all peds which are streamed in filtered by the netOwner == this client
        for (let ped of Ped.getAllStreamedPeds().filter((p)=>p['netOwner'] == alt.Player.local.id
        )){
            //Get the current positions and set them to the ped
            let pos = native.getEntityCoords(ped['scriptID'], true);
            let rot = native.getEntityRotation(ped['scriptID'], 2); //todo
            if (ped['debug']) {
                alt.log("ped " + ped['id'] + " " + JSON.stringify(ped['pos']) + " " + JSON.stringify(pos));
            }
            ped['pos'] = {
                x: pos.x.toFixed(3),
                y: pos.y.toFixed(3),
                z: pos.z.toFixed(3)
            };
            ped['rot'] = {
                x: rot.x.toFixed(3),
                y: rot.y.toFixed(3),
                z: rot.z.toFixed(3)
            };
            ped['heading'] = native.getEntityHeading(ped['scriptID']).toFixed(3);
            ped['armour'] = native.getPedArmour(ped['scriptID']);
            ped['health'] = native.getEntityHealth(ped['scriptID']);
            ped['dead'] = native.isPedDeadOrDying(ped['scriptID'], true);
            if (ped['wandering']) {
                /**
                 * If this peds path has a finalDestination, the position is valid and the final position
                 * is closer than 5 feet: This ped is near to its finalDestination - it path sould be
                 * new calculated
                 */ if (ped.getPathFinalDestination() != null && ped.pos != null && native.getDistanceBetweenCoords(ped.getPathFinalDestination().x, ped.getPathFinalDestination().y, ped.getPathFinalDestination().z, parseInt(ped.pos.x), parseInt(ped.pos.y), parseInt(ped.pos.z), false) < 0.5) {
                    ped.nearFinalPosition = true;
                }
                /**
                 * If this peds path has a nextNavMeshStation and the station is closer than 5 feet: This
                 * peds station is reached, a next station will be calculated
                 */ if (ped.getPathFinalDestination() != null && ped.pos != null && ped.nextNavMeshStation < ped.navmashPositions.length && native.getDistanceBetweenCoords(ped.navmashPositions[ped.nextNavMeshStation].x, ped.navmashPositions[ped.nextNavMeshStation].y, ped.navmashPositions[ped.nextNavMeshStation].z, parseInt(ped.pos.x), parseInt(ped.pos.y), parseInt(ped.pos.z), false) < 2) {
                    ped.pathPositionReached();
                }
            }
        /**
             * Set Ped flags
             
            for (let j = 0; j <= 426; j++) {
                ped.flags[j] = native.getPedConfigFlag(ped.scriptID, j, 1);
            }*/ }
        alt.setTimeout(Ped.setMyPedPoses, 500);
    }
    /**
     * Send the positions of the peds of which this client is the netOwner to the server
     */ static sendMyPedPoses() {
        let pedsToSend = [];
        //Iterate over all peds which are streamed in filtered by the netOwner == this client, add this ped to pedsToSend
        for (let ped of Ped.getAllStreamedPeds().filter((p)=>p['netOwner'] == alt.Player.local.id && p['scriptID'] != 0
        ))pedsToSend.push({
            id: ped['id'],
            pos: JSON.stringify(ped['pos']),
            heading: ped['heading'],
            nearFinalPosition: ped['nearFinalPosition']
        });
        //If pedsToSend is not empty: send it to the server
        if (pedsToSend.length > 0) alt.emitServer("pedSyncer:client:positions", JSON.stringify(pedsToSend));
        alt.setTimeout(Ped.sendMyPedPoses, posSyncTime);
    }
    /**
     * Object Methods
     */ constructor(ped3){
        //Flag to debug this one ped
        this.debug = false;
        //Not active currently
        this.dimension = 0;
        //ID of the Ped
        this.id = null;
        this.scriptID = 0;
        /**
     * Just one Player is the netOwner of a Ped. This Player has the task to
     * tell the server the current position of the Ped.
     * 
     * The first netOwner has also the task to creat the ped on the first time.
     */ this.netOwner = null;
        /**
     * Always true currently
     * 
     * Will give information about the validity
     * ToDo: When is a ped invalid?
     */ this.valid = true;
        /**
     * Tells if the ped was already created on one client
     * 
     * If created is true this object will contain information about the style
     * of this ped
     */ this.created = false;
        /**
     * Current position, rotation and heading of the ped
     * 
     * ToDo: Rotation still needed???
     */ this.pos = {
            x: 0,
            y: 0,
            z: 0
        };
        this.rot = {
            x: 0,
            y: 0,
            z: 0
        };
        this.heading = 0;
        /**
     * Ped-Style Properties
     */ this.model = null;
        this.drawable1 = null;
        this.drawable2 = null;
        this.drawable3 = null;
        this.drawable4 = null;
        this.drawable5 = null;
        this.drawable6 = null;
        this.drawable7 = null;
        this.drawable8 = null;
        this.drawable9 = null;
        this.drawable10 = null;
        this.drawable11 = null;
        this.texture1 = null;
        this.texture2 = null;
        this.texture3 = null;
        this.texture4 = null;
        this.texture5 = null;
        this.texture6 = null;
        this.texture7 = null;
        this.texture8 = null;
        this.texture9 = null;
        this.texture10 = null;
        this.texture11 = null;
        this.prop0 = null;
        this.prop1 = null;
        this.prop2 = null;
        this.prop6 = null;
        this.prop7 = null;
        this.proptexture0 = null;
        this.proptexture1 = null;
        this.proptexture2 = null;
        this.proptexture6 = null;
        this.proptexture7 = null;
        this.gender = null;
        //Will contain information if the ped is invincible
        this.invincible = null;
        //The vehicle the ped sits in
        this.vehicle = null;
        //If the ped is in a vehicle, this tells the current seat of the ped
        this.seat = null;
        //Currently inactive
        this.injuries = null;
        this.hasBlood = null;
        //HP-Stats of the ped
        this.armour = 0;
        this.health = 200;
        this.dead = false;
        //Currently inactive - Weapons of the ped
        this.weapons = [];
        this.ammo = [];
        //Currently inactive - Aim-Position of the Ped
        this.weaponAimPos = {
            x: 0,
            y: 0,
            z: 0
        };
        //Current Task of the Ped with its params
        this.task = null;
        this.taskParams = [];
        //Current Scenario the ped is playing
        this.scenario = null;
        //Will contain information if the ped is never moving
        this.freeze = null;
        //Tells if the ped is randomly wandering
        //Caution: if the ped is not freezed, it will not wandering
        this.wandering = false;
        /**
     * If the Ped is Wandering, this tells the intermediate position
     * of his wandering and also the final destination of his wandering.
     * 
     * After the ped reached his final position a new route will be calculated.
     */ this.navmashPositions = [];
        this.nearFinalPosition = false;
        this.nextNavMeshStation = 200;
        this.spawnTrys = 0;
        //The peds flags
        this.flags = {
        };
        /**
     * Meta-Data
     */ this.metaData = {
        };
        /**
     * Synced-Meta-Data
     */ this.syncedMetaData = {
        };
        //Has to be forbidden on client side
        //Should only be allowed for onServer-Creation
        this.flags = {
        };
        this.id = ped3.id;
        peds[ped3.id] = this;
        for (let key of Object.keys(ped3))if (typeof this[key] !== "undefined") this[key] = ped3[key];
    }
}
//PedClass Proxy
export const Ped = new Proxy(PedClass, {
    construct (target, args) {
        let ped = new Proxy(new target(...args), {
            set (pedTarget, property, value) {
                peds[pedTarget.id][property] = value;
                if (property == "wandering") {
                    if (value == true) pedTarget.startPath();
                    else if (pedTarget.scriptID != 0) native.clearPedTasks(pedTarget.scriptID);
                } else if (property == "task" && value != "" && peds[pedTarget.id]["wandering"] == false) {
                    for(let key in peds[pedTarget.id]["taskParams"]){
                        if (peds[pedTarget.id]["taskParams"][key] == "scriptID") {
                            peds[pedTarget.id]["taskParams"][key] = pedTarget.scriptID;
                            break;
                        }
                    }
                    if (pedTarget.scriptID != 0 && (peds[pedTarget.id][property] != "taskVehicleDriveWander" || pedTarget.netOwner == alt.Player.local.id)) native[peds[pedTarget.id][property]](...peds[pedTarget.id]["taskParams"]);
                    pedTarget.sendTask();
                }
                return true;
            },
            get: (pedTarget, property, receiver)=>{
                if (property == "all") return Object.values(pedsProxies);
                switch(property){
                    case 'firstSpawn':
                    case 'respawn':
                    case 'spawn':
                    case 'becomeNetOwner':
                    case 'outOfRange':
                    case 'setPath':
                    case 'startPath':
                    case 'getPathFinalDestination':
                    case 'setPedComponentVariation':
                    case 'updateProperties':
                    case 'updatePos':
                    case 'deleteMeta':
                    case 'getMeta':
                    case 'hasMeta':
                    case 'setMeta':
                    case 'getSyncedMeta':
                    case 'hasSyncedMeta':
                        return function() {
                            return peds[pedTarget.id][property].apply(this, arguments);
                        };
                    default:
                        return peds[pedTarget.id][property];
                }
            }
        });
        pedsProxies[ped.id] = ped;
        return ped;
    },
    get: (target, property, receiver)=>{
        if (property == "all") return Object.values(pedsProxies);
        return target[property];
    },
    set: (pedTarget, property, value)=>{
        return true;
    }
});
let posSyncTime = 2000;
alt.setTimeout(Ped.setMyPedPoses, 500);
alt.setTimeout(Ped.sendMyPedPoses, posSyncTime);
