using OpenAI.Net.Models.Responses;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CodeGenerator
{
    internal class ClassGenerator
    {
        public static string GenerateModelsLookup(ModelsResponse response)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace OpenAI.Net");
            sb.AppendLine("{");
            sb.AppendLine("    public class ModelTypes");
            sb.AppendLine("    {");



            foreach (var result in response.Data.OrderBy(i=> i.Id))
            {
                var fieldName = result.Id.Replace("-", "");

                //sb.AppendLine(@$"        public const string {GenerateName(result.Id)} = ""{result.Id}"";");
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {result.Id}");
                sb.AppendLine($"        /// </summary>");
                sb.AppendLine(@$"        public static readonly Model {GenerateName(result.Id)} = new Model(""{result.Id}"");");
                sb.AppendLine();

    }


            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateName(string name)
        {
            var index = name.IndexOf("-");
            while (index != -1)
            {
                var nextChar = name.Substring(index + 1, 1);

                name = name.Remove(index, 2);
                name = name.Insert(index, nextChar.ToUpper());
                index = name.IndexOf("-");
            }


            var fistChar = name.Substring(0, 1);
            name = fistChar.ToUpperInvariant() + name.Substring(1, name.Length - 1);


            return name.Replace(":", "_").Replace(".", "_");
        }
       
    }
}
