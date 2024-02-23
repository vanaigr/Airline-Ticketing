using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
    public partial class BookedFlightByPNRForm : Form {
        private Context context;
        private ClientCommunication.ClientService service;

        public BookedFlightByPNRForm(
            Context context,
            ClientCommunication.ClientService service
        ) {
            this.context = context;
            this.service = service;

            InitializeComponent();

            Common.Misc.unfocusOnEscape(this);
        }

        private void ContinueButton_Click(object sender, EventArgs ea) {
            try {
                var result = service.getBookedFlightFromSurnameAndPNR(surnameTextBox.Text, pnrTextBox.Text);

                if(result) {
                    var flight = result.s;

                    var cc = new CustomerContext();
                    cc.customer = null;

                    var bookedFlightIndex = cc.newBookedFlightIndex++;
                    cc.bookedFlightsDetails.Add(bookedFlightIndex, flight.details);
                    cc.flightsBooked.Add(bookedFlightIndex, flight.flight);

                    var passangerIndex = cc.newPassangerIndex++;
                    cc.passangers.Add(passangerIndex, flight.passanger);
                    cc.passangerIds.Add(passangerIndex, new PassangerIdData(flight.passangerId));

                    var bookingStatus = new BookingStatus{ booked = true, bookedFlightIndex = bookedFlightIndex };

                    setFine();
                    Refresh();

                    var form = new FlightDetailsFill(service, cc, context, bookingStatus, null, null);
                    form.Show();
                }
                else {
                    setError(result.f.message, null);
                }
            }
            catch(Exception e) {
                setError(null, e);
            }
        }

        private void setError(string error, Exception e) {
            statusLabel.Text = error ?? "Неизвестная ошибка";
            statusToolTip.SetToolTip(statusLabel, error ?? e.ToString());
        }

        private void setFine() {
            statusLabel.Text = null;
            statusToolTip.SetToolTip(statusLabel, null);
        }
    }
}
