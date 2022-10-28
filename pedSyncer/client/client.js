/// <reference types="@altv/types-client" />

import { startPedControler } from "./pedSyncer/control/PedControler.js";
import { Ped } from "./pedSyncer/class/PedClass.js";
import alt from "alt-client";
import native from "natives";
let DebugMode = true;

startPedControler();

alt.onServer("pedSyncer:debugmode", (state) => {
	DebugMode = state;
});

function drawNameTags(text, x, y, scale, r, g, b, a, outline) {
	native.setTextFont(0);
	native.setTextProportional(0);
	native.setTextScale(scale, scale);
	native.setTextColour(r, g, b, a);
	native.setTextDropShadow(0, 0, 0, 0, 255);
	native.setTextEdge(2, 0, 0, 0, 255);
	native.setTextCentre(1);
	native.setTextDropShadow();

	if (outline) native.setTextOutline();

	native.beginTextCommandDisplayText("STRING");
	native.addTextComponentSubstringPlayerName(text);
	native.endTextCommandDisplayText(x, y, 0);
} 

if (DebugMode) {
	alt.everyTick(() => { 
		let playerPos = alt.Player.local.pos;
		for (let ped of Ped.getAllStreamedPeds()) {
			let playerPos2 = native.getEntityCoords(ped.scriptID, false); // add alive parameter here.
			let distance = Math.round(native.getDistanceBetweenCoords(playerPos.x, playerPos.y, playerPos.z, playerPos2.x, playerPos2.y, playerPos2.z, true));

			let scale = distance / (15 * 15.0);
			if (scale < 0.25) scale = 0.25;

			let screenPos = native.getScreenCoordFromWorldCoord(playerPos2.x, playerPos2.y, playerPos2.z + 0.94);
			
			drawNameTags(`ID: ${ped.id}`, screenPos[1], screenPos[2] - 0.030, 0.3, 255, 255, 255, 220, true);
		}
	});
}