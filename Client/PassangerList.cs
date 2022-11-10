using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClientCommunication;

namespace Client {
	public partial class PassangerList : UserControl {
		private ClientService service;
		private CustomerContext customer;
		private BookingStatus status;

		private Dictionary<int, PassangerDisplay> passangersDisplays;

		private BookingPassanger passanger;

		private DocumentFields generalDataFields;
		private DocumentFields documentFields;

		private sealed class FormPassanger {
			public string name, surname, middleName;
			public DateTime birthday;
			public int selectedDocument;
			public Dictionary<int, DocumentForm> documentsForms;
		}
		private FormPassanger formPassanger;

		private int? currentPassangerIndex {
			get { return passanger.passangerIndex; }
			set { passanger.passangerIndex = value; }
		}

		private enum State {
			none, select, add, edit
		}

		private State curState;
		private void setStateAdd() {
			setStatus(false, null);

			if(status.booked) throw new InvalidOperationException();

			curState = State.add;
			
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			addButton.Enabled = false;
			passangerDataTable.Enabled = true;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = null;

			clearPassangerData();

			saveButton.Visible = true;
			saveButton.Text = "Добавить";
		}
				
		private void setStateSelect(int newSelectedPassangerIndex) {
			setStatus(false, null);

			curState = State.select;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			addButton.Enabled = true;
			passangerDataTable.Enabled = false;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = newSelectedPassangerIndex;
			passangersDisplays[newSelectedPassangerIndex].BackColor = Misc.selectionColor3;
			
			setDataFromPassanger(customer.passangers[newSelectedPassangerIndex]);

			saveButton.Visible = false;
		}

		private void setStateEdit() {
			setStatus(false, null);

			if(status.booked) throw new InvalidOperationException();

			curState = State.edit;

			deleteButton.Enabled = true;
			editButton.Enabled = false;
			addButton.Enabled = true;
			passangerDataTable.Enabled = true;

			saveButton.Visible = true;

			var passanger = customer.passangers[(int) currentPassangerIndex];
			if(passanger.archived) saveButton.Text = "Сохранить копию";
			else saveButton.Text = "Сохранить";
		}

		public void setStateNone() {
			setStatus(false, null);

			curState = State.none;

			addButton.Enabled = true;
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = false;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = null;

			clearPassangerData();

			saveButton.Visible = false;
		}

		public PassangerList() {
			InitializeComponent();

			generalDataFields = new DocumentFields(() => this.ActiveControl = null, setStatus, generalDataTooltip, generalDataPanel, startFieldIndex: 0);
			documentFields = new DocumentFields(() => this.ActiveControl = null, setStatus, documentFieldsTooltip, documentTable, startFieldIndex: 1);
		}

		public void selectNone() {
			if(!promptSaveCustomer()) return;
			setStateNone();
		}

		public void init(ClientService sq, CustomerContext customer, BookingStatus status, BookingPassanger passanger) {
			this.service = sq;
			this.status = status;
			this.customer = customer;
			this.passanger = passanger;
			this.passangersDisplays = new Dictionary<int, PassangerDisplay>();

			ignore__ = true;
			documentTypeCombobox.DataSource = new BindingSource{ DataSource = Documents.DocumentsName.documentsNames };
			documentTypeCombobox.DisplayMember = "Value";
			ignore__ = false;

			setupPassangersDisplay();

			if(status.booked) updateStatusBooked();

			if(passanger.passangerIndex != null) setStateSelect((int) passanger.passangerIndex);
			else if(customer.passangers.Count != 0) setStateNone();
			else setStateAdd();
		}

		public void updateStatusBooked() {
			deleteButton.Visible = false;
			deleteButton.Enabled = false;

			editButton.Visible = false;
			editButton.Enabled = false;

			addButton.Visible = false;
			addButton.Enabled = false;

			saveButton.Enabled = false;
			saveButton.Visible = false;

			passangerMenu.Enabled = false;

			passangersDisplay.Enabled = false;
			passangerDataTable.Enabled = false;
		}

		public bool onClose() {
			return promptSaveCustomer();
		}

		private void setupPassangersDisplay() {
			passangersDisplay.SuspendLayout();
			passangersDisplay.Controls.Clear();
			passangersDisplay.RowStyles.Clear();

			passangersDisplay.RowCount = customer.passangers.Count;

			foreach(var passanger in customer.passangers) {
				var display = addPassangerDisplay(passanger.Value, passanger.Key);
				passangersDisplays.Add(passanger.Key, display);
			}

			passangersDisplay.ResumeLayout(false);
			passangersDisplay.PerformLayout();
		}

