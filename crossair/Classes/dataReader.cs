using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


namespace crossair {
	class DataReader {
		private String filePath = "data.json";

		public DataReader(string path = "") {
			if (path == "" || path == null) return;
			if (!File.Exists(path))
				throw (new System.IO.FileNotFoundException());
			else
				filePath = path;
		}

		public void Deserialize(Config toFill) {
			if (File.Exists(filePath)) {
				try {
					using (StreamReader fs = File.OpenText(filePath)) {
						JsonConvert.PopulateObject(fs.ReadToEnd(), toFill); //#yolo
					}
				} catch (Newtonsoft.Json.JsonSerializationException) {
					using (StreamWriter fs = new StreamWriter(filePath)) {
						fs.Write("{}");
					}

				}
			} else
				throw new FileNotFoundException();
		}

		public void Serialize(Config toFill) {
			if (!File.Exists(filePath)) File.CreateText(filePath);

			using (StreamWriter fs = new StreamWriter(filePath)) {
				fs.Write(JsonConvert.SerializeObject(toFill, Formatting.Indented));

			}
		}
	}
}
