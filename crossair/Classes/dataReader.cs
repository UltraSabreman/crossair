using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


namespace crossair {
	class dataReader {
		public bool closeWithGame = true;
		public string gameWindowTitle = "Calculator";

		private string filePath = "data.json";

		public dataReader(string path = "") {
			if (path == "" || path == null) return;
			if (!File.Exists(path))
				throw (new System.IO.FileNotFoundException());
			else
				filePath = path;
		}

		public void deserialize() {
			if (File.Exists(filePath)) {
				try {
					StreamReader fs = File.OpenText(filePath);
					JsonConvert.PopulateObject(fs.ReadToEnd(), this); //#yolo
					fs.Close();
				} catch (Newtonsoft.Json.JsonSerializationException) {
					StreamWriter fs = new StreamWriter(filePath);
					fs.Write("{}");
					fs.Close();

				}

				
			}
		}

		public void serialize() {
			if (!File.Exists(filePath)) File.CreateText(filePath);

			StreamWriter fs = new StreamWriter(filePath);
			fs.Write(JsonConvert.SerializeObject(this, Formatting.Indented));

			fs.Close();
		}
	}
}
