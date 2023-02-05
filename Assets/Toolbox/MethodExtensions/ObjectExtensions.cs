using System.Linq;

namespace Toolbox.MethodExtensions
{
    public static class ObjectExtensions
    {
        
        /// <summary>
        /// Checks if the given object contains a constructor with the given parameters.
        /// </summary>
        /// <param name="aObject"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool ContainsConstructorWithTheseParams(this object aObject, object[] parameters)
        {
            var possibleConstructors = aObject.GetType().GetConstructors().Where((info) => info.GetParameters().Length == parameters.Length).ToArray();

            return possibleConstructors
                .Select(constructor => constructor.GetParameters())
                .Any(constructorParameters => constructorParameters.TakeWhile((parameterInfo, index) => parameterInfo.ParameterType == parameters[index].GetType()).Any());
        }

        /// <summary>
        /// Checks if the given object contains a empty constructor
        /// </summary>
        /// <param name="aObject"></param>
        /// <returns></returns>
        public static bool HasEmptyConstructor(this object aObject)
        {
            return aObject.GetType().GetConstructors().Where(info => info.GetParameters().IsEmpty()).ToArray().IsEmpty();
        }
    }
}