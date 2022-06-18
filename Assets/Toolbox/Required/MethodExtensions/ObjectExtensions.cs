using System.Linq;

namespace Toolbox.Required
{
    public static class ObjectExtensions
    {
        public static bool ContainsConstructorWithTheseParams(this object aObject, object[] parameters)
        {
            var possibleConstructors = aObject.GetType().GetConstructors().Where((info) => info.GetParameters().Length == parameters.Length).ToArray();

            return possibleConstructors
                .Select(constructor => constructor.GetParameters())
                .Any(constructorParameters => constructorParameters.TakeWhile((parameterInfo, index) => parameterInfo.ParameterType == parameters[index].GetType()).Any());
        }

        public static bool HasEmptyConstructor(this object aObject)
        {
            return aObject.GetType().GetConstructors().Where(info => info.GetParameters().IsEmpty()).ToArray().IsEmpty();
        }
    }
}