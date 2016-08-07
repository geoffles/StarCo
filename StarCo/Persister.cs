using StarCo.Domain;
using StarCo.Domain.Improvements;
using StarCo.Domain.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo
{
    public class Persister
    {
        public void Save(Colony colony)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Colony), new List<Type>
            {
                typeof(BasicMine),
                typeof(BasicWorker),

            });

            using (var stream = new FileStream("Save.xml", FileMode.Create))
            {
                serializer.WriteObject(stream, colony);
            }
        }

        public Colony Load(string filename)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Colony), new List<Type>
            {
                typeof(BasicMine),
                typeof(BasicWorker),

            });

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var result = serializer.ReadObject(stream) as Colony;
                return result;
            }
        }
        
    }
}
