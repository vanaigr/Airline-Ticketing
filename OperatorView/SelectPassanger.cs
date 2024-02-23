using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operator {
    public partial class SelectPassanger : Form {
        private Context context;
        public SelectPassanger(Context context) {
            this.context = context;

            InitializeComponent();

            Common.Misc.unfocusOnEscape(this);
        }

        private void findButton_Click(object sender, EventArgs ea) {
            try {
                var result = context.service.getPassangerBookedFlights(new OperatorCommunication.PassangerSearchParams{
                    name = nameTB.Text.emptyToNull(),
                    surname = surnameTB.Text.emptyToNull(),
                    middleName = noMiddleName.Checked ? "" : middleNameTB.Text.emptyToNull(),
                    pnr = pnrTB.Text.emptyToNull()
                });
                if(result) {
                    var flights = result.s;
                    statusLabel.Text = "";
                    statusTooltip.SetToolTip(statusLabel, null);
                    Refresh();
                    new PassangersList(context, flights).Show();
                }
                else {
                    statusLabel.Text = result.f.message;
                    statusTooltip.SetToolTip(statusLabel, result.f.message);
                }
            }
            catch(Exception e) {
                statusLabel.Text = "Неизвестная ошибка";
                statusTooltip.SetToolTip(statusLabel, e.ToString());
            }
        }

        private void noMiddleName_CheckedChanged(object sender, EventArgs e) {
            middleNameTB.Enabled = !noMiddleName.Checked;
        }
    }

    public static class StringHelper {
        public static string emptyToNull(this string it) {
            return it == "" ? null : it;
        }
    }
}
