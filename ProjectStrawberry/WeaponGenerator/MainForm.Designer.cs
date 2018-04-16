namespace WeaponGenerator
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBoxWeapons = new System.Windows.Forms.ListBox();
            this.labelTier = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.numWeight = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numReqWp = new System.Windows.Forms.NumericUpDown();
            this.numMaxDmg = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numMinDmg = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxWeaponTypes = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxTournamentItem = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqWp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDmg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDmg)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(886, 226);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxTournamentItem);
            this.tabPage1.Controls.Add(this.listBoxWeapons);
            this.tabPage1.Controls.Add(this.labelTier);
            this.tabPage1.Controls.Add(this.numPrice);
            this.tabPage1.Controls.Add(this.btnReset);
            this.tabPage1.Controls.Add(this.btnCreate);
            this.tabPage1.Controls.Add(this.numWeight);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.numReqWp);
            this.tabPage1.Controls.Add(this.numMaxDmg);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.numMinDmg);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.comboBoxWeaponTypes);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(878, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "WeaponGenerator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBoxWeapons
            // 
            this.listBoxWeapons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxWeapons.FormattingEnabled = true;
            this.listBoxWeapons.Location = new System.Drawing.Point(585, 6);
            this.listBoxWeapons.Name = "listBoxWeapons";
            this.listBoxWeapons.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxWeapons.Size = new System.Drawing.Size(287, 182);
            this.listBoxWeapons.TabIndex = 30;
            // 
            // labelTier
            // 
            this.labelTier.AutoSize = true;
            this.labelTier.Location = new System.Drawing.Point(168, 173);
            this.labelTier.Name = "labelTier";
            this.labelTier.Size = new System.Drawing.Size(37, 13);
            this.labelTier.TabIndex = 29;
            this.labelTier.Text = "Tier: 0";
            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(302, 128);
            this.numPrice.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(45, 20);
            this.numPrice.TabIndex = 28;
            this.numPrice.ValueChanged += new System.EventHandler(this.UpdateVariables);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 168);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(87, 168);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 25;
            this.btnCreate.Text = "Send to Db";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // numWeight
            // 
            this.numWeight.Location = new System.Drawing.Point(302, 95);
            this.numWeight.Name = "numWeight";
            this.numWeight.Size = new System.Drawing.Size(45, 20);
            this.numWeight.TabIndex = 21;
            this.numWeight.ValueChanged += new System.EventHandler(this.UpdateVariables);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Weight";
            // 
            // numReqWp
            // 
            this.numReqWp.Location = new System.Drawing.Point(302, 61);
            this.numReqWp.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReqWp.Name = "numReqWp";
            this.numReqWp.Size = new System.Drawing.Size(45, 20);
            this.numReqWp.TabIndex = 19;
            this.numReqWp.ValueChanged += new System.EventHandler(this.UpdateVariables);
            // 
            // numMaxDmg
            // 
            this.numMaxDmg.Location = new System.Drawing.Point(361, 24);
            this.numMaxDmg.Name = "numMaxDmg";
            this.numMaxDmg.Size = new System.Drawing.Size(45, 20);
            this.numMaxDmg.TabIndex = 17;
            this.numMaxDmg.ValueChanged += new System.EventHandler(this.UpdateVariables);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "-";
            // 
            // numMinDmg
            // 
            this.numMinDmg.Location = new System.Drawing.Point(302, 24);
            this.numMinDmg.Name = "numMinDmg";
            this.numMinDmg.Size = new System.Drawing.Size(45, 20);
            this.numMinDmg.TabIndex = 15;
            this.numMinDmg.ValueChanged += new System.EventHandler(this.UpdateVariables);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Weapon Type";
            // 
            // comboBoxWeaponTypes
            // 
            this.comboBoxWeaponTypes.FormattingEnabled = true;
            this.comboBoxWeaponTypes.Location = new System.Drawing.Point(100, 55);
            this.comboBoxWeaponTypes.Name = "comboBoxWeaponTypes";
            this.comboBoxWeaponTypes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxWeaponTypes.TabIndex = 13;
            this.comboBoxWeaponTypes.SelectedIndexChanged += new System.EventHandler(this.UpdateListOfWeapons);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "glods";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Price";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Req Wp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Damage";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(100, 26);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(121, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(878, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxTournamentItem
            // 
            this.checkBoxTournamentItem.AutoSize = true;
            this.checkBoxTournamentItem.Location = new System.Drawing.Point(100, 82);
            this.checkBoxTournamentItem.Name = "checkBoxTournamentItem";
            this.checkBoxTournamentItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxTournamentItem.Size = new System.Drawing.Size(112, 17);
            this.checkBoxTournamentItem.TabIndex = 31;
            this.checkBoxTournamentItem.Text = "Tournament Item?";
            this.checkBoxTournamentItem.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 244);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqWp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDmg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDmg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxWeaponTypes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown numWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numReqWp;
        private System.Windows.Forms.NumericUpDown numMaxDmg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMinDmg;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label labelTier;
        private System.Windows.Forms.ListBox listBoxWeapons;
        private System.Windows.Forms.CheckBox checkBoxTournamentItem;
    }
}

