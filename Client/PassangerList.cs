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

        private FieldsDisplay generalDataFields;
        private FieldsDisplay documentFields;

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

            if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
            currentPassangerIndex = newSelectedPassangerIndex;
            passangersDisplays[newSelectedPassangerIndex].BackColor = Misc.selectionColor3;
            var curPassanger = customer.passangers[newSelectedPassangerIndex];
            setDataFromPassanger(curPassanger);

            deleteButton.Enabled = !curPassanger.archived;
            editButton.Enabled = true;
            addButton.Enabled = true;
            passangerDataTable.Enabled = false;

            saveButton.Visible = false;
        }

        private void setStateEdit() {
            setStatus(false, null);

            if(status.booked) throw new InvalidOperationException();

            curState = State.edit;

            var passanger = customer.passangers[(int) currentPassangerIndex];

            deleteButton.Enabled = true;
            editButton.Enabled = false;
            addButton.Enabled = true;
            passangerDataTable.Enabled = true;

            saveButton.Visible = true;

            if(passanger.archived) saveButton.Text = "Сохранить копию";
            else saveButton.Text = "Сохранить";
        }

        public void setStateNone() {
            setStatus(false, null);

            curState = State.none;

            deleteButton.Enabled = false;
            editButton.Enabled = false;
            addButton.Enabled = true;
            passangerDataTable.Enabled = false;

            if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
            currentPassangerIndex = null;

            clearPassangerData();

            saveButton.Visible = false;
        }

        public PassangerList() {
            InitializeComponent();

            generalDataFields = new FieldsDisplay(
                () => this.ActiveControl = null, setStatus,
                generalDataTooltip, generalDataPanel,
                startFieldIndex: 0, colsCount: 3, rowOffset: 1
            );
            documentFields = new FieldsDisplay(
                () => this.ActiveControl = null, setStatus,
                documentFieldsTooltip, documentTable,
                startFieldIndex: 1, colsCount: 3, rowOffset: 0
            );
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
                documentFields.textField(passport.Number?.ToString(), passport.updateNumber);
            }
            else if(documentId == Documents.InternationalPassport.id) {
                var passport = (InternationalPassportForm) document;

                documentFields.addField();
                documentFields.fieldName("Номер:*");
                documentFields.textField(passport.Number?.ToString(), passport.updateNumber);

                documentFields.addField();
                documentFields.fieldName("Дата окончания срока действия:*");
                var exDate = passport.ExpirationDate ?? DateTime.Now;
                documentFields.dateField(exDate, passport.updateExpirationDate).Format = DateTimePickerFormat.Short;
                passport.ExpirationDate = exDate;

                documentFields.addField();
                documentFields.fieldName("Фамилия (на латинице):*");
                documentFields.textField(passport.Surname, passport.updateSurname);

                documentFields.addField();
                documentFields.fieldName("Имя (на латинице):*");
                documentFields.textField(passport.Name, passport.updateName);

                documentFields.addField();
                documentFields.fieldName("Отчество (на латинице):");
                documentFields.textField(passport.MiddleName, passport.updateMiddleName);
            }

            documentFields.Resume();
        }
    }
}
