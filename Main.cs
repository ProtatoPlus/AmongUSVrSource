using Fusion;
using Fusion.Photon.Realtime.Async;
using Il2CppSystem.Collections.Generic;
using JetBrains.Annotations;
using MelonLoader;
using System.Linq;
using SG.Airlock;
using SG.Airlock.Analytics;
using SG.Airlock.Network;
using SG.Airlock.XR;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.UIElements;
using OVR.OpenVR;
using SG.Airlock.Sabotage;
using Fusion.Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using SG.Airlock.Util;
using UnityEngine.Playables;

namespace TestMod
{
    public static class BuildInfo
    {
        public const string Name = "MungVr";
        public const string Description = "Among Us VR Cheats!";
        public const string Author = "Protato X MSMS";
        public const string Company = null;
        public const string Version = "0.0.1";
        public const string DownloadLink = null;
    }

    public class MungVr : MelonMod
    {
        public static GameObject gameObject;
        public GameObject cube;
        System.Random random = new System.Random();
        bool cooldownOver;
        bool cooldownAd;
        float timeStamp;
        float timeStampAd;
        bool isSkeld;
        public GameObject colliderWorld;
        public GameObject blindbox;
        public bool speedBoost;
        public bool enableCheats;
        public bool crashAll;
        public bool noclip;
        public bool tracers;
        public bool clearBody;
        public bool noKillCooldown;
        public bool getImpBool;
        public bool lobbyTroll;
        public bool allPlayersDrugs;
        public bool nameSpammerAll;
        public bool allPlayersColor;
        public bool allCosmetics;
        public bool kbMove;
        public bool advertise;
        public bool crewmateAll;
        public bool whiteAll;
        public bool frameRand;
        public bool tptoShapeShift;
        Il2CppSystem.Collections.Generic.List<string> nlist = new Il2CppSystem.Collections.Generic.List<string>();
        PlayerState randPlayer;
        
        public int currentAd = 0;
        bool adDone;
        Il2CppSystem.Collections.Generic.List<string> ad = new Il2CppSystem.Collections.Generic.List<string>();

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("MungVRLoaded");
            cooldownOver = true;
        }


