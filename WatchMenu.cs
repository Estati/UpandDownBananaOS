using BananaOS;
using BananaOS.Pages;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace QuickDisconnectBananaOS
{
    internal class WatchMenu:WatchPage
    {
        public override string Title => "<color=green>Up</color> and <color=red>Down</color>";
        public override bool DisplayOnMainMenu => true;
        public bool IsEnabled;
        
        public override string OnGetScreenContent()
        {
            var BuildMenuOptions = new StringBuilder();
            BuildMenuOptions.AppendLine("<color=green>Up</color> and <color=red>Down</color>");
            BuildMenuOptions.AppendLine("By <color=blue>Estatic</color>");
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enabled = " + IsEnabled));
            return BuildMenuOptions.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                    case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        IsEnabled = !IsEnabled;
                    }
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
        void Update()
        {
            // thanks for the gamemode check dean!
            if (IsEnabled && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.bodyCollider.transform.up * 25f * Time.deltaTime;
                }
                if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.5)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.bodyCollider.transform.up * -25f * Time.deltaTime;
                }
            }
        }
    }
}
