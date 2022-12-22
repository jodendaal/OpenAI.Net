using OpenAI.Net.Models.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace OpenAI.Net.Tests.TestModels
{
    internal class TestModel
    {
        [Required]
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    internal class TestModelInheritence : OperationResult<TestModel>
    {
        public TestModelInheritence(TestModel result) : base(result)
        {
        }
        
        public TestModelInheritence(Exception exception, string errorMessaage = null) : base(exception, errorMessaage)
        {
        }
    }
}