		private void promptFillCustomer(string msg = null) {
			var text = "Данные о пользователе должны быть заполнены корректно";
			statusLabel.Text = text;
			statusTooltip.SetToolTip(statusLabel, text);
		
			var lrf = new LoginRegisterForm(service, customer);
		
			lrf.ShowDialog();
			if(msg != null) lrf.setError(msg);
		
			if(lrf.DialogResult == DialogResult.OK) {
				statusLabel.Text = null;
				statusTooltip.SetToolTip(statusLabel, null);
			}
		}

		private void setStatus(bool error, string msg) {
			statusLabel.Text = error ? msg : null;
			statusLabel.ForeColor = error ? Color.Firebrick : SystemColors.ControlText;
			statusTooltip.SetToolTip(statusLabel, error ? msg : null);
		}

		//returns false if save aborted
		private bool promptSaveCustomer() {
			if(curState == State.edit) {
				var prevPassanger = customer.passangers[(int) currentPassangerIndex];
				var curPassangerRes = formPassangerFromData();

				if(curPassangerRes.IsSuccess) {
					if(prevPassanger.Equals(curPassangerRes.s)) return true;

					this.Focus();
					var mb = MessageBox.Show(
						"Данные пассажира были изменены. Хотите сохранить изменения?",
						"",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3
					);

					if(mb == DialogResult.Yes) {
						if(!curPassangerRes.IsSuccess) {
							setStatus(true, curPassangerRes.f);
							return false;
						}
						var result = saveEditedPassanger(curPassangerRes.s, (int) currentPassangerIndex);
						return result != null;
					}
					else if(mb == DialogResult.No) return true;
					else {
						Common.Debug2.AssertPersistent(mb == DialogResult.Cancel);
						return false;
					}
				} 
				else return false;
			}
			else if(curState == State.add) {
				this.Focus();
				var curPassangerRes = formPassangerFromData();
				var mb = MessageBox.Show(
					"Сохранить данные нового пассажира?",
					"",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3
				);

				if(mb == DialogResult.Yes) {
					if(!curPassangerRes.IsSuccess) {
						setStatus(true, curPassangerRes.f);
						return false;
					}

					var result = saveNewPassanger(curPassangerRes.s);
					if(result != null) setStateSelect((int) result);
					return result != null;
				}
				else if(mb == DialogResult.No) return true;
				else {
					Common.Debug2.AssertPersistent(mb == DialogResult.Cancel);
					return false;
				}
			}
			else return true;
		}

		private PassangerDisplay addPassangerDisplay(Passanger passanger, int index) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Data = index,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => {
				if(!promptSaveCustomer()) return;
				setStateSelect((int) ((PassangerDisplay) a).Data);
			};
			it.ContextMenuStrip = passangerMenu;

			passangersDisplay.RowCount++;
			passangersDisplay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			passangersDisplay.Controls.Add(it);

			return it;
		}

		private void saveButton_Click(object sender, EventArgs eventArgs) {
			save();
		}

		private void save() {
			if(status.booked) throw new InvalidOperationException();

			var passangerRes = formPassangerFromData();
			if(!passangerRes.IsSuccess) {
				setStatus(true, passangerRes.f);
				return;
			}
			var passanger = passangerRes.s;

			if(curState == State.add) {
				var result = saveNewPassanger(passanger);
				if(result != null) setStateSelect((int) result); 
			}
			else if(curState == State.edit) {
				var oldPassanger = customer.passangers[(int) currentPassangerIndex];
				if(!oldPassanger.Equals(passanger)) {
					var result = saveEditedPassanger(passanger, (int) currentPassangerIndex);
					if(result != null) setStateSelect((int) result);
				}
				else setStateSelect((int) currentPassangerIndex);
			}
			else Common.Debug2.AssertPersistent(curState == State.select || curState == State.none);
		}

		private Either<Passanger, string> formPassangerFromData() { 
			var it = new Passanger{
				name = formPassanger.name,
				surname = formPassanger.surname,
				middleName = formPassanger.middleName,
				birthday = formPassanger.birthday
			};

			var es = Validation.ErrorString.Create();

			if((it.name?.Length ?? 0) == 0 || (it.surname?.Length ?? 0) == 0) {
				es.ac("ФИО должно быть заполнено");
			}

			var document = formPassanger.documentsForms[formPassanger.selectedDocument].toDocument();
			if(document.IsSuccess) {
				var docRes = document.s.validate();
				if(docRes.Error) es.ac("Данные документа должны быть заполнены: ").append(docRes.Message);
				else it.document = document.s;
			}
			else es.ac("Данные документа должны быть заполнены: ").append(document.f.Message);

			if(es.Error) return Either<Passanger, string>.Failure(es.Message);
			else return Either<Passanger, string>.Success(it);
		}

