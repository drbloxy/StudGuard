function servercmdExplain(%client, %Chat, %Chat2, %Chat3, %Chat4, %Chat5, %Chat6, %Chat7, %Chat8, %Chat9, %Chat10, %Chat11, %Chat12, %Chat13, %Chat14, %Chat15, %Chat16)
{
	if(%client.isFreekillSuspect = 0)
	{
	    messageClient(%client,'MsgError',"\c6You are not a freekill suspect.");
	    return;
	}

	if(%client.isAdmin = 1)
	{
		messageClient(%client,'',"\c6Admins cannot do /explain.");
		return;
	}

    %clientB = ClientGroup.getObject(%cl);
	if(%clientB.isAdmin)
	{
		messageClient(%clientb,'MsgAdminForce', "\c3" @ %client.name @ "\c6 explained: " @ %chat SPC %chat2 SPC %chat3 SPC %chat4 SPC %chat5 SPC %chat6 SPC %chat7 SPC %chat8 SPC %chat9 SPC %chat10 SPC %chat11 SPC %chat12 SPC %chat13 SPC %chat14 SPC %chat15 SPC %chat16 @ "");
	}

	messageClient(%client,'',"\c6You explained to the staff: " @ %chat SPC %chat2 SPC %chat3 SPC %chat4 SPC %chat5 SPC %chat6 SPC %chat7 SPC %chat8 SPC %chat9 SPC %chat10 SPC %chat11 SPC %chat12 SPC %chat13 SPC %chat14 SPC %chat15 SPC %chat16 @ "");
    %client.isFreekillSuspect = 0;
    cancel($freekillschedule);
}

function GameConnection::onDeath(%client, %killerObject, %killerClient, %damageType, %position)
{
	if(%killerClient.isAdmin = 1)
	{
		messageClient(%killerClient,'MsgAdminForce',"\c6You are an admin, so you do not need to do /explain. However, please do not freekill.");
		return;
	}

	if(%client.isWanted = 0) return;

    messageClient(%killerClient,'MsgAdminForce',"<font:impact:35>PLEASE USE /EXPLAIN [REASON] TO EXPLAIN WHY YOU FREEKILLED<font:impact:20>You will be kicked in 30 seconds.");
    messageClient(%client,'',"Please wait to be resurrected if you have been freekilled for a valid reason.");
    
    $freekillschedule = schedule(30000, 0, freekillBan, %client.blid, %client.name);
    %client.addLives(1);
    return parent::onDeath(%client, %killerobject, %killerClient, %damageType, %position);
}

function freekillBan(%client, %victimblid, %victimname)
{
	%victimblid = findClientByBL_ID(%victimblid);
    %victimname = findclientbyName(%victimname);
	serverCmdBan(%victimname, %victimblid, 10, "You have been banned for 10 minutes for freekilling.");
}

function wantedStatusLoop(%client)
{
	cancel($WantedStatusGV);
    if(!%client.tdmTeam = 1) return;
    if(!%client.isWanted = 0) return;
    centerPrintAll("                                                                                 \c6Status: <color:ff0000>Wanted");
    $WantedStatusGV = schedule(10,0,wantedStatusLoop);
}

registerOutputEvent("Player",isWanted,"int 0 1 1",1);

function isWanted(%this)
{
    if(!%client.tdmTeam = 0) return;
	%this.client.isWanted = 1;
}



package AFKD
{
	function Slayer_MinigameSO::onReset(%mini, %guy)  //stolen from Tetro Block's freekill detecter script, credit to him.
	{
   	    for (%i = 0; %i < clientGroup.getCount(); %i++) 
	    {
		    %guy = clientGroup.getObject(%i);
		    %guy.isWanted = 0;
	    }

	    return parent::onReset(%mini, %guy);
    }
};
activatePackage(AFKD);

