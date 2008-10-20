using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using System.Xml;
using System.ServiceModel.Dispatcher;
using BasicLibrary.ServiceModel;
using System.Collections;
using System.Reflection;

namespace SimpleLibrary.ServiceModel
{
    public class CustomDataContractSerializerOperationBehavior : DataContractSerializerOperationBehavior
    {
        public IList<Assembly> KnownAssemblies { get; set; }

        public CustomDataContractSerializerOperationBehavior(OperationDescription operationDescription, IList<Assembly> knownAssemblies) : base(operationDescription) 
        {
            this.KnownAssemblies = knownAssemblies;
        }

        public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
        {
            List<Type> types = new List<Type>();
            foreach (Assembly asm in KnownAssemblies)
            {
                types.AddRange(KnownTypesLister.Locate(asm));
            }
            if (knownTypes != null)
                types.AddRange(knownTypes);

            DataContractSerializer serializer = new DataContractSerializer(type, name, ns, types, 0xFFFFFF, false, true, null);
            return serializer;
        }

        public static void OverrideOperations(OperationDescriptionCollection collection, IList<Assembly> knownAssemblies)
        {
            foreach (OperationDescription operation in collection)
            {
                operation.Behaviors.Remove<DataContractSerializerOperationBehavior>();
                operation.Behaviors.Add(new CustomDataContractSerializerOperationBehavior(operation, knownAssemblies));
            }
        }
    }
}
