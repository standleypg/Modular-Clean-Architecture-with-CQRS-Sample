using System.Reflection;
using System.Text.Json;
using TypeLitePlus;
using TypeLitePlus.TsModels;

namespace CodeGen;

public class TypeScriptGenerator
{
    private static TypeScriptGenConfig? _config;

    private static void LoadConfig()
    {
        string configPath = Path.Combine(AppContext.BaseDirectory, "typescriptgenconfig.json");
        if (File.Exists(configPath))
        {
            var json = File.ReadAllText(configPath);
            _config = JsonSerializer.Deserialize<TypeScriptGenConfig>(json);
            Console.WriteLine("Config loaded successfully.");
        }
        else
        {
            throw new FileNotFoundException("Config file not found.", configPath);
        }
    }

    public static void GenerateTypeScript()
    {
        LoadConfig();

        if (_config?.Namespaces == null || !_config.Namespaces.Any())
        {
            Console.WriteLine("No namespaces specified in config. Nothing to generate.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_config.OutputPath))
        {
            Console.WriteLine("Output path not specified in config.");
            return;
        }

        var sharedAssembly = Assembly.Load("RetailPortal.Shared");

        var modelBuilder = new TsModelBuilder();

        foreach (var nsConfig in _config.Namespaces)
        {
            var types = sharedAssembly.GetTypes()
                .Where(t => t.Namespace == nsConfig.Namespace && nsConfig.IncludeDtos.Contains(t.Name))
                .ToList();

            foreach (var type in types)
            {
                modelBuilder.Add(type);
            }
        }

        var model = modelBuilder.Build();
        // model.Modules = model.Modules.Where(x=>x.Name != "System");
        var visitor = new Visitor();
        model.RunVisitor(visitor);

        var generator = new TsGenerator();
        generator.SetIdentifierFormatter(property => !string.IsNullOrEmpty(property.Name)
            ? char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1) // Convert to camelCase
            : property.Name);
        generator.SetMemberTypeFormatter((formatter, name) =>
            name == "System.Guid" ? "string" : name
        );

        var tsCode = generator.Generate(model, TsGeneratorOutput.Properties | TsGeneratorOutput.Enums);

        var baseDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;

        if (baseDirectory == null)
        {
            throw new InvalidOperationException("Failed to determine base directory.");
        }
        var outputPath = Path.GetFullPath(_config.OutputPath, baseDirectory);
        var outputDir = Path.GetDirectoryName(outputPath);
        if (outputDir != null) Directory.CreateDirectory(outputDir);

        try
        {
            if (outputDir != null)
            {
                string outputFile = Path.Combine(outputDir, _config.OutputFileName);
                File.WriteAllText(outputFile, tsCode);
                Console.WriteLine($"TypeScript models generated at {_config.OutputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate TypeScript models: {ex.Message}");
        }
    }

    private void EnsureFileWritePermission(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Grant full control to the current user
            var fileInfo = new FileInfo(filePath);
            var accessControl = fileInfo.GetAccessControl();

            accessControl.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                Environment.UserName,
                System.Security.AccessControl.FileSystemRights.FullControl,
                System.Security.AccessControl.AccessControlType.Allow));

            fileInfo.SetAccessControl(accessControl);
        }
    }
}