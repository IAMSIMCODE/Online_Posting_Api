using AutoMapper;
using System.Reflection;

namespace OnlinePosting.Application.Shared.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { ApplyMappingFromAssembly(Assembly.GetExecutingAssembly()); }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMappingProfile<>);

            var mappingMethodName = nameof(IMappingProfile<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var iface in interfaces)
                        {
                            var interfaceMethodInfo = iface.GetMethod(mappingMethodName, argumentTypes);
                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
