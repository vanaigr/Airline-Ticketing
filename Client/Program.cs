using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace Client {
	static class Program {
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SelectFlight());
		}
	}
}
