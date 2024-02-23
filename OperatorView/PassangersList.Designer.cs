
namespace Operator {
    partial class PassangersList {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.passangerGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.onlyAvailableCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.passangerGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            //
            // passangerGridView
            //
            this.passangerGridView.AllowUserToAddRows = false;
            this.passangerGridView.AllowUserToDeleteRows = false;
            this.passangerGridView.AllowUserToResizeColumns = false;
            this.passangerGridView.AllowUserToResizeRows = false;
            this.passangerGridView.BackgroundColor = System.Drawing.Color.White;
            this.passangerGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passangerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.passangerGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passangerGridView.GridColor = System.Drawing.Color.White;
            this.passangerGridView.Location = new System.Drawing.Point(0, 23);
            this.passangerGridView.Margin = new System.Windows.Forms.Padding(0);
            this.passangerGridView.Name = "passangerGridView";
            this.passangerGridView.ReadOnly = true;
            this.passangerGridView.Size = new System.Drawing.Size(800, 427);
            this.passangerGridView.TabIndex = 0;
            this.passangerGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.passangerGridView_CellDoubleClick);
            this.passangerGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.passangerGridView_CellFormatting);
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.passangerGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.onlyAvailableCB, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(800, 23);
            this.tableLayoutPanel2.TabIndex = 1;
            //
            // onlyAvailableCB
            //
            this.onlyAvailableCB.AutoSize = true;
            this.onlyAvailableCB.Location = new System.Drawing.Point(3, 3);
            this.onlyAvailableCB.Name = "onlyAvailableCB";
            this.onlyAvailableCB.Size = new System.Drawing.Size(98, 17);
            this.onlyAvailableCB.TabIndex = 0;
            this.onlyAvailableCB.Text = "Только новые";
            this.onlyAvailableCB.UseVisualStyleBackColor = true;
            this.onlyAvailableCB.CheckedChanged += new System.EventHandler(this.onlyAvailableCB_CheckedChanged);
            //
            // PassangersList
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PassangersList";
            this.Text = "Список мест";
            ((System.ComponentModel.ISupportInitialize)(this.passangerGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView passangerGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox onlyAvailableCB;
    }
}
