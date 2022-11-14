using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client {
	public class Context {
		private string[] classesNames;
		private Dictionary<string, City> cities;

		public string[] ClassesNames{ get {
			if(classesNames == null) try{ 
				updateParameters();
			} catch(Exception e) { return null; }

			return classesNames;
		} }
		public Dictionary<string, City> Cities{ get{
			if(cities == null) try{ 
				updateParameters();
			} catch(Exception e) { return null; }
			
			return cities;
		} }

		public ClientCommunication.ClientService service;

		public Context(ClientCommunication.ClientService service) {
			this.service = service;
		}

		public void updateParameters() {
			var it = service.parameters();

			classesNames = it.flightClasses;
			cities = new Dictionary<string, City>(it.cities.Count);
			foreach(var city in it.cities) {
				cities.Add(city.code, city);
			}
		}
	}
}