		private void setDataFromPassanger(Passanger p) {

			DocumentForm form;
			if(p.document.Id == Documents.Passport.id) {
				form = PassportForm.fromDocument((Documents.Passport) p.document);
			}
			else if(p.document.Id == Documents.InternationalPassport.id) {
				form = InternationalPassportForm.fromDocument((Documents.InternationalPassport) p.document);
			}
			else throw new InvalidOperationException();

			formPassanger = new FormPassanger();
			formPassanger.name = p.name;
			formPassanger.surname = p.surname;
			formPassanger.middleName = p.middleName;
			formPassanger.birthday = p.birthday;
			formPassanger.documentsForms = new Dictionary<int, DocumentForm>();
			formPassanger.selectedDocument = p.document.Id;
			formPassanger.documentsForms.Clear();
			formPassanger.documentsForms.Add(formPassanger.selectedDocument, form);

			updatePassanger();
		} 

		private void clearPassangerData() {
			formPassanger = new FormPassanger();
			formPassanger.birthday = DateTime.Now;
			formPassanger.documentsForms = new Dictionary<int, DocumentForm>();

			formPassanger.selectedDocument = Documents.Passport.id;
			formPassanger.documentsForms.Clear();
			formPassanger.documentsForms.Add(Documents.Passport.id, new PassportForm());

			updatePassanger();
		}

		private void updatePassanger() {
			generalDataFields.Suspend();

			generalDataFields.clear();

			generalDataFields.addField();
			generalDataFields.fieldName("Фамилия:*");
			generalDataFields.textField(formPassanger.surname, text => formPassanger.surname = text);

			generalDataFields.addField();
			generalDataFields.fieldName("Имя:*");
			generalDataFields.textField(formPassanger.name, text => formPassanger.name = text);
			
			generalDataFields.addField();
			generalDataFields.fieldName("Отчество:");
			generalDataFields.textField(formPassanger.middleName, text => formPassanger.middleName = text);

			generalDataFields.addField();
			generalDataFields.fieldName("Дата рождения:*");
			generalDataFields.dateField(formPassanger.birthday, date => formPassanger.birthday = date).Format = DateTimePickerFormat.Short;

			generalDataFields.Resume();


			var i = 0;
			foreach(var doc in Documents.DocumentsName.documentsNames) {
				if(doc.Key == formPassanger.selectedDocument) break;
				i++;
			}
			Common.Debug2.AssertPersistent(i != Documents.DocumentsName.documentsNames.Count);
			documentTypeCombobox.SelectedIndex = i;

			updateDocument(formPassanger.selectedDocument);

		}

		private void setErr(Exception e) { 
			statusLabel.Text = "Неизвестная ошибка";
			statusTooltip.SetToolTip(statusLabel, e.ToString());
		}

		private void setFine() {
			statusLabel.Text = "";
			statusTooltip.SetToolTip(statusLabel, null);
		}

		private int? saveNewPassanger(Passanger passanger) {
			if(status.booked) throw new InvalidOperationException();

			try {

			if(customer.LoggedIn) {
				var response = service.addPassanger(customer.Get(), passanger);
				if(response) { 
					var id = response.s;
					var index = customer.newPassangerIndex++;

					customer.passangers.Add(index, passanger);
					customer.passangerIds.Add(index, new PassangerIdData(id));

					var display = addPassangerDisplay(passanger, index);
					passangersDisplays.Add(index, display);
				
					setFine();
				
					return index;
				}
				else if(response.f.isLoginError) {
					promptFillCustomer(response.f.LoginError.message);
				}
				else {
					var msg = response.f.InputError.message;
					statusLabel.Text = msg;
					statusTooltip.SetToolTip(statusLabel, msg);
				}
			}
			else {
				var index = customer.newPassangerIndex++;

				customer.passangers.Add(index, passanger);
				customer.passangerIds.Add(index, new PassangerIdData(null));

				var display = addPassangerDisplay(passanger, index);
				passangersDisplays.Add(index, display);
				
				setFine();
				
				return index;
			}

			}catch(Exception e) { setErr(e); }

			return null;
		}

