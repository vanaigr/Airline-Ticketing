using Communication;
using OperatorCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operator {
	public class Context {
		public OperatorService service;

		public string[] classesNames;
		public Dictionary<string, City> cities;

		public void update() {
			(service as IDisposable)?.Dispose();
			service = OperatorServerQuery.Create();

			var options = service.parameters();
			var classesNames = options.flightClasses;
			var citiesList = options.cities;

			var cities = new Dictionary<string, City>(citiesList.Count);
			foreach(var city in citiesList) {
				cities.Add(city.code, city);
			}
			 
			this.classesNames = classesNames;
			this.cities = cities;
		}
	}
}
