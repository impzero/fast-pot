﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastPot
{
    class ThrowPot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public KeyboardHook.VKeys KeyBind { get; set; } = KeyboardHook.VKeys.KEY_Z;
        public KeyboardHook.VKeys ThrowPotKey { get; set; } = KeyboardHook.VKeys.KEY_Q;
        public KeyboardHook.VKeys InventoryKey { get; set; } = KeyboardHook.VKeys.KEY_E;
        public KeyboardHook.VKeys FirstPot { get; set; } = KeyboardHook.VKeys.KEY_3;
        public KeyboardHook.VKeys LastPot { get; set; } = KeyboardHook.VKeys.KEY_8;
        public int CurrentSlot { get; set; } = 3;

        public bool IsToggled { get; set; }
        public bool IsReadyToPot { get; set; }

        public uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public uint MOUSEEVENTF_RIGHTDOWN = 0x0008;

        KeyboardHook keyboardHook = new KeyboardHook();
        Random rnd = new Random();
        Thread sendKeys;

        public ThrowPot()
        {
            sendKeys = new Thread(ThrowPotion);
            keyboardHook.KeyUp += KeyboardHook_KeyUp;
            keyboardHook.Install();
        }

        void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (key == KeyBind || key == KeyboardHook.VKeys.SHIFT)
            {
                IsToggled = !IsToggled;
                MainForm.MainFr.label1.Text = IsToggled ? "TOGGLED: YES" : "TOGGLED: NO";
            }

            if (key == ThrowPotKey && !IsReadyToPot && IsToggled)
            {
                if (CurrentSlot > (int)LastPot-48) CurrentSlot = (int)FirstPot-48;
                ThrowPotion();
                CurrentSlot++;
            }
            if (key == InventoryKey) CurrentSlot = (int)FirstPot-48;
        }

        void ThrowPotion()
        {
            IsReadyToPot = true;
            SendKeys.Send("{" + CurrentSlot.ToString() + "}");
            Thread.Sleep(1);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, MOUSEEVENTF_RIGHTDOWN, 0);
            Thread.Sleep(1);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, MOUSEEVENTF_RIGHTUP, 0);
            Thread.Sleep(50);
            SendKeys.Send("{1}");
            IsReadyToPot = false;
        }
    }
}