		private int? saveEditedPassanger(Passanger passanger, int index) {
			if(status.booked) throw new InvalidOperationException();

			try {
			var passangerIdData = customer.passangerIds[index];

			if(passangerIdData.IsLocal) {
				customer.passangers[index] = passanger;

				var passangerDisplay = passangersDisplays[index];
				passangerDisplay.Passanger = passanger;
				passangerDisplay.Data = index;

				return index;
			}
			else {
				if(!customer.LoggedIn) throw new InvalidOperationException(
					"Незарегестрированный пользователь не может менять данные пассажиров, зарегестрированных в базе"
				);
				//note: this is a potential bug
				//because button text is not updated
				//and in states that the copy will be saved
				//but actually the passanger itself is saved
				//and not ist copy if there is some error
				//in updating button text
				if(customer.passangers[index].archived) return saveNewPassanger(passanger);

				var response = service.replacePassanger(customer.Get(), passangerIdData.DatabaseId, passanger);
				if(response) { 
					var id = response.s;

					customer.passangers[index] = passanger;
					customer.passangerIds[index] = new PassangerIdData(id);

					var passangerDisplay = passangersDisplays[index];
					passangerDisplay.Passanger = passanger;
					passangerDisplay.Data = index;
					
					setFine();

					return index;
				}
				else if(response.f.isLoginError) {
					promptFillCustomer(response.f.LoginError.message);
				}
				else {
					var msg = response.f.InputError.message;
					statusLabel.Text = msg;
					statusTooltip.SetToolTip(statusLabel, msg);
				}
			}

			} catch(Exception e) { setErr(e); }
			return null;
		}

		private bool removePassanger(int index) {
			if(status.booked) throw new InvalidOperationException();

			try{
			var passangerIdData = customer.passangerIds[index];
			
			if(passangerIdData.IsLocal) {
				if(index == currentPassangerIndex) setStateNone();

				passangersDisplays[index].Dispose();
				passangersDisplays.Remove(index);

				customer.passangers.Remove(index);
				customer.passangerIds.Remove(index);
				return true;
			}
			else {
				if(!customer.LoggedIn) throw new InvalidOperationException(
					"Незарегестрированный пользователь не может удалять пассажиров, зарегестрированных в базе"
				);

				var response = service.removePassanger(customer.Get(), passangerIdData.DatabaseId);
				if(response) { 
					if(index == currentPassangerIndex) setStateNone();

					customer.passangers.Remove(index);
					customer.passangerIds.Remove(index);

					passangersDisplays[index].Dispose();
					passangersDisplays.Remove(index);

					setFine();

					return true;
				}
				else {
					if(response.f.isLoginError) promptFillCustomer(response.f.LoginError.message);
					else {
						var msg = response.f.InputError.message;
						statusLabel.Text = msg;
						statusTooltip.SetToolTip(statusLabel, msg);
					}
				}
			}
			} catch(Exception e) { setErr(e); }

			return false;
		}

		private void editButton_Click(object sender, EventArgs e) {
			setStatus(false, null);
			Common.Debug2.AssertPersistent(curState == State.select || curState == State.none);
			setStateEdit();
		}

		private void addButton_Click(object sender, EventArgs e) {
			setStatus(false, null);
			Common.Debug2.AssertPersistent(curState == State.edit || curState == State.select || curState == State.none);
			promptSaveCustomer();
			setStateAdd();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			setStatus(false, null);
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger((int) currentPassangerIndex);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (int) pass.Data;
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger(number);
		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (int) pass.Data;
			if(!promptSaveCustomer()) return;
			setStateSelect(number);
			setStateEdit();
		}

		private class DocumentFields {
			private int startFieldIndex;

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

			public DocumentFields(
				RemoveFocus removeFocus,
				SetStatus setStatus,
				ToolTip documentFieldsTooltip,
				TableLayoutPanel panel,
				int startFieldIndex
			) {
				this.removeFocus = removeFocus;
				this.fieldIndex = (this.startFieldIndex = startFieldIndex) - 1;
				this.fieldUpdates = new List<Action>();
				this.setStatus = setStatus;
				this.documentFieldsTooltip = documentFieldsTooltip;
				this.panel = panel;
				addedControls = new List<Control>();
			}

