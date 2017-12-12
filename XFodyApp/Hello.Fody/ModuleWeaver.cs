using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;

public class ModuleWeaver
{
    public ModuleDefinition ModuleDefinition { get; set; }

    private MethodInfo DebugWriteLine { get; } =
        typeof(System.Diagnostics.Debug)
            .GetTypeInfo()
            .DeclaredMethods
            .Where(x => x.Name == nameof(System.Diagnostics.Debug.WriteLine))
            .Single(x =>
            {
                var parameters = x.GetParameters();
                return parameters.Length == 1 &&
                       parameters[0].ParameterType == typeof(string);
            });

    public void Execute()
    {
        var methods = ModuleDefinition
            .Types.Where(x => x.Name.EndsWith("ViewModel"))
            .SelectMany(x => x.Methods);
        foreach (var method in methods)
        {
            var processor = method.Body.GetILProcessor();
            var current = method.Body.Instructions.First();

            processor.InsertBefore(current, Instruction.Create(OpCodes.Nop));
            processor.InsertBefore(current, Instruction.Create(OpCodes.Ldstr, $"DEBUG: {method.DeclaringType.Name}#{method.Name}()"));
            processor.InsertBefore(current, Instruction.Create(OpCodes.Call, ModuleDefinition.ImportReference(DebugWriteLine)));
        }
    }
}