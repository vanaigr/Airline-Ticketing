using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common {
	public class FieldsDisplay {
		private static readonly int tableRowsPerRow = 3;

		private int rowOffset;
		private int startFieldIndex;
		private int colsCount;

		private int fieldIndex;

		private TableLayoutPanel panel;
		private SetStatus setStatus;
		private ToolTip documentFieldsTooltip;
		private RemoveFocus removeFocus;

		private List<Control> addedControls;
		private List<Action> fieldUpdates;

		public delegate void RemoveFocus();
		public delegate void SetStatus(bool err, string input);

		public delegate void ValidateText(string input);
		public delegate void ValidateDate(DateTime input);
		public delegate void ValidateCombo(int selectedIndex);

		public FieldsDisplay(
			RemoveFocus removeFocus,
			SetStatus setStatus,
			ToolTip documentFieldsTooltip,
			TableLayoutPanel panel,
			int rowOffset,
			int startFieldIndex,
			int colsCount
		) {
			this.rowOffset = rowOffset;
			this.startFieldIndex = startFieldIndex;
			this.colsCount = colsCount;

			this.removeFocus = removeFocus;
			fieldIndex = startFieldIndex - 1;
			fieldUpdates = new List<Action>();
			this.setStatus = setStatus;
			this.documentFieldsTooltip = documentFieldsTooltip;
			this.panel = panel;
			addedControls = new List<Control>();
		}

		public void addField() {
			fieldIndex++;
			if((fieldIndex % colsCount + colsCount) % colsCount == 0) {
				panel.RowCount += tableRowsPerRow;
				panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
				panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			}
		}

		public Label fieldName(string text) {
			var it = new Label();

			it.Dock = DockStyle.Fill;
			it.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			it.Text = text;
			it.TextAlign = ContentAlignment.BottomLeft;
			it.AutoSize = true;
			documentFieldsTooltip.SetToolTip(it, text);

			panel.Controls.Add(it, fieldIndex%colsCount, rowOffset + fieldIndex/colsCount * tableRowsPerRow + 1);
			addedControls.Add(it);
			return it;
		}

		public TextBox textField(string defaultValue, ValidateText validate) {
			var it = new TextBox();
			it.Dock = DockStyle.Fill;
			it.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			it.Text = defaultValue;

			it.KeyDown += (a, b) => { validateTextField(it, validate); if(b.KeyCode == Keys.Enter) { removeFocus(); } };
			it.LostFocus += (a, b) => { validateTextField(it, validate); };
			fieldUpdates.Add(() => { validateTextField(it, validate); });

			addedControls.Add(it);
			panel.Controls.Add(it, fieldIndex % colsCount, rowOffset + fieldIndex/colsCount * tableRowsPerRow + 2);
			return it;
		}

		private void validateTextField(TextBox it, ValidateText validate) {
			try {
				validate(it.Text);
				it.ForeColor = SystemColors.ControlText;
				documentFieldsTooltip.SetToolTip(it, null);
				setStatus(false, null);
			}
			catch(Documents.IncorrectValue iv) {
				it.ForeColor = Color.Firebrick;
				documentFieldsTooltip.SetToolTip(it, iv.Message);
				setStatus(true, iv.Message);
			}
		}

		public DateTimePicker dateField(DateTime defaultValue, ValidateDate validate) {
			var it = new DateTimePicker();
			it.Dock = DockStyle.Fill;
			it.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			it.Value = defaultValue;

			it.KeyDown += (a, b) => { if(b.KeyCode == Keys.Enter) { validateDateField(it, validate); removeFocus(); } };
			it.LostFocus += (a, b) => { validateDateField(it, validate); };
			fieldUpdates.Add(() => { validateDateField(it, validate); });

			addedControls.Add(it);
			panel.Controls.Add(it, fieldIndex % colsCount, rowOffset + fieldIndex/colsCount * tableRowsPerRow + 2);
			return it;
		}

		private void validateDateField(DateTimePicker it, ValidateDate validate) {
			try {
				validate(it.Value);
				it.ForeColor = SystemColors.ControlText;
				documentFieldsTooltip.SetToolTip(it, null);
				setStatus(false, null);
			}
			catch(Documents.IncorrectValue iv) {
				it.ForeColor = Color.Firebrick;
				documentFieldsTooltip.SetToolTip(it, iv.Message);
				setStatus(true, iv.Message);
			}
		}

		public ComboBox comboField(object dataSource, int defaultValue, ValidateCombo validate) {
			var it = new ComboBox();
			it.Dock = DockStyle.Fill;
			it.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			it.DataSource = new BindingSource{ DataSource = dataSource };
			it.SelectedIndex = defaultValue;

			it.KeyDown += (a, b) => { validateComboField(it, validate); if(b.KeyCode == Keys.Enter) { removeFocus(); } };
			it.LostFocus += (a, b) => { validateComboField(it, validate); };
			fieldUpdates.Add(() => { validateComboField(it, validate); });

			addedControls.Add(it);
			panel.Controls.Add(it, fieldIndex % colsCount, rowOffset + fieldIndex/colsCount * tableRowsPerRow + 2);
			return it;
		}


		private void validateComboField(ComboBox it, ValidateCombo validate) {
			try {
				validate(it.SelectedIndex);
				it.ForeColor = SystemColors.ControlText;
				documentFieldsTooltip.SetToolTip(it, null);
				setStatus(false, null);
			}
			catch(Documents.IncorrectValue iv) {
				it.ForeColor = Color.Firebrick;
				documentFieldsTooltip.SetToolTip(it, iv.Message);
				setStatus(true, iv.Message);
			}
		}
		public void clear() {
			fieldIndex = startFieldIndex - 1;
			documentFieldsTooltip.RemoveAll();
			foreach(var it in addedControls) it.Dispose();
			addedControls.Clear();
			fieldUpdates.Clear();
			panel.RowCount = 2;
		}

		public void Suspend() {
			panel.SuspendLayout();
		}

		public void Resume() {
			panel.ResumeLayout(false);
			panel.PerformLayout();
		}

		public void forceUpdateAllFields() {
			foreach(var update in fieldUpdates) {
				update.Invoke();
			}
		}
	}
}
