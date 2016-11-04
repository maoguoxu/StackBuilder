﻿namespace treeDiM.StackBuilder.Desktop
{
    partial class FormNewAnalysisCasePallet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewAnalysisCasePallet));
            this.cbPallets = new treeDiM.StackBuilder.Graphics.Controls.CCtrlComboFiltered();
            this.cbCases = new treeDiM.StackBuilder.Graphics.Controls.CCtrlComboFiltered();
            this.lbBox = new System.Windows.Forms.Label();
            this.uCtrlOptMaximumWeight = new treeDiM.StackBuilder.Basics.UCtrlOptDouble();
            this.uCtrlOptMaximumHeight = new treeDiM.StackBuilder.Basics.UCtrlOptDouble();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uCtrlOverhang = new treeDiM.StackBuilder.Basics.UCtrlDualDouble();
            this.lbPallet = new System.Windows.Forms.Label();
            this.uCtrlLayerList = new treeDiM.StackBuilder.Graphics.UCtrlLayerList();
            this.checkBoxBestLayersOnly = new System.Windows.Forms.CheckBox();
            this.bnBestCombination = new System.Windows.Forms.Button();
            this.uCtrlCaseOrientation = new treeDiM.StackBuilder.Graphics.uCtrlCaseOrientation();
            this.tabCtrlConstraints = new System.Windows.Forms.TabControl();
            this.tabPageOverhang = new System.Windows.Forms.TabPage();
            this.tabPageStopCriterions = new System.Windows.Forms.TabPage();
            this.tabPageSpaces = new System.Windows.Forms.TabPage();
            this.tabPageLayerFilters = new System.Windows.Forms.TabPage();
            this.uCtrlOptSpace = new treeDiM.StackBuilder.Basics.UCtrlOptDouble();
            this.uCtrlOptMaximumSpace = new treeDiM.StackBuilder.Basics.UCtrlOptDouble();
            this.groupBox1.SuspendLayout();
            this.tabCtrlConstraints.SuspendLayout();
            this.tabPageOverhang.SuspendLayout();
            this.tabPageStopCriterions.SuspendLayout();
            this.tabPageSpaces.SuspendLayout();
            this.tabPageLayerFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            // 
            // cbPallets
            // 
            this.cbPallets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPallets.FormattingEnabled = true;
            resources.ApplyResources(this.cbPallets, "cbPallets");
            this.cbPallets.Name = "cbPallets";
            this.cbPallets.SelectedIndexChanged += new System.EventHandler(this.onInputChanged);
            // 
            // cbCases
            // 
            this.cbCases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCases.FormattingEnabled = true;
            resources.ApplyResources(this.cbCases, "cbCases");
            this.cbCases.Name = "cbCases";
            this.cbCases.SelectedIndexChanged += new System.EventHandler(this.onCaseChanged);
            // 
            // lbBox
            // 
            resources.ApplyResources(this.lbBox, "lbBox");
            this.lbBox.Name = "lbBox";
            // 
            // uCtrlOptMaximumWeight
            // 
            resources.ApplyResources(this.uCtrlOptMaximumWeight, "uCtrlOptMaximumWeight");
            this.uCtrlOptMaximumWeight.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.uCtrlOptMaximumWeight.Name = "uCtrlOptMaximumWeight";
            this.uCtrlOptMaximumWeight.Unit = treeDiM.StackBuilder.Basics.UnitsManager.UnitType.UT_MASS;
            this.uCtrlOptMaximumWeight.Value = ((treeDiM.StackBuilder.Basics.OptDouble)(resources.GetObject("uCtrlOptMaximumWeight.Value")));
            this.uCtrlOptMaximumWeight.ValueChanged += new treeDiM.StackBuilder.Basics.UCtrlOptDouble.onValueChanged(this.onInputChanged);
            // 
            // uCtrlOptMaximumHeight
            // 
            resources.ApplyResources(this.uCtrlOptMaximumHeight, "uCtrlOptMaximumHeight");
            this.uCtrlOptMaximumHeight.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            this.uCtrlOptMaximumHeight.Name = "uCtrlOptMaximumHeight";
            this.uCtrlOptMaximumHeight.Unit = treeDiM.StackBuilder.Basics.UnitsManager.UnitType.UT_LENGTH;
            this.uCtrlOptMaximumHeight.Value = ((treeDiM.StackBuilder.Basics.OptDouble)(resources.GetObject("uCtrlOptMaximumHeight.Value")));
            this.uCtrlOptMaximumHeight.ValueChanged += new treeDiM.StackBuilder.Basics.UCtrlOptDouble.onValueChanged(this.onInputChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uCtrlOptMaximumWeight);
            this.groupBox1.Controls.Add(this.uCtrlOptMaximumHeight);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // uCtrlOverhang
            // 
            resources.ApplyResources(this.uCtrlOverhang, "uCtrlOverhang");
            this.uCtrlOverhang.Name = "uCtrlOverhang";
            this.uCtrlOverhang.Unit = treeDiM.StackBuilder.Basics.UnitsManager.UnitType.UT_LENGTH;
            this.uCtrlOverhang.ValueX = 0D;
            this.uCtrlOverhang.ValueY = 0D;
            this.uCtrlOverhang.ValueChanged += new treeDiM.StackBuilder.Basics.UCtrlDualDouble.onValueChanged(this.onInputChanged);
            // 
            // lbPallet
            // 
            resources.ApplyResources(this.lbPallet, "lbPallet");
            this.lbPallet.Name = "lbPallet";
            // 
            // uCtrlLayerList
            // 
            resources.ApplyResources(this.uCtrlLayerList, "uCtrlLayerList");
            this.uCtrlLayerList.ButtonSizes = new System.Drawing.Size(150, 150);
            this.uCtrlLayerList.Name = "uCtrlLayerList";
            // 
            // checkBoxBestLayersOnly
            // 
            resources.ApplyResources(this.checkBoxBestLayersOnly, "checkBoxBestLayersOnly");
            this.checkBoxBestLayersOnly.Name = "checkBoxBestLayersOnly";
            this.checkBoxBestLayersOnly.UseVisualStyleBackColor = true;
            this.checkBoxBestLayersOnly.CheckedChanged += new System.EventHandler(this.onInputChanged);
            // 
            // bnBestCombination
            // 
            resources.ApplyResources(this.bnBestCombination, "bnBestCombination");
            this.bnBestCombination.Name = "bnBestCombination";
            this.bnBestCombination.UseVisualStyleBackColor = true;
            this.bnBestCombination.Click += new System.EventHandler(this.onBestCombinationClicked);
            // 
            // uCtrlCaseOrientation
            // 
            this.uCtrlCaseOrientation.AllowedOrientations = new bool[] {
        false,
        false,
        true};
            resources.ApplyResources(this.uCtrlCaseOrientation, "uCtrlCaseOrientation");
            this.uCtrlCaseOrientation.Name = "uCtrlCaseOrientation";
            this.uCtrlCaseOrientation.CheckedChanged += new treeDiM.StackBuilder.Graphics.uCtrlCaseOrientation.CheckChanged(this.onInputChanged);
            // 
            // tabCtrlConstraints
            // 
            this.tabCtrlConstraints.Controls.Add(this.tabPageStopCriterions);
            this.tabCtrlConstraints.Controls.Add(this.tabPageOverhang);
            this.tabCtrlConstraints.Controls.Add(this.tabPageSpaces);
            this.tabCtrlConstraints.Controls.Add(this.tabPageLayerFilters);
            resources.ApplyResources(this.tabCtrlConstraints, "tabCtrlConstraints");
            this.tabCtrlConstraints.Name = "tabCtrlConstraints";
            this.tabCtrlConstraints.SelectedIndex = 0;
            // 
            // tabPageOverhang
            // 
            this.tabPageOverhang.Controls.Add(this.uCtrlOverhang);
            resources.ApplyResources(this.tabPageOverhang, "tabPageOverhang");
            this.tabPageOverhang.Name = "tabPageOverhang";
            this.tabPageOverhang.UseVisualStyleBackColor = true;
            // 
            // tabPageStopCriterions
            // 
            this.tabPageStopCriterions.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPageStopCriterions, "tabPageStopCriterions");
            this.tabPageStopCriterions.Name = "tabPageStopCriterions";
            this.tabPageStopCriterions.UseVisualStyleBackColor = true;
            // 
            // tabPageSpaces
            // 
            this.tabPageSpaces.Controls.Add(this.uCtrlOptSpace);
            resources.ApplyResources(this.tabPageSpaces, "tabPageSpaces");
            this.tabPageSpaces.Name = "tabPageSpaces";
            this.tabPageSpaces.UseVisualStyleBackColor = true;
            // 
            // tabPageLayerFilters
            // 
            this.tabPageLayerFilters.Controls.Add(this.uCtrlOptMaximumSpace);
            resources.ApplyResources(this.tabPageLayerFilters, "tabPageLayerFilters");
            this.tabPageLayerFilters.Name = "tabPageLayerFilters";
            this.tabPageLayerFilters.UseVisualStyleBackColor = true;
            // 
            // uCtrlOptSpace
            // 
            resources.ApplyResources(this.uCtrlOptSpace, "uCtrlOptSpace");
            this.uCtrlOptSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.uCtrlOptSpace.Name = "uCtrlOptSpace";
            this.uCtrlOptSpace.Unit = treeDiM.StackBuilder.Basics.UnitsManager.UnitType.UT_LENGTH;
            this.uCtrlOptSpace.Value = ((treeDiM.StackBuilder.Basics.OptDouble)(resources.GetObject("uCtrlOptSpace.Value")));
            // 
            // uCtrlOptMaximumSpace
            // 
            resources.ApplyResources(this.uCtrlOptMaximumSpace, "uCtrlOptMaximumSpace");
            this.uCtrlOptMaximumSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.uCtrlOptMaximumSpace.Name = "uCtrlOptMaximumSpace";
            this.uCtrlOptMaximumSpace.Unit = treeDiM.StackBuilder.Basics.UnitsManager.UnitType.UT_LENGTH;
            this.uCtrlOptMaximumSpace.Value = ((treeDiM.StackBuilder.Basics.OptDouble)(resources.GetObject("uCtrlOptMaximumSpace.Value")));
            // 
            // FormNewAnalysisCasePallet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabCtrlConstraints);
            this.Controls.Add(this.bnBestCombination);
            this.Controls.Add(this.checkBoxBestLayersOnly);
            this.Controls.Add(this.uCtrlLayerList);
            this.Controls.Add(this.uCtrlCaseOrientation);
            this.Controls.Add(this.cbPallets);
            this.Controls.Add(this.cbCases);
            this.Controls.Add(this.lbPallet);
            this.Controls.Add(this.lbBox);
            this.Name = "FormNewAnalysisCasePallet";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.bnCancel, 0);
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.lbDescription, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.tbDescription, 0);
            this.Controls.SetChildIndex(this.lbBox, 0);
            this.Controls.SetChildIndex(this.lbPallet, 0);
            this.Controls.SetChildIndex(this.cbCases, 0);
            this.Controls.SetChildIndex(this.cbPallets, 0);
            this.Controls.SetChildIndex(this.uCtrlCaseOrientation, 0);
            this.Controls.SetChildIndex(this.uCtrlLayerList, 0);
            this.Controls.SetChildIndex(this.checkBoxBestLayersOnly, 0);
            this.Controls.SetChildIndex(this.bnBestCombination, 0);
            this.Controls.SetChildIndex(this.tabCtrlConstraints, 0);
            this.groupBox1.ResumeLayout(false);
            this.tabCtrlConstraints.ResumeLayout(false);
            this.tabPageOverhang.ResumeLayout(false);
            this.tabPageStopCriterions.ResumeLayout(false);
            this.tabPageSpaces.ResumeLayout(false);
            this.tabPageLayerFilters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.ComboBox cbPallets;
        private treeDiM.StackBuilder.Graphics.Controls.CCtrlComboFiltered cbPallets;
        //private System.Windows.Forms.ComboBox cbCases;
        private treeDiM.StackBuilder.Graphics.Controls.CCtrlComboFiltered cbCases;
        private System.Windows.Forms.Label lbBox;
        private Basics.UCtrlOptDouble uCtrlOptMaximumWeight;
        private Basics.UCtrlOptDouble uCtrlOptMaximumHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private Basics.UCtrlDualDouble uCtrlOverhang;
        private System.Windows.Forms.Label lbPallet;
        private Graphics.UCtrlLayerList uCtrlLayerList;
        private System.Windows.Forms.CheckBox checkBoxBestLayersOnly;
        private System.Windows.Forms.Button bnBestCombination;
        private Graphics.uCtrlCaseOrientation uCtrlCaseOrientation;
        private System.Windows.Forms.TabControl tabCtrlConstraints;
        private System.Windows.Forms.TabPage tabPageOverhang;
        private System.Windows.Forms.TabPage tabPageStopCriterions;
        private System.Windows.Forms.TabPage tabPageSpaces;
        private System.Windows.Forms.TabPage tabPageLayerFilters;
        private Basics.UCtrlOptDouble uCtrlOptSpace;
        private Basics.UCtrlOptDouble uCtrlOptMaximumSpace;
    }
}