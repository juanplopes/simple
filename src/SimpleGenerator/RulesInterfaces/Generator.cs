using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DDW;
using SimpleGenerator.Definitions;
using System.Text.RegularExpressions;

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

            #region Calculation
            foreach (string file in Directory.GetFiles(Path.Combine(baseDir, config.RulesDirectory)))
            {
                Lexer lexer = new Lexer(File.OpenText(file));
                Parser parser = new Parser(file);

                CompilationUnitNode comp = parser.Parse(lexer.Lex(), lexer.StringLiterals);
                foreach (NamespaceNode ns in comp.Namespaces)
                {
                    foreach (ClassNode cls in ns.Classes)
                    {
                        InterfaceCandidate current = null;
                        if (!candidates.ContainsKey(cls.Name.Identifier))
                        {
                            current = candidates[cls.Name.Identifier] = new InterfaceCandidate();
                            current.ClassName = InterfacePattern.Replace("%s", cls.Name.Identifier);
                        }
                        else
                        {
                            current = candidates[cls.Name.Identifier];
                        }

                        foreach (var baseClass in cls.BaseClasses)
                        {
                            Match m = BaseRulesRegex.Match(SourceHelper.GetSource(baseClass));
                            if (m.Success)
                            {
                                string ruleName = IBaseRulesPattern.Replace("%s", m.Groups["domainClass"].Value);
                                if (!current.Inherits.Contains(ruleName))
                                    current.Inherits.Add(ruleName);
                            }
                        }

                        foreach (var method in cls.Methods)
                        {
                            string currentMethod = method.GenericIdentifier;

                            List<string> parameters = new List<string>();
                            foreach (var param in method.Params)
                            {
                                string currentParam = SourceHelper.GetSource(param);
                                parameters.Add(currentParam);
                            }

                            current.MethodSignatures.Add(
                                currentMethod + "(" + string.Join(", ", parameters.ToArray()) + ")");
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

