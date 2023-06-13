using System;
using System.Collections.Generic;
using UnityEngine;

using Dialogue.Conditions;
using Dialogue.DTO;
using Exceptions;

namespace Factory
{
    public class Factory<T> where T : class
    {
        public static Factory<T> Instance { get { return _lazy.Value; } }
        private static readonly Lazy<Factory<T>> _lazy = new Lazy<Factory<T>>(() => new Factory<T>());
        private Dictionary<string, string> _classesID = new Dictionary<string, string>();
        private readonly string _fileName = "Factory/classes_id";

        private Factory()
        {
            TextAsset json = Resources.Load<TextAsset>(_fileName);
            try
            {
                var classesIDDTO = JsonUtility.FromJson<ClassesIDDTO>(json.ToString());
                foreach(var classID in classesIDDTO.Classes)
                {
                    _classesID.Add(classID.ID, classID.ClassName);
                }
            }
            catch(System.Exception exception)
            {
                throw new FactoryException("Can't initialize factory: " + exception);
            }
        }

        public T GetObject(string conditionName, List<string> parameters)
        {
            if(!_classesID.ContainsKey(conditionName))
            {
                throw new FactoryException("Can't create " + conditionName + ": this id doesn't exist.");
            }
            var conditionType = _classesID[conditionName];
            var type = Type.GetType(conditionType);
            if(type is null)
            {
                throw new FactoryException("Can't create " + conditionType + ": this type doesn't exist.");
            }
            var constructors = type.GetConstructors();
            var condition = constructors[0].Invoke(parameters.ToArray()) as T;
            if(condition is null)
            {
                throw new FactoryException("Can't create " + conditionType + " with given parameters.");
            }
            return condition;
        }
    }
}
