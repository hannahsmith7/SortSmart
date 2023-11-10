using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace SortSmart.Models
{
        // Represents the model for the Dewey Decimal classification
        public class DeweyDecimalModel
        {
            public ClassificationNode Root { get; private set; }

            // Call this method to load data from a JSON file and build the tree
            public void LoadData(string filePath)
            {
                string json = File.ReadAllText(filePath);
                List<ClassificationNode> classifications = JsonConvert.DeserializeObject<List<ClassificationNode>>(json);
                Root = new ClassificationNode { Number = "Root", Description = "Dewey Decimal Classification", Children = classifications };
                // Additional tree-building logic if necessary
            }

        // (Optional) Add additional methods to traverse or search the tree if needed

        // Represents a node in the classification tree
        public class ClassificationNode
        {
            public string Number { get; set; }
            public string Description { get; set; }
            public List<ClassificationNode> Children { get; set; } = new List<ClassificationNode>();
        }
    }
    }
