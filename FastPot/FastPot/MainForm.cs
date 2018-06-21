﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastPot
{
    public partial class MainForm : Form
    {
        public static MainForm MainFr;
        ThrowPot ThrowPot;
        public MainForm()
        {
            MainFr = this;
            InitializeComponent();
            button1.Click += OnClick;
            button2.Click += OnClick;
            button3.Click += OnClick;
            button4.Click += OnClick;
            button5.Click += OnClick;
            button1.KeyUp += OnRelease;
            button2.KeyUp += OnRelease;
            button3.KeyUp += OnRelease;
            button4.KeyUp += OnRelease;
            button5.KeyUp += OnRelease;
        }
        void OnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = "...";
            if (button.Name == "button5") ThrowPot.IsReadyToPot = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ThrowPot = new ThrowPot();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            ThrowPot.KeyBind = Utility.ConvertToBindable(e);
            button1.Text = ThrowPot.KeyBind.ToString().StartsWith("KEY_") ? $"TOGGLE KEY [{ThrowPot.KeyBind.ToString().Substring(4)}]" : $"TOGGLE KEY [{ThrowPot.KeyBind.ToString()}]";
        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            ThrowPot.ThrowPotKey = Utility.ConvertToBindable(e);
            button5.Text = ThrowPot.ThrowPotKey.ToString().StartsWith("KEY_") ? $"THROW POT [{ThrowPot.ThrowPotKey.ToString().Substring(4)}]" : $"THROW POT [{ThrowPot.ThrowPotKey.ToString()}]";
        }

        private void OnRelease(object sender, KeyEventArgs e)
        {
            ThrowPot.IsReadyToPot = false;
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            ThrowPot.InventoryKey = Utility.ConvertToBindable(e);
            button2.Text = ThrowPot.InventoryKey.ToString().StartsWith("KEY_") ? $"INVENTORY KEY [{ThrowPot.InventoryKey.ToString().Substring(4)}]" : $"INVENTORY KEY [{ThrowPot.InventoryKey.ToString()}]";
        }
    }
}
