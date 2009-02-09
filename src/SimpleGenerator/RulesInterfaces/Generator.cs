using System.Collections.Generic;
using System.Text;
using System.IO;
using SimpleGenerator.Definitions;
using System.Text.RegularExpressions;
using CSharpParser.ProjectModel;

namespace SimpleGenerator.RulesInterfaces
{
    public class Generator
    {
        public static Regex BaseRulesRegex = new Regex(@"BaseRules\<(?<domainClass>[\w\<\>]+)\>");
        public static string IBaseRulesPattern = "IBaseRules<%s>";
        public static string InterfacePattern = "I%s";

        public void Generate(GeneratorConfig config, string baseDir)
        {

            Dictionary<string, InterfaceCandidate> candidates = new Dictionary<string, InterfaceCandidate>();

            CompilationUnit domain = new CompilationUnit(Path.Combine(baseDir, config.DomainDirectory), true);
            domain.AddAssemblyReference("SimpleLibraryCore");
            domain.Parse();

            CompilationUnit project = new CompilationUnit(Path.Combine(baseDir, config.RulesDirectory), true);
            project.AddAssemblyReference("SimpleLibraryServer");
            project.AddProjectReference(domain, "domain");
            project.Parse();

            #region Calculation
            
            foreach (SourceFile source in project.Files)
            {
                foreach (NamespaceFragment ns in source.NestedNamespaces) 
                {
                    foreach (TypeDeclaration cls in ns.TypeDeclarations)
                    {
                        InterfaceCandidate current = null;
                        if (!candidates.ContainsKey(cls.Name))
                        {
                            current = candidates[cls.Name] = new InterfaceCandidate();
                            current.ClassName = InterfacePattern.Replace("%s", cls.Name);
                        }
                        else
                        {
                            current = candidates[cls.Name];
                        }

                        if (cls.BaseType != null)
                        {
                            Match m = BaseRulesRegex.Match(cls.BaseType.ParametrizedName);
                            if (m.Success)
                            {
                                string ruleName = IBaseRulesPattern.Replace("%s", m.Groups["domainClass"].Value);
                                if (!current.Inherits.Contains(ruleName))
                                    current.Inherits.Add(ruleName);
                            }
                        }

                        foreach (MethodDeclaration method in cls.Methods)
                        {
                            if (method.Visibility == Visibility.Public &&
                                !(method is ConstructorDeclaration))
                            {
                                string currentMethod = method.ResultingType.ParametrizedName + " " + method.Signature;
                                current.MethodSignatures.Add(currentMethod);
                            }
                        }

                    }
                }
            }
            #endregion

            StringBuilder builder = new StringBuilder();

            builder.Append(@"using System;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.ServiceModel;

namespace %s
{
".Replace("%s", config.RulesInterfacesNamespace));

            foreach (InterfaceCandidate candidate in candidates.Values)
            {
                builder.Append(@"[ServiceContract, MainContract]
public partial interface %s1 : %s2
{
".Replace("%s1", candidate.ClassName).Replace("%s2", string.Join(", ", candidate.Inherits.ToArray())));

                foreach (string method in candidate.MethodSignatures)
                {
                    builder.Append(@"[OperationContract]
%s;
".Replace("%s", method));
                }
                builder.Append("}\n");
            }
            builder.Append("}\n");
            File.WriteAllText(Path.Combine(baseDir, config.RulesInterfacesGeneratedFile), builder.ToString());
        }
    }
}