        public override void OnApplicationLateStart()
        {
            MelonLogger.Msg("OnApplicationLateStart");
        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName)
        {
            MelonLogger.Msg("OnSceneWasLoaded: " + buildindex.ToString() + " | " + sceneName);
            gameObject = new GameObject();
            gameObject.AddComponent<gui>();
        }

        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            enableCheats = true;
            speedBoost = false;
            MelonLogger.Msg("OnSceneWasInitialized: " + buildindex.ToString() + " | " + sceneName);
            if (sceneName == "Skeld")
            {
                isSkeld = true;
                colliderWorld = GameObject.Find("Skeld_Collision");
                blindbox = GameObject.Find("Blindbox");
                ad.Add("real");
                ad.Add("not real");
                ad.Add("real");
                ad.Add("not real");
            }
            else
            {
                isSkeld = false;
            }
        }

        public override void OnUpdate()
        {
            XRHand xrHandl = GameObject.Find("LeftHand").GetComponent<XRHand>();
            XRHand xrHandr = GameObject.Find("RightHand").GetComponent<XRHand>();
            GameObject leftHand = GameObject.Find("LeftHand");
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            GameObject playerObjPhys = GameObject.Find("XRRig_Gameplay");

            if (advertise)
            {
                timerAd();
                if (currentAd == 0 && cooldownAd == true)
                {
                    changeAllName(ad[0]);
                    currentAd = currentAd + 1;
                    cooldownAd=false;
                    timeStampAd = Time.time + 2f;
                }
                else if (currentAd == 1 && cooldownAd == true)
                {
                    currentAd = currentAd + 1;
                    changeAllName(ad[1]);
                    cooldownAd=false;
                    timeStampAd = Time.time + 2f;
                }
                else if (currentAd == 2 && cooldownAd == true)
                {
                    currentAd = currentAd + 1;
                    changeAllName(ad[2]);
                    cooldownAd = false;
                    timeStampAd = Time.time + 2f;
                }
                else if (currentAd == 3 && cooldownAd == true)
                {
                    currentAd = 0;
                    changeAllName(ad[3]);
                    cooldownAd = false;
                    adDone = true;
                    timeStampAd = Time.time + 2f;
                }
            }

            if (kbMove)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    playerObjPhys.transform.position += Vector3.forward * Time.deltaTime * 4;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    playerObjPhys.transform.position += Vector3.back * Time.deltaTime * 4;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    playerObjPhys.transform.position += Vector3.left * Time.deltaTime * 4;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    playerObjPhys.transform.position += Vector3.right * Time.deltaTime * 4;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerObjPhys.transform.Rotate(0.0f, -1.0f, 0.0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerObjPhys.transform.Rotate(0.0f, 1.0f, 0.0f);
                }
            }



            if (allCosmetics && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    int chcCos = random.Next(0, 5);
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    playerObj.RPC_UpdateHatID(chcCos);
                }
            }

            if (allPlayersColor && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    int chc = random.Next(0, 12);
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    playerObj.RPC_UpdateColorID(chc);
                }
            }


            if (nameSpammerAll && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    int chcName = random.Next(0, 9999);
                    int chcCos = random.Next(0, 5);
                    string name = chcName.ToString();
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    playerObj.RPC_SetNetworkName(name);
                    playerObj.RPC_UpdateHatID(chcCos);
                }
            }

            if (whiteAll && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    int chc = random.Next(0, 0);
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    playerObj.RPC_UpdateColorID(chc);
                    playerObj.RPC_SetNetworkName("IM WHITE");
                }
            }

            if (allPlayersDrugs && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    int chc = random.Next(0, 12);
                    int chcName = random.Next(0, 9999);
                    int chcCos = random.Next(0, 5);
                    string name = chcName.ToString();
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    playerObj.RPC_UpdateColorID(chc);
                    playerObj.RPC_SetNetworkName("RAINBOW");
                    playerObj.RPC_UpdateHatID(chcCos);
                }
            }

            if (lobbyTroll && isSkeld)
            {
                GameObject playerStates = GameObject.Find("PlayerStates");
                int playerCount = playerStates.transform.childCount;
                for (int child = 0; child < playerCount; child++)
                {
                    Transform player = playerStates.transform.GetChild(child);
                    PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                    if (playerObj.ColorId != 1)
                    {
                        playerObj.RPC_UpdateColorID(1);
                    }
                    if (playerObj.HatId != 1)
                    {
                        playerObj.RPC_UpdateHatID(1);
                    }
                    if (playerObj.CachedName != "real")
                    {
                        playerObj.RPC_SetNetworkName("real");
                    }
                }
            }
            if (noKillCooldown && isSkeld && xrHandl._IsTriggerDown_k__BackingField)
            {
                NetworkedKillBehaviour networkedKillBehaviour = GameObject.Find("Networked Components").GetComponent<NetworkedKillBehaviour>();
                networkedKillBehaviour._cachedKillCooldown = 0;
                networkedKillBehaviour._killCooldown = 0;
                networkedKillBehaviour._killCooldownTime = 0;
            }

            if (clearBody && isSkeld && xrHandr._IsTriggerDown_k__BackingField)
            {
                GameObject bodies = GameObject.Find("NetworkBodies");
                int childCount = bodies.transform.childCount;
                for (int child = 0; child < childCount; child++)
                {
                    NetworkedBody body = bodies.transform.GetChild(child).gameObject.GetComponent<NetworkedBody>();
                    body.RPC_ToggleBody(false);
                }
            }

            if (tracers && isSkeld)
            {
                GameObject rhand = GameObject.Find("RightHand");
                GameObject netPlayers = GameObject.Find("NetworkLocomotionPlayers");
                int childCount = netPlayers.transform.childCount;
                MelonLogger.Msg("players: " + childCount);
                for (int child = 0; child < childCount; child++)
                {
                    Transform childTransform = netPlayers.transform.GetChild(child);
                    NetworkedLocomotionPlayer netobj = childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>();
                    LineRenderer tracer = childTransform.gameObject.GetComponent<LineRenderer>();
                    if (childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>()._activated != true)
                    {
                        tracer.enabled = false;
                    }
                    else if (childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>().enabled == true)
                    {
                        Transform physPlayer = childTransform.GetChild(0).Find("Visuals").Find("Player_Crewmate");
                        tracer.useWorldSpace = true;
                        tracer.enabled = true;
                        tracer.SetWidth(0.05f, 0.05f);
                        tracer.SetPosition(0, physPlayer.position);
                        tracer.SetPosition(1, rhand.transform.position);
                    }
                }
            }

            if (noclip)
            {
                colliderWorld.SetActive(false);
                blindbox.SetActive(false);
            }
            else if (noclip != true)
            {
                colliderWorld.SetActive(true);
                blindbox.SetActive(true);
            }
            if (xrHandl._IsGripPressed_k__BackingField && isSkeld && speedBoost)
            {
                playerRig._speed = 30f;
            }
            else if (xrHandl._IsGripPressed_k__BackingField != true && isSkeld)
            {
                playerRig._speed = 6.5f;
            }
            if (xrHandr._IsTriggerDown_k__BackingField == true && isSkeld && xrHandl._IsGripPressed_k__BackingField && enableCheats && crashAll)
            {
                SG.Airlock.Sabotage.SabotageManager sabotageManager = GameObject.Find("SabotageManager").GetComponent<SabotageManager>();
                Il2CppSystem.Collections.Generic.List<Sabotage> sabotages = sabotageManager.Sabotages;
                for (int i = 0; i < 4; i++)
                {
                    sabotageManager.RPC_BeginSabotage(i);
                }
            }
        }

        public void timer()
        {
            if (timeStamp <= Time.time)
            {
                MelonLogger.Msg("Cooldown Over");
                cooldownOver = true;
            }
        }

        public void clearBodies()
        {
            GameObject bodies = GameObject.Find("NetworkBodies");
            int childCount = bodies.transform.childCount;
            for (int child = 0; child < childCount; child++)
            {
                NetworkedBody body = bodies.transform.GetChild(child).gameObject.GetComponent<NetworkedBody>();
                body.RPC_ToggleBody(false);
            }
        }

        public void killAll()
        {
            NetworkedKillBehaviour networkedKillBehaviour = GameObject.Find("Networked Components").GetComponent<NetworkedKillBehaviour>();
            PlayerState playerLocal = new PlayerState();
            GameSession session = GameObject.Find("GameSession").GetComponent<GameSession>();
            Il2CppSystem.Collections.Generic.List<PlayerRef> players = new Il2CppSystem.Collections.Generic.List<PlayerRef>(session.Runner.ActivePlayers);
            int chc = random.Next(players.Count);
            foreach (PlayerRef player in players)
            {
                MelonLogger.Msg(player.PlayerId);
                if (playerLocal.PlayerId != player.PlayerId)
                {
                    networkedKillBehaviour.RPC_KillPlayer(player, player);
                }
            }
        }

        public void killRand()
        {
            NetworkedKillBehaviour networkedKillBehaviour = GameObject.Find("Networked Components").GetComponent<NetworkedKillBehaviour>();
            GameSession session = GameObject.Find("GameSession").GetComponent<GameSession>();
            SpawnManager sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            Il2CppSystem.Collections.Generic.List<PlayerRef> players = new Il2CppSystem.Collections.Generic.List<PlayerRef>(session.Runner.ActivePlayers);
            int rand = random.Next(players.Count);
            MelonLogger.Msg("Doing kill random player");
            networkedKillBehaviour.RPC_KillPlayer(players[rand], players[rand]);
            sm.RPC_HideBodyEmergencyButton(players[rand]);
            cooldownOver = false;
            timeStamp = Time.time + 1f;
            if (clearBody)
            {
                clearBodies();
            }
        }

        public Vector3 getPos(string nameToCheck)
        {
            GameObject netPlayers = GameObject.Find("NetworkLocomotionPlayers");
            int childCount = netPlayers.transform.childCount;
            for (int child = 0; child < childCount; child++)
            {
                Transform childTransform = netPlayers.transform.GetChild(child);
                NetworkedLocomotionPlayer netobj = childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>();
                if(netobj.PState.CachedName == nameToCheck)
                {
                    Transform physPlayer = childTransform.GetChild(0).Find("Visuals").Find("Player_Crewmate");
                    MelonLogger.Msg("Found target player @ " + physPlayer.position.ToString());
                    return physPlayer.position;
                }
            }
            MelonLogger.Msg("Failed to find player");
            return Vector3.zero;
        }
        /*RPC_EnterVent(Fusion.NetworkBool isHigh, UnityEngine.Vector3 entryPosition, UnityEngine.Quaternion entryRotation, UnityEngine.Vector3 storagePosition)*/

        public void MakeAllVent()
        {
            GameObject netPlayers = GameObject.Find("NetworkLocomotionPlayers");
            int childCount = netPlayers.transform.childCount;
            for (int child = 0; child < childCount; child++)
            {
                Transform childTransform = netPlayers.transform.GetChild(child);
                NetworkedLocomotionPlayer netobj = childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>();
                Transform physPlayer = childTransform.GetChild(0).Find("Visuals").Find("Player_Crewmate");
                netobj.RPC_EnterVent(false, physPlayer.transform.position, Quaternion.identity, Vector3.zero);
            }
        }

        public void MakeAllLeaveVent()
        {
            GameObject netPlayers = GameObject.Find("NetworkLocomotionPlayers");
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            int childCount = netPlayers.transform.childCount;
            for (int child = 0; child < childCount; child++)
            {
                Transform childTransform = netPlayers.transform.GetChild(child);
                NetworkedLocomotionPlayer netobj = childTransform.gameObject.GetComponent<NetworkedLocomotionPlayer>();
                Transform physPlayer = childTransform.GetChild(0).Find("Visuals").Find("Player_Crewmate");
                netobj.RPC_ExitVent(false, playerRig.transform.position, Quaternion.identity);
            }
        }

        public void timerAd()
        {
            if (timeStampAd <= Time.time)
            {
                MelonLogger.Msg("Cooldown Over");
                cooldownAd = true;
            }
        }

        public void enableTracers()
        {
            GameObject rhand = GameObject.Find("RightHand");
            GameObject netPlayers = GameObject.Find("NetworkLocomotionPlayers");
            int childCount = netPlayers.transform.childCount;
            MelonLogger.Msg("players: " + childCount);
            for (int child = 0; child < childCount; child++)
            {
                MelonLogger.Msg("adding tracer to: " + child);
                Transform childTransform = netPlayers.transform.GetChild(child);
                LineRenderer tracer = childTransform.gameObject.AddComponent<LineRenderer>();
                tracer.positionCount = 2;
            }
        }

        public void getCrewmate()
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            PlayerState localPlayer = playerRig.PState;
            localPlayer.RPC_IgnoreImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
        }


        public void randomizeTeams()
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            PlayerState localPlayer = playerRig.PState;
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            localPlayer.RPC_ForceImposter(false);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
        }

        public void getImp()
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            PlayerState localPlayer = playerRig.PState;
            localPlayer.RPC_ForceImposter(true);
            localPlayer.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
        }

        public void openDoors()
        {
            GameStateManager game = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
            game.RPC_ToggleLobbyDoors(false);
        }

        public void closeDoors()
        {
            GameStateManager game = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
            game.RPC_ToggleLobbyDoors(true);
        }

        public void changename(string name)
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            PlayerState localPlayer = playerRig.PState;
            localPlayer.RPC_SetNetworkName(name);
        }

        public void samePlayer()
        {
            GameObject playerStates = GameObject.Find("PlayerStates");
            int playerCount = playerStates.transform.childCount;
            int chc = random.Next(0, playerCount);
            Transform playerTarget = playerStates.transform.GetChild(chc);
            PlayerState playerObjTarget = playerTarget.gameObject.GetComponent<PlayerState>();
            for (int child = 0; child < playerCount; child++)
            {
                Transform player = playerStates.transform.GetChild(child);
                PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                playerObj.RPC_SetNetworkName(playerObjTarget.CachedName);
                playerObj.RPC_UpdateColorID(playerObjTarget.ColorId);
                playerObj.RPC_UpdateHatID(playerObjTarget.HatId);
            }
        }

        public void allImp()
        {
            GameObject playerStates = GameObject.Find("PlayerStates");
            int playerCount = playerStates.transform.childCount;
            for (int child = 0; child < playerCount; child++)
            {
                Transform player = playerStates.transform.GetChild(child);
                PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                playerObj.RPC_ForceImposter(true);
                playerObj.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Imposter);
            }
        }

        public void allCrew()
        {
            GameObject playerStates = GameObject.Find("PlayerStates");
            int playerCount = playerStates.transform.childCount;
            for (int child = 0; child < playerCount; child++)
            {
                Transform player = playerStates.transform.GetChild(child);
                PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                playerObj.RPC_IgnoreImposter(true);
                playerObj.RPC_KnownGameRole(SG.Airlock.Roles.GameRole.Crewmember);
            }
        }

        public void revive()
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            PlayerState localPlayer = playerRig.PState;
            localPlayer.IsAlive= true;
            localPlayer._IsAlive= true;
        }

        public void voteRig()
        {
            VoteManager vote = GameObject.Find("VoteManager").GetComponent<VoteManager>();
            GameSession session = GameObject.Find("GameSession").GetComponent<GameSession>();
            Il2CppSystem.Collections.Generic.List<PlayerRef> players = new Il2CppSystem.Collections.Generic.List<PlayerRef>(session.Runner.ActivePlayers);
            for (int player = 0; player < players.Count; player++)
            {
                for (int i = 0; i < 12; i++)
                {
                    vote.RPC_Vote(players[player], players[player]);
                }
            }
        }

        public void changeAllName(string name)
        {
            GameObject playerStates = GameObject.Find("PlayerStates");
            int playerCount = playerStates.transform.childCount;
            for (int child = 0; child < playerCount; child++)
            {
                Transform player = playerStates.transform.GetChild(child);
                PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
                playerObj.RPC_SetNetworkName(name);
            }
        }

        public void joinPub()
        {
            GameObject uiRoot = GameObject.Find("Menus");
            int childCount = uiRoot.transform.childCount;
            GameObject quickStart = null;
            for (int child = 0; child < childCount; child++)
            {
                if (uiRoot.transform.GetChild(child).name == "Quick Match Menu")
                {
                    quickStart = uiRoot.transform.GetChild(child).gameObject;
                }
            }
            quickStart.SetActive(true);
        }

        public void chcPlayerRand()
        {
            GameObject playerStates = GameObject.Find("PlayerStates");
            int playerCount = playerStates.transform.childCount;
            int chc = random.Next(playerCount);
            Transform player = playerStates.transform.GetChild(chc);
            PlayerState playerObj = player.gameObject.GetComponent<PlayerState>();
            if (!playerObj.IsConnected)
            {
                while (!playerObj.IsConnected)
                {
                    chc = random.Next(playerCount);
                    player = playerStates.transform.GetChild(chc);
                    playerObj = player.gameObject.GetComponent<PlayerState>();
                }
            }
            randPlayer = playerObj;
        }

        public void shapeShift()
        {
            SG.Airlock.XR.XRRig playerRig = GameObject.Find("XRRig_Gameplay").GetComponent<SG.Airlock.XR.XRRig>();
            GameObject playerPhys = GameObject.Find("XRRig_Gameplay");
            PlayerState localPlayer = playerRig.PState;
            string cname = localPlayer.CachedName;
            int hid = localPlayer.HatId;
            int cid = localPlayer.ColorId;
            localPlayer.RPC_UpdateColorID(randPlayer.ColorId);
            localPlayer.RPC_UpdateHatID(randPlayer.HatId);
            localPlayer.RPC_SetNetworkName(randPlayer.CachedName);
            randPlayer.RPC_UpdateColorID(cid);
            randPlayer.RPC_UpdateHatID(hid);
            randPlayer.RPC_SetNetworkName(cname);
            if (tptoShapeShift)
            {
                playerPhys.transform.position = getPos(randPlayer.CachedName);
            }
        }

        public string playerName = "Player Name";
        public string allPlayerName = "All Players Name";


        public override void OnGUI()
        {
            GUI.Box(new Rect(20, 0, 160, 20), "Tragic | Full");
            GUI.color = Color.green;
            playerName = GUI.TextField(new Rect(200, 0, 160, 30), playerName, 200);
            allPlayerName = GUI.TextField(new Rect(200, 60, 160, 30), allPlayerName, 200);
            if (GUI.Button(new Rect(200, 30, 160, 30), "Change Name") && isSkeld)
            {
                changename(playerName);
            }
            if (GUI.Button(new Rect(200, 90, 160, 30), "Change All Names") && isSkeld)
            {
                changeAllName(allPlayerName);
            }


            if (GUI.Button(new Rect(20, 20, 160, 30), "Speed Boost") && isSkeld)
            {
                speedBoost = !speedBoost;
            }
            if (GUI.Button(new Rect(20, 50, 160, 30), "Kill Random") && isSkeld)
            {
                killRand();
            }
            if (GUI.Button(new Rect(20, 80, 160, 30), "Kill All") && isSkeld)
            {
                killAll();
            }
            if (GUI.Button(new Rect(20, 110, 160, 30), "Crash All") && isSkeld)
            {
                crashAll= !crashAll;
            }
            if (GUI.Button(new Rect(20, 140, 160, 30), "Noclip") && isSkeld)
            {
                noclip= !noclip;
            }
            if (GUI.Button(new Rect(20, 170, 160, 30), "Tracers") && isSkeld)
            {
                enableTracers();
                tracers= !tracers;
            }
            if (GUI.Button(new Rect(20, 200, 160, 30), "Clear All Bodies") && isSkeld)
            {
                clearBody= !clearBody;
            }
            if (GUI.Button(new Rect(20, 230, 160, 30), "No Kill Cooldown") && isSkeld)
            {
                noKillCooldown= !noKillCooldown;
            }
            if (GUI.Button(new Rect(20, 260, 160, 30), "Get Imposter") && isSkeld)
            {
                getImp();
                getImpBool= !getImpBool;
            }
            if (GUI.Button(new Rect(20, 290, 160, 30), "Open Lobby Doors") && isSkeld)
            {
                openDoors();
            }
            if (GUI.Button(new Rect(20, 320, 160, 30), "Close Lobby Doors") && isSkeld)
            {
                closeDoors();
            }
            if (GUI.Button(new Rect(20, 350, 160, 30), "Player Troll") && isSkeld)
            {
                lobbyTroll= !lobbyTroll;
            }
            if (GUI.Button(new Rect(20, 380, 160, 30), "All Players Same") && isSkeld)
            {
                samePlayer();
            }
            if (GUI.Button(new Rect(20, 410, 160, 30), "Make All Players Imposter") && isSkeld)
            {
                allImp();
            }
            if (GUI.Button(new Rect(20, 440, 160, 30), "All Players Drugs") && isSkeld)
            {
                allPlayersDrugs= !allPlayersDrugs;
            }
            if (GUI.Button(new Rect(20, 470, 160, 30), "WSAD movement") && isSkeld)
            {
                kbMove= !kbMove;
            }
            if (GUI.Button(new Rect(20, 500, 160, 30), "Join Public"))
            {
                joinPub();
            }
            if (GUI.Button(new Rect(20, 530, 160, 30), "Advertise"))
            {
                advertise= !advertise;
            }
            if (GUI.Button(new Rect(20, 560, 160, 30), "Make All Crewmate"))
            {
                crewmateAll = !crewmateAll;
            }
            if (GUI.Button(new Rect(20, 590, 160, 30), "Get Crewmate"))
            {
                this.getCrewmate();
            }
            if (GUI.Button(new Rect(20, 620, 160, 30), "Randomize Roles Spammmer"))
            {
                this.randomizeTeams();
            }
            if (GUI.Button(new Rect(20, 650, 160, 30), "Name Spammer All"))
            {
                nameSpammerAll = !nameSpammerAll;
            }
            if (GUI.Button(new Rect(20, 680, 160, 30), "Cosmetic Spammer All"))
            {
                     allCosmetics= !allCosmetics;
            }
            if (GUI.Button(new Rect(20, 710, 160, 30), "Color Spammer All"))
            {
                allPlayersColor = !allPlayersColor;
            }
            if (GUI.Button(new Rect(20, 740, 160, 30), "White All"))
            {
                whiteAll = !whiteAll;
            }
            if (GUI.Button(new Rect(20, 770, 160, 30), "Choose random player"))
            {
                chcPlayerRand();
            }
            if (GUI.Button(new Rect(20, 830, 160, 30), "Frame player"))
            {
                frameRand = !frameRand;
            }
            if (GUI.Button(new Rect(20, 860, 160, 30), "Toggle tp to shapeshitft"))
            {
                tptoShapeShift = !tptoShapeShift;
            }
            if (GUI.Button(new Rect(20, 890, 160, 30), "Shapeshift"))
            {
                shapeShift();
            }
            if (GUI.Button(new Rect(20, 920, 160, 30), "Invis all"))
            {
                MakeAllVent();
            }
            if (GUI.Button(new Rect(20, 950, 160, 30), "Un-Invis all"))
            {
                MakeAllLeaveVent();
            }




            GUI.Box(new Rect(1500, 10, 200, 3000), "CHEATS ENABLED:\n" +
                "Speed boost: " + speedBoost + "\n" +
                "Crash All: " + crashAll + "\n" +
                "Noclip: " + noclip + "\n" +
                "Tracers: " + tracers + "\n" +
                "Clear Bodies: " + clearBody + "\n" +
                "No Kill Cooldown: " + noKillCooldown + "\n" +
                "Get Imposter: " + getImpBool + "\n+" +
                "Player Troll: " + lobbyTroll + "\n" +
                "LSD: "+ allPlayersDrugs+"\n"+
                "WSAD Movement: "+kbMove+"\n"+
                "Advertise: "+advertise+ "\n" +
                "Name Spam All: " + nameSpammerAll + "\n" +
                "Color Spam All: " + allPlayersColor + "\n" +
                "Cosmetic Spam All: " + allCosmetics + "\n" +
                "White All: "+ whiteAll + "\n" + 
                "Frame Random: " + frameRand + "\n" +
                "Tp to shapeshift: "+tptoShapeShift
            );
            if (randPlayer.CachedName != null)
            {
                GUI.Box(new Rect(20, 800, 160, 30),
                "-> " + randPlayer.CachedName
                );
            }

            else
            {
                GUI.Box(new Rect(20, 800, 160, 30),
                "CHOOSE RANDOM"
                );
            }
        }
    }
}