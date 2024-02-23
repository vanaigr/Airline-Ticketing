using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operator {
    public partial class MenuForm : Form {
        private Context context;
        public MenuForm() {
            Exception ex = null;
            try {
                context = new Context();
                context.update();
            }
            catch(Exception e) { ex = e; }

            InitializeComponent();

            Common.Misc.unfocusOnEscape(this);

            if(ex != null) {
                statusLabel.Text = ex.Message;
                statusTooltip.SetToolTip(statusLabel, ex.ToString());
            }
        }

        private void findPassangerButton_Click(object sender, EventArgs e) {
            new SelectPassanger(context).Show();
        }

        private void findFlightsButton_Click(object sender, EventArgs e) {
            new SelectFlight(context).Show();
        }
    }
}