			public void addField() {
				fieldIndex++;
				if(fieldIndex != 0 && fieldIndex % 3 == 0) {
					panel.RowCount += 3;
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

				panel.Controls.Add(it, fieldIndex%3, fieldIndex/3 * 3 + 1);
				addedControls.Add(it);
				return it;
			}

			public TextBox textField(string defaultValue, ValidateText validate) {
				var it = new TextBox();
				it.Dock = DockStyle.Fill;
				it.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
				it.Text = defaultValue;

				it.KeyDown += (a, b) => { validateTextField(it, validate);  if(b.KeyCode == Keys.Enter) { removeFocus(); } };
				it.LostFocus += (a, b) => { validateTextField(it, validate); };
				fieldUpdates.Add(() => { validateTextField(it, validate); });

				addedControls.Add(it);
				panel.Controls.Add(it, fieldIndex%3, fieldIndex/3 * 3 + 2);
				return it;
			}

			private void validateTextField(TextBox it, ValidateText validate) {
				try{ 
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
				panel.Controls.Add(it, fieldIndex%3, fieldIndex/3 * 3 + 2);
				return it;
			}

			private void validateDateField(DateTimePicker it, ValidateDate validate) {
				try{ 
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

			public void clear() {
				this.fieldIndex = this.startFieldIndex - 1;
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

		
		private bool ignore__;
		private void documentTypeCombobox_SelectedIndexChanged(object sender, EventArgs e) {
			if(ignore__) return;
			updateDocument(((KeyValuePair<int, string>) documentTypeCombobox.SelectedItem).Key);
		}

		private void updateDocument(int documentId) {
			DocumentForm document;
			formPassanger.selectedDocument = documentId;
			if(!formPassanger.documentsForms.TryGetValue(documentId, out document)) {
				if(documentId == Documents.Passport.id) { 
					document = new PassportForm();
				}
				else if(documentId == Documents.InternationalPassport.id) {
					document = new InternationalPassportForm();
				}

				Common.Debug2.AssertPersistent(document != null);
				formPassanger.documentsForms.Add(documentId, document);
			}

			documentFields.Suspend();
			documentFields.clear();

			if(documentId == Documents.Passport.id) {
				var passport = (PassportForm) document;

				documentFields.addField();
				documentFields.fieldName("Номер:*");
				documentFields.textField(passport.Number?.ToString(), text => {
					try { 
						long res;
						var success = long.TryParse(text, out res);
						if(!success) throw new Documents.IncorrectValue("Номер паспорта дожлен включать только цифры");
						Documents.PassportValidation.checkNumber(res)
							.exception((msg) => new Documents.IncorrectValue(msg));
						passport.Number = res;
					}
					catch(Exception e) {
						passport.Number = null;
						throw e;
					}
				});
			}
			else if(documentId == Documents.InternationalPassport.id) {
				var passport = (InternationalPassportForm) document;

				documentFields.addField();
				documentFields.fieldName("Номер:*");
				documentFields.textField(passport.Number?.ToString(), text => {
					try {
						int res;
						var success = int.TryParse(text, out res);
						if(!success) throw new Documents.IncorrectValue("Номер заграничного паспорта дожлен включать только цифры");
						Documents.InternationalPassportValidation.checkNumber(res)
							.exception((msg) => new Documents.IncorrectValue(msg));
						passport.Number = res;
					}
					catch(Exception e) {
						passport.Number = null;
						throw e;
					}
				});

				documentFields.addField();
				documentFields.fieldName("Дата окончания срока действия:*");
				var exDate = passport.ExpirationDate ?? DateTime.Now;
				documentFields.dateField(exDate, date => {
					var res = Documents.InternationalPassportValidation.checkExpirationDate(date);
					if(res.Error) {
						passport.ExpirationDate = null;
						throw new Documents.IncorrectValue(res.Message);
					}
					else passport.ExpirationDate = date;
				})
					.Format = DateTimePickerFormat.Short;
				passport.ExpirationDate = exDate;

				documentFields.addField();
				documentFields.fieldName("Фамилия (на латинице):*");
				documentFields.textField(passport.Surname, text => {
					var res = Documents.InternationalPassportValidation.checkSurname(text);
					if(res.Error) {
						passport.Surname = null;
						throw new Documents.IncorrectValue(res.Message);
					}
					else passport.Surname = text;
				});

				documentFields.addField();
				documentFields.fieldName("Имя (на латинице):*");
				documentFields.textField(passport.Name, text => {
					var res = Documents.InternationalPassportValidation.checkName(text);
					if(res.Error) {
						passport.Name = null;
						throw new Documents.IncorrectValue(res.Message);
					}
					else passport.Name = text;
				});

				documentFields.addField();
				documentFields.fieldName("Отчество (на латинице):");
				documentFields.textField(passport.MiddleName, text => {
					var res = Documents.InternationalPassportValidation.checkMiddleName(text);
					if(res.Error) {
						passport.MiddleName = null;
						throw new Documents.IncorrectValue(res.Message);
					}
					else passport.MiddleName = text;
				});
			}

			documentFields.Resume();
		}
	}
}
